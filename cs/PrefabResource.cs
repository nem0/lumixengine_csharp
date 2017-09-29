using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class PrefabResource : Resource,IResourceType
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void unload(IntPtr instance);


		public string ResourceType
		{
			 get { return "prefab"; }
		}

		public PrefabResource(IntPtr _instance)
			:base(_instance){ }

		public void Unload()
		{
			unload(instance_);
		}

		public static implicit operator System.IntPtr(PrefabResource _value)
		{
			 return _value.instance_;
		}
	}

}
