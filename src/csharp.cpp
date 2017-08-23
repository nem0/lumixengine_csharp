#include "csharp.h"
#include "animation/animation.h"
#include "animation/animation_system.h"
#include "engine/blob.h"
#include "engine/crc32.h"
#include "engine/engine.h"
#include "engine/hash_map.h"
#include "engine/iallocator.h"
#include "engine/iplugin.h"
#include "engine/log.h"
#include "engine/path.h"
#include "engine/path_utils.h"
#include "engine/property_register.h"
#include "engine/serializer.h"
#include "engine/universe/universe.h"
#include "imgui/imgui.h"
#include "renderer/render_scene.h"

#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/mono-config.h>
#include <mono/metadata/tokentype.h>

#pragma comment(lib, "mono-2.0-sgen.lib")


namespace Lumix
{


static const ComponentType CSHARP_SCRIPT_TYPE = PropertyRegister::getComponentType("csharp_script");
enum { INVALID_GC_HANDLE = 0xffffFFFF };


struct CSharpPlugin : public IPlugin
{
	CSharpPlugin(Engine& engine);
	~CSharpPlugin();
	const char* getName() const override { return "csharp_script"; }
	void createScenes(Universe& universe) override;
	void destroyScene(IScene* scene) override { LUMIX_DELETE(m_engine.getAllocator(), scene); }

	Engine& m_engine;
	IAllocator& m_allocator;
	MonoDomain* m_domain;
};


IScene* csharp_Component_getScene(Universe* universe, MonoString* type_str)
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



template <typename R, typename C, R(C::*Function)(ComponentHandle)>
R csharp_getProperty(C* scene, int cmp)
{
	return (scene->*Function)({cmp});
}


template <typename T, typename C, void (C::*Function)(ComponentHandle, T)>
void csharp_setProperty(C* scene, int cmp, T value)
{
	(scene->*Function)({ cmp }, value);
}


template <typename T, typename C, void (C::*Function)(ComponentHandle, const T&)>
void csharp_setProperty(C* scene, int cmp, T value)
{
	(scene->*Function)({ cmp }, value);
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
	};


	struct ScriptComponent
	{
		ScriptComponent(IAllocator& allocator) : scripts(allocator) {}

		Array<Script> scripts;
		Entity entity;
		u32 entity_gc_handle;
	};


	CSharpScriptSceneImpl(CSharpPlugin& plugin, Universe& universe)
		: m_system(plugin)
		, m_universe(universe)
		, m_scripts(plugin.m_allocator)
		, m_names(plugin.m_allocator)
		, m_updates(plugin.m_allocator)
		, m_is_game_running(false)
	{
		universe.registerComponentType(CSHARP_SCRIPT_TYPE, this, &CSharpScriptSceneImpl::serializeCSharpScript, &CSharpScriptSceneImpl::deserializeCSharpScript);

		createEngineAPI();
		createRendererAPI();
		createAnimationAPI();

		load("cs\\main.dll");
	}


	void createEngineAPI()
	{
		mono_add_internal_call("Lumix.Engine::logError", csharp_logError);
		mono_add_internal_call("Lumix.Component::create", csharp_Component_create);
		mono_add_internal_call("Lumix.Component::getScene", csharp_Component_getScene);
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
		}  while (false)


	void createRendererAPI()
	{
		CSHARP_PROPERTY(float, RenderScene, Camera, FOV);
		CSHARP_PROPERTY(float, RenderScene, Camera, NearPlane);
		CSHARP_PROPERTY(float, RenderScene, Camera, FarPlane);
		CSHARP_PROPERTY(float, RenderScene, Camera, OrthoSize);

		CSHARP_PROPERTY(Vec3, RenderScene, Decal, Scale);

		CSHARP_PROPERTY(float, RenderScene, Terrain, XZScale);
		CSHARP_PROPERTY(float, RenderScene, Terrain, YScale);

		CSHARP_PROPERTY(Vec3, RenderScene, GlobalLight, Color);
		CSHARP_PROPERTY(float, RenderScene, GlobalLight, Intensity);
		CSHARP_PROPERTY(float, RenderScene, GlobalLight, IndirectIntensity);

		CSHARP_PROPERTY(float, RenderScene, PointLight, Intensity);
		CSHARP_PROPERTY(Vec3, RenderScene, PointLight, Color);
		CSHARP_PROPERTY(Vec3, RenderScene, PointLight, SpecularColor);

		CSHARP_PROPERTY(float, RenderScene, Fog, Bottom);
		CSHARP_PROPERTY(Vec3, RenderScene, Fog, Color);

		CSHARP_PROPERTY(float, RenderScene, Fog, Density);
		CSHARP_PROPERTY(float, RenderScene, Fog, Height);
	}


	void createAnimationAPI()
	{
		CSHARP_PROPERTY(float, AnimationScene, Animable, Time);
		mono_add_internal_call("Lumix.Animable::setSource", csharp_Animable_setSource);
		mono_add_internal_call("Lumix.Animable::getSource", csharp_Animable_getSource);
	}


	#undef CSHARP_PROPERTY


	void unloadAssembly() override
	{
		if (!m_assembly) return;

		m_names.clear();
		m_updates.clear();
		for (ScriptComponent* cmp : m_scripts)
		{
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts)
			{
				mono_gchandle_free(script.gc_handle);
				script.gc_handle = INVALID_GC_HANDLE;
			}
		}
		mono_assembly_close(m_assembly);
		m_assembly = nullptr;
		mono_domain_free(m_system.m_domain, false);
		m_system.m_domain = mono_domain_create();
	}


	void loadAssembly() override
	{
		load("cs\\main.dll");
		for (ScriptComponent* cmp : m_scripts)
		{
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts)
			{
				setScriptNameHash(*cmp, script, script.script_name_hash);
			}
		}
	}


	void startGame() override
	{
		for (ScriptComponent* cmp : m_scripts)
		{
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts)
			{
				tryCallMethod(script.gc_handle, "startGame");
			}
		}
		m_is_game_running = true;
	}


	void stopGame() override { m_is_game_running = false; }


	void getClassName(u32 name_hash, char(&out_name)[256]) const
	{
		int idx = m_names.find(name_hash);
		if (idx < 0)
		{
			out_name[0] = 0;
			return;
		}
		
		copyString(out_name, m_names.at(idx).c_str());
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
					case MONO_TYPE_R4:
					{
						float value;
						mono_field_get_value(obj, field, &value);
						serializer.write("value", value);
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
		createCSharpEntity(*script);

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

				switch (type)
				{
					case MONO_TYPE_R4:
					{
						float value;
						serializer.read(&value);
						mono_field_set_value(obj, field, &value);
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


	int getNamesCount() const override { return m_names.size(); }
	const char* getName(int idx) const override { return m_names.at(idx).c_str(); }


	u32 getGCHandle(ComponentHandle cmp, int scr_index) const override
	{
		Script& scr = m_scripts[{cmp.index}]->scripts[scr_index];
		return scr.gc_handle;
	}


	const char* getScriptName(ComponentHandle cmp, int scr_index) override
	{
		Script& scr = m_scripts[{cmp.index}]->scripts[scr_index];
		int idx = m_names.find(scr.script_name_hash);
		if (idx < 0) return "";
		return m_names.at(idx).c_str();
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
			mono_gchandle_free(script.gc_handle);
			script.gc_handle = INVALID_GC_HANDLE;
			script.script_name_hash = 0;
		}

		if (name_hash != 0)
		{
			char class_name[256];
			getClassName(name_hash, class_name);
			script.gc_handle = createObject("", class_name);
			ASSERT(script.gc_handle != INVALID_GC_HANDLE);
			
			setCSharpComponent(script, cmp);

			script.script_name_hash = name_hash;
		}
	}


	void setCSharpComponent(Script& script, ScriptComponent& cmp)
	{
		MonoObject* obj = mono_gchandle_get_target(script.gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);

		MonoClassField* field = mono_class_get_field_from_name(mono_class, "entity");
		ASSERT(field);

		MonoObject* entity_obj = mono_gchandle_get_target(cmp.entity_gc_handle);

		mono_field_set_value(obj, field, entity_obj);

		if (mono_class_get_method_from_name(mono_class, "update", 1))
		{
			m_updates.push(script.gc_handle);
		}
	}


	void setScriptNameHash(ComponentHandle cmp, int scr_index, u32 name_hash) override
	{
		ScriptComponent* script_cmp = m_scripts[{cmp.index}];
		if (script_cmp->scripts.size() <= scr_index) return;

		setScriptNameHash(*script_cmp, script_cmp->scripts[scr_index], name_hash);
	}


	void createCSharpEntity(ScriptComponent& cmp)
	{
		// TODO cleanup
		cmp.entity_gc_handle = createObject("Lumix", "Entity");
		
		MonoObject* obj = mono_gchandle_get_target(cmp.entity_gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);
		
		MonoClassField* field = mono_class_get_field_from_name(mono_class, "_entity_id");
		ASSERT(field);

		mono_field_set_value(obj, field, &cmp.entity.index);

		MonoClassField* universe_field = mono_class_get_field_from_name(mono_class, "_universe");
		ASSERT(universe_field);

		void* y = &m_universe;
		mono_field_set_value(obj, universe_field, &y);

		Universe* x;
		mono_field_get_value(obj, universe_field, &x);
	}


	ComponentHandle createComponent(ComponentType type, Entity entity) override
	{
		if (type != CSHARP_SCRIPT_TYPE) return INVALID_COMPONENT;

		auto& allocator = m_system.m_allocator;
		
		ScriptComponent* script = LUMIX_NEW(allocator, ScriptComponent)(allocator);
		ComponentHandle cmp = {entity.index};
		script->entity = entity;
		m_scripts.insert(entity, script);
		createCSharpEntity(*script);
		m_universe.addComponent(entity, type, this, cmp);
		return cmp;
	}


	void destroyComponent(ComponentHandle component, ComponentType type) override
	{
		if (type != CSHARP_SCRIPT_TYPE) return;

		Entity entity = {component.index};
		auto* script = m_scripts[entity];
		for (auto& scr : script->scripts)
		{
			mono_gchandle_free(scr.gc_handle);
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
						case MONO_TYPE_R4:
						{
							float value;
							mono_field_get_value(obj, field, &value);
							serializer.write(value);
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
			createCSharpEntity(*script);
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
						case MONO_TYPE_R4:
						{
							float value;
							serializer.read(value);
							mono_field_set_value(obj, field, &value);
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
			tryCallMethod(gc_handle, "update", time_delta);
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


	bool load(const char* path)
	{
		if(m_assembly) mono_assembly_close(m_assembly);

		IAllocator& allocator = m_system.m_engine.getAllocator();
		m_assembly = mono_domain_assembly_open(m_system.m_domain, path);
		if (!m_assembly) return false;

		MonoImage* img = mono_assembly_get_image(m_assembly);
		MonoClass* component_class = mono_class_from_name(img, "Lumix", "Component");

		m_names.clear();
		int num_types = mono_image_get_table_rows(img, MONO_TABLE_TYPEDEF);
		for (int i = 2; i <= num_types; ++i)
		{
			MonoClass* cl = mono_class_get(img, i | MONO_TOKEN_TYPE_DEF);
			const char* n = mono_class_get_name(cl);
			MonoClass* parent = mono_class_get_parent(cl);
			if (component_class == parent)
			{
				m_names.insert(crc32(n), string(n, allocator));
			}
		}
		return true;
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
	bool tryCallMethod(u32 gc_handle, const char* method_name, T arg0)
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


	bool tryCallMethod(u32 gc_handle, const char* method_name)
	{
		MonoObject* obj = mono_gchandle_get_target(gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);
		ASSERT(mono_class);
		MonoMethod* method = mono_class_get_method_from_name(mono_class, method_name, 0);
		if (!method) return false;

		MonoObject* exc = nullptr;
		mono_runtime_invoke(method, obj, nullptr, &exc);
		
		handleException(exc);
		return exc == nullptr;
	}


	u32 createObject(const char* name_space, const char* class_name)
	{
		MonoClass* mono_class = mono_class_from_name(mono_assembly_get_image(m_assembly), name_space, class_name);
		if (!mono_class) return INVALID_GC_HANDLE;

		MonoObject* obj = mono_object_new(m_system.m_domain, mono_class);
		if (!obj) return INVALID_GC_HANDLE;

		mono_runtime_object_init(obj);
		return mono_gchandle_new(obj, false);
	}


	AssociativeArray<Entity, ScriptComponent*> m_scripts;
	AssociativeArray<u32, string> m_names;
	Array<u32> m_updates;
	CSharpPlugin& m_system;
	Universe& m_universe;
	MonoAssembly* m_assembly = nullptr;
	bool m_is_game_running;
};


CSharpPlugin::CSharpPlugin(Engine& engine)
	: m_engine(engine)
	, m_allocator(engine.getAllocator())
{
	mono_set_dirs("C:\\Program Files\\Mono\\lib", "C:\\Program Files\\Mono\\etc");
	mono_config_parse(nullptr);
	m_domain = mono_jit_init("lumix");
}


CSharpPlugin::~CSharpPlugin()
{
	mono_jit_cleanup(m_domain);
}


void CSharpPlugin::createScenes(Universe& universe)
{
	CSharpScriptSceneImpl* scene = LUMIX_NEW(m_engine.getAllocator(), CSharpScriptSceneImpl)(*this, universe);
	universe.addScene(scene);
}


LUMIX_PLUGIN_ENTRY(lumixengine_csharp)
{
	return LUMIX_NEW(engine.getAllocator(), CSharpPlugin)(engine);
}


}