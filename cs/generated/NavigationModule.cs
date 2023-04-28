using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class NavigationModule : IModule
	{
		public static string Type { get { return "navigation"; } }

		public NavigationModule(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(NavigationModule _value)
		{
			return _value.instance_;
		}

	}
}
