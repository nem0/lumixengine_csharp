#include "../csharp.h"
#include "../helpers.h"
#include "editor/asset_browser.h"
#include "editor/file_system_watcher.h"
#include "editor/property_grid.h"
#include "editor/settings.h"
#include "editor/studio_app.h"
#include "editor/utils.h"
#include "editor/world_editor.h"
#include "engine/engine.h"
#include "engine/log.h"
#include "engine/os.h"
#include "engine/reflection.h"
#include "engine/resource.h"
#include "engine/resource_manager.h"
#include "imgui/imgui.h"
#include "subprocess.h"
#include <stdlib.h>
#include <mono/metadata/mono-debug.h>


namespace Lumix {


static const ComponentType CSHARP_SCRIPT_TYPE = reflection::getComponentType("csharp_script");

template <typename T> static bool isDirectType() { return true; }
template <> static bool isDirectType<const char*>() { return false; }
template <> static bool isDirectType<Path>() { return false; }

struct CSPropertyBindingsVisitor : reflection::IPropertyVisitor {
	template <typename T> void write(const reflection::Property<T>& prop, const char* cs_type, const char* cpp_type) {
		StaticString<128> csharp_name;
		getCSharpName(prop.name, csharp_name);

		*api_blob << "{\n"
			"	const reflection::Property<" << cpp_type << ">* prop = getProperty<" << cpp_type << ">(\"" << cmp->name << "\", \""<< prop.name << "\");\n"
			"	ASSERT(prop);\n";

		if (isDirectType<T>()) {
			*api_blob << 
				"	mono_add_internal_call(\"Lumix." << class_name << "::get" << csharp_name << "\", prop->getter);\n"
				"	mono_add_internal_call(\"Lumix." << class_name << "::set" << csharp_name << "\", prop->setter);\n";
		}
		else {
			*api_blob <<
				"	struct S : CSPropertyWrapper<" << cpp_type << "> {};\n"
				"	S::getter = prop->getter;\n"
				"	S::setter = prop->setter;\n"
				"	mono_add_internal_call(\"Lumix." << class_name << "::get" << csharp_name << "\", &S::cs_getter);\n"
				"	mono_add_internal_call(\"Lumix." << class_name << "::set" << csharp_name << "\", &S::cs_setter);\n";
		}
		*api_blob << "}\n\n";

		*blob << "		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
				 "		extern static "
			  << cs_type << " get" << csharp_name
			  << "(IntPtr module, int cmp);\n"
				 "\n"
				 "		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
				 "		extern static void set"
			  << csharp_name << "(IntPtr module, int cmp, " << cs_type
			  << " value);\n"
				 "\n\n";

		bool is_bool = equalStrings(cs_type, "bool");
		*blob << "		public " << cs_type << " " << (is_bool ? "Is" : "") << csharp_name
			  << "\n"
				 "		{\n"
				 "			get { return get"
			  << csharp_name
			  << "(module_, entity_.entity_Id_); }\n"
				 "			set { set"
			  << csharp_name
			  << "(module_, entity_.entity_Id_, value); }\n"
				 "		}\n"
				 "\n";
	}

	void visit(const reflection::Property<float>& prop) override { write(prop, "float", "float"); }
	void visit(const reflection::Property<u32>& prop) override { write(prop, "uint", "u32"); }
	void visit(const reflection::Property<i32>& prop) override { write(prop, "int", "int"); }
	void visit(const reflection::Property<EntityPtr>& prop) override {}
	void visit(const reflection::Property<IVec3>& prop) override { write(prop, "IVec3", "IVec3"); }
	void visit(const reflection::Property<Vec2>& prop) override { write(prop, "Vec2", "Vec2"); }
	void visit(const reflection::Property<Vec3>& prop) override { write(prop, "Vec3", "Vec3"); }
	void visit(const reflection::Property<Vec4>& prop) override { write(prop, "Vec4", "Vec4"); }
	void visit(const reflection::Property<Path>& prop) override { write(prop, "string", "Path"); }
	void visit(const reflection::Property<bool>& prop) override { write(prop, "bool", "bool"); }
	void visit(const reflection::Property<const char*>& prop) override { write(prop, "string", "const char*"); }
	void visit(const reflection::ArrayProperty& prop) override {}
	void visit(const reflection::BlobProperty& prop) override {}

	const reflection::ComponentBase* cmp;
	const char* class_name;
	OutputMemoryStream* api_blob;
	OutputMemoryStream* blob;
};

struct PropertyGridCSharpPlugin final : public PropertyGrid::IPlugin {
	struct SetPropertyCommand final : public IEditorCommand {
		SetPropertyCommand(WorldEditor& _editor, EntityRef entity, int scr_index, const char* property_name, const char* old_value, const char* new_value, IAllocator& allocator)
			: property_name(property_name, allocator)
			, new_value(new_value, allocator)
			, old_value(old_value, allocator)
			, entity(entity)
			, script_index(scr_index)
			, editor(_editor)
		{}

		bool execute() override {
			set(new_value);
			return true;
		}

		void set(const String& value) {
			CSharpScriptModule* module = static_cast<CSharpScriptModule*>(editor.getWorld()->getModule("csharp_script"));
			u32 gc_handle = module->getGCHandle(entity, script_index);
			MonoString* prop_mono_str = mono_string_new(mono_domain_get(), property_name.c_str());
			MonoString* value_str = mono_string_new(mono_domain_get(), value.c_str());
			auto that = this;
			void* args[] = {&that, prop_mono_str, value_str};
			module->tryCallMethod(gc_handle, "OnUndo", args, lengthOf(args), true);
		}

		void undo() override { set(old_value); }
		const char* getType() override { return "set_csharp_script_property"; }

		bool merge(IEditorCommand& command) override {
			auto& cmd = static_cast<SetPropertyCommand&>(command);
			if (cmd.entity == entity && cmd.script_index == script_index && cmd.property_name == property_name) {
				cmd.new_value = new_value;
				return true;
			}
			return false;
		}

		WorldEditor& editor;
		String property_name;
		String old_value;
		String new_value;
		int value_type;
		EntityRef entity;
		int script_index;
	};


	struct AddCSharpScriptCommand final : public IEditorCommand {
		explicit AddCSharpScriptCommand(WorldEditor& editor)
			: editor(editor)
			, name(editor.getAllocator())
		{}

		bool execute() override {
			auto* module = static_cast<CSharpScriptModule*>(editor.getWorld()->getModule("csharp_script"));
			scr_index = module->addScript(entity, scr_index);
			module->setScriptName(entity, scr_index, name.c_str());
			return true;
		}

		void undo() override {
			auto* module = static_cast<CSharpScriptModule*>(editor.getWorld()->getModule("csharp_script"));
			module->removeScript(entity, scr_index);
		}

		const char* getType() override { return "add_csharp_script"; }
		bool merge(IEditorCommand& command) override { return false; }

		WorldEditor& editor;
		EntityRef entity;
		String name;
		int scr_index = -1;
	};


	struct RemoveScriptCommand final : public IEditorCommand {
		explicit RemoveScriptCommand(CSharpScriptModule& module, EntityRef entity, i32 scr_index, IAllocator& allocator)
			: blob(allocator)
			, module(module)
			, scr_index(scr_index)
			, entity(entity)
		{}

		bool execute() override {
			module.serializeScript(entity, scr_index, blob);
			module.removeScript(entity, scr_index);
			return true;
		}

		void undo() override {
			module.insertScript(entity, scr_index);
			InputMemoryStream input(blob);
			module.deserializeScript(entity, scr_index, input);
		}

		const char* getType() override { return "remove_csharp_script"; }

		bool merge(IEditorCommand& command) override { return false; }

		OutputMemoryStream blob;
		CSharpScriptModule& module;
		EntityRef entity;
		int scr_index;
	};


	static Resource* csharp_resourceInput(PropertyGridCSharpPlugin* that, MonoString* label, MonoString* type, Resource* resource) {
		MonoStringHolder label_str = label;
		MonoStringHolder type_str = type;
		ResourceType res_type((const char*)type_str);
		AssetBrowser& browser = that->m_app.getAssetBrowser();
		Path path = resource ? resource->getPath() : Path();
		if (browser.resourceInput((const char*)label_str, path, res_type)) {
			if (path.isEmpty()) return nullptr;
			ResourceManagerHub& rm = that->m_app.getWorldEditor().getEngine().getResourceManager();
			return rm.load(res_type, path);
		}
		return resource;
	}


	static i32 csharp_entityInput(PropertyGridCSharpPlugin* that, World* world, MonoString* label_mono, i32 entity) {
		StudioApp& app = that->m_app;
		PropertyGrid& prop_grid = app.getPropertyGrid();
		MonoStringHolder label = label_mono;
		ASSERT(false); // TODO
		//prop_grid.entityInput((const char*)label, entity);
		return entity;
	}


	static void csharp_Component_pushUndoCommand(PropertyGridCSharpPlugin* that,
		World* world,
		EntityRef entity,
		MonoObject* cmp_obj,
		MonoString* prop,
		MonoString* old_value,
		MonoString* new_value) {
		CSharpScriptModule* module = (CSharpScriptModule*)world->getModule(CSHARP_SCRIPT_TYPE);
		MonoStringHolder prop_str = prop;
		MonoStringHolder new_value_str = new_value;
		MonoStringHolder old_value_str = old_value;
		WorldEditor& editor = that->m_app.getWorldEditor();
		IAllocator& allocator = editor.getAllocator();
		int script_count = module->getScriptCount(entity);
		for (int i = 0; i < script_count; ++i) {
			u32 gc_handle = module->getGCHandle(entity, i);
			if (mono_gchandle_get_target(gc_handle) == cmp_obj) {
				
				using CmdTypePtr = UniquePtr<PropertyGridCSharpPlugin::SetPropertyCommand>;
				CmdTypePtr set_source_cmd =
					CmdTypePtr::create(allocator, editor, entity, i, (const char*)prop_str, (const char*)old_value_str, (const char*)new_value_str, allocator);
				editor.executeCommand(set_source_cmd.move());
				break;
			}
		}
	}


	explicit PropertyGridCSharpPlugin(StudioApp& app)
		: m_app(app)
	{
		mono_add_internal_call("Lumix.Component::pushUndoCommand", &csharp_Component_pushUndoCommand);
		mono_add_internal_call("Lumix.Component::entityInput", &csharp_entityInput);
		mono_add_internal_call("Lumix.Component::resourceInput", &csharp_resourceInput);
	}


	void onGUI(PropertyGrid& grid, Span<const EntityRef> entities, ComponentType cmp_type, WorldEditor& editor) override {
		if (cmp_type != CSHARP_SCRIPT_TYPE) return;
		if (entities.length() != 1) return;

		World* world = editor.getWorld();
		auto* module = static_cast<CSharpScriptModule*>(world->getModule(CSHARP_SCRIPT_TYPE));
		auto& plugin = static_cast<CSharpSystem&>(module->getSystem());
		IAllocator& allocator = editor.getAllocator();

		if (ImGui::Button("Add script")) ImGui::OpenPopup("add_csharp_script_popup");

		if (ImGui::BeginPopup("add_csharp_script_popup")) {
			for (const String& name : plugin.getNamesArray()) {
				bool b = false;
				if (ImGui::Selectable(name.c_str(), &b)) {
					UniquePtr<AddCSharpScriptCommand> cmd = UniquePtr<AddCSharpScriptCommand>::create(allocator, editor);
					cmd->entity = entities[0];
					cmd->name = name;
					editor.executeCommand(cmd.move());
					break;
				}
			}
			ImGui::EndPopup();
		}

		for (int j = 0; j < module->getScriptCount(entities[0]); ++j) {
			const char* script_name = module->getScriptName(entities[0], j);
			StaticString<MAX_PATH + 20> header(script_name);
			if (header.empty()) header.append(j);
			header.append("###", j);
			if (ImGui::CollapsingHeader(header)) {
				u32 gc_handle = module->getGCHandle(entities[0], j);
				if (gc_handle == INVALID_GC_HANDLE) continue;
				ImGui::PushID(j);
				auto* that = this;
				void* args[] = {&that};
				module->tryCallMethod(gc_handle, "OnInspector", args, 1, true);
				if (ImGui::Button("Edit")) {
					FileSystem& fs = m_app.getEngine().getFileSystem();
					StaticString<MAX_PATH> fullpath(fs.getBasePath(), "cs/src/", script_name, ".cs");
					os::ExecuteOpenResult res = os::shellExecuteOpen(fullpath);
					switch(res) {
						case os::ExecuteOpenResult::SUCCESS: break;
						case os::ExecuteOpenResult::OTHER_ERROR: logError("Could not open ", fullpath); break;
						case os::ExecuteOpenResult::NO_ASSOCIATION: logError("No program associated with ", fullpath); break;
					}
				}
				ImGui::SameLine();
				if (ImGui::Button("Remove script")) {
					UniquePtr<RemoveScriptCommand> cmd = UniquePtr<RemoveScriptCommand>::create(allocator, *module, entities[0], j, allocator);
					editor.executeCommand(cmd.move());
					ImGui::PopID();
					break;
				}
				ImGui::PopID();
			}
		}
	}

	StudioApp& m_app;
};

struct StudioCSharpPlugin : public StudioApp::GUIPlugin {
	StudioCSharpPlugin(StudioApp& app)
		: m_app(app)
		, m_compile_log(app.getWorldEditor().getAllocator())
		, m_property_grid_plugin(app)
	{
		m_filter[0] = '\0';
		m_new_script_name[0] = '\0';

		IAllocator& allocator = app.getWorldEditor().getAllocator();
		m_watcher = FileSystemWatcher::create("cs/src", allocator);
		m_watcher->getCallback().bind<&StudioCSharpPlugin::onFileChanged>(this);

		findMono();

		makeUpToDate();

		app.getPropertyGrid().addPlugin(m_property_grid_plugin);

		m_toggle_ui.init("C#", "C#", "csharp", "", Action::IMGUI_PRIORITY);
		m_toggle_ui.func.bind<&StudioCSharpPlugin::toggleOpen>(this);
		m_toggle_ui.is_selected.bind<&StudioCSharpPlugin::isOpen>(this);
		
		app.addWindowAction(&m_toggle_ui);
	}

	~StudioCSharpPlugin() {
		m_app.removeAction(&m_toggle_ui);
		m_app.getPropertyGrid().removePlugin(m_property_grid_plugin);
	}

	void onSettingsLoaded() override {
		m_is_open = m_app.getSettings().getValue(Settings::GLOBAL, "is_csharp_open", false);
	}

	void onBeforeSettingsSaved() override {
		m_app.getSettings().setValue(Settings::GLOBAL, "is_csharp_open", m_is_open);
	}

	void toggleOpen() { m_is_open = !m_is_open; }
	bool isOpen() const { return m_is_open; }

	bool packData(const char* dest_dir) {
		char exe_path[MAX_PATH];
		os::getExecutablePath(Span(exe_path));
		StringView exe_dir = Path::getDir(exe_path);

		const char* mono_dlls[] = {"mono-2.0-sgen.dll", "System.dll", "mscorlib.dll", "System.Configuration.dll"};
		for (const char* dll : mono_dlls) {
			StaticString<MAX_PATH> tmp(exe_dir, dll);
			if (!os::fileExists(tmp)) return false;
			StaticString<MAX_PATH> dest(dest_dir, dll);
			if (!os::copyFile(tmp, dest)) {
				logError("Failed to copy ", tmp, " to ", dest);
				return false;
			}
		}

		StaticString<MAX_PATH> dest(dest_dir, "main.dll");
		if (!os::copyFile("cs/bin/main.dll", dest)) {
			logError("Failed to copy cs/main.dll to ", dest);
			return false;
		}

		return true;
	}


	void findMono() {
		if (!os::fileExists("C:\\Program Files\\Mono\\bin\\mcs.bat")) {
			logError("C:\\Program Files\\Mono\\bin\\mcs.bat does not exist, can not compile C# scripts");
		}
	}

	void update(float) {
		if (m_deferred_compile) compile();
		if (!m_compilation_running) return;

		if (subprocess_finished(&m_compile_process)) {
			int ret_code;
			subprocess_join(&m_compile_process, &ret_code);
			if (ret_code == 0) {
				CSharpScriptModule* module = getModule();
				CSharpSystem& system = (CSharpSystem&)module->getSystem();
				system.loadAssembly();
			} else {
				char tmp[1024];
				FILE* p_stderr = subprocess_stderr(&m_compile_process);
				if (fgets(tmp, sizeof(tmp), p_stderr)) {
					m_compile_log.append(tmp);
				}
				logError(m_compile_log);

				FILE* p_stdout = subprocess_stdout(&m_compile_process);
				if (fgets(tmp, sizeof(tmp), p_stdout)) {
					m_compile_log.append(tmp);
				}
				logError(m_compile_log);
			}
			subprocess_destroy(&m_compile_process);
			m_compilation_running = false;
		} else {
			char tmp[1024];
			FILE* p_stderr = subprocess_stderr(&m_compile_process);
			if (fgets(tmp, sizeof(tmp), p_stderr)) {
				m_compile_log.append(tmp);
			}
		}
	}

	static void copyDir(const char* src, const char* dest, IAllocator& allocator) 	{
		PathInfo fi(src);
		StaticString<MAX_PATH> dst_dir(dest, "/", fi.basename);
		if (!os::makePath(dst_dir)) logError("Could not create ", dst_dir);
		os::FileIterator* iter = os::createFileIterator(src, allocator);

		os::FileInfo cfi;
		while(os::getNextFile(iter, &cfi)) {
			if (cfi.is_directory) {
				if (cfi.filename[0] != '.') {
					StaticString<MAX_PATH> tmp_src(src, "/", cfi.filename);
					StaticString<MAX_PATH> tmp_dst(dest, "/", fi.basename);
					copyDir(tmp_src, tmp_dst, allocator);
				}
			}
			else {
				StaticString<MAX_PATH> tmp_src(src, "/", cfi.filename);
				StaticString<MAX_PATH> tmp_dst(dest, "/", fi.basename, "/", cfi.filename);
				if(!os::copyFile(tmp_src, tmp_dst)) {
					logError("Failed to copy ", tmp_src, " to ", tmp_dst);
				}
			}
		}
		os::destroyFileIterator(iter);
	}

	void initProject() {
		FileSystem& fs = m_app.getEngine().getFileSystem();
		StaticString<MAX_PATH> cs_dir_path(fs.getBasePath(), "cs");
		if (!os::makePath(cs_dir_path)) logError("Failed to create ", cs_dir_path);
		StaticString<MAX_PATH> bin_dir_path(cs_dir_path, "/bin");
		if (!os::makePath(bin_dir_path)) logError("Failed to create ", bin_dir_path);
		StaticString<MAX_PATH> src_dir_path(cs_dir_path, "/src");
		if (!os::makePath(src_dir_path)) logError("Failed to create ", src_dir_path);

		StaticString<MAX_PATH> vs_code_project_dir(cs_dir_path, "/.vscode/");
		if (!os::dirExists(vs_code_project_dir)) {
			if (!os::makePath(vs_code_project_dir)) {
				logError("Failed to create ", vs_code_project_dir);
			}
			const char* launch_json_content = "{\n"
											 "	\"version\": \"0.2.0\",\n"
											 "	\"configurations\": [\n"
											 "		{\n"
											 "			\"name\": \"Attach to Lumix\",\n"
											 "			\"type\": \"mono\",\n"
											 "			\"request\": \"attach\",\n"
											 "			\"address\": \"127.0.0.1\",\n"
											 "			\"port\": 55555\n"
											 "		}\n"
											 "	]\n"
											 "}\n";

			if (!fs.saveContentSync(Path("cs/.vscode/launch.json"), Span((const u8*)launch_json_content, stringLength(launch_json_content)))) {
				logError("Failed to save cs/.vscode/launch.json");
			}
		}	

		copyDir(StaticString<MAX_PATH>(fs.getBasePath(), "../plugins/csharp/cs/"), StaticString<MAX_PATH>(fs.getBasePath(), "cs/src/"), m_app.getAllocator());
	}

	void makeUpToDate() {
		IAllocator& allocator = m_app.getWorldEditor().getAllocator();
		if (os::dirExists("cs") && !os::fileExists("cs/bin/main.dll")) {
			compile();
			return;
		}

		u64 dll_modified = os::getLastModified("cs/bin/main.dll");
		os::FileIterator* iter = os::createFileIterator("cs", allocator);
		os::FileInfo info;
		while (os::getNextFile(iter, &info)) {
			if (info.is_directory) continue;

			StaticString<MAX_PATH> tmp("cs\\", info.filename);
			u64 script_modified = os::getLastModified(tmp);
			if (script_modified > dll_modified) {
				compile();
				os::destroyFileIterator(iter);
				return;
			}
		}
		os::destroyFileIterator(iter);
	}


	void onFileChanged(const char* path) {
		if (Path::hasExtension(path, "cs")) m_deferred_compile = true;
	}


	void createNewScript(const char* name) {
		char class_name[128];

		const char* cin = name;
		char* cout = class_name;
		bool to_upper = true;
		while (*cin && cout - class_name < lengthOf(class_name) - 1) {
			char c = *cin;
			if (c >= 'a' && c <= 'z') {
				*cout = to_upper ? *cin - 'a' + 'A' : *cin;
				to_upper = false;
			} else if (c >= 'A' && c <= 'Z') {
				*cout = *cin;
				to_upper = false;
			} else if (c >= '0' && c <= '9') {
				*cout = *cin;
				to_upper = true;
			} else {
				to_upper = true;
				--cout;
			}
			++cout;
			++cin;
		}
		*cout = '\0';

		OutputMemoryStream blob(m_app.getAllocator());
		blob << "public class " << class_name << " : Lumix.Component\n{\n}\n";

		StaticString<MAX_PATH> path("cs/src/", class_name, ".cs");
		FileSystem& fs = m_app.getEngine().getFileSystem();
		if (fs.fileExists(path)) {
			logError(path, " already exists");
			return;
		}
		if (!fs.saveContentSync(Path(path), blob)) {
			logError("Failed to save ", path);
		}
	}


	void listDirInCSProj(OutputMemoryStream& blob, const char* dirname) {
		IAllocator& allocator = m_app.getWorldEditor().getAllocator();
		FileSystem& fs = m_app.getEngine().getFileSystem();
		StaticString<MAX_PATH> path("cs/src/", dirname);
		os::FileIterator* iter = fs.createFileIterator(path);
		os::FileInfo info;
		while (os::getNextFile(iter, &info)) {
			if (info.filename[0] == '.' || info.is_directory) continue;
			blob << "\t\t<Compile Include=\"" << dirname << info.filename << "\" />\n";
		}
		os::destroyFileIterator(iter);
	}

	void openCSFolder() {
		const char* base_path = m_app.getWorldEditor().getEngine().getFileSystem().getBasePath();
		StaticString<MAX_PATH> full_path(base_path, "cs/");
		os::shellExecuteOpen("code", ".", full_path);
	}

	void openVSProject() {
		const char* base_path = m_app.getWorldEditor().getEngine().getFileSystem().getBasePath();
		StaticString<MAX_PATH> full_path(base_path, "cs/src/main.csproj");
		os::shellExecuteOpen(full_path);
	}

	void generateCSProj() {
		OutputMemoryStream blob(m_app.getAllocator());
		blob << "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n"
				"\t<Project ToolsVersion=\"15.0\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n"
				"\t<ItemGroup>\n";

		listDirInCSProj(blob, "");
		listDirInCSProj(blob, "manual\\");
		listDirInCSProj(blob, "generated\\");

		blob << "\t</ItemGroup>\n"
				"\t<Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />\n"
				"</Project>\n";

		FileSystem& fs = m_app.getEngine().getFileSystem();
		if (!fs.saveContentSync(Path("cs/src/main.csproj"), blob)) {
			logError("Faile to save cs/src/main.csproj");
		}
	}


	void open(const char* filename) {
		WorldEditor& editor = m_app.getWorldEditor();
		FileSystem& fs = editor.getEngine().getFileSystem();
		StaticString<MAX_PATH> file_path(fs.getBasePath(), "cs/src/");
		if (filename) file_path.append(filename);
		os::shellExecuteOpen(file_path);
	}


	void onGUI() override {
		if (!m_is_open) return;

		if (!ImGui::Begin("C#", &m_is_open)) {
			ImGui::End();
			return;
		}

		bool need_init = !os::dirExists("cs");

		CSharpScriptModule* module = getModule();
		CSharpSystem& system = (CSharpSystem&)module->getSystem();
		if (m_compilation_running) {
			ImGui::Text("Compiling...");
		} else {
			if (mono_is_debugger_attached()) {
				ImGui::Text("Debugger attached");
				ImGui::SameLine();
			}

			if (need_init) {
				if (ImGui::Button("Init")) initProject();
			}
			else {
				if (ImGui::Button("Compile")) compile();
				ImGui::SameLine();
				if (ImGui::Button("Bindings")) generateBindings();
				ImGui::SameLine();
				if (ImGui::Button("Generate project")) generateCSProj();

				if (ImGui::Button("VS Code")) openCSFolder();
				ImGui::SameLine();
				if (ImGui::Button("Open VS project")) openVSProject();
				ImGui::SameLine();
				if (ImGui::Button("New script")) ImGui::OpenPopup("new_csharp_script");
				if (ImGui::BeginPopup("new_csharp_script")) {
					ImGui::InputText("Name", m_new_script_name, sizeof(m_new_script_name));
					if (ImGui::Button("Create")) {
						createNewScript(m_new_script_name);
						ImGui::CloseCurrentPopup();
					}
					ImGui::EndPopup();
				}
			}
		}

		ImGui::InputTextWithHint("##filter", "Filter", m_filter, sizeof(m_filter));

		for (const String& name : system.getNames()) {
			if (m_filter[0] != '\0' && findInsensitive(name.c_str(), m_filter) == 0) continue;
			ImGui::PushID((const void*)name.c_str());
			if (ImGui::Button("Edit")) {
				StaticString<MAX_PATH> filename(name.c_str(), ".cs");
				open(filename);
			}
			ImGui::SameLine();
			ImGui::Text("%s", name.c_str());
			ImGui::PopID();
		}

		if (m_compile_log.length() > 0 && ImGui::CollapsingHeader("Log")) {
			ImGui::Text("%s", m_compile_log.c_str());
		}

		ImGui::End();
	}


	const char* getName() const override { return "csharp_script"; }


	CSharpScriptModule* getModule() const {
		WorldEditor& editor = m_app.getWorldEditor();
		return (CSharpScriptModule*)editor.getWorld()->getModule("csharp_script");
	}


	static const char* toString(reflection::Variant::Type type) {
		switch (type) {
			case reflection::Variant::FLOAT: return "float";
			case reflection::Variant::BOOL: return "bool";
			case reflection::Variant::I32: return "int";
			case reflection::Variant::U32: return "u32";
			case reflection::Variant::VOID: return "void";
			case reflection::Variant::VEC2: return "Vec2";
			case reflection::Variant::VEC3: return "Vec3";
			// TODO 
			case reflection::Variant::DVEC3: return "DVec3";
			case reflection::Variant::COLOR: return "Color";
			case reflection::Variant::ENTITY: return "EntityRef";
			case reflection::Variant::PTR: return "void*";
			case reflection::Variant::CSTR: return "Path";
		}
		ASSERT(false);
		return "N/A";
	}

	static void getCSType(reflection::TypeDescriptor type, StaticString<64>& cs_type) {
		cs_type = "";
		if (type.is_reference && !type.is_const) cs_type.append("ref ");
		switch(type.type) {
			case reflection::Variant::BOOL: cs_type.append("bool"); break;
			case reflection::Variant::U32: cs_type.append("uint"); break;
			case reflection::Variant::I32: cs_type.append("int"); break;
			case reflection::Variant::FLOAT: cs_type.append("float"); break;
			case reflection::Variant::VOID: cs_type.append("void"); break;
			case reflection::Variant::VEC2: cs_type.append("Vec2"); break;
			case reflection::Variant::VEC3: cs_type.append("Vec3"); break;
		
			case reflection::Variant::DVEC3: cs_type.append("DVec3"); break;
			case reflection::Variant::COLOR: cs_type.append("Vec4"); break;
			case reflection::Variant::ENTITY: cs_type.append("int"); break;
			case reflection::Variant::PTR: cs_type.append("IntPtr"); break;
			case reflection::Variant::CSTR: cs_type.append("string"); break;
		}
	}


	static void getInteropType(reflection::TypeDescriptor cpp_type, StaticString<64>& cs_type) {
		switch(cpp_type.type) {
			case reflection::Variant::BOOL: cs_type = "bool"; break;
			case reflection::Variant::U32: cs_type = "uint"; break;
			case reflection::Variant::I32: cs_type = "int"; break;
			case reflection::Variant::FLOAT: cs_type = "float"; break;
			case reflection::Variant::VOID: cs_type = "void"; break;
			case reflection::Variant::VEC2: cs_type = "Vec2"; break;
			case reflection::Variant::VEC3: cs_type = "Vec3"; break;

			case reflection::Variant::DVEC3: cs_type = "DVec3"; break;
			case reflection::Variant::COLOR: cs_type = "Vec4"; break;
			case reflection::Variant::PTR: cs_type = "IntPtr"; break;
			case reflection::Variant::CSTR: cs_type = "string"; break;
			case reflection::Variant::ENTITY: cs_type = "int"; break;
		}
	}


	static void writeCSArgs(const reflection::FunctionBase& func, IOutputStream& blob, int skip_args, bool cs_internal_call, bool start_with_comma) {
		for (int i = skip_args, c = func.getArgCount(); i < c; ++i) {
			if (i > skip_args || start_with_comma) blob << ", ";
			StaticString<64> cs_type;
			getCSType(func.getArgType(i), cs_type);
			if (cs_internal_call && cs_type == "Entity") cs_type = "int";
			blob << cs_type << " a" << i - skip_args;
		}
	}

	/*
	static void generateEnumsBindings() {
		os::OutputFile cs_file;
		if (!cs_file.open("cs/src/generated/enums.cs")) {
			logError("C#") << "Failed to create cs/src/generated/enums.cs";
			return;
		}

		cs_file << "namespace Lumix {\n";

		for (int i = 0, count = reflection::getEnumsCount(); i < count; ++i) {
			const reflection::EnumBase& e = reflection::getEnum(i);
			const char* name_start = e.name;
			if (startsWith(name_start, "enum ")) name_start += stringLength("enum ");
			name_start = reverseFind(name_start, nullptr, ':');
			if (name_start[0] == ':') ++name_start;
			cs_file << "\tenum " << name_start << " {\n";
			for (int j = 0; j < e.values_count; ++j) {
				cs_file << "\t\t" << e.values[j].name;
				if (j < e.values_count - 1) cs_file << ",";
				cs_file << "\n";
			}

			cs_file << "\t}\n";
		}

		cs_file << "}\n";
		cs_file.close();
	}*/

	static StringView skipStruct(StringView v) {
		if (startsWith(v, "struct ")) v.removePrefix(7);
		return v;
	}

	static void printType(reflection::FunctionBase* func, OutputMemoryStream& blob) {
		blob << skipStruct(func->getReturnTypeName()) << "(" << skipStruct(func->getThisTypeName()) << "::*)(";
		for (u32 i = 0; i < func->getArgCount(); ++i) {
			if (i > 0) blob << ", ";
			reflection::TypeDescriptor td = func->getArgType(i);
			if (td.is_const) blob << "const ";
			blob << toString(td.type);
			if (td.is_reference) blob << "&";
		}
		blob << ")";
		if (func->isConstMethod()) blob << " const";
	}

	static void generateModule(OutputMemoryStream& api_blob) {
		using namespace reflection;
		for (Module* module_ptr = getFirstModule(); module_ptr; module_ptr = module_ptr->next) {
			Module& module = *module_ptr;
			StaticString<128> class_name;
			getCSharpName(module.name, class_name);
			class_name.append("Module");

			os::OutputFile cs_file;
			StaticString<MAX_PATH> filepath("cs/src/generated/", class_name, ".cs");
			if (!cs_file.open(filepath)) {
				logError("Failed to create ", filepath);
				continue;
			}

			cs_file << "using System;\n"
					   "using System.Runtime.InteropServices;\n"
					   "using System.Runtime.CompilerServices;\n"
					   "\n"
					   "namespace Lumix\n"
					   "{\n"
					   "	public unsafe partial class "
					<< class_name
					<< " : IModule\n"
					   "	{\n"
					   "		public static string Type { get { return \""
					<< module.name
					<< "\"; } }\n"
					   "\n"
					   "		public "
					<< class_name
					<< "(IntPtr _instance)\n"
					   "			: base(_instance) { }\n"
					   "\n"
					   "		public static implicit operator System.IntPtr("
					<< class_name
					<< " _value)\n"
					   "		{\n"
					   "			return _value.instance_;\n"
					   "		}\n"
					   "\n";

			for (FunctionBase* func_ptr : module.functions) {
				FunctionBase& func = *func_ptr;
				StaticString<128> cs_method_name;
				const char* cpp_method_name = func.decl_code + stringLength(func.decl_code);
				while (cpp_method_name > func.decl_code && *cpp_method_name != ':') --cpp_method_name;
				StaticString<64> cs_return_type;
				getCSType(func.getReturnType(), cs_return_type);
				StaticString<64> interop_return_type;
				getInteropType(func.getReturnType(), interop_return_type);

				if (*cpp_method_name == ':') ++cpp_method_name;
				getCSharpName(cpp_method_name, cs_method_name);
				api_blob << "{\n"
						"	using T = ";
						printType(&func, api_blob);

				api_blob << ";\n"
							"	auto f = &CSharpMethodProxy<T>::call<(T)&" << func.decl_code << ">;\n"
							"	mono_add_internal_call(\"Lumix." << class_name << "::" << cpp_method_name << "\", f);\n"
							"}\n"
							"\n\n";

				cs_file << "		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
							"		extern static "
						<< interop_return_type << " " << cpp_method_name << "(IntPtr instance";

				writeCSArgs(func, cs_file, 0, true, true);

				cs_file << ");\n"
							"\n"
							"		public "
						<< cs_return_type << " " << cs_method_name << "(";

				writeCSArgs(func, cs_file, 0, false, false);

				const char* return_expr = cs_return_type == "void" ? "" : (cs_return_type == "Entity" ? "var ret =" : "return");

				cs_file << ")\n"
							"		{\n"
							"			"
						<< return_expr << " " << cpp_method_name << "(instance_";

				for (int i = 0, c = func.getArgCount(); i < c; ++i) {
					StaticString<64> cs_type;
					getCSType(func.getArgType(i), cs_type);
					cs_file << ", ";
					if (startsWith(cs_type, "ref ")) {
						cs_file << "ref ";
					}
					cs_file << "a" << i;
					if (equalStrings(cs_type, "Entity")) {
						cs_file << ".entity_Id_";
					}
				}

				cs_file << ");\n";

				if (cs_return_type == "Entity") {
					cs_file << "			return World.GetEntity(ret);\n";
				}

				cs_file << "		}\n"
							"\n";

				api_blob << "{\n"
							"	using T = ";
							printType(&func, api_blob);
				api_blob << ";\n"
							"	auto f = &CSharpMethodProxy<T>::call<(T)&" << func.decl_code << ">;\n"
							"	mono_add_internal_call(\"Lumix." << class_name << "::" << cs_method_name << "\", f);\n"
							"}\n";
			}
			
			cs_file << "	}\n}\n";
			cs_file.close();
		}
	}


	void generateBindings() {
		FileSystem& fs = m_app.getWorldEditor().getEngine().getFileSystem();
		StaticString<MAX_PATH> path(fs.getBasePath(), "cs/src/generated");
		if (!os::makePath(path) && !os::dirExists(path)) {
			logError("Failed to create ", path);
			return;
		}

		OutputMemoryStream api_blob(m_app.getAllocator());

		// generateEnumsBindings();
		generateModule(api_blob);

		using namespace reflection;
		Span<const RegisteredComponent> cmps = reflection::getComponents();
		for (const RegisteredComponent& rc : cmps) {
			const ComponentBase* cmp = rc.cmp;
			if (!cmp) continue;
			const char* cmp_name = cmp->name;
			ComponentType cmp_type = cmp->component_type;

			StaticString<128> class_name;
			getCSharpName(cmp_name, class_name);

			OutputMemoryStream cs_blob(m_app.getAllocator());
			cs_blob << "using System;\n"
					   "using System.Runtime.InteropServices;\n"
					   "using System.Runtime.CompilerServices;\n"
					   "\n"
					   "namespace Lumix\n"
					   "{\n";
			cs_blob << "	[NativeComponent(Type = \"" << cmp_name << "\")]\n";
			cs_blob << "	public class " << class_name << " : Component\n";
			cs_blob << "	{\n";

			cs_blob << "		public " << class_name
					<< "(Entity _entity)\n"
					   "			: base(_entity,  getModule(_entity.instance_, \""
					<< cmp_name
					<< "\" )) { }\n"
					   "\n\n";

			CSPropertyBindingsVisitor visitor;
			visitor.cmp = cmp;
			visitor.class_name = class_name;
			visitor.blob = &cs_blob;
			visitor.api_blob = &api_blob;
			cmp->visit(visitor);

			for (reflection::FunctionBase* func : cmp->functions) {
				StaticString<128> cs_method_name;
				const char* cpp_method_name = func->decl_code + stringLength(func->decl_code);
				while (cpp_method_name > func->decl_code && *cpp_method_name != ':') --cpp_method_name;
				if (*cpp_method_name == ':') ++cpp_method_name;
				getCSharpName(cpp_method_name, cs_method_name);
				api_blob << "{\n"
							"	using T = ";
							printType(func, api_blob);
				api_blob << ";\n"
							"	auto f = &CSharpMethodProxy<T>::call<(T)&" << func->decl_code << ">;\n"
							"	mono_add_internal_call(\"Lumix." << class_name << "::" << cpp_method_name << "\", f);\n"
							"}\n"
							"\n\n";

				StaticString<64> ret_cs;
				getCSType(func->getReturnType(), ret_cs);
				cs_blob << "		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
							"		extern static "
						<< ret_cs << " " << cpp_method_name << "(IntPtr instance, int cmp";

				writeCSArgs(*func, cs_blob, 1, true, true);

				cs_blob << ");\n"
							"\n"
							"		public "
						<< ret_cs << " " << cs_method_name << "(";

				writeCSArgs(*func, cs_blob, 1, false, false);

				cs_blob << ")\n"
							"		{\n"
							"			"
						<< (ret_cs != "void" ? "return " : "") << cpp_method_name << "(module_, entity_.entity_Id_";

				for (int i = 1, c = func->getArgCount(); i < c; ++i) {
					cs_blob << ", a" << i - 1;
				}

				cs_blob << ");\n"
							"		}\n"
							"\n";
			}

			cs_blob << "\t} // class\n"
					   "} // namespace\n";

			StaticString<MAX_PATH> filepath("cs/src/generated/", class_name, ".cs");
			if (!fs.saveContentSync(Path(filepath), cs_blob)) {
				logError("Failed to create ", filepath);
			}
		}

		const char* api_path = "../plugins/csharp/src/api.inl";
		if (!fs.saveContentSync(Path(api_path), api_blob)) {
			logError("Failed to save ", api_path);
		}
	}


	void compile() {
		m_deferred_compile = false;
		if (m_compilation_running) return;

		m_compile_log = "";
		CSharpScriptModule* module = getModule();
		CSharpSystem& plugin = (CSharpSystem&)module->getSystem();
		plugin.unloadAssembly();
		IAllocator& allocator = m_app.getWorldEditor().getAllocator();
		const char* args[] = {"c:\\windows\\system32\\cmd.exe", "/c \"\"C:\\Program Files\\Mono\\bin\\mcs.bat\" -out:\"cs\\bin\\main.dll\" -target:library -debug -unsafe -recurse:\"cs\\src\\*.cs\"", nullptr};
		const int res = subprocess_create(args, 0, &m_compile_process);
		m_compilation_running = res == 0;
	}

	subprocess_s m_compile_process;
	bool m_compilation_running = false;
	StudioApp& m_app;
	UniquePtr<FileSystemWatcher> m_watcher;
	String m_compile_log;
	char m_filter[128];
	char m_new_script_name[128];
	bool m_deferred_compile = false;
	PropertyGridCSharpPlugin m_property_grid_plugin;
	bool m_is_open = false;
	Action m_toggle_ui;
};

struct AddCSharpComponentPlugin final : public StudioApp::IAddComponentPlugin {
	AddCSharpComponentPlugin(StudioApp& _app)
		: app(_app) {}


	void onGUI(bool create_entity, bool from_filter, EntityPtr parent, struct WorldEditor& editor) override {
		ImGui::SetNextWindowSize(ImVec2(300, 300));
		if (!ImGui::BeginMenu(getLabel())) return;

		CSharpScriptModule* script_module = (CSharpScriptModule*)editor.getWorld()->getModule(CSHARP_SCRIPT_TYPE);
		CSharpSystem& plugin = (CSharpSystem&)script_module->getSystem();
		for (auto& iter : plugin.getNamesArray()) {
			const char* name = iter.c_str();
			bool b = false;
			if (ImGui::Selectable(name, &b)) {
				if (create_entity) {
					EntityRef entity = editor.addEntity();
					editor.selectEntities(Span(&entity, 1), false);
				}
				if (editor.getSelectedEntities().empty()) return;
				EntityRef entity = editor.getSelectedEntities()[0];

				if (!editor.getWorld()->hasComponent(entity, CSHARP_SCRIPT_TYPE)) {
					editor.addComponent(Span(&entity, 1), CSHARP_SCRIPT_TYPE);
				}

				const ComponentUID cmp = editor.getWorld()->getComponent(entity, CSHARP_SCRIPT_TYPE);
				editor.beginCommandGroup("add_cs_script");
				editor.addArrayPropertyItem(cmp, "scripts");

				int scr_count = script_module->getScriptCount(entity);
				editor.setProperty(cmp.type, "scripts", scr_count - 1, "Path", Span((const EntityRef*)&entity, 1), name);
				editor.endCommandGroup();
				editor.lockGroupCommand();
				if (parent.isValid()) editor.makeParent(parent, entity);
				ImGui::CloseCurrentPopup();
			}
		}

		ImGui::EndMenu();
	}

	const char* getLabel() const override { return "C# Script"; }

	StudioApp& app;
};


LUMIX_STUDIO_ENTRY(csharp) {
	WorldEditor& editor = app.getWorldEditor();
	IAllocator& allocator = editor.getAllocator();
	StudioCSharpPlugin* plugin = LUMIX_NEW(allocator, StudioCSharpPlugin)(app);
	app.addPlugin(*plugin);

	auto* cmp_plugin = LUMIX_NEW(allocator, AddCSharpComponentPlugin)(app);
	app.registerComponent("", "csharp_script", *cmp_plugin);
	return nullptr;
}


} // namespace Lumix