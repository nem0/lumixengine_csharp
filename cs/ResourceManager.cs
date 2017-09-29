using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ResourceManager
	{
		internal IntPtr instance_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr get(IntPtr instance, uint type);


		public ResourceManager(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public ResourceManagerBase Get(uint type)
		{
			return new ResourceManagerBase(get(instance_, type));
		}

		public static implicit operator System.IntPtr(ResourceManager _value)
		{
			 return _value.instance_;
		}
	}

}
