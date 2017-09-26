using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class LuaScript : NativeComponent
	{

		public static string GetCmpType{ get { return "lua_script"; } }


		public LuaScript(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "lua_script");
		}

		public LuaScript(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "lua_script");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "lua_script");
		}

	}//end class

}//end namespace
