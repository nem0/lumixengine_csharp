using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "camera")]
	public class Camera : Component
	{
		public Camera(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "camera" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFOV(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFOV(IntPtr scene, int cmp, float value);


		public float FOV
		{
			get { return getFOV(scene_, entity_.entity_Id_); }
			set { setFOV(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getNear(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setNear(IntPtr scene, int cmp, float value);


		public float Near
		{
			get { return getNear(scene_, entity_.entity_Id_); }
			set { setNear(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFar(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFar(IntPtr scene, int cmp, float value);


		public float Far
		{
			get { return getFar(scene_, entity_.entity_Id_); }
			set { setFar(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getOrthographic(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOrthographic(IntPtr scene, int cmp, bool value);


		public bool IsOrthographic
		{
			get { return getOrthographic(scene_, entity_.entity_Id_); }
			set { setOrthographic(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getOrthographicSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOrthographicSize(IntPtr scene, int cmp, float value);


		public float OrthographicSize
		{
			get { return getOrthographicSize(scene_, entity_.entity_Id_); }
			set { setOrthographicSize(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
