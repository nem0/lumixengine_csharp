using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class LuaScript : NativeComponent
	{
		int componentId_;
		IntPtr scene_;


		public static string GetCmpType{ get { return "lua_script"; } }


		public LuaScript(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "lua_script");
		}

		public LuaScript(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "lua_script");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "lua_script");
		}

	}//end class

}//end namespace
