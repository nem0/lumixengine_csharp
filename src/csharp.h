#pragma once


#include "engine/plugin.h"
#include "engine/hash_map.h"
#include "engine/string.h"


namespace Lumix
{


enum { INVALID_GC_HANDLE = 0xffffFFFF };


struct Path;
template <int N> struct StaticString;


void getCSharpName(const char* cmp_name, StaticString<128>& class_name);


struct CSharpSystem : ISystem
{
	virtual void* getDomain() const = 0;
	virtual void* getAssembly() const = 0;
	virtual void unloadAssembly() = 0;
	virtual void loadAssembly() = 0;
	virtual const HashMap<RuntimeHash, String>& getNames() const = 0;
	virtual const Array<String>& getNamesArray() const = 0;
};


struct CSharpScriptModule : IModule
{
	virtual int addScript(EntityRef entity, int scr_index) = 0;
	virtual void removeScript(EntityRef entity, int scr_index) = 0;
	virtual void insertScript(EntityRef entity, int idx) = 0;
	virtual void serializeScript(EntityRef entity, int scr_index, OutputMemoryStream& blob) = 0;
	virtual void deserializeScript(EntityRef entity, int scr_index, InputMemoryStream& blob) = 0;
	virtual int getScriptCount(EntityRef entity) const = 0;
	virtual const char* getScriptName(EntityRef entity, int scr_index) = 0;
	virtual void setScriptName(EntityRef entity, int scr_index, const char* name) = 0;
	virtual u32 getGCHandle(EntityRef entity, int scr_index) const = 0;
	virtual u32 getEntityGCHandle(EntityRef entity) = 0;
	virtual bool tryCallMethod(u32 gc_handle, const char* method_name, void** args, int args_count, bool try_parents) = 0;
};


} // namespace Lumix