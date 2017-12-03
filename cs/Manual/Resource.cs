using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Lumix
{
    

public abstract class Resource
{
    public System.IntPtr __Instance;
    public abstract string GetResourceType();

    [MethodImplAttribute(MethodImplOptions.InternalCall)]
    public extern static string getPath(IntPtr resource);

    [MethodImplAttribute(MethodImplOptions.InternalCall)]
    public extern static IntPtr load(IntPtr engine, string path, string type);

    public string GetPath()
    {
        return getPath(__Instance);
    }
}


public class PrefabResource : Resource
{
    public PrefabResource(string path) 
    {
        __Instance = load(Engine.instance_, path, GetResourceType());
    }

    public override string GetResourceType() { return "prefab"; }
}


}