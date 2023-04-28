using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "distance_joint")]
	public class DistanceJoint : Component
	{
		public DistanceJoint(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "distance_joint" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getAxisPosition(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAxisPosition(IntPtr module, int cmp, Vec3 value);


		public Vec3 AxisPosition
		{
			get { return getAxisPosition(module_, entity_.entity_Id_); }
			set { setAxisPosition(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDamping(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDamping(IntPtr module, int cmp, float value);


		public float Damping
		{
			get { return getDamping(module_, entity_.entity_Id_); }
			set { setDamping(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getStiffness(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setStiffness(IntPtr module, int cmp, float value);


		public float Stiffness
		{
			get { return getStiffness(module_, entity_.entity_Id_); }
			set { setStiffness(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTolerance(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTolerance(IntPtr module, int cmp, float value);


		public float Tolerance
		{
			get { return getTolerance(module_, entity_.entity_Id_); }
			set { setTolerance(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getLimits(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLimits(IntPtr module, int cmp, Vec2 value);


		public Vec2 Limits
		{
			get { return getLimits(module_, entity_.entity_Id_); }
			set { setLimits(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
