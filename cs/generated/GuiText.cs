using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_text")]
	public class GuiText : Component
	{
		public GuiText(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "gui_text" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getText(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setText(IntPtr module, int cmp, string value);


		public string Text
		{
			get { return getText(module_, entity_.entity_Id_); }
			set { setText(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getFont(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFont(IntPtr module, int cmp, string value);


		public string Font
		{
			get { return getFont(module_, entity_.entity_Id_); }
			set { setFont(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getFontSize(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFontSize(IntPtr module, int cmp, int value);


		public int FontSize
		{
			get { return getFontSize(module_, entity_.entity_Id_); }
			set { setFontSize(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getHorizontalAlign(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHorizontalAlign(IntPtr module, int cmp, int value);


		public int HorizontalAlign
		{
			get { return getHorizontalAlign(module_, entity_.entity_Id_); }
			set { setHorizontalAlign(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getVerticalAlign(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setVerticalAlign(IntPtr module, int cmp, int value);


		public int VerticalAlign
		{
			get { return getVerticalAlign(module_, entity_.entity_Id_); }
			set { setVerticalAlign(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getColor(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setColor(IntPtr module, int cmp, Vec4 value);


		public Vec4 Color
		{
			get { return getColor(module_, entity_.entity_Id_); }
			set { setColor(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
