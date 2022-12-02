using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_image")]
	public class GuiImage : Component
	{
		public GuiImage(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "gui_image" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabled(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabled(IntPtr scene, int cmp, bool value);


		public bool IsEnabled
		{
			get { return getEnabled(scene_, entity_.entity_Id_); }
			set { setEnabled(scene_, entity_.entity_Id_, value); }
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

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSprite(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSprite(IntPtr scene, int cmp, string value);


		public string Sprite
		{
			get { return getSprite(scene_, entity_.entity_Id_); }
			set { setSprite(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
