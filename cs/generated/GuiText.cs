using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_text")]
	public class GuiText : Component
	{
		public GuiText(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "gui_text" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getText(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setText(IntPtr scene, int cmp, string value);


		public string Text
		{
			get { return getText(scene_, entity_.entity_Id_); }
			set { setText(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getFont(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFont(IntPtr scene, int cmp, string value);


		public string Font
		{
			get { return getFont(scene_, entity_.entity_Id_); }
			set { setFont(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getFontSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFontSize(IntPtr scene, int cmp, int value);


		public int FontSize
		{
			get { return getFontSize(scene_, entity_.entity_Id_); }
			set { setFontSize(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getHorizontalAlign(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHorizontalAlign(IntPtr scene, int cmp, int value);


		public int HorizontalAlign
		{
			get { return getHorizontalAlign(scene_, entity_.entity_Id_); }
			set { setHorizontalAlign(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getVerticalAlign(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setVerticalAlign(IntPtr scene, int cmp, int value);


		public int VerticalAlign
		{
			get { return getVerticalAlign(scene_, entity_.entity_Id_); }
			set { setVerticalAlign(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setColor(IntPtr scene, int cmp, Vec4 value);


		public Vec4 Color
		{
			get { return getColor(scene_, entity_.entity_Id_); }
			set { setColor(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
