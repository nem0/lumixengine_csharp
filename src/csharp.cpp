#include "csharp.h"
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

#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
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
	};


	CSharpScriptSceneImpl(CSharpPlugin& plugin, Universe& universe)
		: m_system(plugin)
		, m_universe(universe)
		, m_scripts(plugin.m_allocator)
		, m_names(plugin.m_allocator)
		, m_is_game_running(false)
	{
		universe.registerComponentType(CSHARP_SCRIPT_TYPE, this, &CSharpScriptSceneImpl::serializeCSharpScript, &CSharpScriptSceneImpl::deserializeCSharpScript);
		mono_add_internal_call("Lumix.Engine::logError", csharp_logError);
		load("cs\\main.dll");
	}


	void unloadAssembly() override
	{
		if (!m_assembly) return;

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
	}


	void startGame() override
	{
		for (ScriptComponent* cmp : m_scripts)
		{
			Array<Script>& scripts = cmp->scripts;
			for (Script& script : scripts)
			{
				tryCallMethod(script, "StartGame");
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
		}
	}


	void deserializeCSharpScript(IDeserializer& serializer, Entity entity, int scene_version)
	{
		auto& allocator = m_system.m_allocator;
		ScriptComponent* script = LUMIX_NEW(allocator, ScriptComponent)(allocator);
		ComponentHandle cmp = { entity.index };
		script->entity = entity;
		m_scripts.insert(entity, script);

		int count;
		serializer.read(&count);
		script->scripts.reserve(count);
		for (int i = 0; i < count; ++i)
		{
			Script& inst = script->scripts.emplace();
			u32 hash;
			serializer.read(&hash);
			setScriptNameHash(cmp, i, hash);
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

		char class_name[256];
		getClassName(name_hash, class_name);
		script.gc_handle = createObject("", class_name);
		script.script_name_hash = name_hash;
	}


	void setScriptNameHash(ComponentHandle cmp, int scr_index, u32 name_hash) override
	{
		ScriptComponent* script_cmp = m_scripts[{cmp.index}];
		if (script_cmp->scripts.size() <= scr_index) return;

		setScriptNameHash(*script_cmp, script_cmp->scripts[scr_index], name_hash);
	}


	ComponentHandle createComponent(ComponentType type, Entity entity) override
	{
		if (type != CSHARP_SCRIPT_TYPE) return INVALID_COMPONENT;

		auto& allocator = m_system.m_allocator;
		
		ScriptComponent* script = LUMIX_NEW(allocator, ScriptComponent)(allocator);
		ComponentHandle cmp = {entity.index};
		script->entity = entity;
		m_scripts.insert(entity, script);
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


	void serialize(OutputBlob& serializer) override {}
	void deserialize(InputBlob& serializer) override {}
	IPlugin& getPlugin() const override { return m_system; }
	void update(float time_delta, bool paused) override {}
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


	bool tryCallMethod(Script& script, const char* method_name)
	{
		MonoObject* obj = mono_gchandle_get_target(script.gc_handle);
		ASSERT(obj);
		MonoClass* mono_class = mono_object_get_class(obj);
		ASSERT(mono_class);
		MonoMethod* method = mono_class_get_method_from_name(mono_class, method_name, 0);
		if (!method) return false;

		MonoObject* exc = nullptr;
		mono_runtime_invoke(method, obj, nullptr, &exc);
		
		MonoClass *exceptionClass;
		MonoType *exceptionType;
		const char *typeName, *message, *source, *stackTrace;
		if (exc)
		{
			exceptionClass = mono_object_get_class(exc);
			exceptionType = mono_class_get_type(exceptionClass);
			typeName = mono_type_get_name(exceptionType);
			message = GetStringProperty("Message", exceptionClass, exc);
			source = GetStringProperty("Source", exceptionClass, exc);
			stackTrace = GetStringProperty("StackTrace", exceptionClass, exc);
			g_log_error.log("C#") << message;
			return false;
		}
		
		return true;
	}


	uint32_t createObject(const char* name_space, const char* class_name)
	{
		MonoClass* mono_class = mono_class_from_name(mono_assembly_get_image(m_assembly), name_space, class_name);
		if (!mono_class) return 0;

		MonoObject* obj = mono_object_new(m_system.m_domain, mono_class);
		if (!obj) return 0;

		mono_runtime_object_init(obj);
		return mono_gchandle_new(obj, false);
	}


	AssociativeArray<Entity, ScriptComponent*> m_scripts;
	AssociativeArray<u32, string> m_names;
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