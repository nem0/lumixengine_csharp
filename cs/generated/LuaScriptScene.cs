using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class LuaScriptScene : IScene
	{
		public static string Type { get { return "lua_script"; } }

		public LuaScriptScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(LuaScriptScene _value)
		{
			return _value.instance_;
		}

	}
}
