using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class CsharpScriptScene : IScene
	{
		public static string Type { get { return "csharp_script"; } }

		public CsharpScriptScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(CsharpScriptScene _value)
		{
			return _value.instance_;
		}

	}
}
