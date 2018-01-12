#pragma once


#include "engine/iplugin.h"


namespace Lumix
{


enum { INVALID_GC_HANDLE = 0xffffFFFF };


class Path;
template <int N> struct StaticString;


void getCSharpName(const char* cmp_name, StaticString<128>& class_name);


struct CSharpPlugin : IPlugin
{
	virtual void* getDomain() const = 0;
	virtual void* getAssembly() const = 0;
	virtual void unloadAssembly() = 0;
	virtual void loadAssembly() = 0;
	virtual int getNamesCount() const = 0;
	virtual const char* getName(int idx) const = 0;
};


struct CSharpScriptScene : IScene
{
	virtual int addScript(Entity entity) = 0;
	virtual void removeScript(Entity entity, int scr_index) = 0;
	virtual void insertScript(Entity entity, int idx) = 0;
	virtual void serializeScript(Entity entity, int scr_index, OutputBlob& blob) = 0;
	virtual void deserializeScript(Entity entity, int scr_index, InputBlob& blob) = 0;
	virtual int getScriptCount(Entity entity) const = 0;
	virtual u32 getScriptNameHash(Entity entity, int scr_index) = 0;
	virtual void setScriptNameHash(Entity entity, int scr_index, u32 name_hash) = 0;
	virtual const char* getScriptName(Entity entity, int scr_index) = 0;
	virtual u32 getGCHandle(Entity entity, int scr_index) const = 0;
	virtual u32 getEntityGCHandle(Entity entity) = 0;
	virtual bool tryCallMethod(u32 gc_handle, const char* method_name, void** args, int args_count, bool try_parents) = 0;
};


} // namespace Lumix