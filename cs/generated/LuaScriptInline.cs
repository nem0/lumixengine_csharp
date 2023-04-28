using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "lua_script_inline")]
	public class LuaScriptInline : Component
	{
		public LuaScriptInline(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "lua_script_inline" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getCode(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCode(IntPtr module, int cmp, string value);


		public string Code
		{
			get { return getCode(module_, entity_.entity_Id_); }
			set { setCode(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
