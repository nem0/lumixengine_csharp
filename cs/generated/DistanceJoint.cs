using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "distance_joint")]
	public class DistanceJoint : Component
	{
		public DistanceJoint(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "distance_joint" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getAxisPosition(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAxisPosition(IntPtr scene, int cmp, Vec3 value);


		public Vec3 AxisPosition
		{
			get { return getAxisPosition(scene_, entity_.entity_Id_); }
			set { setAxisPosition(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDamping(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDamping(IntPtr scene, int cmp, float value);


		public float Damping
		{
			get { return getDamping(scene_, entity_.entity_Id_); }
			set { setDamping(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getStiffness(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setStiffness(IntPtr scene, int cmp, float value);


		public float Stiffness
		{
			get { return getStiffness(scene_, entity_.entity_Id_); }
			set { setStiffness(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTolerance(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTolerance(IntPtr scene, int cmp, float value);


		public float Tolerance
		{
			get { return getTolerance(scene_, entity_.entity_Id_); }
			set { setTolerance(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getLimits(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLimits(IntPtr scene, int cmp, Vec2 value);


		public Vec2 Limits
		{
			get { return getLimits(scene_, entity_.entity_Id_); }
			set { setLimits(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
