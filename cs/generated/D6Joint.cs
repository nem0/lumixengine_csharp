using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "d6_joint")]
	public class D6Joint : Component
	{
		public D6Joint(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "d6_joint" )) { }


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
		extern static Vec3 getAxisDirection(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAxisDirection(IntPtr module, int cmp, Vec3 value);


		public Vec3 AxisDirection
		{
			get { return getAxisDirection(module_, entity_.entity_Id_); }
			set { setAxisDirection(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getXMotion(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setXMotion(IntPtr module, int cmp, int value);


		public int XMotion
		{
			get { return getXMotion(module_, entity_.entity_Id_); }
			set { setXMotion(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getYMotion(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setYMotion(IntPtr module, int cmp, int value);


		public int YMotion
		{
			get { return getYMotion(module_, entity_.entity_Id_); }
			set { setYMotion(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getZMotion(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setZMotion(IntPtr module, int cmp, int value);


		public int ZMotion
		{
			get { return getZMotion(module_, entity_.entity_Id_); }
			set { setZMotion(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getSwing1(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSwing1(IntPtr module, int cmp, int value);


		public int Swing1
		{
			get { return getSwing1(module_, entity_.entity_Id_); }
			set { setSwing1(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getSwing2(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSwing2(IntPtr module, int cmp, int value);


		public int Swing2
		{
			get { return getSwing2(module_, entity_.entity_Id_); }
			set { setSwing2(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getTwist(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTwist(IntPtr module, int cmp, int value);


		public int Twist
		{
			get { return getTwist(module_, entity_.entity_Id_); }
			set { setTwist(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLinearLimit(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLinearLimit(IntPtr module, int cmp, float value);


		public float LinearLimit
		{
			get { return getLinearLimit(module_, entity_.entity_Id_); }
			set { setLinearLimit(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getSwingLimit(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSwingLimit(IntPtr module, int cmp, Vec2 value);


		public Vec2 SwingLimit
		{
			get { return getSwingLimit(module_, entity_.entity_Id_); }
			set { setSwingLimit(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getTwistLimit(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTwistLimit(IntPtr module, int cmp, Vec2 value);


		public Vec2 TwistLimit
		{
			get { return getTwistLimit(module_, entity_.entity_Id_); }
			set { setTwistLimit(module_, entity_.entity_Id_, value); }
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
		extern static float getRestitution(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRestitution(IntPtr module, int cmp, float value);


		public float Restitution
		{
			get { return getRestitution(module_, entity_.entity_Id_); }
			set { setRestitution(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
