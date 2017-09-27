using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class LuaScript : NativeComponent
	{

		public static string GetCmpType{ get { return "lua_script"; } }


		//public LuaScriptScene Scene
		//{
		//	 get { return new LuaScriptScene(scene_); }
		//}
		public LuaScript(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
