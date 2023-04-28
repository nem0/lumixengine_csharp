using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class CsharpScriptModule : IModule
	{
		public static string Type { get { return "csharp_script"; } }

		public CsharpScriptModule(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(CsharpScriptModule _value)
		{
			return _value.instance_;
		}

	}
}
