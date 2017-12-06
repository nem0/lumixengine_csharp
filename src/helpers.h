#pragma once


#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/debug-helpers.h>
#include <mono/metadata/mono-config.h>
#include <mono/metadata/mono-debug.h>
#include <mono/metadata/tokentype.h>
#include <mono/utils/mono-logger.h>


namespace Lumix
{


bool inherits(MonoClass* mono_class, const char* base);


struct MonoStringHolder
{
	MonoStringHolder(MonoString* mono_string) : str(mono_string_to_utf8(mono_string)) {}
	MonoStringHolder(char* ptr) : str(ptr) {}
	MonoStringHolder(MonoStringHolder&& rhs) { str = rhs.str; rhs.str = nullptr; }
	~MonoStringHolder() { mono_free(str); }
	explicit operator const char*() const { return str; }
	bool isValid() const { return str != nullptr; }
	char* str;

	MonoStringHolder(const MonoStringHolder& rhs) = delete;
	void operator =(const MonoStringHolder& rhs) = delete;
};


} // namespace Lumix