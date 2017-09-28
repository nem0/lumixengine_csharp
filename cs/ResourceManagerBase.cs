using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ResourceManagerBase
	{
		IntPtr instance_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr get(IntPtr instance, string path);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void enableUnload(IntPtr instance, bool enable);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr load(IntPtr instance, string path);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void load(IntPtr instance, System.IntPtr resource);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void removeUnreferenced(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void unload(IntPtr instance, string path);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void unload(IntPtr instance, System.IntPtr resource);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void reload(IntPtr instance, string path);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void reload(IntPtr instance, System.IntPtr resource);


		internal ResourceManagerBase(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public Resource Get(string path)
		{
			return new Resource(get(instance_, path));
		}

		public void EnableUnload(bool enable)
		{
			enableUnload(instance_, enable);
		}

		public Resource Load(string path)
		{
			return new Resource(load(instance_, path));
		}

		public void Load(Resource resource)
		{
			load(instance_, resource);
		}

		public void RemoveUnreferenced()
		{
			removeUnreferenced(instance_);
		}

		public void Unload(string path)
		{
			unload(instance_, path);
		}

		public void Unload(Resource resource)
		{
			unload(instance_, resource);
		}

		public void Reload(string path)
		{
			reload(instance_, path);
		}

		public void Reload(Resource resource)
		{
			reload(instance_, resource);
		}

		public static implicit operator System.IntPtr(ResourceManagerBase _value)
		{
			 return _value.instance_;
		}
	}

}
