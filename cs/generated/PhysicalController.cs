using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "physical_controller")]
	public class PhysicalController : Component
	{
		public PhysicalController(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "physical_controller" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRadius(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadius(IntPtr module, int cmp, float value);


		public float Radius
		{
			get { return getRadius(module_, entity_.entity_Id_); }
			set { setRadius(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeight(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeight(IntPtr module, int cmp, float value);


		public float Height
		{
			get { return getHeight(module_, entity_.entity_Id_); }
			set { setHeight(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr module, int cmp, int value);


		public int Layer
		{
			get { return getLayer(module_, entity_.entity_Id_); }
			set { setLayer(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getUseRootMotion(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUseRootMotion(IntPtr module, int cmp, bool value);


		public bool IsUseRootMotion
		{
			get { return getUseRootMotion(module_, entity_.entity_Id_); }
			set { setUseRootMotion(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getUseCustomGravity(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUseCustomGravity(IntPtr module, int cmp, bool value);


		public bool IsUseCustomGravity
		{
			get { return getUseCustomGravity(module_, entity_.entity_Id_); }
			set { setUseCustomGravity(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCustomGravityAcceleration(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCustomGravityAcceleration(IntPtr module, int cmp, float value);


		public float CustomGravityAcceleration
		{
			get { return getCustomGravityAcceleration(module_, entity_.entity_Id_); }
			set { setCustomGravityAcceleration(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void moveController(IntPtr instance, int cmp, Vec3 a0);

		public void MoveController(Vec3 a0)
		{
			moveController(module_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isControllerCollisionDown(IntPtr instance, int cmp);

		public bool IsControllerCollisionDown()
		{
			return isControllerCollisionDown(module_, entity_.entity_Id_);
		}

	} // class
} // namespace
