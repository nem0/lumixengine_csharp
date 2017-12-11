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


public class ModelResource : Resource
{
    public ModelResource(string path) 
    {
        __Instance = load(Engine.instance_, path, GetResourceType());
    }

    public override string GetResourceType() { return "model"; }
}


public class MaterialResource : Resource
{
    public MaterialResource(string path) 
    {
        __Instance = load(Engine.instance_, path, GetResourceType());
    }

    public override string GetResourceType() { return "material"; }
}


public class TextureResource : Resource
{
    public TextureResource(string path) 
    {
        __Instance = load(Engine.instance_, path, GetResourceType());
    }

    public override string GetResourceType() { return "texture"; }
}


public class AnimationResource : Resource
{
    public AnimationResource(string path) 
    {
        __Instance = load(Engine.instance_, path, GetResourceType());
    }

    public override string GetResourceType() { return "animation"; }
}


public class PhysicsResource : Resource
{
    public PhysicsResource(string path) 
    {
        __Instance = load(Engine.instance_, path, GetResourceType());
    }

    public override string GetResourceType() { return "physics"; }
}


public class ShaderResource : Resource
{
    public ShaderResource(string path) 
    {
        __Instance = load(Engine.instance_, path, GetResourceType());
    }

    public override string GetResourceType() { return "shader"; }
}


public class AnimControllerResource : Resource
{
    public AnimControllerResource(string path) 
    {
        __Instance = load(Engine.instance_, path, GetResourceType());
    }

    public override string GetResourceType() { return "anim_controller"; }
}


public class ClipResource : Resource
{
    public ClipResource(string path) 
    {
        __Instance = load(Engine.instance_, path, GetResourceType());
    }

    public override string GetResourceType() { return "clip"; }
}

}