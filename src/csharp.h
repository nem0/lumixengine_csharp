#pragma once


#include "engine/iplugin.h"


namespace Lumix
{


enum { INVALID_GC_HANDLE = 0xffffFFFF };


class Path;


struct CSharpPlugin : IPlugin
{
	virtual void unloadAssembly() = 0;
	virtual void loadAssembly() = 0;
	virtual int getNamesCount() const = 0;
	virtual const char* getName(int idx) const = 0;
};


struct CSharpScriptScene : IScene
{
	virtual int addScript(ComponentHandle cmp) = 0;
	virtual void removeScript(ComponentHandle cmp, int scr_index) = 0;
	virtual void insertScript(ComponentHandle cmp, int idx) = 0;
	virtual void serializeScript(ComponentHandle cmp, int scr_index, OutputBlob& blob) = 0;
	virtual void deserializeScript(ComponentHandle cmp, int scr_index, InputBlob& blob) = 0;
	virtual int getScriptCount(ComponentHandle cmp) const = 0;
	virtual u32 getScriptNameHash(ComponentHandle cmp, int scr_index) = 0;
	virtual void setScriptNameHash(ComponentHandle cmp, int scr_index, u32 name_hash) = 0;
	virtual const char* getScriptName(ComponentHandle cmp, int scr_index) = 0;
	virtual u32 getGCHandle(ComponentHandle cmp, int scr_index) const = 0;
};


} // namespace Lumix