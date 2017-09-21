#include "editor/asset_browser.h"
#include "editor/file_system_watcher.h"
#include "editor/ieditor_command.h"
#include "editor/platform_interface.h"
#include "editor/property_grid.h"
#include "editor/studio_app.h"
#include "editor/utils.h"
#include "editor/world_editor.h"
#include "engine/blob.h"
#include "engine/crc32.h"
#include "engine/fs/disk_file_device.h"
#include "engine/fs/os_file.h"
#include "engine/json_serializer.h"
#include "engine/log.h"
#include "engine/path_utils.h"
#include "engine/property_register.h"
#include "engine/resource.h"
#include "engine/universe/universe.h"
#include "imgui/imgui.h"
#include "csharp.h"
#include <cstdlib>

#include <mono/jit/jit.h>
#include <mono/metadata/metadata.h>

namespace Lumix
{


static const ComponentType CSHARP_SCRIPT_TYPE = PropertyRegister::getComponentType("csharp_script");
static const ResourceType CSHARP_SCRIPT_RESOURCE_TYPE("csharp_script");


struct PropertyGridCSharpPlugin LUMIX_FINAL : public PropertyGrid::IPlugin
{
	struct SetPropertyCommand LUMIX_FINAL : public IEditorCommand
	{
		explicit SetPropertyCommand(WorldEditor& _editor)
			: property_name(_editor.getAllocator())
			, value(_editor.getAllocator())
			, old_value(_editor.getAllocator())
			, editor(_editor)
		{
		}


		SetPropertyCommand(WorldEditor& _editor,
			ComponentHandle cmp,
			int scr_index,
			const char* property_name,
			const char* val,
			IAllocator& allocator)
			: property_name(property_name, allocator)
			, value(allocator)
			, old_value(allocator)
			, component(cmp)
			, script_index(scr_index)
			, editor(_editor)
		{
			CSharpScriptScene* scene = static_cast<CSharpScriptScene*>(editor.getUniverse()->getScene(crc32("csharp_script")));
			if (property_name[0] == '-')
			{
				u32 hash = scene->getScriptNameHash(component, script_index);
				old_value.write(hash);
				u32 new_hash;
				fromCString(val, stringLength(val), &new_hash);
				value.write(new_hash);
			}
			else
			{
				u32 gc_handle = scene->getGCHandle(component, script_index);
				MonoObject* obj = mono_gchandle_get_target(gc_handle);
				MonoClass* mono_class = mono_object_get_class(obj);
				MonoClassField* field = mono_class_get_field_from_name(mono_class, property_name);

				value_type = mono_type_get_type(mono_field_get_type(field));
				const char* field_name = mono_field_get_name(field);
				switch (value_type)
				{
					case MONO_TYPE_BOOLEAN:
					{
						bool b;
						mono_field_get_value(obj, field, &b);
						old_value.write(b);
						value.write(val[0] == 'T' || val[0] == 't' ? true : false);
						break;
					}
					case MONO_TYPE_R4:
					{
						float f;
						mono_field_get_value(obj, field, &f);
						old_value.write(f);
						f = (float)atof(val);
						value.write(f);
						break;
					}
					case MONO_TYPE_I4:
					{
						int i;
						mono_field_get_value(obj, field, &i);
						old_value.write(i);
						fromCString(val, stringLength(val), &i);
						value.write(i);
						break;
					}
					case MONO_TYPE_STRING:
					{
						MonoString* str;
						mono_field_get_value(obj, field, &str);
						char* tmp = mono_string_to_utf8(str);
						old_value.write(tmp, stringLength(tmp) + 1);
						mono_free(tmp);
						value.write(val, stringLength(val) + 1);
						break;
					}
					default: ASSERT(false); break;
				}
			}
		}


		bool execute() override
		{
			set(value);
			return true;
		}


		void set(const OutputBlob& value)
		{
			CSharpScriptScene* scene = static_cast<CSharpScriptScene*>(editor.getUniverse()->getScene(crc32("csharp_script")));
			if (property_name[0] == '-')
			{
				u32 hash = InputBlob(value).read<u32>();
				scene->setScriptNameHash(component, script_index, hash);
			}
			else
			{
				u32 gc_handle = scene->getGCHandle(component, script_index);
				MonoObject* obj = mono_gchandle_get_target(gc_handle);
				if (!obj) return;

				MonoClass* mono_class = mono_object_get_class(obj);
				MonoClassField* field = mono_class_get_field_from_name(mono_class, property_name.c_str());
				if (!field) return;

				int type = mono_type_get_type(mono_field_get_type(field));
				if (type != value_type) return;

				const char* field_name = mono_field_get_name(field);
				switch (type)
				{
					case MONO_TYPE_BOOLEAN:
					{
						bool b = InputBlob(value).read<bool>();
						mono_field_set_value(obj, field, &b);
						break;
					}
					case MONO_TYPE_R4:
					{
						float f = InputBlob(value).read<float>();
						mono_field_set_value(obj, field, &f);
						break;
					}
					case MONO_TYPE_I4:
					{
						int i = InputBlob(value).read<int>();;
						mono_field_set_value(obj, field, &i);
						break;
					}
					case MONO_TYPE_STRING:
					{
						MonoString* str = mono_string_new(mono_domain_get(), (const char*)value.getData());
						mono_field_set_value(obj, field, str);
						break;
					}
					default: ASSERT(false); break;
				}
			}
		}


		void undo() override
		{
			set(old_value);
		}


		void serialize(JsonSerializer& serializer) override
		{
			// TODO
		}


		void deserialize(JsonSerializer& serializer) override
		{
			// TODO
		}


		const char* getType() override { return "set_csharp_script_property"; }


		bool merge(IEditorCommand& command) override
		{
			auto& cmd = static_cast<SetPropertyCommand&>(command);
			if (cmd.script_index == script_index && cmd.property_name == property_name)
			{
				//cmd.scene = scene;
				cmd.value = value;
				return true;
			}
			return false;
		}


		WorldEditor& editor;
		string property_name;
		OutputBlob value;
		int value_type;
		OutputBlob old_value;
		ComponentHandle component;
		int script_index;
	};


	struct AddCSharpScriptCommand LUMIX_FINAL : public IEditorCommand
	{
		explicit AddCSharpScriptCommand(WorldEditor& _editor)
			: editor(_editor)
		{
		}


		bool execute() override
		{
			auto* scene = static_cast<CSharpScriptScene*>(editor.getUniverse()->getScene(crc32("csharp_script")));
			scr_index = scene->addScript(cmp);
			scene->setScriptNameHash(cmp, scr_index, name_hash);
			return true;
		}


		void undo() override
		{
			auto* scene = static_cast<CSharpScriptScene*>(editor.getUniverse()->getScene(crc32("csharp_script")));
			scene->removeScript(cmp, scr_index);
		}


		void serialize(JsonSerializer& serializer) override
		{ 
			serializer.serialize("component", cmp);
			serializer.serialize("name_hash", name_hash);
		}


		void deserialize(JsonSerializer& serializer) override
		{
			serializer.deserialize("component", cmp, INVALID_COMPONENT);
			serializer.deserialize("name_hash", name_hash, 0);
		}


		const char* getType() override { return "add_csharp_script"; }


		bool merge(IEditorCommand& command) override { return false; }


		WorldEditor& editor;
		ComponentHandle cmp;
		u32 name_hash;
		int scr_index;
	};


	struct RemoveScriptCommand LUMIX_FINAL : public IEditorCommand
	{
		explicit RemoveScriptCommand(WorldEditor& editor)
			: blob(editor.getAllocator())
			, scr_index(-1)
			, cmp(INVALID_COMPONENT)
		{
			scene = static_cast<CSharpScriptScene*>(editor.getUniverse()->getScene(crc32("csharp_script")));
		}


		explicit RemoveScriptCommand(IAllocator& allocator)
			: blob(allocator)
			, scene(nullptr)
			, scr_index(-1)
			, cmp(INVALID_COMPONENT)
		{
		}


		bool execute() override
		{
			scene->serializeScript(cmp, scr_index, blob);
			scene->removeScript(cmp, scr_index);
			return true;
		}


		void undo() override
		{
			scene->insertScript(cmp, scr_index);
			InputBlob input(blob);
			scene->deserializeScript(cmp, scr_index, input);
		}


		void serialize(JsonSerializer& serializer) override
		{
			serializer.serialize("component", cmp);
			serializer.serialize("scr_index", scr_index);
		}


		void deserialize(JsonSerializer& serializer) override
		{
			serializer.deserialize("component", cmp, INVALID_COMPONENT);
			serializer.deserialize("scr_index", scr_index, 0);
		}


		const char* getType() override { return "remove_csharp_script"; }


		bool merge(IEditorCommand& command) override { return false; }

		OutputBlob blob;
		CSharpScriptScene* scene;
		ComponentHandle cmp;
		int scr_index;
	};


	static void csharp_Component_setProperty(PropertyGridCSharpPlugin* that, Universe* universe, Entity entity, MonoObject* cmp_obj, MonoString* prop, MonoString* value)
	{
		CSharpScriptScene* scene = (CSharpScriptScene*)universe->getScene(CSHARP_SCRIPT_TYPE);
		ComponentHandle cmp = scene->getComponent(entity, CSHARP_SCRIPT_TYPE);
		char* prop_str = mono_string_to_utf8(prop);
		char* value_str = mono_string_to_utf8(value);
		WorldEditor& editor = *that->m_app.getWorldEditor();
		IAllocator& allocator = editor.getAllocator();
		int script_count = scene->getScriptCount(cmp);
		for (int i = 0; i < script_count; ++i)
		{
			u32 gc_handle = scene->getGCHandle(cmp, i);
			if (mono_gchandle_get_target(gc_handle) == cmp_obj)
			{
				auto* set_source_cmd = LUMIX_NEW(allocator, PropertyGridCSharpPlugin::SetPropertyCommand)(
					editor, cmp, i, prop_str, value_str, allocator);
				editor.executeCommand(set_source_cmd);
				break;
			}
		}
		// TODO free all mono_string_to_utf8
		mono_free(prop_str);
		mono_free(value_str);
	}


	explicit PropertyGridCSharpPlugin(StudioApp& app)
		: m_app(app)
	{
		mono_add_internal_call("Lumix.Component::setCSharpProperty", &csharp_Component_setProperty);
	}


	void propertiesGUI(ComponentUID cmp, int script_idx)
	{
		WorldEditor& editor = *m_app.getWorldEditor();
		IAllocator& allocator = editor.getAllocator();
		auto* scene = static_cast<CSharpScriptScene*>(cmp.scene);
		u32 gc_handle = scene->getGCHandle(cmp.handle, script_idx);
		if (gc_handle == INVALID_GC_HANDLE) return;

		MonoObject* obj = mono_gchandle_get_target(gc_handle);
		MonoClass* mono_class = mono_object_get_class(obj);

		void* iter = nullptr;
		while (MonoClassField* field = mono_class_get_fields(mono_class, &iter))
		{
			bool is_public = (mono_field_get_flags(field) & 0x6) != 0;
			if (!is_public) continue;

			int type = mono_type_get_type(mono_field_get_type(field));

			const char* field_name = mono_field_get_name(field);
			switch (type)
			{
				case MONO_TYPE_BOOLEAN:
				{
					bool value;
					mono_field_get_value(obj, field, &value);
					if (ImGui::Checkbox(field_name, &value))
					{
						char tmp[2] = "0";
						if (value) tmp[0] = '1';
						auto* set_source_cmd = LUMIX_NEW(allocator, PropertyGridCSharpPlugin::SetPropertyCommand)(
							editor, cmp.handle, script_idx, field_name, tmp, allocator);
						editor.executeCommand(set_source_cmd);
					}
					break;
				}
				case MONO_TYPE_I4:
				{
					int value;
					mono_field_get_value(obj, field, &value);
					if (ImGui::InputInt(field_name, &value))
					{
						char tmp[50];
						toCString(value, tmp, lengthOf(tmp), 10);
						auto* set_source_cmd = LUMIX_NEW(allocator, PropertyGridCSharpPlugin::SetPropertyCommand)(
							editor, cmp.handle, script_idx, field_name, tmp, allocator);
						editor.executeCommand(set_source_cmd);
					}
					break;
				}
				case MONO_TYPE_R4:
				{
					float value;
					mono_field_get_value(obj, field, &value);
					if (ImGui::InputFloat(field_name, &value))
					{
						char tmp[50];
						toCString(value, tmp, lengthOf(tmp), 10);
						auto* set_source_cmd = LUMIX_NEW(allocator, PropertyGridCSharpPlugin::SetPropertyCommand)(
							editor, cmp.handle, script_idx, field_name, tmp, allocator);
						editor.executeCommand(set_source_cmd);
					}
					break;
				}
				case MONO_TYPE_STRING:
				{
					MonoString* str_val;
					mono_field_get_value(obj, field, &str_val);
					char tmp[1024];
					copyString(tmp, mono_string_to_utf8(str_val));
					if (ImGui::InputText(field_name, tmp, sizeof(tmp)))
					{
						auto* set_prop_cmd = LUMIX_NEW(allocator, PropertyGridCSharpPlugin::SetPropertyCommand)(
							editor, cmp.handle, script_idx, field_name, tmp, allocator);
						editor.executeCommand(set_prop_cmd);
					}
					break;
				}
				case MONO_TYPE_CLASS:
				{
					MonoType* mono_type = mono_field_get_type(field);
					MonoClass* mono_class = mono_type_get_class(mono_type);
					if (equalStrings(mono_class_get_name(mono_class), "Entity"))
					{
						MonoObject* field_obj = mono_field_get_value_object(mono_domain_get(), field, obj);
						Entity entity = INVALID_ENTITY;
						MonoClassField* entity_id_field = mono_class_get_field_from_name(mono_class, "_entity_id");
						if (field_obj)
						{
							mono_field_get_value(field_obj, entity_id_field, &entity.index);
						}
						if (m_app.getPropertyGrid()->entityInput(field_name, field_name, entity))
						{
							u32 entity_gc_handle = scene->getEntityGCHandle(entity);
							MonoObject* entity_obj = mono_gchandle_get_target(entity_gc_handle);
							mono_field_set_value(obj, field, entity_obj);
						}
					}
					break;
				}
				default: ASSERT(false);
			}
		}
	}


	void onGUI(PropertyGrid& grid, ComponentUID cmp) override
	{
		if (cmp.type != CSHARP_SCRIPT_TYPE) return;

		auto* scene = static_cast<CSharpScriptScene*>(cmp.scene);
		auto& plugin = static_cast<CSharpPlugin&>(scene->getPlugin());
		WorldEditor& editor = *m_app.getWorldEditor();
		IAllocator& allocator = editor.getAllocator();

		if (ImGui::Button("Add script")) ImGui::OpenPopup("add_csharp_script_popup");

		if (ImGui::BeginPopup("add_csharp_script_popup"))
		{
			int count = plugin.getNamesCount();
			for (int i = 0; i < count; ++i)
			{
				const char* name = plugin.getName(i);
				bool b = false;
				if (ImGui::Selectable(name, &b))
				{
					auto* cmd = LUMIX_NEW(allocator, AddCSharpScriptCommand)(editor);
					cmd->cmp = cmp.handle;
					cmd->name_hash = crc32(name);
					editor.executeCommand(cmd);
					break;
				}
			}
			ImGui::EndPopup();
		}

		for (int j = 0; j < scene->getScriptCount(cmp.handle); ++j)
		{
			const char* script_name = scene->getScriptName(cmp.handle, j);
			StaticString<MAX_PATH_LENGTH + 20> header(script_name);
			if (header.empty()) header << j;
			header << "###" << j;
			if (ImGui::CollapsingHeader(header))
			{
				ImGui::PushID(j);
				u32 gc_handle = scene->getGCHandle(cmp.handle, j);
				scene->tryCallMethod(gc_handle, "onInspector", this, true);
				if (ImGui::Button("Edit"))
				{
					StaticString<MAX_PATH_LENGTH> full_path(editor.getEngine().getDiskFileDevice()->getBasePath(), "cs/", script_name, ".cs");
					PlatformInterface::shellExecuteOpen(full_path);
				}
				ImGui::SameLine();
				if (ImGui::Button("Remove script"))
				{
					auto* cmd = LUMIX_NEW(allocator, RemoveScriptCommand)(allocator);
					cmd->cmp = cmp.handle;
					cmd->scr_index = j;
					cmd->scene = scene;
					editor.executeCommand(cmd);
					ImGui::PopID();
					break;
				}
				ImGui::PopID();
				propertiesGUI(cmp, j);
			}
		}
	}

	StudioApp& m_app;
};


struct AddCSharpComponentPlugin LUMIX_FINAL : public StudioApp::IAddComponentPlugin
{
	AddCSharpComponentPlugin(StudioApp& _app)
		: app(_app)
	{
	}


	void onGUI(bool create_entity, bool) override
	{
		ImGui::SetNextWindowSize(ImVec2(300, 300));
		if (!ImGui::BeginMenu(getLabel())) return;
		auto* asset_browser = app.getAssetBrowser();
		
		WorldEditor& editor = *app.getWorldEditor();
		CSharpScriptScene* script_scene = (CSharpScriptScene*)editor.getUniverse()->getScene(CSHARP_SCRIPT_TYPE);
		CSharpPlugin& plugin = (CSharpPlugin&)script_scene->getPlugin();
		for (int i = 0, count = plugin.getNamesCount(); i < count; ++i)
		{
			const char* name = plugin.getName(i);
			bool b = false;
			if (ImGui::Selectable(name, &b))
			{
				if (create_entity)
				{
					Entity entity = editor.addEntity();
					editor.selectEntities(&entity, 1);
				}
				if (editor.getSelectedEntities().empty()) return;
				Entity entity = editor.getSelectedEntities()[0];

				if (!editor.getUniverse()->hasComponent(entity, CSHARP_SCRIPT_TYPE))
				{
					editor.addComponent(CSHARP_SCRIPT_TYPE);
				}

				IAllocator& allocator = editor.getAllocator();
				auto* cmd = LUMIX_NEW(allocator, PropertyGridCSharpPlugin::AddCSharpScriptCommand)(editor);

				ComponentHandle cmp = editor.getUniverse()->getComponent(entity, CSHARP_SCRIPT_TYPE).handle;
				cmd->cmp = cmp;
				cmd->name_hash = crc32(name);
				editor.executeCommand(cmd);

				int scr_count = script_scene->getScriptCount(cmp);
				char hash_str[64];
				toCString(crc32(name), hash_str, lengthOf(hash_str));
				auto* set_source_cmd = LUMIX_NEW(allocator, PropertyGridCSharpPlugin::SetPropertyCommand)(
					editor, cmp, scr_count - 1, "-source", hash_str, allocator);
				editor.executeCommand(set_source_cmd);

				ImGui::CloseCurrentPopup();
			}
		}

		ImGui::EndMenu();
	}


	const char* getLabel() const override
	{
		return "C# Script";
	}


	StudioApp& app;
};


struct StudioCSharpPlugin : public StudioApp::IPlugin
{
	StudioCSharpPlugin(StudioApp& app)
		: m_app(app)
	{
		m_filter[0] = '\0';
		m_new_script_name[0] = '\0';
		
		IAllocator& allocator = app.getWorldEditor()->getAllocator();
		m_watcher = FileSystemWatcher::create("cs", allocator);
		m_watcher->getCallback().bind<StudioCSharpPlugin, &StudioCSharpPlugin::onFileChanged>(this);

		makeUpToDate();
	}


	~StudioCSharpPlugin()
	{
		FileSystemWatcher::destroy(m_watcher);
	}


	void update(float)
	{
		if (m_deferred_compile) compile();
		if (!m_compile_process) return;
		if (PlatformInterface::isProcessFinished(*m_compile_process))
		{
			if (PlatformInterface::getProcessExitCode(*m_compile_process) != 0)
			{
				char tmp[1024];
				int tmp_size = PlatformInterface::getProcessOutput(*m_compile_process, tmp, lengthOf(tmp) - 1);
				if (tmp_size != -1) tmp[tmp_size] = 0;
				g_log_error.log("C#") << tmp;
			}
			else
			{
				CSharpScriptScene* scene = getScene();
				CSharpPlugin& plugin = (CSharpPlugin&)scene->getPlugin();
				plugin.loadAssembly();
			}
			PlatformInterface::destroyProcess(*m_compile_process);
			m_compile_process = nullptr;
		}
	}


	void makeUpToDate()
	{
		IAllocator& allocator = m_app.getWorldEditor()->getAllocator();
		if (!PlatformInterface::fileExists("cs\\main.dll"))
		{
			compile();
			return;
		}

		u64 dll_modified = PlatformInterface::getLastModified("cs\\main.dll");
		PlatformInterface::FileIterator* iter = PlatformInterface::createFileIterator("cs", allocator);
		PlatformInterface::FileInfo info;
		while (PlatformInterface::getNextFile(iter, &info))
		{
			if (info.is_directory) continue;
			
			StaticString<MAX_PATH_LENGTH> tmp("cs\\", info.filename);
			u64 script_modified = PlatformInterface::getLastModified(tmp);
			if (script_modified > dll_modified)
			{
				compile();
				PlatformInterface::destroyFileIterator(iter);
				return;
			}
		}
		PlatformInterface::destroyFileIterator(iter);
	}


	void onFileChanged(const char* path)
	{
		if(PathUtils::hasExtension(path, "cs")) m_deferred_compile = true;
	}


	void createNewScript(const char* name)
	{
		FS::OsFile file;
		char class_name[128];

		const char* cin = name;
		char* cout = class_name;
		bool to_upper = true;
		while (*cin && cout - class_name < lengthOf(class_name) - 1)
		{
			char c = *cin;
			if (c >= 'a' && c <= 'z')
			{
				*cout = to_upper ? *cin - 'a' + 'A' : *cin;
				to_upper = false;
			}
			else if (c >= 'A' && c <= 'Z')
			{
				*cout = *cin;
				to_upper = false;
			}
			else if (c >= '0' && c <= '9')
			{
				*cout = *cin;
				to_upper = true;
			}
			else
			{
				to_upper = true;
				--cout;
			}
			++cout;
			++cin;
		}
		*cout = '\0';

		StaticString<MAX_PATH_LENGTH> path("cs/", class_name, ".cs");
		if (PlatformInterface::fileExists(path))
		{
			g_log_error.log("C#") << path << "already exists";
			return;
		}
		if (!file.open(path, FS::Mode::CREATE_AND_WRITE, m_app.getWorldEditor()->getAllocator()))
		{
			g_log_error.log("C#") << "Failed to create file " << path;
			return;
		}

		file.writeText("public class ");
		file.writeText(class_name);
		file.writeText(" : Lumix.Component\n{\n}\n");

		file.close();
	}


	void onWindowGUI() override
	{
		if (!ImGui::BeginDock("C#"))
		{
			ImGui::EndDock();
			return;
		}

		CSharpScriptScene* scene = getScene();
		CSharpPlugin& plugin = (CSharpPlugin&)scene->getPlugin();
		if (m_compile_process)
		{
			ImGui::Text("Compiling...");
		}
		else
		{
			if (ImGui::Button("Compile")) compile();
			ImGui::SameLine();
			if (ImGui::Button("New script")) ImGui::OpenPopup("new_csharp_script");
			if (ImGui::BeginPopup("new_csharp_script"))
			{
				ImGui::InputText("Name", m_new_script_name, sizeof(m_new_script_name));
				if (ImGui::Button("Create"))
				{
					createNewScript(m_new_script_name);
					ImGui::CloseCurrentPopup();
				}
				ImGui::EndPopup();
			}
		}

		ImGui::FilterInput("Filter", m_filter, sizeof(m_filter));

		for (int i = 0, c = plugin.getNamesCount(); i < c; ++i)
		{
			const char* name = plugin.getName(i);
			if (m_filter[0] != '\0' && stristr(name, m_filter) == 0) continue;
			ImGui::PushID(i);
			if (ImGui::Button("Edit"))
			{
				StaticString<MAX_PATH_LENGTH> path("cs\\", name, ".cs");
				PlatformInterface::shellExecuteOpen(path);
			}
			ImGui::SameLine();
			ImGui::Text("%s", name);
			ImGui::PopID();
		}

		ImGui::EndDock();
	}


	const char* getName() const override { return "csharp_script"; }


	CSharpScriptScene* getScene() const
	{
		WorldEditor& editor = *m_app.getWorldEditor();
		return (CSharpScriptScene*)editor.getUniverse()->getScene(crc32("csharp_script"));
	}


	void compile()
	{
		m_deferred_compile = false;
		if (m_compile_process) return;

		CSharpScriptScene* scene = getScene();
		CSharpPlugin& plugin = (CSharpPlugin&)scene->getPlugin();
		plugin.unloadAssembly();
		IAllocator& allocator = m_app.getWorldEditor()->getAllocator();
		m_compile_process = PlatformInterface::createProcess("c:\\windows\\system32\\cmd.exe", "/c \"\"C:\\Program Files\\Mono\\bin\\mcs.bat\" -out:\"cs\\main.dll\" -target:library -unsafe -recurse:\"cs\\*.cs\"", allocator);
	}

	PlatformInterface::Process* m_compile_process = nullptr;
	StudioApp& m_app;
	FileSystemWatcher* m_watcher;
	char m_filter[128];
	char m_new_script_name[128];
	bool m_deferred_compile = false;
};


namespace {


IEditorCommand* createAddCSharpScriptCommand(WorldEditor& editor)
{
	return LUMIX_NEW(editor.getAllocator(), PropertyGridCSharpPlugin::AddCSharpScriptCommand)(editor);
}


IEditorCommand* createSetPropertyCommand(WorldEditor& editor)
{
	return LUMIX_NEW(editor.getAllocator(), PropertyGridCSharpPlugin::SetPropertyCommand)(editor);
}


IEditorCommand* createRemoveScriptCommand(WorldEditor& editor)
{
	return LUMIX_NEW(editor.getAllocator(), PropertyGridCSharpPlugin::RemoveScriptCommand)(editor);
}


}


LUMIX_STUDIO_ENTRY(lumixengine_csharp)
{
	WorldEditor& editor = *app.getWorldEditor();
	IAllocator& allocator = editor.getAllocator();
	StudioCSharpPlugin* plugin = LUMIX_NEW(allocator, StudioCSharpPlugin)(app);
	app.addPlugin(*plugin);

	auto* cmp_plugin = LUMIX_NEW(allocator, AddCSharpComponentPlugin)(app);
	app.registerComponent("csharp_script", *cmp_plugin);

	editor.registerEditorCommandCreator("add_csharp_script", createAddCSharpScriptCommand);
	editor.registerEditorCommandCreator("remove_csharp_script", createRemoveScriptCommand);
	editor.registerEditorCommandCreator("set_csharp_script_property", createSetPropertyCommand);

	auto* pg_plugin = LUMIX_NEW(editor.getAllocator(), PropertyGridCSharpPlugin)(app);
	app.getPropertyGrid()->addPlugin(*pg_plugin);
}


}