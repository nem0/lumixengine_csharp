using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_image")]
	public class GuiImage : Component
	{
		public GuiImage(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "gui_image" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabled(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabled(IntPtr module, int cmp, bool value);


		public bool IsEnabled
		{
			get { return getEnabled(module_, entity_.entity_Id_); }
			set { setEnabled(module_, entity_.entity_Id_, value); }
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

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSprite(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSprite(IntPtr module, int cmp, string value);


		public string Sprite
		{
			get { return getSprite(module_, entity_.entity_Id_); }
			set { setSprite(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
