#include "csharp.h"
#include "animation/animation.h"
#include "animation/animation_module.h"
#include "audio/audio_module.h"
#include "engine/engine.h"
#include "engine/geometry.h"
#include "engine/hash_map.h"
#include "engine/input_system.h"
#include "engine/log.h"
#include "engine/path.h"
#include "engine/prefab.h"
#include "engine/reflection.h"
#include "engine/resource_manager.h"
#include "engine/world.h"
#include "gui/gui_module.h"
#include "helpers.h"
#include "imgui/imgui.h"
#include "navigation/navigation_module.h"
#include "physics/physics_module.h"
#include "renderer/render_module.h"
#include "renderer/renderer.h"
#include <mono/metadata/exception.h>
#include <mono/metadata/mono-gc.h>
#include <mono/metadata/threads.h>

#pragma comment(lib, "mono-2.0-sgen.lib")


namespace Lumix {


static const ComponentType CSHARP_SCRIPT_TYPE = reflection::getComponentType("csharp_script");

static MonoString* GetStringProperty(const char* propertyName, MonoClass* classType, MonoObject* classObject) {
	MonoProperty* messageProperty;
	MonoMethod* messageGetter;
	MonoString* messageString;

	messageProperty = mono_class_get_property_from_name(classType, propertyName);
	messageGetter = mono_property_get_get_method(messageProperty);
	messageString = (MonoString*)mono_runtime_invoke(messageGetter, classObject, NULL, NULL);
	return messageString;
}


static void handleException(MonoObject* exc) {
	if (!exc) return;

	MonoClass* exception_class = mono_object_get_class(exc);
	MonoType* exception_type = mono_class_get_type(exception_class);
	MonoStringHolder type_name = mono_type_get_name(exception_type);
	MonoStringHolder message = GetStringProperty("Message", exception_class, exc);
	MonoStringHolder source = GetStringProperty("Source", exception_class, exc);
	MonoStringHolder stack_trace = GetStringProperty("StackTrace", exception_class, exc);
	MonoStringHolder target_site = GetStringProperty("TargetSite", exception_class, exc);
	if (message.isValid()) logError((const char*)message);
	if (source.isValid()) logError((const char*)source);
	if (stack_trace.isValid()) logError((const char*)stack_trace);
	if (target_site.isValid()) logError((const char*)target_site);
}


void getCSharpName(const char* in_name, StaticString<128>& class_name) {
	char* out = class_name.data;
	const char* in = in_name;
	bool to_upper = true;
	while (*in && out - class_name.data < lengthOf(class_name.data) - 1) {
		if (*in == '_' || *in == ' ' || *in == '-') {
			to_upper = true;
			++in;
			continue;
		}

		if (*in == '(') {
			to_upper = true;
			while (*in && *in != ')') ++in;
			if (*in == ')') ++in;
			continue;
		}

		if (to_upper) {
			*out = *in >= 'a' && *in <= 'z' ? *in - 'a' + 'A' : *in;
			to_upper = false;
		} else {
			*out = *in;
		}
		++out;
		++in;
	}
	*out = '\0';
}


struct CSharpSystemImpl : public CSharpSystem {
	CSharpSystemImpl(Engine& engine);
	~CSharpSystemImpl();
	const char* getName() const override { return "csharp_script"; }
	void createModules(World& world) override;
	void* getAssembly() const override;
	void* getDomain() const override;
	void unloadAssembly() override;
	void loadAssembly() override;
	const HashMap<RuntimeHash, String>& getNames() const override { return m_names; }
	const Array<String>& getNamesArray() const override { return m_names_array; }
	void setStaticField(const char* name_space, const char* class_name, const char* field_name, void* value);
	void registerProperties();
	void serialize(OutputMemoryStream& serializer) const override {}
	bool deserialize(i32 version, InputMemoryStream& serializer) override { return version == 0; }

	Engine& m_engine;
	IAllocator& m_allocator;
	MonoDomain* m_domain = nullptr;
	MonoAssembly* m_assembly = nullptr;
	MonoDomain* m_assembly_domain = nullptr;
	HashMap<RuntimeHash, String> m_names;
	Array<String> m_names_array;
	DelegateList<void()> m_on_assembly_unload;
	DelegateList<void()> m_on_assembly_load;
};


MonoString* csharp_Resource_getPath(Resource* resource) {
	if (resource == nullptr) return mono_string_new(mono_domain_get(), "");
	return mono_string_new(mono_domain_get(), resource->getPath().c_str());
}

Resource* csharp_Resource_load(Engine& engine, MonoString* path, MonoString* type) {
	MonoStringHolder type_str = type;
	ResourceType res_type((const char*)type_str);
	MonoStringHolder path_str = path;
	ResourceManagerHub& rm = engine.getResourceManager();
	return rm.load(res_type, Path((const char*)path_str));
}

MonoObject* csharp_getEntity(World* world, int entity_idx) {
	auto* cs_module = (CSharpScriptModule*)world->getModule(CSHARP_SCRIPT_TYPE);
	u32 gc_handle = cs_module->getEntityGCHandle({entity_idx});
	return mono_gchandle_get_target(gc_handle);
}

/*
bool csharp_Entity_hasComponent(World* world, Entity entity, MonoString* type) {
	MonoStringHolder type_str = type;
	ComponentType cmp_type = reflection::getComponentType((const char*)type_str);
	return world->hasComponent(entity, cmp_type);
}


void csharp_Entity_setParent(World* world, Entity parent, Entity child) {
	world->setParent(parent, child);
}


Entity csharp_Entity_getParent(World* world, Entity entity) {
	return world->getParent(entity);
}


World* csharp_getUniverse(IModule* module) {
	return &module->getUniverse();
}



IModule* csharp_getScene(World* world, MonoString* type_str) {
	MonoStringHolder type = type_str;
	ComponentType cmp_type = reflection::getComponentType((const char*)type);
	return world->getModule(cmp_type);
}


Entity csharp_instantiatePrefab(World* world, PrefabResource* prefab, const Vec3& pos, const Quat& rot, float scale) {
	return world->instantiatePrefab(*prefab, pos, rot, scale);
}


IModule* csharp_getSceneByName(World* world, MonoString* type_str) {
	MonoStringHolder name = type_str;
	return world->getModule(crc32((const char*)name));
}



u64 csharp_Component_getEntityGUIDFromID(ISerializer* serializer, int id) {
	return serializer->getGUID({id}).value;
}


void csharp_Component_create(World* world, int entity, MonoString* type_str) {
	MonoStringHolder type = type_str;
	ComponentType cmp_type = reflection::getComponentType((const char*)type);
	IModule* module = world->getModule(cmp_type);
	if (!module) return;
	if (world->hasComponent({entity}, cmp_type)) {
		logError("C# Script") << "Component " << (const char*)type << " already exists in entity " << entity;
		return;
	}

	world->createComponent(cmp_type, {entity});
}


void csharp_Entity_destroy(World* world, int entity) {
	world->destroyEntity({entity});
}
*/

int csharp_Component_getEntityFromEntityMap(EntityMap* entity_map, int entity_id) {
	return entity_map->get(EntityPtr{entity_id}).index;
}

void csharp_Entity_setPosition(World* world, int entity, const DVec3& pos) {
	world->setPosition({entity}, pos);
}

void csharp_Entity_setRotation(World* world, int entity, const Quat& pos) {
	world->setRotation({entity}, pos);
}

void csharp_Entity_setLocalRotation(World* world, int entity, const Quat& pos) {
	world->setLocalRotation({entity}, pos);
}

DVec3 csharp_Entity_getPosition(World* world, int entity) {
	return world->getPosition({entity});
}

void csharp_Entity_setLocalPosition(World* world, int entity, const DVec3& pos) {
	world->setLocalPosition({entity}, pos);
}

DVec3 csharp_Entity_getLocalPosition(World* world, int entity) {
	return world->getLocalTransform({entity}).pos;
}

Quat csharp_Entity_getLocalRotation(World* world, int entity) {
	return world->getLocalTransform({entity}).rot;
}


Quat csharp_Entity_getRotation(World* world, int entity) {
	return world->getRotation({entity});
}


void csharp_Entity_setName(World* world, int entity, MonoString* name) {
	MonoStringHolder str = name;
	world->setEntityName({entity}, (const char*)str);
}


MonoString* csharp_Entity_getName(World* world, int entity) {
	return mono_string_new(mono_domain_get(), world->getEntityName({entity}));
}


template <typename T> struct ToCSharpType { typedef T Type; };
template <> struct ToCSharpType<const char*> { typedef MonoString* Type; };
template <> struct ToCSharpType<Path> { typedef MonoString* Type; };
template <typename T> T toCSharpValue(T val) {
	return val;
}
MonoString* toCSharpValue(const char* val) { return mono_string_new(mono_domain_get(), val); }
MonoString* toCSharpValue(const Path& val) { return mono_string_new(mono_domain_get(), val.c_str()); }

template <typename T> T fromCSharpValue(T val) { return val; }
const char* fromCSharpValue(MonoString* val) { return mono_string_to_utf8(val); }

template <typename T> struct CSharpTypeConvertor {
	using Type = T;

	static Type convert(Type val) { return val; }
	static Type convertRet(Type val) { return val; }
};


template <> struct CSharpTypeConvertor<void> { using Type = void; };


template <> struct CSharpTypeConvertor<const char*> {
	using Type = MonoString*;

	static const char* convert(MonoString* val) { return mono_string_to_utf8(val); }
	static MonoString* convertRet(const char* val) { return mono_string_new(mono_domain_get(), val); }
};


template <> struct CSharpTypeConvertor<const Path&> {
	using Type = MonoString*;

	static Path convert(MonoString* val) { return Path(mono_string_to_utf8(val)); }
	static MonoString* convertRet(const Path& val) { return mono_string_new(mono_domain_get(), val.c_str()); }
};


template <> struct CSharpTypeConvertor<const ImVec2&> {
	struct Vec2POD {
		float x;
		float y;
	};
	using Type = Vec2POD;

	static ImVec2 convert(Type& val) { return *(const ImVec2*)&val; }
};


template <> struct CSharpTypeConvertor<ImVec2> {
	struct Vec2POD {
		float x;
		float y;
	};
	using Type = Vec2POD;

	static Type convert(ImVec2& val) { return *(Vec2POD*)&val; }
	static Type convert(const ImVec2& val) { return *(Vec2POD*)&val; }
};


template <typename F> struct CSharpFunctionProxy;
template <typename F> struct CSharpMethodProxy;


template <typename R, typename... Args> struct CSharpFunctionProxy<R(Args...)> {
	using F = R(Args...);

	template <F fnc> static typename CSharpTypeConvertor<R>::Type call(typename CSharpTypeConvertor<Args>::Type... args) {
		return CSharpTypeConvertor<R>::convert(fnc(CSharpTypeConvertor<Args>::convert(args)...));
	}
};

template <typename... Args> struct CSharpFunctionProxy<void(Args...)> {
	using F = void(Args...);

	template <F fnc> static void call(typename CSharpTypeConvertor<Args>::Type... args) { fnc(CSharpTypeConvertor<Args>::convert(args)...); }
};


template <bool B, class T = void> struct enable_if {};

template <class T> struct enable_if<true, T> { typedef T type; };


template <typename T1, typename T2> struct is_same { static constexpr bool value = false; };

template <typename T> struct is_same<T, T> { static constexpr bool value = true; };

template <typename R, typename T, typename... Args> struct CSharpMethodProxy<R (T::*)(Args...) const> {
	using F = R (T::*)(Args...) const;
	using ConvertedR = typename CSharpTypeConvertor<R>::Type;

	template <F fnc, typename Ret = R> static typename enable_if<is_same<Ret, void>::value, Ret>::type call(T* inst, typename CSharpTypeConvertor<Args>::Type... args) {
		(inst->*fnc)(CSharpTypeConvertor<Args>::convert(args)...);
	}

	template <F fnc, typename Ret = R> static typename enable_if<!is_same<Ret, void>::value, ConvertedR>::type call(T* inst, typename CSharpTypeConvertor<Args>::Type... args) {
		return CSharpTypeConvertor<R>::convertRet((inst->*fnc)(CSharpTypeConvertor<Args>::convert(args)...));
	}
};


template <typename R, typename T, typename... Args> struct CSharpMethodProxy<R (T::*)(Args...)> {
	using F = R (T::*)(Args...);
	using ConvertedR = typename CSharpTypeConvertor<R>::Type;

	template <F fnc, typename Ret = R> static typename enable_if<is_same<Ret, void>::value, void>::type call(T* inst, typename CSharpTypeConvertor<Args>::Type... args) {
		(inst->*fnc)(CSharpTypeConvertor<Args>::convert(args)...);
	}

	template <F fnc, typename Ret = R> static typename enable_if<!is_same<Ret, void>::value, ConvertedR>::type call(T* inst, typename CSharpTypeConvertor<Args>::Type... args) {
		return CSharpTypeConvertor<R>::convertRet((inst->*fnc)(CSharpTypeConvertor<Args>::convert(args)...));
	}
};

template <typename T>
struct CSPropertyWrapper {
	using CSType = typename ToCSharpType<T>::Type;
	
	static void cs_setter(IModule* module, EntityRef e, u32 array_idx, CSType val) {
		setter(module, e, array_idx, T(fromCSharpValue(val)));
	}

	static CSType cs_getter(IModule* module, EntityRef e, u32 array_idx) {
		return toCSharpValue(getter(module, e, array_idx));
	}
		
	static inline T (*getter)(IModule*, EntityRef, u32);
	static inline void (*setter)(IModule*, EntityRef, u32, const T&);
};

template <typename T>
const reflection::Property<T>* getProperty(const char* cmp_type_name, const char* prop_name) {
	ComponentType cmp_type = reflection::getComponentType(cmp_type_name);
	return static_cast<const reflection::Property<T>*>(reflection::getProperty(cmp_type, prop_name));
}

template <typename C, int (C::*Function)(EntityRef)> int csharp_getSubobjectCount(C* module, int entity) {
	return (module->*Function)({entity});
}


template <typename C, void (C::*Function)(EntityRef, int)> void csharp_addSubobject(C* module, int entity) {
	(module->*Function)({entity}, -1);
}


template <typename C, void (C::*Function)(EntityRef, int)> void csharp_removeSubobject(C* module, int entity, int index) {
	(module->*Function)({entity}, index);
}


template <typename R, typename C, R (C::*Function)(EntityRef, int)> typename ToCSharpType<R>::Type csharp_getSubproperty(C* module, int entity, int index) {
	R val = (module->*Function)({entity}, index);
	return toCSharpValue(val);
}

/*
template <auto setter>
void csharp_setProperty(typename ClassOf<decltype(setter)>::Type* module, int cmp, typename ToCSharpType<typename ArgNType<1, decltype(setter)>::Type>::Type value) {
	(module->*setter)({cmp}, fromCSharpValue(value));
}


template <typename T, typename C, void (C::*Function)(EntityRef, int, T)> void csharp_setSubproperty(C* module, int entity, typename ToCSharpType<T>::Type value, int index) {
	(module->*Function)({entity}, index, fromCSharpValue(value));
}


template <typename T, typename C, void (C::*Function)(EntityRef, const T&)> void csharp_setProperty(C* module, int entity, typename ToCSharpType<T>::Type value) {
	(module->*Function)({entity}, T(fromCSharpValue(value)));
}


Resource* csharp_loadResource(Engine* engine, MonoString* path, MonoString* type) {
	MonoStringHolder type_str = type;
	ResourceType res_type((const char*)type_str);
	ResourceManagerBase* manager = engine->getResourceManager().get(res_type);
	if (!manager) return nullptr;

	MonoStringHolder path_str = path;
	return manager->load(Path((const char*)path_str));
}
*/

void csharp_logError(MonoString* message) {
	MonoStringHolder tmp = message;
	logError((const char*)tmp);
}


struct CSharpScriptModuleImpl : public CSharpScriptModule {
	struct Script {
		Script(IAllocator& allocator)
			: properties(allocator) {}

		enum Flags : u32 { HAS_UPDATE = 1 << 0, HAS_ON_INPUT = 1 << 2 };

		RuntimeHash script_name_hash;
		u32 gc_handle = INVALID_GC_HANDLE;
		Flags flags;
		String properties;
	};


	struct ScriptComponent {
		ScriptComponent(IAllocator& allocator)
			: scripts(allocator) {}

		Array<Script> scripts;
		EntityRef entity;
	};


	CSharpScriptModuleImpl(CSharpSystemImpl& plugin, World& world)
		: m_system(plugin)
		, m_world(world)
		, m_scripts(plugin.m_allocator)
		, m_entities_gc_handles(plugin.m_allocator)
		, m_updates(plugin.m_allocator)
		, m_on_inputs(plugin.m_allocator)
		, m_is_game_running(false) {

#include "api.inl"

		createImGuiAPI();
		createEngineAPI();

		m_system.m_on_assembly_load.bind<&CSharpScriptModuleImpl::onAssemblyLoad>(this);
		m_system.m_on_assembly_unload.bind<&CSharpScriptModuleImpl::onAssemblyUnload>(this);
		onAssemblyLoad();
	}


	~CSharpScriptModuleImpl() {
		for (u32 handle : m_entities_gc_handles) {
			mono_gchandle_free(handle);
		}
		for (ScriptComponent* script_cmp : m_scripts) {
			for (Script& script : script_cmp->scripts) {
				setScriptNameHash(*script_cmp, script, RuntimeHash());
			}
			LUMIX_DELETE(m_system.m_allocator, script_cmp);
		}
		m_system.m_on_assembly_load.unbind<&CSharpScriptModuleImpl::onAssemblyLoad>(this);
		m_system.m_on_assembly_unload.unbind<&CSharpScriptModuleImpl::onAssemblyUnload>(this);
	}

	const char* getName() const override { return "csharp_script"; }

	void onAssemblyLoad() {
		for (ScriptComponent* cmp : m_scripts) {
			createCSharpEntity(cmp->entity);
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts) {
				setScriptNameHash(*cmp, script, script.script_name_hash);
			}
			applyProperties(*cmp);
		}
	}


	void onAssemblyUnload() {
		m_updates.clear();
		m_on_inputs.clear();
		for (u32 handle : m_entities_gc_handles) {
			mono_gchandle_free(handle);
		}
		m_entities_gc_handles.clear();
		for (ScriptComponent* cmp : m_scripts) {
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts) {
				if (script.gc_handle != INVALID_GC_HANDLE) mono_gchandle_free(script.gc_handle);
				script.gc_handle = INVALID_GC_HANDLE;
			}
		}
	}


	static void imgui_Text(const char* text) { ImGui::Text("%s", text); }


	static void imgui_LabelText(const char* label, const char* text) { ImGui::LabelText(label, "%s", text); }
	
	void createImGuiAPI() {
		mono_add_internal_call("ImGui::Begin", &CSharpFunctionProxy<bool(const char*, bool*, ImGuiWindowFlags)>::call<ImGui::Begin>);
		mono_add_internal_call("ImGui::CollapsingHeader", &CSharpFunctionProxy<bool(const char*, bool*, ImGuiTreeNodeFlags)>::call<ImGui::CollapsingHeader>);
		mono_add_internal_call("ImGui::LabelText", &CSharpFunctionProxy<void(const char*, const char*)>::call<imgui_LabelText>);
		mono_add_internal_call("ImGui::PushID", &CSharpFunctionProxy<void(const char*)>::call<ImGui::PushID>);
		mono_add_internal_call("ImGui::Selectable", &CSharpFunctionProxy<bool(const char*, bool, ImGuiSelectableFlags, const ImVec2&)>::call<ImGui::Selectable>);
		mono_add_internal_call("ImGui::Text", &CSharpFunctionProxy<void(const char*)>::call<imgui_Text>);
		mono_add_internal_call("ImGui::InputText", &CSharpFunctionProxy<decltype(ImGui::InputText)>::call<ImGui::InputText>);

#define REGISTER_FUNCTION(F) mono_add_internal_call("ImGui::" #F, &CSharpFunctionProxy<decltype(ImGui::F)>::call<ImGui::F>);
		REGISTER_FUNCTION(AlignTextToFramePadding);
		REGISTER_FUNCTION(BeginChildFrame);
		REGISTER_FUNCTION(BeginPopup);
		REGISTER_FUNCTION(Button);
		REGISTER_FUNCTION(Checkbox);
		REGISTER_FUNCTION(Columns);
		REGISTER_FUNCTION(DragFloat);
		REGISTER_FUNCTION(Dummy);
		REGISTER_FUNCTION(End);
		REGISTER_FUNCTION(EndChildFrame);
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
//		REGISTER_FUNCTION(OpenPopup);
		REGISTER_FUNCTION(PopItemWidth);
		REGISTER_FUNCTION(PopID);
		REGISTER_FUNCTION(PushItemWidth);
//		REGISTER_FUNCTION(Rect);
		REGISTER_FUNCTION(SameLine);
		REGISTER_FUNCTION(Separator);
		REGISTER_FUNCTION(SetCursorScreenPos);
		REGISTER_FUNCTION(SetNextWindowPos);
		REGISTER_FUNCTION(SetNextWindowSize);
		REGISTER_FUNCTION(SliderFloat);
		REGISTER_FUNCTION(Unindent);
#undef REGISTER_FUNCTION
	}


	void createEngineAPI() {
		mono_add_internal_call("Lumix.Engine::logError", csharp_logError);
		// TODO
		/*
		mono_add_internal_call("Lumix.Engine::loadResource", csharp_loadResource);
		mono_add_internal_call("Lumix.Component::getEntityIDFromGUID", csharp_Component_getEntityIDFromGUID);
		mono_add_internal_call("Lumix.Component::create", csharp_Component_create);
		mono_add_internal_call("Lumix.Component::getScene", csharp_getScene);
		mono_add_internal_call("Lumix.World::instantiatePrefab", csharp_instantiatePrefab);
		mono_add_internal_call("Lumix.World::getSceneByName", csharp_getSceneByName);
		mono_add_internal_call("Lumix.IModule::getUniverse", csharp_getUniverse);
		mono_add_internal_call("Lumix.Entity::hasComponent", csharp_Entity_hasComponent);
		mono_add_internal_call("Lumix.Entity::setParent", csharp_Entity_setParent);
		mono_add_internal_call("Lumix.Entity::getParent", csharp_Entity_getParent);
		mono_add_internal_call("Lumix.Entity::destroy", csharp_Entity_destroy);*/
		mono_add_internal_call("Lumix.World::getEntity", csharp_getEntity);
		mono_add_internal_call("Lumix.Component::getEntityFromEntityMap", csharp_Component_getEntityFromEntityMap);
		mono_add_internal_call("Lumix.Entity::setPosition", csharp_Entity_setPosition);
		mono_add_internal_call("Lumix.Entity::setRotation", csharp_Entity_setRotation);
		mono_add_internal_call("Lumix.Entity::getPosition", csharp_Entity_getPosition);
		mono_add_internal_call("Lumix.Entity::setLocalPosition", csharp_Entity_setLocalPosition);
		mono_add_internal_call("Lumix.Entity::getLocalPosition", csharp_Entity_getLocalPosition);
		mono_add_internal_call("Lumix.Entity::setLocalRotation", csharp_Entity_setLocalRotation);
		mono_add_internal_call("Lumix.Entity::getLocalRotation", csharp_Entity_getLocalRotation);
		mono_add_internal_call("Lumix.Entity::getRotation", csharp_Entity_getRotation);
		mono_add_internal_call("Lumix.Entity::setName", csharp_Entity_setName);
		mono_add_internal_call("Lumix.Entity::getName", csharp_Entity_getName);
		mono_add_internal_call("Lumix.Resource::getPath", csharp_Resource_getPath);
		mono_add_internal_call("Lumix.Resource::load", csharp_Resource_load);
	}


	void onContact(const PhysicsModule::ContactData& data) {
		ASSERT(false); // TODO
					   /*MonoObject* e1_obj = mono_gchandle_get_target(getEntityGCHandle(data.e1));
					   MonoObject* e2_obj = mono_gchandle_get_target(getEntityGCHandle(data.e2));
					   auto call = [this, &data](const ScriptComponent* cmp, MonoObject* entity) {
						   for (const Script& scr : cmp->scripts) {
							   tryCallMethod(true, scr.gc_handle, nullptr, "OnContact", entity, data.position);
						   }
					   };
			   
					   int idx = m_scripts.find(data.e1);
					   if (idx >= 0) call(m_scripts.at(idx), e2_obj);
					   idx = m_scripts.find(data.e2);
					   if (idx >= 0) call(m_scripts.at(idx), e1_obj);*/
	}

	void startGame() override {
		PhysicsModule* phy_scene = (PhysicsModule*)m_world.getModule("physics");
		if (phy_scene) {
			phy_scene->onContact().bind<&CSharpScriptModuleImpl::onContact>(this);
		}

		for (ScriptComponent* cmp : m_scripts) {
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts) {
				tryCallMethod(script.gc_handle, "Start", nullptr, 0, false);
			}
		}
		m_is_game_running = true;
	}


	void stopGame() override {
		m_is_game_running = false;
		PhysicsModule* phy_scene = (PhysicsModule*)m_world.getModule("physics");
		if (phy_scene) {
			phy_scene->onContact().unbind<&CSharpScriptModuleImpl::onContact>(this);
			;
		}
	}


	void getClassName(RuntimeHash name_hash, char (&out_name)[256]) const {
		auto iter = m_system.m_names.find(name_hash);
		if (!iter.isValid()) {
			out_name[0] = 0;
			return;
		}

		copyString(out_name, iter.value().c_str());
	}


	enum class ScriptClass : u32 { UNKNOWN, ENTITY, RESOURCE };

	/*
	void serializeCSharpScript(ISerializer& serializer, Entity entity) {
		ScriptComponent* script = m_scripts[entity];
		serializer.write("count", script->scripts.size());
		for (Script& inst : script->scripts) {
			serializer.write("script_name_hash", inst.script_name_hash);
			if (inst.gc_handle == INVALID_GC_HANDLE) {
				serializer.write("properties", "");
				continue;
			}

			MonoObject* obj = mono_gchandle_get_target(inst.gc_handle);
			MonoObject* res;
			if (tryCallMethod(true, obj, &res, "Serialize", &serializer)) {
				MonoObject* exc;
				MonoStringHolder str = mono_object_to_string(res, &exc);
				if (exc) {
					handleException(exc);
				} else {
					serializer.write("properties", (const char*)str);
				}
			} else {
				serializer.write("properties", "");
			}
		}
	}
	*/

	void applyProperties(ScriptComponent& script) {
		EntityMap map(m_system.m_allocator);
		for (Script& inst : script.scripts) {
			if (inst.gc_handle == INVALID_GC_HANDLE) continue;
			if (inst.properties.length() == 0) continue;

			MonoObject* obj = mono_gchandle_get_target(inst.gc_handle);
			MonoString* str = mono_string_new(mono_domain_get(), inst.properties.c_str());
			tryCallMethod(true, obj, nullptr, "Deserialize", str, &map);
		}
	}

	/*
	void deserializeCSharpScript(IDeserializer& serializer, Entity entity, int scene_version) {
		auto& allocator = m_system.m_allocator;
		ScriptComponent* script = LUMIX_NEW(allocator, ScriptComponent)(allocator);
		script->entity = entity;
		m_scripts.insert(entity, script);
		createCSharpEntity(script->entity);

		int count;
		serializer.read(&count);
		script->scripts.reserve(count);
		string tmp(allocator);
		for (int i = 0; i < count; ++i) {
			Script& inst = script->scripts.emplace(allocator);
			u32 hash;
			serializer.read(&hash);
			setScriptNameHash(entity, i, hash);

			serializer.read(&tmp);
			if (m_system.m_assembly) {
				MonoObject* res;
				MonoClass* mono_class = mono_class_from_name(mono_assembly_get_image(m_system.m_assembly), "Lumix", "Component");

				MonoString* str_arg = mono_string_new(mono_domain_get(), tmp.c_str());
				if (tryCallStaticMethod(true, mono_class, &res, "ConvertGUIDToID", str_arg, &serializer)) {
					MonoObject* exc;
					MonoStringHolder str = mono_object_to_string(res, &exc);
					if (exc) {
						handleException(exc);
					} else {
						inst.properties = (const char*)str;
					}
				}
			}
		}

		if (m_system.m_assembly) applyProperties(*script);

		m_world.onComponentCreated(entity, CSHARP_SCRIPT_TYPE, this);
	}
	*/

	void serializeScript(EntityRef entity, int scr_index, OutputMemoryStream& blob) override {
		Script& scr = m_scripts[entity]->scripts[scr_index];
		blob.write(scr.script_name_hash);
	}


	void deserializeScript(EntityRef entity, int scr_index, InputMemoryStream& blob) override {
		RuntimeHash name_hash = blob.read<RuntimeHash>();
		setScriptNameHash(entity, scr_index, name_hash);
	}

	void insertScript(EntityRef entity, int idx) override { m_scripts[entity]->scripts.emplaceAt(idx, m_system.m_allocator); }

	int addScript(EntityRef entity, int scr_index) override {
		ScriptComponent* script_cmp = m_scripts[entity];
		if (scr_index == -1) scr_index = script_cmp->scripts.size();
		script_cmp->scripts.emplaceAt(scr_index, m_system.m_allocator);
		return scr_index;
	}


	void removeScript(EntityRef entity, int scr_index) override {
		setScriptNameHash(entity, scr_index, RuntimeHash());
		m_scripts[entity]->scripts.erase(scr_index);
	}


	u32 getEntityGCHandle(EntityRef entity) override {
		auto iter = m_entities_gc_handles.find(entity);
		if (iter.isValid()) return iter.value();
		u32 handle = createCSharpEntity(entity);
		return handle;
	}


	u32 getGCHandle(EntityRef entity, int scr_index) const override {
		Script& scr = m_scripts[entity]->scripts[scr_index];
		return scr.gc_handle;
	}


	int getScriptCount(EntityRef entity) const override { return m_scripts[entity]->scripts.size(); }


	const char* getScriptName(EntityRef entity, int scr_index) override { 
		const RuntimeHash hash = m_scripts[entity]->scripts[scr_index].script_name_hash;
		auto iter = m_system.m_names.find(hash);
		return iter.isValid() ? iter.value().c_str() : "N/A";
	}
	
	void setScriptName(EntityRef entity, int scr_index, const char* name) override {
		const RuntimeHash name_hash(name);
		ScriptComponent* cmp = m_scripts[entity];
		setScriptNameHash(*cmp, cmp->scripts[scr_index], name_hash);
	}

	void setScriptNameHash(ScriptComponent& cmp, Script& script, RuntimeHash name_hash) {
		if (script.gc_handle != INVALID_GC_HANDLE) {
			ASSERT(m_system.m_assembly);
			mono_gchandle_free(script.gc_handle);
			if (script.flags & Script::HAS_UPDATE) {
				script.flags &= ~Script::HAS_UPDATE;
				m_updates.eraseItems([&script](u32 iter) { return iter == script.gc_handle; });
			}
			if (script.flags & Script::HAS_ON_INPUT) {
				script.flags &= ~Script::HAS_ON_INPUT;
				m_on_inputs.eraseItems([&script](u32 iter) { return iter == script.gc_handle; });
			}
			script.script_name_hash = RuntimeHash();
			script.gc_handle = INVALID_GC_HANDLE;
		}

		if (name_hash != RuntimeHash()) {
			if (m_system.m_assembly) {
				char class_name[256];
				getClassName(name_hash, class_name);
				script.gc_handle = createObjectGC("", class_name);
				if (script.gc_handle != INVALID_GC_HANDLE) setCSharpComponent(script, cmp);
			}

			script.script_name_hash = name_hash;
		}
	}


	void setCSharpComponent(Script& script, ScriptComponent& cmp) {
		MonoObject* obj = mono_gchandle_get_target(script.gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);

		MonoProperty* prop = mono_class_get_property_from_name(mono_class, "entity");
		ASSERT(prop);

		u32 handle = m_entities_gc_handles[cmp.entity];
		MonoObject* entity_obj = mono_gchandle_get_target(handle);
		ASSERT(entity_obj);

		MonoMethod* method = mono_property_get_set_method(prop);
		MonoObject* exc;
		void* mono_args[] = {entity_obj};
		MonoObject* res = mono_runtime_invoke(method, obj, mono_args, &exc);

		if (mono_class_get_method_from_name(mono_class, "Update", 1)) {
			m_updates.push(script.gc_handle);
			script.flags |= Script::HAS_UPDATE;
		}
		if (mono_class_get_method_from_name(mono_class, "OnInput", 1)) {
			m_on_inputs.push(script.gc_handle);
			script.flags |= Script::HAS_ON_INPUT;
		}
	}


	void setScriptNameHash(EntityRef entity, int scr_index, RuntimeHash name_hash) {
		ScriptComponent* script_cmp = m_scripts[entity];
		if (script_cmp->scripts.size() <= scr_index) return;

		setScriptNameHash(*script_cmp, script_cmp->scripts[scr_index], name_hash);
	}


	u32 createCSharpEntity(EntityRef entity) {
		if (!m_system.m_assembly) return INVALID_GC_HANDLE;

		u32 handle = createObjectGC("Lumix", "Entity", &m_world);
		m_entities_gc_handles.insert(entity, handle);

		MonoObject* obj = mono_gchandle_get_target(handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);

		MonoClassField* field = mono_class_get_field_from_name(mono_class, "entity_Id_");
		ASSERT(field);

		mono_field_set_value(obj, field, &entity.index);

		MonoClassField* universe_field = mono_class_get_field_from_name(mono_class, "instance_");
		ASSERT(universe_field);

		void* y = &m_world;
		mono_field_set_value(obj, universe_field, &y);

		World* x;
		mono_field_get_value(obj, universe_field, &x);

		return handle;
	}


	void createScriptComponent(EntityRef entity) {
		auto& allocator = m_system.m_allocator;

		ScriptComponent* script = LUMIX_NEW(allocator, ScriptComponent)(allocator);
		script->entity = entity;
		m_scripts.insert(entity, script);
		createCSharpEntity(script->entity);
		m_world.onComponentCreated(entity, CSHARP_SCRIPT_TYPE, this);
	}


	void destroyScriptComponent(EntityRef entity) {
		auto* script = m_scripts[entity];
		auto handle_iter = m_entities_gc_handles.find(script->entity);
		if (handle_iter.isValid()) {
			mono_gchandle_free(handle_iter.value());
			m_entities_gc_handles.erase(handle_iter);
		}
		for (Script& scr : script->scripts) {
			setScriptNameHash(*script, scr, RuntimeHash());
		}
		LUMIX_DELETE(m_system.m_allocator, script);
		m_scripts.erase(entity);
		m_world.onComponentDestroyed(entity, CSHARP_SCRIPT_TYPE, this);
	}


	void serialize(OutputMemoryStream& serializer) override {
		serializer.write(m_scripts.size());
		for (auto iter = m_scripts.begin(), end = m_scripts.end(); iter != end; ++iter) {
			ScriptComponent* script_cmp = iter.value();
			serializer.write(script_cmp->entity);
			serializer.write(script_cmp->scripts.size());
			for (int i = 0, n = script_cmp->scripts.size(); i < n; ++i) {
				Script& scr = script_cmp->scripts[i];
				serializer.write(scr.script_name_hash);

				MonoObject* obj = mono_gchandle_get_target(scr.gc_handle);
				MonoObject* cs_serialized;
				if (obj && tryCallMethod(true, obj, &cs_serialized, "Serialize")) {
					MonoObject* exc;
					MonoStringHolder str = mono_object_to_string(cs_serialized, &exc);
					if (exc) {
						handleException(exc);
						serializer.writeString("");
					} else {
						serializer.writeString((const char*)str);
					}
				} else {
					serializer.writeString("");
				}
			}
		}
	}


	void deserialize(InputMemoryStream& serializer, const EntityMap& entity_map, i32 version) override {
		const i32 len = serializer.read<int>();
		m_scripts.reserve(len);
		IAllocator& allocator = m_system.m_allocator;
		for (int i = 0; i < len; ++i) {
			ScriptComponent* script = LUMIX_NEW(allocator, ScriptComponent)(allocator);

			serializer.read(script->entity);
			m_scripts.insert(script->entity, script);
			createCSharpEntity(script->entity);
			int scr_count;
			serializer.read(scr_count);
			for (int j = 0; j < scr_count; ++j) {
				Script& scr = script->scripts.emplace(m_system.m_allocator);
				scr.gc_handle = INVALID_GC_HANDLE;
				serializer.read(scr.script_name_hash);
				setScriptNameHash(*script, scr, scr.script_name_hash);

				MonoObject* obj = mono_gchandle_get_target(scr.gc_handle);
				const char* c_str = serializer.readString();
				if (obj) {
					MonoString* str = mono_string_new(mono_domain_get(), (const char*)c_str);
					tryCallMethod(true, obj, nullptr, "Deserialize", str, &entity_map);
				}
			}
			m_world.onComponentCreated(script->entity, CSHARP_SCRIPT_TYPE, this);
		}
	}

	ISystem& getSystem() const override { return m_system; }


	MonoObject* createKeyboardEvent(const InputSystem::Event& event) {
		if (event.type == InputSystem::Event::BUTTON) {
			u32 event_gc_handle = createObjectGC("Lumix", "KeyboardInputEvent");
			MonoObject* obj = mono_gchandle_get_target(event_gc_handle);

			MonoClass* mono_class = mono_object_get_class(obj);
			MonoClassField* field = mono_class_get_field_from_name(mono_class, "key_id");
			mono_field_set_value(obj, field, (void*)&event.data.button.key_id);

			field = mono_class_get_field_from_name(mono_class, "key_id");
			mono_field_set_value(obj, field, (void*)&event.data.button.key_id);

			field = mono_class_get_field_from_name(mono_class, "is_down");
			bool is_down = event.data.button.down;
			mono_field_set_value(obj, field, (void*)&is_down);

			return obj;
		}

		return nullptr;
	}


	MonoObject* createMouseEvent(const InputSystem::Event& event) {
		switch (event.type) {
			case InputSystem::Event::AXIS: {
				u32 event_gc_handle = createObjectGC("Lumix", "MouseAxisInputEvent");
				MonoObject* obj = mono_gchandle_get_target(event_gc_handle);

				MonoClass* mono_class = mono_object_get_class(obj);

				MonoClassField* field = mono_class_get_field_from_name(mono_class, "x");
				mono_field_set_value(obj, field, (void*)&event.data.axis.x);

				field = mono_class_get_field_from_name(mono_class, "y");
				mono_field_set_value(obj, field, (void*)&event.data.axis.y);

				field = mono_class_get_field_from_name(mono_class, "x_abs");
				mono_field_set_value(obj, field, (void*)&event.data.axis.x_abs);

				field = mono_class_get_field_from_name(mono_class, "y_abs");
				mono_field_set_value(obj, field, (void*)&event.data.axis.y_abs);

				return obj;
			}
			case InputSystem::Event::BUTTON: {
				u32 event_gc_handle = createObjectGC("Lumix", "MouseButtonInputEvent");
				MonoObject* obj = mono_gchandle_get_target(event_gc_handle);

				MonoClass* mono_class = mono_object_get_class(obj);

				MonoClassField* field = mono_class_get_field_from_name(mono_class, "key_id");
				mono_field_set_value(obj, field, (void*)&event.data.button.key_id);

				field = mono_class_get_field_from_name(mono_class, "x");
				mono_field_set_value(obj, field, (void*)&event.data.button.x);

				field = mono_class_get_field_from_name(mono_class, "y");
				mono_field_set_value(obj, field, (void*)&event.data.button.y);

				field = mono_class_get_field_from_name(mono_class, "is_down");
				bool is_down = event.data.button.down;
				mono_field_set_value(obj, field, (void*)&is_down);

				return obj;
			}
			default: 
				ASSERT(false);
				return nullptr;
		}
	}


	void processInput() {
		InputSystem& input = m_system.m_engine.getInputSystem();
		Span<const InputSystem::Event> events = input.getEvents();
		for (u32 gc_handle : m_on_inputs) {
			MonoObject* cs_event = nullptr;
			for (const InputSystem::Event& event : events) {
				switch (event.device->type) {
					case InputSystem::Device::KEYBOARD: {
						cs_event = createKeyboardEvent(event);
						break;
					}
					case InputSystem::Device::MOUSE: {
						cs_event = createMouseEvent(event);
						break;
					}
					case InputSystem::Device::CONTROLLER: ASSERT(false); /* TODO */ break;
				}
				tryCallMethod(true, gc_handle, nullptr, "OnInput", cs_event);
			}
		}
	}


	void update(float time_delta) override {
		if (!m_is_game_running) return;

		processInput();

		for (u32 gc_handle : m_updates) {
			tryCallMethod(true, gc_handle, nullptr, "Update", time_delta);
		}
	}


	World& getWorld() override { return m_world; }



	template <typename T> void* toCSharpArg(T* arg) { return (void*)arg; }

	template <> void* toCSharpArg<MonoString*>(MonoString** arg) { return *arg; }

	template <> void* toCSharpArg<MonoObject*>(MonoObject** arg) { return *arg; }


	template <typename... T> bool tryCallMethod(bool try_parents, u32 gc_handle, MonoObject** result, const char* method_name, T... args) {
		MonoObject* obj = mono_gchandle_get_target(gc_handle);
		if (!obj) return false;
		return tryCallMethod(try_parents, obj, result, method_name, args...);
	}

	template <typename... T> bool tryCallMethod(bool try_parents, MonoObject* obj, MonoObject** result, const char* method_name, T... args) {
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);
		ASSERT(mono_class);
		MonoMethod* method = mono_class_get_method_from_name(mono_class, method_name, sizeof...(args));
		if (!method && try_parents) {
			while (!method) {
				mono_class = mono_class_get_parent(mono_class);
				if (!mono_class) return false;
				method = mono_class_get_method_from_name(mono_class, method_name, sizeof...(args));
			}
		}
		if (!method) return false;

		MonoObject* exc = nullptr;
		void* mono_args[] = {toCSharpArg(&args)...};
		MonoObject* res = mono_runtime_invoke(method, obj, mono_args, &exc);
		handleException(exc);
		if (result && !exc) *result = res;

		return exc == nullptr;
	}

	template <typename... T> bool tryCallStaticMethod(bool try_parents, MonoClass* mono_class, MonoObject** result, const char* method_name, T... args) {
		ASSERT(mono_class);
		MonoMethod* method = mono_class_get_method_from_name(mono_class, method_name, sizeof...(args));
		if (!method && try_parents) {
			while (!method) {
				mono_class = mono_class_get_parent(mono_class);
				if (!mono_class) return false;
				method = mono_class_get_method_from_name(mono_class, method_name, sizeof...(args));
			}
		}
		if (!method) return false;

		MonoObject* exc = nullptr;
		void* mono_args[] = {toCSharpArg(&args)...};
		MonoObject* res = mono_runtime_invoke(method, nullptr, mono_args, &exc);
		handleException(exc);
		if (result && !exc) *result = res;

		return exc == nullptr;
	}


	bool tryCallMethod(bool try_parents, MonoObject* obj, MonoObject** result, const char* method_name) {
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);
		ASSERT(mono_class);
		MonoMethod* method = mono_class_get_method_from_name(mono_class, method_name, 0);
		if (!method && try_parents) {
			while (!method) {
				mono_class = mono_class_get_parent(mono_class);
				if (!mono_class) return false;
				method = mono_class_get_method_from_name(mono_class, method_name, 0);
			}
		}
		if (!method) return false;

		MonoObject* exc = nullptr;
		MonoObject* res = mono_runtime_invoke(method, obj, nullptr, &exc);
		handleException(exc);
		if (result && !exc) *result = res;

		return exc == nullptr;
	}


	bool tryCallMethod(u32 gc_handle, const char* method_name, void** args, int args_count, bool try_parents) override {
		if (gc_handle == INVALID_GC_HANDLE) return false;
		MonoObject* obj = mono_gchandle_get_target(gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);

		ASSERT(mono_class);
		MonoMethod* method = mono_class_get_method_from_name(mono_class, method_name, args_count);
		if (!method && try_parents) {
			while (!method) {
				mono_class = mono_class_get_parent(mono_class);
				if (!mono_class) return false;
				method = mono_class_get_method_from_name(mono_class, method_name, args_count);
			}
		}
		if (!method) return false;

		MonoObject* exc = nullptr;
		mono_runtime_invoke(method, obj, args, &exc);

		handleException(exc);
		return exc == nullptr;
	}


	u32 createObjectGC(const char* name_space, const char* class_name) {
		MonoObject* obj = createObject(name_space, class_name);
		if (!obj) return INVALID_GC_HANDLE;
		return mono_gchandle_new(obj, false);
	}


	MonoObject* createObject(const char* name_space, const char* class_name) {
		MonoClass* mono_class = mono_class_from_name(mono_assembly_get_image(m_system.m_assembly), name_space, class_name);
		if (!mono_class) return nullptr;

		MonoObject* obj = mono_object_new(mono_domain_get(), mono_class);
		if (!obj) return nullptr;

		mono_runtime_object_init(obj);
		return obj;
	}


	u32 createObjectGC(const char* name_space, const char* class_name, void* arg) {
		MonoClass* mono_class = mono_class_from_name(mono_assembly_get_image(m_system.m_assembly), name_space, class_name);
		if (!mono_class) return INVALID_GC_HANDLE;

		MonoObject* obj = mono_object_new(mono_domain_get(), mono_class);
		if (!obj) return INVALID_GC_HANDLE;

		u32 gc_handle = mono_gchandle_new(obj, false);

		void* args[] = {&arg};
		tryCallMethod(gc_handle, ".ctor", args, 1, false);
		return gc_handle;
	}


	HashMap<EntityRef, ScriptComponent*> m_scripts;
	HashMap<EntityRef, u32> m_entities_gc_handles;
	Array<u32> m_updates;
	Array<u32> m_on_inputs;
	CSharpSystemImpl& m_system;
	World& m_world;
	bool m_is_game_running;
};

struct CSProperties : reflection::DynamicProperties {
	CSProperties(IAllocator& allocator)
	: reflection::DynamicProperties(allocator)
	{ name = "cs_properties"; }
		
	u32 getCount(ComponentUID cmp, int index) const override { 
		CSharpScriptModuleImpl& module = (CSharpScriptModuleImpl&)*cmp.module;
		const EntityRef e = (EntityRef)cmp.entity;
		//return module.getPropertyCount(e, index);
		return {};
	}

	Type getType(ComponentUID cmp, int array_idx, u32 idx) const override { 
		//LuaScriptSceneImpl& scene = (LuaScriptSceneImpl&)*cmp.scene;
		//const EntityRef e = (EntityRef)cmp.entity;
		//const LuaScriptScene::Property::Type type = scene.getPropertyType(e, array_idx, idx);
		//switch(type) {
		//	case LuaScriptScene::Property::Type::BOOLEAN: return BOOLEAN;
		//	case LuaScriptScene::Property::Type::INT: return I32;
		//	case LuaScriptScene::Property::Type::FLOAT: return FLOAT;
		//	case LuaScriptScene::Property::Type::STRING: return STRING;
		//	case LuaScriptScene::Property::Type::ENTITY: return ENTITY;
		//	case LuaScriptScene::Property::Type::RESOURCE: return RESOURCE;
		//	case LuaScriptScene::Property::Type::COLOR: return COLOR;
		//	default: ASSERT(false); return NONE;
		//}
		return NONE;
	}

	const char* getName(ComponentUID cmp, int array_idx, u32 idx) const override {
		//LuaScriptSceneImpl& scene = (LuaScriptSceneImpl&)*cmp.scene;
		//const EntityRef e = (EntityRef)cmp.entity;
		//return scene.getPropertyName(e, array_idx, idx);
		return "";
	}

	reflection::ResourceAttribute getResourceAttribute(ComponentUID cmp, int array_idx, u32 idx) const override {
		//reflection::ResourceAttribute attr;
		//LuaScriptSceneImpl& scene = (LuaScriptSceneImpl&)*cmp.scene;
		//const EntityRef e = (EntityRef)cmp.entity;
		//const LuaScriptScene::Property::Type type = scene.getPropertyType(e, array_idx, idx);
		//ASSERT(type == LuaScriptScene::Property::Type::RESOURCE);
		//attr.file_type = "*.*";
		//attr.type  = scene.getPropertyResourceType(e, array_idx, idx);
		//return attr;
		return {};
	}


	Value getValue(ComponentUID cmp, int array_idx, u32 idx) const override { 
		//LuaScriptSceneImpl& scene = (LuaScriptSceneImpl&)*cmp.scene;
		//const EntityRef e = (EntityRef)cmp.entity;
		//const LuaScriptScene::Property::Type type = scene.getPropertyType(e, array_idx, idx);
		//const char* name = scene.getPropertyName(e, array_idx, idx);
		//Value v = {};
		//switch(type) {
		//	case LuaScriptScene::Property::Type::COLOR: reflection::set(v, scene.getPropertyValue<Vec3>(e, array_idx, name)); break;
		//	case LuaScriptScene::Property::Type::BOOLEAN: reflection::set(v, scene.getPropertyValue<bool>(e, array_idx, name)); break;
		//	case LuaScriptScene::Property::Type::INT: reflection::set(v, scene.getPropertyValue<i32>(e, array_idx, name)); break;
		//	case LuaScriptScene::Property::Type::FLOAT: reflection::set(v, scene.getPropertyValue<float>(e, array_idx, name)); break;
		//	case LuaScriptScene::Property::Type::STRING: reflection::set(v, scene.getPropertyValue<const char*>(e, array_idx, name)); break;
		//	case LuaScriptScene::Property::Type::ENTITY: reflection::set(v, scene.getPropertyValue<EntityPtr>(e, array_idx, name)); break;
		//	case LuaScriptScene::Property::Type::RESOURCE: {
		//		const i32 res_idx = scene.getPropertyValue<i32>(e, array_idx, name);
		//		if (res_idx < 0) {
		//			reflection::set(v, ""); 
		//		}
		//		else {
		//			Resource* res = scene.m_system.m_engine.getLuaResource(res_idx);
		//			reflection::set(v, res ? res->getPath().c_str() : ""); 
		//		}
		//		break;
		//	}
		//	default: ASSERT(false); break;
		//}
		//return v;
		return {};
	}
		
	void set(ComponentUID cmp, int array_idx, const char* name, Type type, Value v) const override { 
		//LuaScriptSceneImpl& scene = (LuaScriptSceneImpl&)*cmp.scene;
		//const EntityRef e = (EntityRef)cmp.entity;
		//switch(type) {
		//	case BOOLEAN: scene.setPropertyValue(e, array_idx, name, v.b); break;
		//	case I32: scene.setPropertyValue(e, array_idx, name, v.i); break;
		//	case FLOAT: scene.setPropertyValue(e, array_idx, name, v.f); break;
		//	case STRING: scene.setPropertyValue(e, array_idx, name, v.s); break;
		//	case ENTITY: scene.setPropertyValue(e, array_idx, name, v.e); break;
		//	case RESOURCE: scene.setPropertyValue(e, array_idx, name, v.s); break;
		//	case COLOR: scene.setPropertyValue(e, array_idx, name, v.v3); break;
		//	default: ASSERT(false); break;
		//}
	}

	void set(ComponentUID cmp, int array_idx, u32 idx, Value v) const override {
		//LuaScriptSceneImpl& scene = (LuaScriptSceneImpl&)*cmp.scene;
		//const EntityRef e = (EntityRef)cmp.entity;
		//const LuaScriptScene::Property::Type type = scene.getPropertyType(e, array_idx, idx);
		//const char* name = scene.getPropertyName(e, array_idx, idx);
		//switch(type) {
		//	case LuaScriptScene::Property::Type::BOOLEAN: scene.setPropertyValue(e, array_idx, name, v.b); break;
		//	case LuaScriptScene::Property::Type::INT: scene.setPropertyValue(e, array_idx, name, v.i); break;
		//	case LuaScriptScene::Property::Type::FLOAT: scene.setPropertyValue(e, array_idx, name, v.f); break;
		//	case LuaScriptScene::Property::Type::STRING: scene.setPropertyValue(e, array_idx, name, v.s); break;
		//	case LuaScriptScene::Property::Type::ENTITY: scene.setPropertyValue(e, array_idx, name, v.e); break;
		//	case LuaScriptScene::Property::Type::RESOURCE: scene.setPropertyValue(e, array_idx, name, v.s); break;
		//	case LuaScriptScene::Property::Type::COLOR: scene.setPropertyValue(e, array_idx, name, v.v3); break;
		//	default: ASSERT(false); break;
		//}
	}
};

static void initDebug() {
	mono_debug_init(MONO_DEBUG_FORMAT_MONO);
	const char* options[] = {"--soft-breakpoints", "--debugger-agent=transport=dt_socket,address=127.0.0.1:55555,embedding=1,server=y,suspend=n"};
	mono_jit_parse_options(2, (char**)options);
}


CSharpSystemImpl::CSharpSystemImpl(Engine& engine)
	: m_engine(engine)
	, m_allocator(engine.getAllocator())
	, m_names(m_allocator)
	, m_names_array(m_allocator)
	, m_on_assembly_load(m_allocator)
	, m_on_assembly_unload(m_allocator) {
	registerProperties();

	// mono_trace_set_level_string("debug");
	auto printer = [](const char* msg, mono_bool is_stdout) { logError(msg); };

	auto logger = [](const char* log_domain, const char* log_level, const char* message, mono_bool fatal, void* user_data) { logError(message); };

	mono_trace_set_print_handler(printer);
	mono_trace_set_printerr_handler(printer);
	mono_trace_set_log_handler(logger, nullptr);

	mono_set_dirs("C:\\Program Files\\Mono\\lib", "C:\\Program Files\\Mono\\etc");
	mono_config_parse(nullptr);
	
	mono_set_dirs(nullptr, nullptr);
	char exe_path[MAX_PATH];
	os::getExecutablePath(Span(exe_path));
	StringView exe_dir = Path::getDir(exe_path);
	StaticString<MAX_PATH * 3> assemblies_paths(exe_dir, ";.");
	mono_set_assemblies_path(assemblies_paths);
	initDebug();
	m_domain = mono_jit_init("lumix");
	mono_thread_set_main(mono_thread_current());
	loadAssembly();
	mono_install_unhandled_exception_hook([](MonoObject* exc, void* user_data) { handleException(exc); }, nullptr);
}


void CSharpSystemImpl::registerProperties() {
	struct ScriptEnum : reflection::StringEnumAttribute {
		u32 count(ComponentUID cmp) const override { return getPlugin(cmp).getNamesArray().size(); }
		const char* name(ComponentUID cmp, u32 idx) const override { return getPlugin(cmp).getNamesArray()[idx].c_str(); }
		static CSharpSystem& getPlugin(ComponentUID cmp) { return static_cast<CSharpSystem&>(cmp.module->getSystem()); }
	};

	using namespace reflection;
	LUMIX_MODULE(CSharpScriptModuleImpl, "csharp_script")
		.LUMIX_CMP(ScriptComponent, "csharp_script", "C# script")
			.begin_array<&CSharpScriptModule::getScriptCount, &CSharpScriptModule::addScript, &CSharpScriptModule::removeScript>("scripts")
				.LUMIX_PROP(ScriptName, "Path").attribute<ScriptEnum>()
				.property<CSProperties>()
			.end_array();
}


void CSharpSystemImpl::setStaticField(const char* name_space, const char* class_name, const char* field_name, void* value) {
	MonoClass* mono_class = mono_class_from_name(mono_assembly_get_image(m_assembly), name_space, class_name);
	ASSERT(mono_class);
	if (!mono_class) return;

	MonoVTable* vtable = mono_class_vtable(mono_domain_get(), mono_class);
	ASSERT(vtable);
	if (!vtable) return;

	MonoClassField* field = mono_class_get_field_from_name(mono_class, field_name);
	ASSERT(field);
	if (!field) return;

	mono_field_static_set_value(vtable, field, &value);
}


CSharpSystemImpl::~CSharpSystemImpl() {
	unloadAssembly();
	mono_jit_cleanup(m_domain);
}


void* CSharpSystemImpl::getAssembly() const {
	return m_assembly;
}


void* CSharpSystemImpl::getDomain() const {
	return m_domain;
}


void CSharpSystemImpl::unloadAssembly() {
	if (!m_assembly) return;

	m_on_assembly_unload.invoke();

	m_names.clear();
	m_names_array.clear();
	if (mono_domain_get() != m_domain) mono_domain_set(m_domain, true);
	MonoObject* exc = NULL;
	mono_gc_collect(mono_gc_max_generation());
	mono_domain_finalize(m_assembly_domain, 2000);
	mono_gc_collect(mono_gc_max_generation());
	mono_domain_try_unload(m_assembly_domain, &exc);
	if (exc) {
		handleException(exc);
		return;
	}
	m_assembly = nullptr;
	m_assembly_domain = nullptr;
}


static bool hasAttribute(MonoClass* cl, MonoClass* attr) {
	MonoCustomAttrInfo* attrs = mono_custom_attrs_from_class(cl);
	if (!attrs) return false;
	MonoObject* obj = mono_custom_attrs_get_attr(attrs, attr);
	return obj;
}


void CSharpSystemImpl::loadAssembly() {
	ASSERT(!m_assembly);

	const char* path = "cs/bin/main.dll";

	IAllocator& allocator = m_engine.getAllocator();
	m_assembly_domain = mono_domain_create_appdomain("lumix_runtime", nullptr);
	mono_domain_set_config(m_assembly_domain, ".", "");
	mono_domain_set(m_assembly_domain, true);
	m_assembly = mono_domain_assembly_open(m_assembly_domain, path);
	if (!m_assembly) return;
	mono_assembly_set_main(m_assembly);

	MonoImage* img = mono_assembly_get_image(m_assembly);

	m_names.clear();
	m_names_array.clear();
	int num_types = mono_image_get_table_rows(img, MONO_TABLE_TYPEDEF);
	MonoClass* native_component_class = mono_class_from_name(img, "Lumix", "NativeComponent");
	for (int i = 2; i <= num_types; ++i) {
		MonoClass* cl = mono_class_get(img, i | MONO_TOKEN_TYPE_DEF);
		const char* n = mono_class_get_name(cl);
		MonoClass* parent = mono_class_get_parent(cl);

		if (!hasAttribute(cl, native_component_class) && inherits(cl, "Component")) {
			m_names.insert(RuntimeHash(n), String(n, allocator));
			m_names_array.push(String(n, allocator));
		}
	}
	setStaticField("Lumix", "Engine", "instance_", &m_engine);
	m_on_assembly_load.invoke();
}

void CSharpSystemImpl::createModules(World& world) {
	auto module = UniquePtr<CSharpScriptModuleImpl>::create(m_engine.getAllocator(), *this, world);
	world.addModule(module.move());
}


LUMIX_PLUGIN_ENTRY(csharp) {
	return LUMIX_NEW(engine.getAllocator(), CSharpSystemImpl)(engine);
}


} // namespace Lumix
