#include "editor/asset_browser.h"
#include "editor/ieditor_command.h"
#include "editor/platform_interface.h"
#include "editor/property_grid.h"
#include "editor/studio_app.h"
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
			, value(val, allocator)
			, old_value(allocator)
			, component(cmp)
			, script_index(scr_index)
			, editor(_editor)
		{
			CSharpScriptScene* scene = static_cast<CSharpScriptScene*>(editor.getUniverse()->getScene(crc32("csharp_script")));
			StaticString<32> tmp("", scene->getScriptNameHash(component, script_index));
			old_value = tmp;
		}


		bool execute() override
		{
			CSharpScriptScene* scene = static_cast<CSharpScriptScene*>(editor.getUniverse()->getScene(crc32("csharp_script")));
			u32 name_hash;
			if (property_name[0] == '-')
			{
				fromCString(value.c_str(), value.length(), &name_hash);
				scene->setScriptNameHash(component, script_index, name_hash);
			}
			else
			{
				u32 gc_handle = scene->getGCHandle(component, script_index);
				MonoObject* obj = mono_gchandle_get_target(gc_handle);
				MonoClass* mono_class = mono_object_get_class(obj);
				MonoClassField* field = mono_class_get_field_from_name(mono_class, property_name.c_str());

				int type = mono_type_get_type(mono_field_get_type(field));
				const char* field_name = mono_field_get_name(field);
				switch (type)
				{
					case MONO_TYPE_R4:
					{
						float f = atof(value.c_str());
						mono_field_set_value(obj, field, &f);
						break;
					}
					default: ASSERT(false); break;
				}

				
			}
			return true;
		}


		void undo() override
		{
			CSharpScriptScene* scene = static_cast<CSharpScriptScene*>(editor.getUniverse()->getScene(crc32("csharp_script")));
			u32 name_hash;
			fromCString(old_value.c_str(), old_value.length(), &name_hash);
			scene->setScriptNameHash(component, script_index, name_hash);
		}


		void serialize(JsonSerializer& serializer) override
		{
			serializer.serialize("component", component);
			serializer.serialize("script_index", script_index);
			serializer.serialize("property_name", property_name.c_str());
			serializer.serialize("value", value.c_str());
			serializer.serialize("old_value", old_value.c_str());
		}


		void deserialize(JsonSerializer& serializer) override
		{
			serializer.deserialize("component", component, INVALID_COMPONENT);
			serializer.deserialize("script_index", script_index, 0);
			char buf[256];
			serializer.deserialize("property_name", buf, lengthOf(buf), "");
			property_name = buf;
			serializer.deserialize("value", buf, lengthOf(buf), "");
			value = buf;
			serializer.deserialize("old_value", buf, lengthOf(buf), "");
			old_value = buf;
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
		string value;
		string old_value;
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


	explicit PropertyGridCSharpPlugin(StudioApp& app)
		: m_app(app)
	{
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
				default: ASSERT(false);
			}
		}
	}


	void onGUI(PropertyGrid& grid, ComponentUID cmp) override
	{
		if (cmp.type != CSHARP_SCRIPT_TYPE) return;

		auto* scene = static_cast<CSharpScriptScene*>(cmp.scene);
		WorldEditor& editor = *m_app.getWorldEditor();
		IAllocator& allocator = editor.getAllocator();

		if (ImGui::Button("Add script")) ImGui::OpenPopup("add_csharp_script_popup");

		if (ImGui::BeginPopup("add_csharp_script_popup"))
		{
			int count = scene->getNamesCount();
			for (int i = 0; i < count; ++i)
			{
				const char* name = scene->getName(i);
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
		for (int i = 0, count = script_scene->getNamesCount(); i < count; ++i)
		{
			const char* name = script_scene->getName(i);
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
	}


	void onWindowGUI() override
	{
		if (!ImGui::BeginDock("C#"))
		{
			ImGui::EndDock();
			return;
		}

		if (m_compile_process)
		{
			ImGui::Text("Compiling...");
			if (PlatformInterface::isProcessFinished(*m_compile_process))
			{
				if (PlatformInterface::getProcessExitCode(*m_compile_process) != 0)
				{
					char tmp[1024];
					int tmp_size = PlatformInterface::getProcessOutput(*m_compile_process, tmp, lengthOf(tmp) - 1);
					if(tmp_size != -1) tmp[tmp_size] = 0;
					g_log_error.log("C#") << tmp;
				}
				else
				{
					CSharpScriptScene* scene = getScene();
					scene->loadAssembly();
				}
				PlatformInterface::destroyProcess(*m_compile_process);
				m_compile_process = nullptr;
			}
		}
		else
		{
			if (ImGui::Button("Compile")) compile();
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
		CSharpScriptScene* scene = getScene();
		scene->unloadAssembly();
		IAllocator& allocator = m_app.getWorldEditor()->getAllocator();
		m_compile_process = PlatformInterface::createProcess("c:\\windows\\system32\\cmd.exe", "/c \"\"C:\\Program Files\\Mono\\bin\\mcs.bat\" -out:\"cs\\main.dll\" -target:library -recurse:\"cs\\*.cs\"", allocator);
	}

	PlatformInterface::Process* m_compile_process = nullptr;
	StudioApp& m_app;
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