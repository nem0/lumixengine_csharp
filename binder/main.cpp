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
	char property[32];
};


template <typename F>
struct privDefer {
	F f;
	privDefer(F f) : f(f) {}
	~privDefer() { f(); }
};

template <typename F>
privDefer<F> defer_func(F f) {
	return privDefer<F>(f);
}

#define DEFER_1(x, y) x##y
#define DEFER_2(x, y) DEFER_1(x, y)
#define DEFER_3(x)    DEFER_2(x, __COUNTER__)
#define defer(code)   auto DEFER_3(_defer_) = defer_func([&](){code;})


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


bool addProperty(const char* str, std::vector<Property>& properties)
{
	assert(str);
	
	Property prop;
	const char* c = str + strlen("CSHARP_PROPERTY(");
	c = copyIdentifier(prop.type, c);
	if (strcmp(prop.type, "Type") == 0) return true;
	if (!*c) return false;
	c = copyIdentifier(prop.scene, c);
	if (!*c) return false;
	c = copyIdentifier(prop.object, c);
	if (!*c) return false;
	copyIdentifier(prop.property, c);
	properties.push_back(prop);
	return true;
}


bool parse(const char* path, std::vector<Property>& properties)
{
	FILE* fp = fopen(path, "rb");
	if (!fp) return false;
	
	defer(fclose(fp));
	
	fseek(fp, 0, SEEK_END);
	int size = (int)ftell(fp);
	fseek(fp, 0, SEEK_SET);

	std::vector<char> text;
	text.resize(size + 1);

	if (fread(&text[0], 1, size, fp) != size) return false;

	text[size] = 0;

	const char* prop_str = &text[0];
	while (prop_str = strstr(prop_str, "CSHARP_PROPERTY("))
	{
		if (!addProperty(prop_str, properties)) return false;
		++prop_str;
	}
	return true;
}


void writeCSharpFooter(FILE* fp)
{
	fprintf(fp, "\t}\n\n}\n");
}


void writeCSharpHeader(FILE* fp, const char* obj)
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
		"			scene = getScene(entity._universe, \"%s\");\n"
		"		}\n"
		"\n"
		"\n";

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
}


void writeCSharpProperty(FILE* fp, const Property& prop)
{
	const char* text =
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


bool writeCSharp(const char* out_dir, std::vector<Property>& properties)
{
	auto cmp = [](const void* a, const void* b) -> int {
		auto* prop_a = (Property*)a;
		auto* prop_b = (Property*)b;
		return strcmp(prop_a->object, prop_b->object);
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
				writeCSharpFooter(fp);
				fclose(fp);
			}
			char path[256];
			strcpy_s(path, out_dir);
			strcat_s(path, prop.object);
			strcat_s(path, ".cs");
			fp = fopen(path, "wb");
			last_obj = prop.object;
			if (!fp) return false;

			writeCSharpHeader(fp, prop.object);
		}
		writeCSharpProperty(fp, prop);
	}
	writeCSharpFooter(fp);
	if (fp) fclose(fp);
	return true;
}


int main()
{
	std::vector<Property> properties;
	parse("C:\\projects\\lumixengine_csharp\\src\\csharp.cpp", properties);
	writeCSharp("C:\\projects\\lumixengine_csharp\\cs\\", properties);
}