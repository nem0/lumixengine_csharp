using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "lua_script_inline")]
	public class LuaScriptInline : Component
	{
		public LuaScriptInline(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "lua_script_inline" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getCode(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCode(IntPtr scene, int cmp, string value);


		public string Code
		{
			get { return getCode(scene_, entity_.entity_Id_); }
			set { setCode(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
