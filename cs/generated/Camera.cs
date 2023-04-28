using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "camera")]
	public class Camera : Component
	{
		public Camera(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "camera" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFOV(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFOV(IntPtr module, int cmp, float value);


		public float FOV
		{
			get { return getFOV(module_, entity_.entity_Id_); }
			set { setFOV(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getNear(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setNear(IntPtr module, int cmp, float value);


		public float Near
		{
			get { return getNear(module_, entity_.entity_Id_); }
			set { setNear(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFar(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFar(IntPtr module, int cmp, float value);


		public float Far
		{
			get { return getFar(module_, entity_.entity_Id_); }
			set { setFar(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getOrthographic(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOrthographic(IntPtr module, int cmp, bool value);


		public bool IsOrthographic
		{
			get { return getOrthographic(module_, entity_.entity_Id_); }
			set { setOrthographic(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getOrthographicSize(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOrthographicSize(IntPtr module, int cmp, float value);


		public float OrthographicSize
		{
			get { return getOrthographicSize(module_, entity_.entity_Id_); }
			set { setOrthographicSize(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
