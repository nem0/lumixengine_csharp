using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "physical_controller")]
	public class PhysicalController : Component
	{
		public PhysicalController(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "physical_controller" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadius(IntPtr scene, int cmp, float value);


		public float Radius
		{
			get { return getRadius(scene_, entity_.entity_Id_); }
			set { setRadius(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeight(IntPtr scene, int cmp, float value);


		public float Height
		{
			get { return getHeight(scene_, entity_.entity_Id_); }
			set { setHeight(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr scene, int cmp, int value);


		public int Layer
		{
			get { return getLayer(scene_, entity_.entity_Id_); }
			set { setLayer(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getUseRootMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUseRootMotion(IntPtr scene, int cmp, bool value);


		public bool IsUseRootMotion
		{
			get { return getUseRootMotion(scene_, entity_.entity_Id_); }
			set { setUseRootMotion(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getUseCustomGravity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUseCustomGravity(IntPtr scene, int cmp, bool value);


		public bool IsUseCustomGravity
		{
			get { return getUseCustomGravity(scene_, entity_.entity_Id_); }
			set { setUseCustomGravity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCustomGravityAcceleration(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCustomGravityAcceleration(IntPtr scene, int cmp, float value);


		public float CustomGravityAcceleration
		{
			get { return getCustomGravityAcceleration(scene_, entity_.entity_Id_); }
			set { setCustomGravityAcceleration(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
