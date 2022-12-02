using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "js_script")]
	public class JsScript : Component
	{
		public JsScript(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "js_script" )) { }


	} // class
} // namespace
