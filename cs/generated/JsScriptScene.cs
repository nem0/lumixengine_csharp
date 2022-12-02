using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class JsScriptScene : IScene
	{
		public static string Type { get { return "js_script"; } }

		public JsScriptScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(JsScriptScene _value)
		{
			return _value.instance_;
		}

	}
}
