using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_render_target")]
	public class GuiRenderTarget : Component
	{
		public GuiRenderTarget(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "gui_render_target" )) { }


	} // class
} // namespace
