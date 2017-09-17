#include <cassert>
#include <cstdio>
#include <ctype.h>
#include <windows.h>
#include <vector>


struct Property
{
	char type[32];
	char scene[32];
	char object[32];
	char subobject[32];
	char property[32];
};


template <int N>
const char* copyIdentifier(char(&out)[N], const char* src)
{
	char* c = out;
	while (*src != ',' && *src != ')')
	{
		*c = *src;
		++c;
		++src;
	}
	*c = '\0';
	while (!isalnum(*src) && *src != '_') ++src;
	return src;
}


bool addProperty(const char* str, std::vector<Property>& properties, bool is_subproperty)
{
	assert(str);
	
	Property prop;
	const char* c = str;
	c += is_subproperty ? strlen("CSHARP_SUBPROPERTY(") : strlen("CSHARP_PROPERTY(");
	c = copyIdentifier(prop.type, c);
	if (strcmp(prop.type, "Type") == 0) return true;
	if (!*c) return false;
	c = copyIdentifier(prop.scene, c);
	if (!*c) return false;
	c = copyIdentifier(prop.object, c);
	if (!*c) return false;
	if (is_subproperty)
	{
		c = copyIdentifier(prop.subobject, c);
	}
	else
	{
		prop.subobject[0] = '\0';
	}
	if (!*c) return false;
	copyIdentifier(prop.property, c);
	properties.push_back(prop);
	return true;
}


bool parse(const char* path, std::vector<Property>& properties)
{
	FILE* fp = fopen(path, "rb");
	if (!fp) return false;
	
	fseek(fp, 0, SEEK_END);
	int size = (int)ftell(fp);
	fseek(fp, 0, SEEK_SET);

	std::vector<char> text;
	text.resize(size + 1);

	if (fread(&text[0], 1, size, fp) != size)
	{
		fclose(fp);
		return false;
	}
	fclose(fp);

	text[size] = 0;

	const char* prop_str = &text[0];
	while (prop_str = strstr(prop_str, "CSHARP_PROPERTY("))
	{
		if (!addProperty(prop_str, properties, false)) return false;
		++prop_str;
	}

	prop_str = &text[0];
	while (prop_str = strstr(prop_str, "CSHARP_SUBPROPERTY("))
	{
		if (!addProperty(prop_str, properties, true)) return false;
		++prop_str;
	}

	return true;
}


void writeCSharpFooter(FILE* fp, const char* obj, const std::vector<Property>& properties)
{
	const char* last_subobj = "";
	for (const Property& prop : properties)
	{
		if (!prop.subobject[0]) continue;
		if (strcmp(prop.object, obj)) continue;

		if (strcmp(prop.subobject, last_subobj))
		{
			if(last_subobj[0]) fprintf(fp, "%s", "\t\t}\n\n");
			last_subobj = prop.subobject;
			static const char* container_text =
				"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
				"		private extern static int get%sCount(IntPtr scene, int cmp);\n"
				"\n"
				"\n"
				"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
				"		private extern static void add%s(IntPtr scene, int cmp);\n"
				"\n"
				"\n"
				"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
				"		private extern static void remove%s(IntPtr scene, int cmp, int index);\n"
				"\n"
				"\n"
				"		public class %sContainer\n"
				"		{\n"
				"			public int Length { get { return %s.getGrassCount(_object.scene, _object.component_id); } }\n"
				"\n"
				"			public %sType this[int index]\n"
				"			{\n"
				"				get {\n"
				"					return new %sType(_object, index);\n"
				"				}\n"
				"			}\n"
				"\n"
				"			public void add()\n"
				"			{\n"
				"				%s.add%s(_object.scene, _object.component_id);\n"
				"			}\n"
				"\n"
				"			public void remove(int index)\n"
				"			{\n"
				"				%s.remove%s(_object.scene, _object.component_id, index);\n"
				"			}\n"
				"\n"
				"			public %s _object;\n"
				"		}\n"
				"\n"
				"\n"
				"		public %sContainer %s = new %sContainer();\n"
				"\n"
				"\n";

			fprintf(fp, container_text, prop.subobject, prop.subobject, prop.subobject, prop.subobject, prop.object, prop.subobject, prop.subobject, prop.object, prop.subobject, prop.object, prop.subobject, prop.object, prop.subobject, prop.subobject, prop.subobject);

			static const char* text =
				"		public class %sType\n"
				"		{\n"
				"			private int _index;\n"
				"			private %s _object;\n"
				"\n"
				"			public %sType(%s obj, int index)\n"
				"			{\n"
				"				_index = index;\n"
				"				_object = obj;\n"
				"			}\n"
				"\n"
				;
			fprintf(fp, text, prop.subobject, prop.object, prop.subobject, prop.object);
		}

		const char* prop_text =
			"			public %s %s\n"
			"			{\n"
			"				get{ return %s.get%s%s(_object.scene, _object.component_id, _index); }\n"
			"				set{ %s.set%s%s(_object.scene, _object.component_id, value, _index); }\n"
			"			}\n"
			"\n"
			"\n";
		fprintf(fp, prop_text, prop.type, prop.property, prop.object, prop.subobject, prop.property, prop.object, prop.subobject, prop.property);
	}
	if (last_subobj[0]) fprintf(fp, "%s", "\t\t}\n\n");
	
	fprintf(fp, "\t}\n\n}\n");
}


void writeCSharpHeader(FILE* fp, const char* obj, const std::vector<Property>& properties)
{
	static const char* header =
		"using System;\n"
		"using System.Runtime.InteropServices;\n"
		"using System.Runtime.CompilerServices;\n"
		"\n"
		"\n"
		"namespace Lumix\n"
		"{\n"
		"\n"
		"\n"
		"	public class %s : NativeComponent\n"
		"	{\n"
		"		private int component_id;\n"
		"		private IntPtr scene;\n"
		"\n"
		"\n"
		"		public override void create()\n"
		"		{\n"
		"			component_id = create(entity._universe, entity._entity_id, \"%s\");\n"
		"			if (component_id < 0) throw new Exception(\"Failed to create component\");\n"
		"			scene = getScene(entity._universe, \"%s\");\n";
	
	char cmp_name[128];
	char* c = cmp_name;
	const char* in = obj;
	while (*in && c - cmp_name < sizeof(cmp_name) - 3)
	{
		if (isupper(*in))
		{
			if (in != obj)
			{
				*c = '_';
				++c;
			}
			*c = *in - 'A' + 'a';
		}
		else
		{
			*c = *in;
		}
		++c;
		++in;
	}
	*c = '\0';
	fprintf(fp, header, obj, cmp_name, cmp_name);

	static const char* header_end = 
		"		}\n"
		"\n"
		"\n";

	const char* last_subobj = "";
	for (const Property& prop : properties)
	{
		if (!prop.subobject[0]) continue;
		if (strcmp(prop.object, obj)) continue;
		if (strcmp(last_subobj, prop.subobject) == 0) continue;

		last_subobj = prop.subobject;
		fprintf(fp, "			%s._object = this;\n", prop.subobject);
	}

	fprintf(fp, "%s", header_end);
}


void writeCSharpProperty(FILE* fp, const Property& prop)
{
	static const char* text =
		"		/* %s */\n"
		"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
		"		private extern static void set%s(IntPtr scene, int cmp, %s source);\n"
		"		\n"
		"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
		"		private extern static %s get%s(IntPtr scene, int cmp);\n"
		"		\n"
		"		public %s %s\n"
		"		{\n"
		"			get{ return get%s(scene, component_id); }\n"
		"			set{ set%s(scene, component_id, value); }\n"
		"		}\n"
		"\n"
		"\n";

	const char* type = prop.type;
	if (strcmp(type, "const char*") == 0) type = "string";
	else if (strcmp(type, "Path") == 0) type = "string";
	const char* name = prop.property;
	fprintf(fp, text, name, name, type, type, name, type, name, name, name);
}


void writeCSharpSubproperty(FILE* fp, const Property& prop)
{
	static const char* text =
		"		/* %s %s */\n"
		"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
		"		private extern static void set%s%s(IntPtr scene, int cmp, int index, %s source);\n"
		"		\n"
		"		[MethodImplAttribute(MethodImplOptions.InternalCall)]\n"
		"		private extern static %s get%s%s(IntPtr scene, int cmp, int index);\n"
		"\n"
		"\n";

	const char* type = prop.type;
	if (strcmp(type, "const char*") == 0) type = "string";
	else if (strcmp(type, "Path") == 0) type = "string";
	const char* name = prop.property;
	const char* subobject = prop.subobject;
	fprintf(fp, text, subobject, name, subobject, name, type, type, subobject, name);
}


bool writeCSharp(const char* out_dir, std::vector<Property>& properties)
{
	auto cmp = [](const void* a, const void* b) -> int {
		auto* prop_a = (Property*)a;
		auto* prop_b = (Property*)b;
		int cmp = strcmp(prop_a->object, prop_b->object);
		if (cmp) return cmp;
		return strcmp(prop_a->subobject, prop_b->subobject);
	};
	qsort(&properties[0], properties.size(), sizeof(properties[0]), cmp);

	FILE* fp = nullptr;
	const char* last_obj = "";
	for (const Property& prop : properties)
	{
		if (strcmp(last_obj, prop.object) != 0)
		{
			if (fp)
			{
				writeCSharpFooter(fp, last_obj, properties);
				fclose(fp);
			}
			char path[256];
			strcpy_s(path, out_dir);
			strcat_s(path, prop.object);
			strcat_s(path, ".cs");
			fp = fopen(path, "wb");
			last_obj = prop.object;
			if (!fp) return false;

			writeCSharpHeader(fp, prop.object, properties);
		}
		if (prop.subobject[0])
		{
			writeCSharpSubproperty(fp, prop);
		}
		else
		{
			writeCSharpProperty(fp, prop);
		}
	}
	writeCSharpFooter(fp, last_obj, properties);
	if (fp) fclose(fp);
	return true;
}


int main()
{
	std::vector<Property> properties;
	parse("../../../../lumixengine_csharp/src/csharp.cpp", properties);
	writeCSharp("../../../../lumixengine_csharp/cs/", properties);
}