using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class NavigationScene
	{
		IntPtr instance_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isNavmeshReady(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getUniverse(IntPtr instance);


		internal NavigationScene(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public bool IsNavmeshReady()
		{
			return isNavmeshReady(instance_);
		}

		public static implicit operator System.IntPtr(NavigationScene _value)
		{
			 return _value.instance_;
		}
	}

}
