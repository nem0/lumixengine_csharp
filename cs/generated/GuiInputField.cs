using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_input_field")]
	public class GuiInputField : Component
	{
		public GuiInputField(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "gui_input_field" )) { }


	} // class
} // namespace
