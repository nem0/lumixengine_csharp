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
#include "engine/reflection.h"
#include "engine/resource.h"
#include "engine/universe/universe.h"
#include "imgui/imgui.h"
#include "csharp.h"
#include <cstdlib>

#include <mono/jit/jit.h>
#include <mono/metadata/debug-helpers.h>
#include <mono/metadata/metadata.h>
#include <mono/metadata/mono-debug.h>


namespace Lumix
{


static const ComponentType CSHARP_SCRIPT_TYPE = Reflection::getComponentType("csharp_script");
static const ResourceType CSHARP_SCRIPT_RESOURCE_TYPE("csharp_script");


struct StudioCSharpPlugin : public StudioApp::IPlugin
{
	StudioCSharpPlugin(StudioApp& app)
		: m_app(app)
		, m_compile_log(app.getWorldEditor().getAllocator())
	{
		m_filter[0] = '\0';
		m_new_script_name[0] = '\0';

		IAllocator& allocator = app.getWorldEditor().getAllocator();
		m_watcher = FileSystemWatcher::create("cs", allocator);
		m_watcher->getCallback().bind<StudioCSharpPlugin, &StudioCSharpPlugin::onFileChanged>(this);

		findVSCode();

		makeUpToDate();
	}


	~StudioCSharpPlugin()
	{
		FileSystemWatcher::destroy(m_watcher);
	}


	void findVSCode()
	{
		const char* code_path = "C:\\Program Files (x86)\\Microsoft VS Code\\Code.exe";
		if (PlatformInterface::fileExists(code_path)) m_vs_code_path = code_path;
		const char* code_path_64 = "C:\\Program Files\\Microsoft VS Code\\Code.exe";
		if (PlatformInterface::fileExists(code_path_64)) m_vs_code_path = code_path_64;
	}


	void update(float)
	{
		if (m_deferred_compile) compile();
		if (!m_compile_process) return;
		if (PlatformInterface::isProcessFinished(*m_compile_process))
		{
			if (PlatformInterface::getProcessExitCode(*m_compile_process) == 0)
			{
				CSharpScriptScene* scene = getScene();
				CSharpPlugin& plugin = (CSharpPlugin&)scene->getPlugin();
				plugin.loadAssembly();
			}
			else
			{
				char tmp[1024];
				int tmp_size;
				while ((tmp_size = PlatformInterface::getProcessOutput(*m_compile_process, tmp, lengthOf(tmp) - 1)) != -1)
				{
					tmp[tmp_size] = 0;
					m_compile_log.cat(tmp);
				}
				g_log_error.log("C#") << m_compile_log;
			}
			PlatformInterface::destroyProcess(*m_compile_process);
			m_compile_process = nullptr;
		}
		else
		{
			char tmp[1024];
			int tmp_size = PlatformInterface::getProcessOutput(*m_compile_process, tmp, lengthOf(tmp) - 1);
			if (tmp_size != -1)
			{
				tmp[tmp_size] = 0;
				m_compile_log.cat(tmp);
			}
		}
	}


	void makeUpToDate()
	{
		IAllocator& allocator = m_app.getWorldEditor().getAllocator();
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
		if (PathUtils::hasExtension(path, "cs")) m_deferred_compile = true;
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
		if (!file.open(path, FS::Mode::CREATE_AND_WRITE))
		{
			g_log_error.log("C#") << "Failed to create file " << path;
			return;
		}

		file.writeText("public class ");
		file.writeText(class_name);
		file.writeText(" : Lumix.Component\n{\n}\n");

		file.close();
	}


	void openVSCode(const char* filename)
	{
		if (!PlatformInterface::fileExists(m_vs_code_path)) return;

		WorldEditor& editor = m_app.getWorldEditor();
		StaticString<MAX_PATH_LENGTH> root(editor.getEngine().getDiskFileDevice()->getBasePath(), "cs/");
		StaticString<MAX_PATH_LENGTH> vs_code_project_dir(root, ".vscode/");
		if (!PlatformInterface::dirExists(vs_code_project_dir))
		{
			StaticString<MAX_PATH_LENGTH> launch_json_path(vs_code_project_dir, "launch.json");

			PlatformInterface::makePath(vs_code_project_dir);
			const char* laung_json_content =
				"{\n"
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
				
			m_app.makeFile(launch_json_path, laung_json_content);
		}

		StaticString<MAX_PATH_LENGTH> file_path(root);
		if (filename) file_path << filename;
		PlatformInterface::shellExecuteOpen(m_vs_code_path, file_path);
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
			if (mono_is_debugger_attached())
			{
				ImGui::Text("Debugger attached");
				ImGui::SameLine();
			}

			if (ImGui::Button("Compile")) compile();
			ImGui::SameLine();
			if (ImGui::Button("Bindings")) generateBindings();
			ImGui::SameLine();
			if (ImGui::Button("Open VS Code")) openVSCode(nullptr);
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

		ImGui::LabellessInputText("Filter", m_filter, sizeof(m_filter));

		for (int i = 0, c = plugin.getNamesCount(); i < c; ++i)
		{
			const char* name = plugin.getName(i);
			if (m_filter[0] != '\0' && stristr(name, m_filter) == 0) continue;
			ImGui::PushID(i);
			if (ImGui::Button("Edit"))
			{
				StaticString<MAX_PATH_LENGTH> filename(name, ".cs");
				openVSCode(filename);
			}
			ImGui::SameLine();
			ImGui::Text("%s", name);
			ImGui::PopID();
		}

		if (m_compile_log.length() > 0 && ImGui::CollapsingHeader("Log"))
		{
			ImGui::Text("%s", m_compile_log.c_str());
		}

		ImGui::EndDock();
	}


	const char* getName() const override { return "csharp_script"; }


	CSharpScriptScene* getScene() const
	{
		WorldEditor& editor = m_app.getWorldEditor();
		return (CSharpScriptScene*)editor.getUniverse()->getScene(crc32("csharp_script"));
	}


	static void getCSType(const char* cpp_type, StaticString<64>& cs_type)
	{
		const char* c = cpp_type;
		auto skip = [&c](const char* value) {
			if (startsWith(c, value)) c += stringLength(value);
		};
		skip("struct ");
		skip("Lumix::");
		cs_type = c;
		char* end = cs_type.data + stringLength(cs_type.data) - 1;
		while (end >= cs_type.data && (*end == ' ' || *end == '&')) --end;
		++end;
		*end = 0;
		if (endsWith(cs_type, " const")) cs_type.data[stringLength(cs_type.data) - sizeof(" const") + 1] = '\0';
		if (cs_type == "const char*") cs_type = "string";
		if (cs_type == "Path") cs_type = "string";
	}


	static void writeCSArgs(const Reflection::FunctionBase& func, FS::OsFile& file, int skip_args, bool cs_internal_call)
	{
		for (int i = skip_args, c = func.getArgCount(); i < c; ++i)
		{
			if (i > skip_args) file << ", ";
			StaticString<64> cs_type;
			getCSType(func.getArgType(i), cs_type);
			if (cs_internal_call && cs_type == "Entity") cs_type = "int";
			file << cs_type << " a" << i - skip_args;
		}
	}


	static void generateScenesBindings(FS::OsFile& api_file)
	{
		using namespace Reflection;
		for (int i = 0, c = getScenesCount(); i < c; ++i)
		{
			const SceneBase& scene = Reflection::getScene(i);

			StaticString<128> class_name;
			getCSharpName(scene.name, class_name);
			class_name << "Scene";

			FS::OsFile cs_file;
			StaticString<MAX_PATH_LENGTH> filepath("cs/generated/", class_name, ".cs");
			if (!cs_file.open(filepath, FS::Mode::CREATE_AND_WRITE))
			{
				g_log_error.log("C#") << "Failed to create " << filepath;
				continue;
			}

			cs_file <<
				"using System;\n"
				"using System.Runtime.InteropServices;\n"
				"using System.Runtime.CompilerServices;\n"
				"\n"
				"namespace Lumix\n"
				"{\n"
				"	public class " << class_name << " : IScene\n"
				"	{\n"
				"		public static string Type { get { return \"" << scene.name << "\"; } }\n"
				"\n"
				"		public " << class_name << "(IntPtr _instance)\n"
				"			: base(_instance) { }\n"
				"\n"
				"		public static implicit operator System.IntPtr(" << class_name << " _value)\n"
				"		{\n"
				"			return _value.instance_;\n"
				"		}\n"
				"\n";

			struct : IFunctionVisitor
			{
				void visit(const struct FunctionBase& func) override
				{
					StaticString<128> cs_method_name;
					const char* cpp_method_name = func.decl_code + stringLength(func.decl_code);
					while (cpp_method_name > func.decl_code && *cpp_method_name != ':') --cpp_method_name;
					StaticString<64> cs_return_type;
					getCSType(func.getReturnType(), cs_return_type);
					if (*cpp_method_name == ':') ++cpp_method_name;
					getCSharpName(cpp_method_name, cs_method_name);
					*api_file <<
						"{\n"
						"	auto f = &CSharpMethodProxy<decltype(&" << func.decl_code << ")>::call<&" << func.decl_code << ">;\n"
						"	mono_add_internal_call(\"Lumix." << class_name << "::" << cpp_method_name << "\", f);\n"
						"}\n"
						"\n\n";

					*cs_file <<
						"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
						"		extern static "<< cs_return_type << " " << cpp_method_name << "(IntPtr instance, ";

					writeCSArgs(func, *cs_file, 0, true);

					*cs_file << ");\n"
						"\n"
						"		public " << cs_return_type <<  " " << cs_method_name << "(";

					writeCSArgs(func, *cs_file, 0, false);

					const char* return_expr = cs_return_type == "void" ? "" : (cs_return_type == "Entity" ? "var ret =" : "return");

					*cs_file <<
						")\n"
						"		{\n"
						"			" << return_expr << " " << cpp_method_name << "(instance_, ";

					for (int i = 0, c = func.getArgCount(); i < c; ++i)
					{
						if (i > 0) *cs_file << ", ";
						*cs_file << "a" << i;
						StaticString<64> cs_type;
						getCSType(func.getArgType(i), cs_type);
						if (equalStrings(cs_type, "Entity"))
						{
							*cs_file << ".entity_Id_";
						}
					}

					*cs_file << ");\n";

					if (cs_return_type == "Entity")
					{
						*cs_file << "			return Universe.getEntity(ret);\n";
					}

					*cs_file <<
						"		}\n"
						"\n";

					*api_file <<
						"{\n"
						"	auto f = &CSharpMethodProxy<decltype(&" << func.decl_code << ")>::call<&" << func.decl_code << ">;\n"
						"	mono_add_internal_call(\"Lumix." << class_name << "::" << cs_method_name << "\", f);\n"
						"}\n";
				}

				const char* class_name;
				FS::OsFile* cs_file;
				FS::OsFile* api_file;
			} visitor;

			visitor.class_name = class_name;
			visitor.cs_file = &cs_file;
			visitor.api_file = &api_file;
			scene.visit(visitor);
			
			cs_file << "	}\n}\n";

			cs_file.close();
		}
	}


	void generateBindings()
	{
		FS::OsFile api_file;
		const char* api_h_filepath = "../lumixengine_csharp/src/api.inl";
		if (!api_file.open(api_h_filepath, FS::Mode::CREATE_AND_WRITE))
		{
			g_log_error.log("C#") << "Failed to create " << api_h_filepath;
			return;
		}

		const char* base_path = m_app.getWorldEditor().getEngine().getDiskFileDevice()->getBasePath();
		StaticString<MAX_PATH_LENGTH> path(base_path, "cs/generated");
		if (!PlatformInterface::makePath(path) && !PlatformInterface::dirExists(path))
		{
			g_log_error.log("C#") << "Failed to create " << path;
			return;
		}

		generateScenesBindings(api_file);

		using namespace Reflection;
		int cmps_count = getComponentTypesCount();
		for (int i = 0; i < cmps_count; ++i)
		{
			const char* cmp_name = getComponentTypeID(i);
			ComponentType cmp_type = getComponentType(cmp_name);
			const ComponentBase* cmp = Reflection::getComponent(cmp_type);

			StaticString<128> class_name;
			getCSharpName(cmp_name, class_name);

			FS::OsFile cs_file;
			StaticString<MAX_PATH_LENGTH> filepath("cs/generated/", class_name, ".cs");
			if (!cs_file.open(filepath, FS::Mode::CREATE_AND_WRITE))
			{
				g_log_error.log("C#") << "Failed to create " << filepath;
				continue;
			}

			cs_file.writeText(
				"using System;\n"
				"using System.Runtime.InteropServices;\n"
				"using System.Runtime.CompilerServices;\n"
				"\n"
				"namespace Lumix\n"
				"{\n"
			);
			cs_file << "	[NativeComponent(Type = \"" << cmp_name << "\")]\n";
			cs_file << "	public class " << class_name << " : Component\n";
			cs_file << "	{\n";

			cs_file <<
				"		public " << class_name << "(Entity _entity, int _cmpId)\n"
				"			: base(_entity, _cmpId, getScene(_entity.instance_, \"" << cmp_name << "\" )) { }\n"
				"\n\n";

			struct : Reflection::IPropertyVisitor
			{
				void write(const PropertyBase& prop, const char* cs_type, const char* cpp_type)
				{
					StaticString<128> csharp_name;
					getCSharpName(prop.name, csharp_name);

					*api_file <<
						"{\n"
						"	auto getter = &csharp_getProperty<decltype(&" << prop.getter_code << "), &" << prop.getter_code << ">;\n"
						"	mono_add_internal_call(\"Lumix." << class_name << "::get" << csharp_name << "\", getter);\n"
						"	auto setter = &csharp_setProperty<decltype(&" << prop.setter_code << "), &" << prop.setter_code << ">;\n"
						"	mono_add_internal_call(\"Lumix." << class_name << "::set" << csharp_name << "\", setter);\n"
						"}\n\n";

					*file <<
						"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
						"		extern static " << cs_type << " get" << csharp_name << "(IntPtr scene, int cmp);\n"
						"\n"
						"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
						"		extern static void set" << csharp_name << "(IntPtr scene, int cmp, " << cs_type << " value);\n"
						"\n\n";

					bool is_bool = equalStrings(cs_type, "bool");
					*file <<
						"		public " << cs_type << " " << (is_bool ? "Is" : "") << csharp_name << "\n"
						"		{\n"
						"			get { return get" << csharp_name << "(scene_, componentId_); }\n"
						"			set { set" << csharp_name << "(scene_, componentId_, value); }\n"
						"		}\n"
						"\n";
				}

				void visit(const Property<float>& prop)  override { write(prop, "float", "float"); }
				void visit(const Property<int>& prop)  override { write(prop, "int", "int"); }
				void visit(const Property<Entity>& prop)  override {}
				void visit(const Property<Int2>& prop)  override { write(prop, "Int2", "Int2"); }
				void visit(const Property<Vec2>& prop)  override { write(prop, "Vec2", "Vec2"); }
				void visit(const Property<Vec3>& prop)  override { write(prop, "Vec3", "Vec3"); }
				void visit(const Property<Vec4>& prop)  override { write(prop, "Vec4", "Vec4"); }
				void visit(const Property<Path>& prop)  override { write(prop, "string", "Path"); }
				void visit(const Property<bool>& prop)  override { write(prop, "bool", "bool"); }
				void visit(const Property<const char*>& prop)  override { write(prop, "string", "const char*"); }
				void visit(const IArrayProperty& prop)  override {}
				void visit(const IEnumProperty& prop)  override {}
				void visit(const IBlobProperty& prop)  override {}
				void visit(const ISampledFuncProperty& prop) override {}

				const ComponentBase* cmp;
				const char* class_name;
				FS::OsFile* api_file;
				FS::OsFile* file;
			} visitor;

			visitor.cmp = cmp;
			visitor.class_name = class_name;
			visitor.file = &cs_file;
			visitor.api_file = &api_file;
			cmp->visit(visitor);
			
			struct : IFunctionVisitor
			{
				void visit(const FunctionBase& func) override
				{
					StaticString<128> cs_method_name;
					const char* cpp_method_name = func.decl_code + stringLength(func.decl_code);
					while (cpp_method_name > func.decl_code && *cpp_method_name != ':') --cpp_method_name;
					if (*cpp_method_name == ':') ++cpp_method_name;
					getCSharpName(cpp_method_name, cs_method_name);
					*api_file <<
						"{\n"
						"	auto f = &CSharpMethodProxy<decltype(&" << func.decl_code << ")>::call<&" << func.decl_code << ">;\n"
						"	mono_add_internal_call(\"Lumix." << class_name << "::" << cpp_method_name << "\", f);\n"
						"}\n"
						"\n\n";

					*cs_file <<
						"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
						"		extern static void " << cpp_method_name << "(IntPtr instance, int cmp, ";
					
					writeCSArgs(func, *cs_file, 1, true);

					*cs_file << ");\n"
						"\n"
						"		public void " << cs_method_name << "(";

					writeCSArgs(func, *cs_file, 1, false);

					*cs_file <<
						")\n"
						"		{\n"
						"			" << cpp_method_name << "(scene_, componentId_, ";
					
					for (int i = 1, c = func.getArgCount(); i < c; ++i)
					{
						if (i > 1) *cs_file << ", ";
						*cs_file << "a" << i - 1;
					}

					*cs_file << ");\n"
						"		}\n"
						"\n";
				}
				const char* class_name;
				FS::OsFile* cs_file;
				FS::OsFile* api_file;
			} fnc_visitor;

			fnc_visitor.cs_file = &cs_file;
			fnc_visitor.class_name = class_name;
			fnc_visitor.api_file = &api_file;
			cmp->visit(fnc_visitor);

			cs_file <<
				"\t} // class\n"
				"} // namespace\n";

			cs_file.close();
		}
		api_file.close();
	}


	void compile()
	{
		m_deferred_compile = false;
		if (m_compile_process) return;

		m_compile_log = "";
		CSharpScriptScene* scene = getScene();
		CSharpPlugin& plugin = (CSharpPlugin&)scene->getPlugin();
		plugin.unloadAssembly();
		IAllocator& allocator = m_app.getWorldEditor().getAllocator();
		m_compile_process = PlatformInterface::createProcess("c:\\windows\\system32\\cmd.exe", "/c \"\"C:\\Program Files\\Mono\\bin\\mcs.bat\" -out:\"cs\\main.dll\" -target:library -g -unsafe -recurse:\"cs\\*.cs\"", allocator);
	}

	PlatformInterface::Process* m_compile_process = nullptr;
	StudioApp& m_app;
	FileSystemWatcher* m_watcher;
	string m_compile_log;
	StaticString<MAX_PATH_LENGTH> m_vs_code_path;
	char m_filter[128];
	char m_new_script_name[128];
	bool m_deferred_compile = false;
};


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
					case MONO_TYPE_CLASS:
					{
						MonoType* mono_type = mono_field_get_type(field);
						MonoClass* mono_class = mono_type_get_class(mono_type);
						if (equalStrings(mono_class_get_name(mono_class), "Entity"))
						{
							MonoObject* field_obj = mono_field_get_value_object(mono_domain_get(), field, obj);
							Entity entity = INVALID_ENTITY;
							MonoClassField* entity_id_field = mono_class_get_field_from_name(mono_class, "entity_Id_");
							if (field_obj)
							{
								mono_field_get_value(field_obj, entity_id_field, &entity.index);
							}
							old_value.write(entity.index);
							int index;
							fromCString(val, stringLength(val), &index);
							value.write(index);
						}
						else
						{
							ASSERT(false);
						}
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
					case MONO_TYPE_CLASS:
					{
						MonoType* mono_type = mono_field_get_type(field);
						MonoClass* mono_class = mono_type_get_class(mono_type);
						if (equalStrings(mono_class_get_name(mono_class), "Entity"))
						{
							Entity entity;
							entity.index = InputBlob(value).read<int>();;
							u32 gc_handle = scene->getEntityGCHandle(entity);
							MonoObject* entity_obj = mono_gchandle_get_target(gc_handle);
							mono_field_set_value(obj, field, entity_obj);
						}
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


	static Entity csharp_entityInput(PropertyGridCSharpPlugin* that, Universe* universe, MonoString* label_mono, Entity entity)
	{
		StudioApp& app = that->m_app;
		PropertyGrid& prop_grid = app.getPropertyGrid();
		const char* label = mono_string_to_utf8(label_mono);
		prop_grid.entityInput(label, label, entity);
		return entity;
	}


	static void csharp_Component_setProperty(PropertyGridCSharpPlugin* that, Universe* universe, Entity entity, MonoObject* cmp_obj, MonoString* prop, MonoString* value)
	{
		CSharpScriptScene* scene = (CSharpScriptScene*)universe->getScene(CSHARP_SCRIPT_TYPE);
		ComponentHandle cmp = scene->getComponent(entity, CSHARP_SCRIPT_TYPE);
		char* prop_str = mono_string_to_utf8(prop);
		char* value_str = mono_string_to_utf8(value);
		WorldEditor& editor = that->m_app.getWorldEditor();
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


	explicit PropertyGridCSharpPlugin(StudioCSharpPlugin& studio_plugin)
		: m_app(studio_plugin.m_app)
		, m_studio_plugin(studio_plugin)
	{
		mono_add_internal_call("Lumix.Component::setCSharpProperty", &csharp_Component_setProperty);
		mono_add_internal_call("Lumix.Component::entityInput", &csharp_entityInput);
	}


	void onGUI(PropertyGrid& grid, ComponentUID cmp) override
	{
		if (cmp.type != CSHARP_SCRIPT_TYPE) return;

		auto* scene = static_cast<CSharpScriptScene*>(cmp.scene);
		auto& plugin = static_cast<CSharpPlugin&>(scene->getPlugin());
		WorldEditor& editor = m_app.getWorldEditor();
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
				u32 gc_handle = scene->getGCHandle(cmp.handle, j);
				if (gc_handle == INVALID_GC_HANDLE) continue;
				ImGui::PushID(j);
				scene->tryCallMethod(gc_handle, "OnInspector", this, true);
				if (ImGui::Button("Edit"))
				{
					StaticString<MAX_PATH_LENGTH> filename(script_name, ".cs");
					m_studio_plugin.openVSCode(filename);
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
			}
		}
	}

	StudioApp& m_app;
	StudioCSharpPlugin& m_studio_plugin;
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
		
		WorldEditor& editor = app.getWorldEditor();
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
	WorldEditor& editor = app.getWorldEditor();
	IAllocator& allocator = editor.getAllocator();
	StudioCSharpPlugin* plugin = LUMIX_NEW(allocator, StudioCSharpPlugin)(app);
	app.addPlugin(*plugin);

	auto* cmp_plugin = LUMIX_NEW(allocator, AddCSharpComponentPlugin)(app);
	app.registerComponent("csharp_script", *cmp_plugin);

	editor.registerEditorCommandCreator("add_csharp_script", createAddCSharpScriptCommand);
	editor.registerEditorCommandCreator("remove_csharp_script", createRemoveScriptCommand);
	editor.registerEditorCommandCreator("set_csharp_script_property", createSetPropertyCommand);

	auto* pg_plugin = LUMIX_NEW(editor.getAllocator(), PropertyGridCSharpPlugin)(*plugin);
	app.getPropertyGrid().addPlugin(*pg_plugin);
}


}