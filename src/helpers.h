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


} // namespace Lumix