using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_canvas")]
	public class GuiCanvas : Component
	{
		public GuiCanvas(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "gui_canvas" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getIs3D(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIs3D(IntPtr scene, int cmp, bool value);


		public bool IsIs3D
		{
			get { return getIs3D(scene_, entity_.entity_Id_); }
			set { setIs3D(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getOrientToCamera(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOrientToCamera(IntPtr scene, int cmp, bool value);


		public bool IsOrientToCamera
		{
			get { return getOrientToCamera(scene_, entity_.entity_Id_); }
			set { setOrientToCamera(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getVirtualSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setVirtualSize(IntPtr scene, int cmp, Vec2 value);


		public Vec2 VirtualSize
		{
			get { return getVirtualSize(scene_, entity_.entity_Id_); }
			set { setVirtualSize(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
