using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_button")]
	public class GuiButton : Component
	{
		public GuiButton(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "gui_button" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getHoveredColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHoveredColor(IntPtr scene, int cmp, Vec4 value);


		public Vec4 HoveredColor
		{
			get { return getHoveredColor(scene_, entity_.entity_Id_); }
			set { setHoveredColor(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getCursor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCursor(IntPtr scene, int cmp, int value);


		public int Cursor
		{
			get { return getCursor(scene_, entity_.entity_Id_); }
			set { setCursor(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
