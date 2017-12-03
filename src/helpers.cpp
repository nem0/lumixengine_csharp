#include "helpers.h"
#include "engine/string.h"


namespace Lumix
{


bool inherits(MonoClass* mono_class, const char* base)
{
	MonoClass* parent = mono_class_get_parent(mono_class);
	while (parent)
	{
		const char* n = mono_class_get_name(parent);
		if (equalIStrings(n, base)) return true;
		parent = mono_class_get_parent(parent);
	}
	return false;
}


} // namespace Lumix