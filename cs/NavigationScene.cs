using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class NavigationScene : IScene
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isNavmeshReady(IntPtr instance);


		public NavigationScene(IntPtr _instance)
			:base(_instance){ }

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
