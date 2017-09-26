#include "csharp.h"
#include "audio/audio_scene.h"
#include "animation/animation.h"
#include "animation/animation_scene.h"
#include "engine/blob.h"
#include "engine/crc32.h"
#include "engine/engine.h"
#include "engine/geometry.h"
#include "engine/hash_map.h"
#include "engine/iallocator.h"
#include "engine/input_system.h"
#include "engine/iplugin.h"
#include "engine/log.h"
#include "engine/path.h"
#include "engine/path_utils.h"
#include "engine/property_register.h"
#include "engine/serializer.h"
#include "engine/universe/component.h"
#include "engine/universe/universe.h"
#include "imgui/imgui.h"
#include "navigation/navigation_scene.h"
#include "physics/physics_scene.h"
#include "renderer/render_scene.h"
#include "renderer/renderer.h"

#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/debug-helpers.h>
#include <mono/metadata/mono-config.h>
#include <mono/metadata/tokentype.h>


#pragma comment(lib, "mono-2.0-sgen.lib")


namespace Lumix
{


static const ComponentType CSHARP_SCRIPT_TYPE = PropertyRegister::getComponentType("csharp_script");


struct CSharpPluginImpl : public CSharpPlugin
{
	CSharpPluginImpl(Engine& engine);
	~CSharpPluginImpl();
	const char* getName() const override { return "csharp_script"; }
	void createScenes(Universe& universe) override;
	void destroyScene(IScene* scene) override { LUMIX_DELETE(m_engine.getAllocator(), scene); }
	void unloadAssembly() override;
	void loadAssembly() override;
	int getNamesCount() const override { return m_names.size(); }
	const char* getName(int idx) const override { return m_names.at(idx).c_str(); }

	Engine& m_engine;
	IAllocator& m_allocator;
	MonoDomain* m_domain = nullptr;
	MonoAssembly* m_assembly = nullptr;
	MonoDomain* m_assembly_domain = nullptr;
	AssociativeArray<u32, string> m_names;
	DelegateList<void()> m_on_assembly_unload;
	DelegateList<void()> m_on_assembly_load;
};


ComponentHandle csharp_Entity_getComponent(Universe* universe, Entity entity, MonoString* cmp_type)
{
	const char* type = mono_string_to_utf8(cmp_type);
	return universe->getComponent(entity, PropertyRegister::getComponentType(type)).handle;
}


IScene* csharp_Entity_getScene(Universe* universe, MonoString* type_str)
{
	const char* type = mono_string_to_utf8(type_str);
	ComponentType cmp_type = PropertyRegister::getComponentType(type);
	return universe->getScene(cmp_type);
}


int csharp_Component_create(Universe* universe, int entity, MonoString* type_str)
{
	const char* type = mono_string_to_utf8(type_str);
	ComponentType cmp_type = PropertyRegister::getComponentType(type);
	IScene* scene = universe->getScene(cmp_type);
	if (!scene) return INVALID_COMPONENT.index;
	if (scene->getComponent({entity}, cmp_type) != INVALID_COMPONENT)
	{
		g_log_error.log("C# Script") << "Component " << type << " already exists in entity " << entity;
		return INVALID_COMPONENT.index;
	}

	return scene->createComponent(cmp_type, {entity}).index;
}


void csharp_Entity_destroy(Universe* universe, int entity)
{
	universe->destroyEntity({entity});
}


void csharp_Entity_setPosition(Universe* universe, int entity, const Vec3& pos)
{
	universe->setPosition({entity}, pos);
}


Vec3 csharp_Entity_getPosition(Universe* universe, int entity)
{
	return universe->getPosition({entity});
}


void csharp_Entity_setRotation(Universe* universe, int entity, const Quat& pos)
{
	universe->setRotation({entity}, pos);
}


Quat csharp_Entity_getRotation(Universe* universe, int entity)
{
	return universe->getRotation({entity});
}


void csharp_Entity_setName(Universe* universe, int entity, MonoString* name)
{
	universe->setEntityName({ entity }, mono_string_to_utf8(name));
}


MonoString* csharp_Entity_getName(Universe* universe, int entity)
{
	return mono_string_new(mono_domain_get(), universe->getEntityName({entity}));
}


template <typename T> struct ToCSharpType { typedef T Type; };
template <> struct ToCSharpType<const char*> { typedef MonoString* Type; };
template <> struct ToCSharpType<Path> { typedef MonoString* Type; };
template <typename T> T toCSharpValue(T val) { return val; }
MonoString* toCSharpValue(const char* val) { return mono_string_new(mono_domain_get(), val); }
MonoString* toCSharpValue(const Path& val) { return mono_string_new(mono_domain_get(), val.c_str()); }
template <typename T> T fromCSharpValue(T val) { return val; }
const char* fromCSharpValue(MonoString* val) { return mono_string_to_utf8(val); }


template<typename T> struct CSharpTypeConvertor
{
	using Type = T;

	static Type convert(Type val) { return val; }
	static Type convertRet(Type val) { return val; }
};


template<> struct CSharpTypeConvertor<void>
{
	using Type = void;
};



template<> struct CSharpTypeConvertor<const char*>
{
	using Type = MonoString*;
	
	static const char* convert(MonoString* val) { return mono_string_to_utf8(val); }
	static MonoString* convertRet(const char* val) { return mono_string_new(mono_domain_get(), val); }
};


template <> struct CSharpTypeConvertor<const ImVec2&>
{
	struct Vec2POD { float x; float y; };
	using Type = Vec2POD;

	static ImVec2 convert(Type& val) { return *(const ImVec2*)&val; }
};


template <> struct CSharpTypeConvertor<ImVec2>
{
	struct Vec2POD { float x; float y; };
	using Type = Vec2POD;

	static Type convert(ImVec2& val) { return *(Vec2POD*)&val; }
	static Type convert(const ImVec2& val) { return *(Vec2POD*)&val; }
};


template<typename T> struct CSharpTypeConvertor<const T&>
{
	using Type = T;

	static T convert(T& val) { return val; }
	static T convert(const T& val) { return val; }
};


template<typename F> struct CSharpFunctionProxy;
template<typename F> struct CSharpMethodProxy;


template<typename R, typename... Args>
struct CSharpFunctionProxy<R (Args...)>
{
	using F = R (Args...);

	template<F fnc>
	static typename CSharpTypeConvertor<R>::Type call(typename CSharpTypeConvertor<Args>::Type... args)
	{
		return CSharpTypeConvertor<R>::convert(fnc(CSharpTypeConvertor<Args>::convert(args)...));
	}
};

template<typename... Args>
struct CSharpFunctionProxy<void(Args...)>
{
	using F = void(Args...);

	template<F fnc>
	static void call(typename CSharpTypeConvertor<Args>::Type... args)
	{
		fnc(CSharpTypeConvertor<Args>::convert(args)...);
	}
};


template<bool B, class T = void>
struct enable_if {};

template<class T>
struct enable_if<true, T> { typedef T type; };


template <typename T1, typename T2> struct is_same
{
	static constexpr bool value = false;
};

template <typename T> struct is_same<T, T>
{
	static constexpr bool value = true;
};


template<typename R, typename T, typename... Args>
struct CSharpMethodProxy<R (T::*)(Args...) const>
{
	using F = R (T::*)(Args...)const;
	using ConvertedR = typename CSharpTypeConvertor<R>::Type;

	template<F fnc, typename Ret = R>
	static typename enable_if<is_same<Ret, void>::value, Ret>::type call(T* inst, typename CSharpTypeConvertor<Args>::Type... args)
	{
		(inst->*fnc)(CSharpTypeConvertor<Args>::convert(args)...);
	}

	template<F fnc, typename Ret = R>
	static typename enable_if<!is_same<Ret, void>::value, ConvertedR>::type call(T* inst, typename CSharpTypeConvertor<Args>::Type... args)
	{
		return CSharpTypeConvertor<R>::convertRet((inst->*fnc)(CSharpTypeConvertor<Args>::convert(args)...));
	}
};


template<typename R, typename T, typename... Args>
struct CSharpMethodProxy<R(T::*)(Args...)>
{
	using F = R(T::*)(Args...);
	using ConvertedR = typename CSharpTypeConvertor<R>::Type;

	template<F fnc, typename Ret = R>
	static typename enable_if<is_same<Ret, void>::value, void>::type call(T* inst, typename CSharpTypeConvertor<Args>::Type... args)
	{
		(inst->*fnc)(CSharpTypeConvertor<Args>::convert(args)...);
	}

	template<F fnc, typename Ret = R>
	static typename enable_if<!is_same<Ret, void>::value, ConvertedR>::type call(T* inst, typename CSharpTypeConvertor<Args>::Type... args)
	{
		return CSharpTypeConvertor<R>::convertRet((inst->*fnc)(CSharpTypeConvertor<Args>::convert(args)...));
	}
};


template <typename R, typename C, R(C::*Function)(ComponentHandle)>
typename ToCSharpType<R>::Type csharp_getProperty(C* scene, int cmp)
{
	R val = (scene->*Function)({ cmp });
	return toCSharpValue(val);
}


template <typename C, int (C::*Function)(ComponentHandle cmp)>
int csharp_getSubobjectCount(C* scene, int cmp)
{
	return (scene->*Function)({ cmp });
}


template <typename C, void (C::*Function)(ComponentHandle cmp, int)>
void csharp_addSubobject(C* scene, int cmp)
{
	(scene->*Function)({ cmp }, -1);
}


template <typename C, void (C::*Function)(ComponentHandle cmp, int)>
void csharp_removeSubobject(C* scene, int cmp, int index)
{
	(scene->*Function)({ cmp }, index);
}


template <typename R, typename C, R(C::*Function)(ComponentHandle, int)>
typename ToCSharpType<R>::Type csharp_getSubproperty(C* scene, int cmp, int index)
{
	R val = (scene->*Function)({cmp}, index);
	return toCSharpValue(val);
}


template <typename T, typename C, void (C::*Function)(ComponentHandle, T)>
void csharp_setProperty(C* scene, int cmp, typename ToCSharpType<T>::Type value)
{
	(scene->*Function)({ cmp }, fromCSharpValue(value));
}


template <typename T, typename C, void (C::*Function)(ComponentHandle, int, T)>
void csharp_setSubproperty(C* scene, int cmp, typename ToCSharpType<T>::Type value, int index)
{
	(scene->*Function)({cmp}, index, fromCSharpValue(value));
}


template <typename T, typename C, void (C::*Function)(ComponentHandle, const T&)>
void csharp_setProperty(C* scene, int cmp, typename ToCSharpType<T>::Type value)
{
	(scene->*Function)({ cmp }, T(fromCSharpValue(value)));
}


MonoString* csharp_Animable_getSource(AnimationScene* scene, int cmp)
{
	Path src = scene->getAnimation({cmp});
	return mono_string_new(mono_domain_get(), src.c_str());
}


void csharp_Animable_setSource(AnimationScene* scene, int cmp, MonoString* src)
{
	const char* str = mono_string_to_utf8(src);
	scene->setAnimation({cmp}, Path(str));
}


void csharp_logError(MonoString* message)
{
	g_log_error.log("C#") << mono_string_to_utf8(message);
}


struct CSharpScriptSceneImpl : public CSharpScriptScene
{
	struct Script
	{
		u32 script_name_hash;
		u32 gc_handle = INVALID_GC_HANDLE;
		bool has_update = false;
	};


	struct ScriptComponent
	{
		ScriptComponent(IAllocator& allocator) : scripts(allocator) {}

		Array<Script> scripts;
		Entity entity;
	};


	CSharpScriptSceneImpl(CSharpPluginImpl& plugin, Universe& universe)
		: m_system(plugin)
		, m_universe(universe)
		, m_scripts(plugin.m_allocator)
		, m_entities_gc_handles(plugin.m_allocator)
		, m_updates(plugin.m_allocator)
		, m_is_game_running(false)
	{
		universe.registerComponentType(CSHARP_SCRIPT_TYPE, this, &CSharpScriptSceneImpl::serializeCSharpScript, &CSharpScriptSceneImpl::deserializeCSharpScript);

		#include "api.h"

		createImGuiAPI();
		createEngineAPI();
		/*createRendererAPI();
		createPhysicsAPI();
		createAnimationAPI();*/

		m_system.m_on_assembly_load.bind<CSharpScriptSceneImpl, &CSharpScriptSceneImpl::onAssemblyLoad>(this);
		m_system.m_on_assembly_unload.bind<CSharpScriptSceneImpl, &CSharpScriptSceneImpl::onAssemblyUnload>(this);
		onAssemblyLoad();
	}


	~CSharpScriptSceneImpl()
	{
		m_system.m_on_assembly_load.unbind<CSharpScriptSceneImpl, &CSharpScriptSceneImpl::onAssemblyLoad>(this);
		m_system.m_on_assembly_unload.unbind<CSharpScriptSceneImpl, &CSharpScriptSceneImpl::onAssemblyUnload>(this);
	}


	void onAssemblyLoad()
	{
		for (ScriptComponent* cmp : m_scripts)
		{
			createCSharpEntity(cmp->entity);
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts)
			{
				setScriptNameHash(*cmp, script, script.script_name_hash);
			}
		}
	}


	void onAssemblyUnload()
	{
		m_updates.clear();
		for(u32 handle : m_entities_gc_handles)
		{
			mono_gchandle_free(handle);
		}
		m_entities_gc_handles.clear();
		for (ScriptComponent* cmp : m_scripts)
		{
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts)
			{
				if(script.gc_handle != INVALID_GC_HANDLE) mono_gchandle_free(script.gc_handle);
				script.gc_handle = INVALID_GC_HANDLE;
			}
		}
	}


	static void imgui_Text(const char* text)
	{
		ImGui::Text("%s", text);
	}


	static void imgui_LabelText(const char* label, const char* text)
	{
		ImGui::LabelText(label, "%s", text);
	}


	void createImGuiAPI()
	{
		mono_add_internal_call("ImGui::Begin", &CSharpFunctionProxy<bool(const char*, bool*, ImGuiWindowFlags)>::call<ImGui::Begin>);
		mono_add_internal_call("ImGui::CollapsingHeader", &CSharpFunctionProxy<bool(const char*, bool*, ImGuiTreeNodeFlags)>::call<ImGui::CollapsingHeader>);
		mono_add_internal_call("ImGui::LabelText", &CSharpFunctionProxy<void(const char*, const char*)>::call<imgui_LabelText>);
		mono_add_internal_call("ImGui::PushID", &CSharpFunctionProxy<void(const char*)>::call<ImGui::PushID>);
		mono_add_internal_call("ImGui::Selectable", &CSharpFunctionProxy<bool(const char*, bool, ImGuiSelectableFlags, const ImVec2&)>::call<ImGui::Selectable>);
		mono_add_internal_call("ImGui::Text", &CSharpFunctionProxy<void(const char*)>::call<imgui_Text>);
		mono_add_internal_call("ImGui::InputText", &CSharpFunctionProxy<decltype(ImGui::InputText)>::call<ImGui::InputText>);
		#define REGISTER_FUNCTION(F) \
			mono_add_internal_call("ImGui::" #F, &CSharpFunctionProxy<decltype(ImGui::F)>::call<ImGui::F>);

		REGISTER_FUNCTION(AlignFirstTextHeightToWidgets);
		REGISTER_FUNCTION(BeginChildFrame);
		REGISTER_FUNCTION(BeginDock);
		REGISTER_FUNCTION(BeginPopup);
		REGISTER_FUNCTION(Button);
		REGISTER_FUNCTION(Checkbox);
		REGISTER_FUNCTION(Columns);
		REGISTER_FUNCTION(DragFloat);
		REGISTER_FUNCTION(Dummy);
		REGISTER_FUNCTION(End);
		REGISTER_FUNCTION(EndChildFrame);
		REGISTER_FUNCTION(EndDock);
		REGISTER_FUNCTION(EndPopup);
		REGISTER_FUNCTION(GetColumnWidth);
		REGISTER_FUNCTION(GetWindowWidth);
		REGISTER_FUNCTION(GetWindowHeight);
		REGISTER_FUNCTION(GetWindowSize);
		REGISTER_FUNCTION(Indent);
		REGISTER_FUNCTION(IsItemHovered);
		REGISTER_FUNCTION(IsMouseClicked);
		REGISTER_FUNCTION(IsMouseDown);
		REGISTER_FUNCTION(InputInt);
		REGISTER_FUNCTION(NewLine);
		REGISTER_FUNCTION(NextColumn);
		REGISTER_FUNCTION(OpenPopup);
		REGISTER_FUNCTION(PopItemWidth);
		REGISTER_FUNCTION(PopID);
		REGISTER_FUNCTION(PopStyleColor);
		REGISTER_FUNCTION(PushItemWidth);
		REGISTER_FUNCTION(PushStyleColor);
		REGISTER_FUNCTION(Rect);
		REGISTER_FUNCTION(SameLine);
		REGISTER_FUNCTION(Separator);
		REGISTER_FUNCTION(SetCursorScreenPos);
		REGISTER_FUNCTION(SetNextWindowPos);
		REGISTER_FUNCTION(SetNextWindowPosCenter);
		REGISTER_FUNCTION(SetNextWindowSize);
		REGISTER_FUNCTION(ShowTestWindow);
		REGISTER_FUNCTION(SliderFloat);
		REGISTER_FUNCTION(Unindent);

		#undef REGISTER_FUNCTION
	}


	void createEngineAPI()
	{
		mono_add_internal_call("Lumix.Engine::logError", csharp_logError);
		mono_add_internal_call("Lumix.Component::create", csharp_Component_create);
		mono_add_internal_call("Lumix.Component::getScene", csharp_Entity_getScene);
		mono_add_internal_call("Lumix.Entity::getComponent", csharp_Entity_getComponent);
		mono_add_internal_call("Lumix.Entity::destroy", csharp_Entity_destroy);
		mono_add_internal_call("Lumix.Entity::setPosition", csharp_Entity_setPosition);
		mono_add_internal_call("Lumix.Entity::getPosition", csharp_Entity_getPosition);
		mono_add_internal_call("Lumix.Entity::setRotation", csharp_Entity_setRotation);
		mono_add_internal_call("Lumix.Entity::getRotation", csharp_Entity_getRotation);
		mono_add_internal_call("Lumix.Entity::setName", csharp_Entity_setName);
		mono_add_internal_call("Lumix.Entity::getName", csharp_Entity_getName);
	}


	#define CSHARP_PROPERTY(Type, Scene, Class, Property) \
		do { \
			auto f = &csharp_getProperty<Type, Scene, &Scene::get##Class##Property>; \
			mono_add_internal_call("Lumix." ## #Class ## "::get" ## #Property, f); \
			auto f2 = &csharp_setProperty<Type, Scene, &Scene::set##Class##Property>; \
			mono_add_internal_call("Lumix." ## #Class ## "::set" ## #Property, f2); \
		} while (false)

	#define CSHARP_SUBOBJECT(Scene, Class, Subclass) \
		do { \
			auto f = csharp_getSubobjectCount<Scene, &Scene::get##Subclass##Count>; \
			mono_add_internal_call("Lumix." ## #Class ## "::get" ## #Subclass ##  "Count", f); \
			auto f2 = csharp_addSubobject<Scene, &Scene::add##Subclass##>; \
			mono_add_internal_call("Lumix." ## #Class ## "::add" ## #Subclass, f2); \
			auto f3 = csharp_removeSubobject<Scene, &Scene::remove##Subclass##>; \
			mono_add_internal_call("Lumix." ## #Class ## "::remove" ## #Subclass, f3); \
		} while(false)

	#define CSHARP_SUBPROPERTY(Type, Scene, Class, Subclass, Property) \
		do { \
			auto f = &csharp_getSubproperty<Type, Scene, &Scene::get##Subclass##Property>; \
			mono_add_internal_call("Lumix." ## #Class ## "::get" ## #Subclass ## #Property, f); \
			auto f2 = &csharp_setSubproperty<Type, Scene, &Scene::set##Subclass##Property>; \
			mono_add_internal_call("Lumix." ## #Class ## "::set" ## #Subclass ## #Property, f2); \
		} while (false)

	void createRendererAPI()
	{
		CSHARP_PROPERTY(Entity, RenderScene, BoneAttachment, Parent);
		CSHARP_PROPERTY(int, RenderScene, BoneAttachment, Bone);
		CSHARP_PROPERTY(Vec3, RenderScene, BoneAttachment, Position);
		CSHARP_PROPERTY(Vec3, RenderScene, BoneAttachment, Rotation);

		CSHARP_PROPERTY(float, RenderScene, Camera, FOV);
		CSHARP_PROPERTY(float, RenderScene, Camera, NearPlane);
		CSHARP_PROPERTY(float, RenderScene, Camera, FarPlane);
		CSHARP_PROPERTY(float, RenderScene, Camera, OrthoSize);
		CSHARP_PROPERTY(const char*, RenderScene, Camera, Slot);

		CSHARP_PROPERTY(Vec3, RenderScene, Decal, Scale);
		CSHARP_PROPERTY(Path, RenderScene, Decal, MaterialPath);

		CSHARP_PROPERTY(Path, RenderScene, ModelInstance, Path);

		CSHARP_PROPERTY(float, RenderScene, Terrain, XZScale);
		CSHARP_PROPERTY(float, RenderScene, Terrain, YScale);
		CSHARP_PROPERTY(Path, RenderScene, Terrain, MaterialPath);

		CSHARP_SUBOBJECT(RenderScene, Terrain, Grass);
		CSHARP_SUBPROPERTY(int, RenderScene, Terrain, Grass, Density);

		CSHARP_PROPERTY(Vec3, RenderScene, GlobalLight, Color);
		CSHARP_PROPERTY(float, RenderScene, GlobalLight, Intensity);
		CSHARP_PROPERTY(float, RenderScene, GlobalLight, IndirectIntensity);

		CSHARP_PROPERTY(float, RenderScene, PointLight, Intensity);
		CSHARP_PROPERTY(Vec3, RenderScene, PointLight, Color);
		CSHARP_PROPERTY(float, RenderScene, PointLight, SpecularIntensity);
		CSHARP_PROPERTY(Vec3, RenderScene, PointLight, SpecularColor);

		CSHARP_PROPERTY(float, RenderScene, Fog, Bottom);
		CSHARP_PROPERTY(Vec3, RenderScene, Fog, Color);
		CSHARP_PROPERTY(float, RenderScene, Fog, Density);
		CSHARP_PROPERTY(float, RenderScene, Fog, Height);

		CSHARP_PROPERTY(Vec3, RenderScene, ParticleEmitter, Acceleration);
		CSHARP_PROPERTY(Path, RenderScene, ParticleEmitter, MaterialPath);
		CSHARP_PROPERTY(bool, RenderScene, ParticleEmitter, Autoemit);

		CSHARP_PROPERTY(float, RenderScene, ParticleEmitterPlane, Bounce);
		
		CSHARP_PROPERTY(float, RenderScene, ParticleEmitterShape, Radius);
	}


	void createPhysicsAPI()
	{
		CSHARP_PROPERTY(float, PhysicsScene, Capsule, Radius);
		CSHARP_PROPERTY(float, PhysicsScene, Capsule, Height);

		CSHARP_PROPERTY(int, PhysicsScene, Controller, Layer);

		CSHARP_PROPERTY(float, PhysicsScene, Heightmap, XZScale);
		CSHARP_PROPERTY(float, PhysicsScene, Heightmap, YScale);

		CSHARP_PROPERTY(float, PhysicsScene, Sphere, Radius);
	}


	void createAnimationAPI()
	{
		CSHARP_PROPERTY(Entity, AnimationScene, SharedController, Parent);

		CSHARP_PROPERTY(float, AnimationScene, Animable, Time);

		mono_add_internal_call("Lumix.Animable::setSource", csharp_Animable_setSource);
		mono_add_internal_call("Lumix.Animable::getSource", csharp_Animable_getSource);
	}


	#undef CSHARP_PROPERTY
	#undef CSHARP_SUBPROPERTY
	#undef CSHARP_SUBOBJECT


	void startGame() override
	{
		for (ScriptComponent* cmp : m_scripts)
		{
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts)
			{
				tryCallMethod(script.gc_handle, "startGame", false);
			}
		}
		m_is_game_running = true;
	}


	void stopGame() override { m_is_game_running = false; }


	void getClassName(u32 name_hash, char(&out_name)[256]) const
	{
		int idx = m_system.m_names.find(name_hash);
		if (idx < 0)
		{
			out_name[0] = 0;
			return;
		}
		
		copyString(out_name, m_system.m_names.at(idx).c_str());
	}


	void serializeCSharpScript(ISerializer& serializer, ComponentHandle cmp)
	{
		ScriptComponent* script = m_scripts[{cmp.index}];
		serializer.write("count", script->scripts.size());
		for (Script& inst : script->scripts)
		{
			serializer.write("script_name_hash", inst.script_name_hash);
			if (inst.gc_handle == INVALID_GC_HANDLE)
			{
				serializer.write("prop_count", (int)0);
				continue;
			}
			
			MonoObject* obj = mono_gchandle_get_target(inst.gc_handle);
			MonoClass* mono_class = mono_object_get_class(obj);

			void* iter = nullptr;
			int count = 0;
			while (MonoClassField* field = mono_class_get_fields(mono_class, &iter))
			{
				bool is_public = (mono_field_get_flags(field) & 0x6) != 0;
				if (is_public) ++count;
			}

			serializer.write("prop_count", count);

			iter = nullptr;
			while (MonoClassField* field = mono_class_get_fields(mono_class, &iter))
			{
				bool is_public = (mono_field_get_flags(field) & 0x6) != 0;
				if (!is_public) continue;
				int type = mono_type_get_type(mono_field_get_type(field));

				const char* field_name = mono_field_get_name(field);
				serializer.write("name", field_name);
				serializer.write("type", type);
				switch (type)
				{
					case MONO_TYPE_BOOLEAN:
					{
						bool value;
						mono_field_get_value(obj, field, &value);
						serializer.write("value", value);
						break;
					}
					case MONO_TYPE_R4:
					{
						float value;
						mono_field_get_value(obj, field, &value);
						serializer.write("value", value);
						break;
					}
					case MONO_TYPE_I4:
					{
						i32 value;
						mono_field_get_value(obj, field, &value);
						serializer.write("value", value);
						break;
					}
					case MONO_TYPE_STRING:
					{
						MonoString* str;
						mono_field_get_value(obj, field, &str);
						serializer.write("value", mono_string_to_utf8(str));
						break;
					}
					default: ASSERT(false);
				}
			}
		}
	}


	void deserializeCSharpScript(IDeserializer& serializer, Entity entity, int scene_version)
	{
		auto& allocator = m_system.m_allocator;
		ScriptComponent* script = LUMIX_NEW(allocator, ScriptComponent)(allocator);
		ComponentHandle cmp = { entity.index };
		script->entity = entity;
		m_scripts.insert(entity, script);
		createCSharpEntity(script->entity);

		int count;
		serializer.read(&count);
		script->scripts.reserve(count);
		for (int i = 0; i < count; ++i)
		{
			Script& inst = script->scripts.emplace();
			u32 hash;
			serializer.read(&hash);
			setScriptNameHash(cmp, i, hash);

			int prop_count;
			serializer.read(&prop_count);

			for (int i = 0; i < prop_count; ++i)
			{
				char name[128];
				int type;
				serializer.read(name, lengthOf(name));
				serializer.read(&type);

				MonoObject* obj = mono_gchandle_get_target(inst.gc_handle);
				MonoClass* mono_class = mono_object_get_class(obj);
				MonoClassField* field = mono_class_get_field_from_name(mono_class, name);
				MonoType* mono_type = mono_field_get_type(field);
				bool is_matching = mono_type_get_type(mono_type) == type;

				switch (type)
				{
					case MONO_TYPE_BOOLEAN:
					{
						bool value;
						serializer.read(&value);
						if(is_matching) mono_field_set_value(obj, field, &value);
						break;
					}
					case MONO_TYPE_I4:
					{
						i32 value;
						serializer.read(&value);
						if(is_matching) mono_field_set_value(obj, field, &value);
						break;
					}
					case MONO_TYPE_R4:
					{
						float value;
						serializer.read(&value);
						if (is_matching) mono_field_set_value(obj, field, &value);
						break;
					}
					case MONO_TYPE_STRING:
					{
						char tmp[1024];
						serializer.read(tmp, lengthOf(tmp));
						if (is_matching)
						{
							MonoString* str = mono_string_new(mono_domain_get(), tmp);
							mono_field_set_value(obj, field, str);
						}
						break;
					}

					default: ASSERT(false);
				}
			}
		}

		m_universe.addComponent(entity, CSHARP_SCRIPT_TYPE, this, cmp);
	}



	void serializeScript(ComponentHandle cmp, int scr_index, OutputBlob& blob) override
	{
		Script& scr = m_scripts[{cmp.index}]->scripts[scr_index];
		blob.write(scr.script_name_hash);
	}


	void deserializeScript(ComponentHandle cmp, int scr_index, InputBlob& blob) override
	{
		u32 name_hash = blob.read<u32>();
		setScriptNameHash(cmp, scr_index, name_hash);
	}


	void insertScript(ComponentHandle cmp, int idx) override
	{
		m_scripts[{cmp.index}]->scripts.emplaceAt(idx);
	}


	int addScript(ComponentHandle cmp) override
	{
		ScriptComponent* script_cmp = m_scripts[{cmp.index}];
		script_cmp->scripts.emplace();
		return script_cmp->scripts.size() - 1;
	}


	void removeScript(ComponentHandle cmp, int scr_index) override
	{
		setScriptNameHash(cmp, scr_index, 0);
		m_scripts[{cmp.index}]->scripts.erase(scr_index);
	}


	u32 getEntityGCHandle(Entity entity) override
	{
		if (!entity.isValid()) return INVALID_GC_HANDLE;
		auto iter = m_entities_gc_handles.find(entity);
		if (iter.isValid()) return iter.value();
		u32 handle = createCSharpEntity(entity);
		return handle;
	}


	u32 getGCHandle(ComponentHandle cmp, int scr_index) const override
	{
		Script& scr = m_scripts[{cmp.index}]->scripts[scr_index];
		return scr.gc_handle;
	}


	const char* getScriptName(ComponentHandle cmp, int scr_index) override
	{
		Script& scr = m_scripts[{cmp.index}]->scripts[scr_index];
		int idx = m_system.m_names.find(scr.script_name_hash);
		if (idx < 0) return "";
		return m_system.m_names.at(idx).c_str();
	}


	int getScriptCount(ComponentHandle cmp) const override
	{
		return m_scripts[{cmp.index}]->scripts.size();
	}


	u32 getScriptNameHash(ComponentHandle cmp, int scr_index) override
	{
		return m_scripts[{cmp.index}]->scripts[scr_index].script_name_hash;
	}


	void setScriptNameHash(ScriptComponent& cmp, Script& script, u32 name_hash)
	{
		if (script.gc_handle != INVALID_GC_HANDLE)
		{
			ASSERT(m_system.m_assembly);
			mono_gchandle_free(script.gc_handle);
			if (script.has_update)
			{
				script.has_update = false;
				m_updates.eraseItems([&script](u32 iter) { return iter == script.gc_handle; });
			}
			script.script_name_hash = 0;
			script.gc_handle = INVALID_GC_HANDLE;
		}

		if (name_hash != 0)
		{
			if (m_system.m_assembly)
			{
				char class_name[256];
				getClassName(name_hash, class_name);
				script.gc_handle = createObject("Lumix", class_name);
				ASSERT(script.gc_handle != INVALID_GC_HANDLE);

				setCSharpComponent(script, cmp);
			}

			script.script_name_hash = name_hash;
		}
	}


	void setCSharpComponent(Script& script, ScriptComponent& cmp)
	{
		MonoObject* obj = mono_gchandle_get_target(script.gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);

		MonoClassField* field = mono_class_get_field_from_name(mono_class, "entity_");
		ASSERT(field);

		u32 handle = m_entities_gc_handles[cmp.entity];
		MonoObject* entity_obj = mono_gchandle_get_target(handle);
		ASSERT(entity_obj);

		mono_field_set_value(obj, field, entity_obj);

		if (mono_class_get_method_from_name(mono_class, "update", 1))
		{
			m_updates.push(script.gc_handle);
			script.has_update = true;
		}
	}


	void setScriptNameHash(ComponentHandle cmp, int scr_index, u32 name_hash) override
	{
		ScriptComponent* script_cmp = m_scripts[{cmp.index}];
		if (script_cmp->scripts.size() <= scr_index) return;

		setScriptNameHash(*script_cmp, script_cmp->scripts[scr_index], name_hash);
	}


	u32 createCSharpEntity(Entity entity)
	{
		u32 handle = createObject("Lumix", "Entity", &m_universe);
		m_entities_gc_handles.insert(entity, handle);

		MonoObject* obj = mono_gchandle_get_target(handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);
		
		MonoClassField* field = mono_class_get_field_from_name(mono_class, "entity_Id_");
		ASSERT(field);

		mono_field_set_value(obj, field, &entity.index);

		MonoClassField* universe_field = mono_class_get_field_from_name(mono_class, "instance_");
		ASSERT(universe_field);

		void* y = &m_universe;
		mono_field_set_value(obj, universe_field, &y);

		Universe* x;
		mono_field_get_value(obj, universe_field, &x);

		return handle;
	}


	ComponentHandle createComponent(ComponentType type, Entity entity) override
	{
		if (type != CSHARP_SCRIPT_TYPE) return INVALID_COMPONENT;

		auto& allocator = m_system.m_allocator;
		
		ScriptComponent* script = LUMIX_NEW(allocator, ScriptComponent)(allocator);
		ComponentHandle cmp = {entity.index};
		script->entity = entity;
		m_scripts.insert(entity, script);
		createCSharpEntity(script->entity);
		m_universe.addComponent(entity, type, this, cmp);
		return cmp;
	}


	void destroyComponent(ComponentHandle component, ComponentType type) override
	{
		if (type != CSHARP_SCRIPT_TYPE) return;

		Entity entity = {component.index};
		auto* script = m_scripts[entity];
		auto handle_iter = m_entities_gc_handles.find(script->entity);
		if(handle_iter.isValid())
		{
			mono_gchandle_free(handle_iter.value());
			m_entities_gc_handles.erase(handle_iter);
		}
		for (Script& scr : script->scripts)
		{
			setScriptNameHash(*script, scr, 0);
		}
		LUMIX_DELETE(m_system.m_allocator, script);
		m_scripts.erase(entity);
		m_universe.destroyComponent(entity, type, this, component);
	}


	void serialize(OutputBlob& serializer) override
	{
		serializer.write(m_scripts.size());
		for (ScriptComponent** iter = m_scripts.begin(), **end = m_scripts.end(); iter != end; ++iter)
		{
			ScriptComponent* script_cmp = *iter;
			serializer.write(script_cmp->entity);
			serializer.write(script_cmp->scripts.size());
			for (Script& scr : script_cmp->scripts)
			{
				serializer.write(scr.script_name_hash);

				MonoObject* obj = mono_gchandle_get_target(scr.gc_handle);
				MonoClass* mono_class = mono_object_get_class(obj);

				void* iter = nullptr;
				int count = 0;
				while (MonoClassField* field = mono_class_get_fields(mono_class, &iter))
				{
					bool is_public = (mono_field_get_flags(field) & 0x6) != 0;
					if (is_public) ++count;
				}

				serializer.write(count);

				iter = nullptr;
				while (MonoClassField* field = mono_class_get_fields(mono_class, &iter))
				{
					bool is_public = (mono_field_get_flags(field) & 0x6) != 0;
					if (!is_public) continue;
					int type = mono_type_get_type(mono_field_get_type(field));

					const char* field_name = mono_field_get_name(field);
					serializer.writeString(field_name);
					serializer.write(type);
					switch (type)
					{
						case MONO_TYPE_BOOLEAN:
						{
							bool value;
							mono_field_get_value(obj, field, &value);
							serializer.write(value);
							break;
						}
						case MONO_TYPE_I4:
						{
							i32 value;
							mono_field_get_value(obj, field, &value);
							serializer.write(value);
							break;
						}
						case MONO_TYPE_R4:
						{
							float value;
							mono_field_get_value(obj, field, &value);
							serializer.write(value);
							break;
						}
						case MONO_TYPE_STRING:
						{
							MonoString* str_val;
							mono_field_get_value(obj, field, &str_val);
							serializer.writeString(mono_string_to_utf8(str_val));
							break;
						}
						case MONO_TYPE_CLASS:
						{
							MonoType* type = mono_field_get_type(field);
							MonoClass* mono_class = mono_type_get_class(type);
							if (equalStrings(mono_class_get_name(mono_class), "Entity"))
							{
								MonoObject* field_obj = mono_field_get_value_object(mono_domain_get(), field, obj);
								Entity entity = INVALID_ENTITY;
								MonoClassField* entity_id_field = mono_class_get_field_from_name(mono_class, "_entity_id");
								if (field_obj)
								{
									mono_field_get_value(field_obj, entity_id_field, &entity.index);
								}
								serializer.write(entity.index);
							}
							break;
						}
						default: ASSERT(false);
					}
				}
			}
		}
	}


	void deserialize(InputBlob& serializer) override
	{
		int len = serializer.read<int>();
		m_scripts.reserve(len);
		for (int i = 0; i < len; ++i)
		{
			IAllocator& allocator = m_system.m_allocator;
			ScriptComponent* script = LUMIX_NEW(allocator, ScriptComponent)(allocator);

			serializer.read(script->entity);
			m_scripts.insert(script->entity, script);
			createCSharpEntity(script->entity);
			int scr_count;
			serializer.read(scr_count);
			for (int j = 0; j < scr_count; ++j)
			{
				Script& scr = script->scripts.emplace();
				scr.gc_handle = INVALID_GC_HANDLE;
				scr.script_name_hash = serializer.read<u32>();
				setScriptNameHash(*script, scr, scr.script_name_hash);

				int prop_count;
				serializer.read(prop_count);

				MonoObject* obj = mono_gchandle_get_target(scr.gc_handle);
				MonoClass* mono_class = mono_object_get_class(obj);

				for (int i = 0; i < prop_count; ++i)
				{
					char name[128];
					int type;
					serializer.readString(name, lengthOf(name));
					serializer.read(type);

					MonoClassField* field = mono_class_get_field_from_name(mono_class, name);

					switch (type)
					{
						case MONO_TYPE_BOOLEAN:
						{
							bool value;
							serializer.read(value);
							mono_field_set_value(obj, field, &value);
							break;
						}
						case MONO_TYPE_I4:
						{
							i32 value;
							serializer.read(value);
							mono_field_set_value(obj, field, &value);
							break;
						}
						case MONO_TYPE_R4:
						{
							float value;
							serializer.read(value);
							mono_field_set_value(obj, field, &value);
							break;
						}
						case MONO_TYPE_STRING:
						{
							char tmp[1024];
							serializer.readString(tmp, lengthOf(tmp));
							MonoString* str = mono_string_new(mono_domain_get(), tmp);
							mono_field_set_value(obj, field, str);
							break;
						}
						case MONO_TYPE_CLASS:
						{
							MonoType* type = mono_field_get_type(field);
							MonoClass* mono_class = mono_type_get_class(type);
							if (equalStrings(mono_class_get_name(mono_class), "Entity"))
							{
								Entity entity;
								serializer.read(entity.index);
								u32 handle = getEntityGCHandle(entity);
								MonoObject* entity_obj = mono_gchandle_get_target(handle);
								mono_field_set_value(obj, field, entity_obj);
							}
							break;
						}
						default: ASSERT(false);
					}
				}
			}
			ComponentHandle cmp = {script->entity.index};
			m_universe.addComponent(script->entity, CSHARP_SCRIPT_TYPE, this, cmp);
		}
	}


	IPlugin& getPlugin() const override { return m_system; }
	
	
	void update(float time_delta, bool paused) override
	{
		if (paused) return;
		if (!m_is_game_running) return;

		for (u32 gc_handle : m_updates)
		{
			tryCallMethodInternal(gc_handle, "update", time_delta);
		}
	}
	
	
	void lateUpdate(float time_delta, bool paused) override {}


	ComponentHandle getComponent(Entity entity, ComponentType type) override 
	{
		if(type != CSHARP_SCRIPT_TYPE) return INVALID_COMPONENT; 
		if (m_scripts.find(entity) != -1) return {entity.index};
		return INVALID_COMPONENT;
	}


	Universe& getUniverse() override { return m_universe; }


	void clear() override
	{
		for (u32 handle : m_entities_gc_handles)
		{
			mono_gchandle_free(handle);
		}
		m_entities_gc_handles.clear();
		for (ScriptComponent* script_cmp : m_scripts)
		{
			for (Script& script : script_cmp->scripts)
			{
				setScriptNameHash(*script_cmp, script, 0);
			}
			LUMIX_DELETE(m_system.m_allocator, script_cmp);
		}
		m_scripts.clear();
	}


	static const char *GetStringProperty(const char *propertyName, MonoClass *classType, MonoObject *classObject)
	{
		MonoProperty *messageProperty;
		MonoMethod *messageGetter;
		MonoString *messageString;

		messageProperty = mono_class_get_property_from_name(classType, propertyName);
		messageGetter = mono_property_get_get_method(messageProperty);
		messageString = (MonoString *)mono_runtime_invoke(messageGetter, classObject, NULL, NULL);
		return mono_string_to_utf8(messageString);
	}


	void handleException(MonoObject* exc)
	{
		if (!exc) return;

		MonoClass *exception_class = mono_object_get_class(exc);
		MonoType* exception_type = mono_class_get_type(exception_class);
		const char* type_name = mono_type_get_name(exception_type);
		const char* message = GetStringProperty("Message", exception_class, exc);
		const char* source = GetStringProperty("Source", exception_class, exc);
		const char* stack_trace = GetStringProperty("StackTrace", exception_class, exc);
		if(message) g_log_error.log("C#") << message;
		if(source) g_log_error.log("C#") << source;
		if(stack_trace) g_log_error.log("C#") << stack_trace;
	}


	template <typename T>
	bool tryCallMethodInternal(u32 gc_handle, const char* method_name, T arg0)
	{
		MonoObject* obj = mono_gchandle_get_target(gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);
		ASSERT(mono_class);
		MonoMethod* method = mono_class_get_method_from_name(mono_class, method_name, 1);
		if (!method) return false;

		MonoObject* exc = nullptr;
		void* args[] = { &arg0 };
		mono_runtime_invoke(method, obj, args, &exc);
		handleException(exc);

		return exc == nullptr;
	}


	bool tryCallMethod(u32 gc_handle, const char* method_name, bool try_parents) override
	{
		if (gc_handle == INVALID_GC_HANDLE) return false;
		MonoObject* obj = mono_gchandle_get_target(gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);

		ASSERT(mono_class);
		MonoMethod* method = mono_class_get_method_from_name(mono_class, method_name, 0);
		if (!method && try_parents)
		{
			while (!method)
			{
				mono_class = mono_class_get_parent(mono_class);
				if (!mono_class) return false;
				method = mono_class_get_method_from_name(mono_class, method_name, 0);
			}
		}
		if (!method) return false;

		MonoObject* exc = nullptr;
		mono_runtime_invoke(method, obj, nullptr, &exc);
		
		handleException(exc);
		return exc == nullptr;
	}


	bool tryCallMethod(u32 gc_handle, const char* method_name, void* arg, bool try_parents) override
	{
		if (gc_handle == INVALID_GC_HANDLE) return false;
		MonoObject* obj = mono_gchandle_get_target(gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);

		ASSERT(mono_class);
		MonoMethod* method = mono_class_get_method_from_name(mono_class, method_name, 1);
		if (!method && try_parents)
		{
			while (!method)
			{
				mono_class = mono_class_get_parent(mono_class);
				if (!mono_class) return false;
				method = mono_class_get_method_from_name(mono_class, method_name, 1);
			}
		}
		if (!method) return false;

		MonoObject* exc = nullptr;
		void* args[] = { &arg };
		mono_runtime_invoke(method, obj, args, &exc);

		handleException(exc);
		return exc == nullptr;
	}


	u32 createObject(const char* name_space, const char* class_name)
	{
		MonoClass* mono_class = mono_class_from_name(mono_assembly_get_image(m_system.m_assembly), name_space, class_name);
		if (!mono_class) return INVALID_GC_HANDLE;

		MonoObject* obj = mono_object_new(m_system.m_domain, mono_class);
		if (!obj) return INVALID_GC_HANDLE;

		mono_runtime_object_init(obj);
		return mono_gchandle_new(obj, false);
	}


	u32 createObject(const char* name_space, const char* class_name, void* arg)
	{
		MonoClass* mono_class = mono_class_from_name(mono_assembly_get_image(m_system.m_assembly), name_space, class_name);
		if (!mono_class) return INVALID_GC_HANDLE;

		MonoObject* obj = mono_object_new(m_system.m_domain, mono_class);
		if (!obj) return INVALID_GC_HANDLE;

		u32 gc_handle = mono_gchandle_new(obj, false);

		tryCallMethod(gc_handle, ".ctor", arg, false);
		return gc_handle;
	}


	AssociativeArray<Entity, ScriptComponent*> m_scripts;
	HashMap<Entity, u32> m_entities_gc_handles;
	Array<u32> m_updates;
	CSharpPluginImpl& m_system;
	Universe& m_universe;
	bool m_is_game_running;
};


CSharpPluginImpl::CSharpPluginImpl(Engine& engine)
	: m_engine(engine)
	, m_allocator(engine.getAllocator())
	, m_names(m_allocator)
	, m_on_assembly_load(m_allocator)
	, m_on_assembly_unload(m_allocator)
{
	mono_set_dirs("C:\\Program Files\\Mono\\lib", "C:\\Program Files\\Mono\\etc");
	mono_config_parse(nullptr);
	m_domain = mono_jit_init("lumix");
	loadAssembly();
}


CSharpPluginImpl::~CSharpPluginImpl()
{
	unloadAssembly();
	mono_jit_cleanup(m_domain);
}


void CSharpPluginImpl::unloadAssembly()
{
	if (!m_assembly) return;

	m_on_assembly_unload.invoke();

	m_names.clear();
	if(mono_domain_get() != m_domain) mono_domain_set(m_domain, true);
	MonoObject *exc = NULL;
	mono_domain_finalize(m_assembly_domain, 2000);
	mono_domain_try_unload(m_assembly_domain, &exc);

	if (exc) {
		
		return;
	}
	m_assembly = nullptr;
	m_assembly_domain = nullptr;
}


void CSharpPluginImpl::loadAssembly()
{
	ASSERT(!m_assembly);
	
	const char* path = "cs\\main.dll";

	IAllocator& allocator = m_engine.getAllocator();
	m_assembly_domain = mono_domain_create_appdomain("lumix_runtime", nullptr);
	mono_domain_set(m_assembly_domain, false);
	m_assembly = mono_domain_assembly_open(m_assembly_domain, path);
	if (!m_assembly) return;

	MonoImage* img = mono_assembly_get_image(m_assembly);
	MonoClass* component_class = mono_class_from_name(img, "Lumix", "Component");

	m_names.clear();
	int num_types = mono_image_get_table_rows(img, MONO_TABLE_TYPEDEF);
	for (int i = 2; i <= num_types; ++i)
	{
		MonoClass* cl = mono_class_get(img, i | MONO_TOKEN_TYPE_DEF);
		const char* n = mono_class_get_name(cl);
		MonoClass* parent = mono_class_get_parent(cl);
		if (component_class == parent && !equalStrings(n, "NativeComponent"))
		{
			m_names.insert(crc32(n), string(n, allocator));
		}
	}
	m_on_assembly_load.invoke();
}

void CSharpPluginImpl::createScenes(Universe& universe)
{
	CSharpScriptSceneImpl* scene = LUMIX_NEW(m_engine.getAllocator(), CSharpScriptSceneImpl)(*this, universe);
	universe.addScene(scene);
}


LUMIX_PLUGIN_ENTRY(lumixengine_csharp)
{
	return LUMIX_NEW(engine.getAllocator(), CSharpPluginImpl)(engine);
}


}
#define CSHARP_FUNCTION(a,b,c,d,e);
//animation
CSHARP_FUNCTION(AnimationScene, getControllerInputIndex, nostatic, AnimController, component);
CSHARP_FUNCTION(AnimationScene, setControllerInput, nostatic, AnimController, component);

//navigation
CSHARP_FUNCTION(NavigationScene, cancelNavigation, nostatic, NavmeshAgent, component);
CSHARP_FUNCTION(NavigationScene, navigate, nostatic, NavmeshAgent, component);
CSHARP_FUNCTION(NavigationScene, getAgentSpeed, nostatic, NavmeshAgent, component);

//engine
CSHARP_FUNCTION(Universe, emplaceEntity, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, createEntity, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, destroyEntity, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, addComponent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, destroyComponent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, hasComponent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getComponent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getFirstComponent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getNextComponent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getFirstEntity, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getNextEntity, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getEntityName, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setEntityName, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, hasEntity, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, isDescendant, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getParent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getFirstChild, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getNextSibling, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getLocalTransform, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getLocalScale, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setParent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setLocalPosition, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setLocalRotation, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setLocalTransform, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, computeLocalTransform, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setMatrix, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getPositionAndRotation, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getMatrix, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setTransform, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setTransformKeepChildren, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getTransform, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setRotation, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setPosition, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setScale, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getScale, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getPosition, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getRotation, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getName, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, setName, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, serializeComponent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, deserializeComponent, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, serialize, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, deserialize, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getScene, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, addScene, nostatic, Entity, partial);
CSHARP_FUNCTION(Universe, getEntityByName, nostatic, Universe, partial);

//audio
CSHARP_FUNCTION(AudioScene, setEcho, nostatic, AudioScene, component);
CSHARP_FUNCTION(AudioScene, play, nostatic, AudioScene, component);
CSHARP_FUNCTION(AudioScene, stop, nostatic, AudioScene, component);
CSHARP_FUNCTION(AudioScene, setVolume, nostatic, AudioScene, component);
CSHARP_FUNCTION(InputSystem, isMouseDown, static, Input, class);
CSHARP_FUNCTION(NavigationScene, load, nostatic, Navigation, component);
CSHARP_FUNCTION(NavigationScene, save, nostatic, Navigation, component);
CSHARP_FUNCTION(PhysicsScene, raycast, nostatic, PhysicsScene, component);
CSHARP_FUNCTION(PhysicsScene, raycastEx, nostatic, PhysicsScene, component);
CSHARP_FUNCTION(PhysicsScene, getCollisionLayerName, nostatic, PhysicsScene, component);
CSHARP_FUNCTION(PhysicsScene, setCollisionLayerName, nostatic, PhysicsScene, component);
CSHARP_FUNCTION(PhysicsScene, canLayersCollide, nostatic, PhysicsScene, component);
CSHARP_FUNCTION(PhysicsScene, setLayersCanCollide, nostatic, PhysicsScene, component);
CSHARP_FUNCTION(PhysicsScene, getCollisionsLayersCount, nostatic, PhysicsScene, component);
CSHARP_FUNCTION(PhysicsScene, addCollisionLayer, nostatic, PhysicsScene, component);
CSHARP_FUNCTION(PhysicsScene, removeCollisionLayer, nostatic, PhysicsScene, component);
CSHARP_FUNCTION(Renderer, makeScreenshot, static, Renderer, class);
CSHARP_FUNCTION(Renderer, isOpenGL, static, Renderer, class);
CSHARP_FUNCTION(Renderer, getLayersCount, static, Renderer, class);
CSHARP_FUNCTION(Renderer, getLayer, static, Renderer, class);
CSHARP_FUNCTION(Renderer, getLayerName, static, Renderer, class);
CSHARP_FUNCTION(RenderScene, addDebugTriangle, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugPoint, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugCone, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugLine, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugCross, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugCube, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugCubeSolid, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugCircle, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugSphere, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugFrustum, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugCapsule, nostatic, RenderScene, component);
CSHARP_FUNCTION(RenderScene, addDebugCylinder, nostatic, RenderScene, component);


CSHARP_FUNCTION(AnimationScene, getAnimableAnimation, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getAnimation, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, setAnimation, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getAnimableTime, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, setAnimableTime, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, updateAnimable, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, updateController, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getControllerEntity, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getAnimableTimeScale, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, setAnimableTimeScale, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getAnimableStartTime, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, setAnimableStartTime, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getControllerInput, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, setControllerInput, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getControllerRootMotion, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, setControllerSource, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getControllerSource, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getControllerRoot, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getControllerInputIndex, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getSharedControllerParent, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, setSharedControllerParent, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, applyControllerSet, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, setControllerDefaultSet, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getControllerDefaultSet, nostatic, AnimationScene, component);
CSHARP_FUNCTION(AnimationScene, getControllerResource, nostatic, AnimationScene, component);
