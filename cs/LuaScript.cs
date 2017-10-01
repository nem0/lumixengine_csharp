using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "lua_script")]
	public class LuaScript :Component
	{

		public static string GetCmpType{ get { return "lua_script"; } }


		public LuaScript(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
