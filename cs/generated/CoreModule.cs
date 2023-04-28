using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class CoreModule : IModule
	{
		public static string Type { get { return "core"; } }

		public CoreModule(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(CoreModule _value)
		{
			return _value.instance_;
		}

	}
}
