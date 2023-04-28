using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class LuaScriptModule : IModule
	{
		public static string Type { get { return "lua_script"; } }

		public LuaScriptModule(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(LuaScriptModule _value)
		{
			return _value.instance_;
		}

	}
}
