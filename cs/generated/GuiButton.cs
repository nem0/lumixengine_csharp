using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_button")]
	public class GuiButton : Component
	{
		public GuiButton(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "gui_button" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getHoveredColor(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHoveredColor(IntPtr module, int cmp, Vec4 value);


		public Vec4 HoveredColor
		{
			get { return getHoveredColor(module_, entity_.entity_Id_); }
			set { setHoveredColor(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getCursor(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCursor(IntPtr module, int cmp, int value);


		public int Cursor
		{
			get { return getCursor(module_, entity_.entity_Id_); }
			set { setCursor(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
