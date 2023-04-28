using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_canvas")]
	public class GuiCanvas : Component
	{
		public GuiCanvas(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "gui_canvas" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getIs3D(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIs3D(IntPtr module, int cmp, bool value);


		public bool IsIs3D
		{
			get { return getIs3D(module_, entity_.entity_Id_); }
			set { setIs3D(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getOrientToCamera(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOrientToCamera(IntPtr module, int cmp, bool value);


		public bool IsOrientToCamera
		{
			get { return getOrientToCamera(module_, entity_.entity_Id_); }
			set { setOrientToCamera(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getVirtualSize(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setVirtualSize(IntPtr module, int cmp, Vec2 value);


		public Vec2 VirtualSize
		{
			get { return getVirtualSize(module_, entity_.entity_Id_); }
			set { setVirtualSize(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
