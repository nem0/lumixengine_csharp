using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "d6_joint")]
	public class D6Joint : Component
	{
		public D6Joint(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "d6_joint" )) { }


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
		extern static Vec3 getAxisDirection(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAxisDirection(IntPtr scene, int cmp, Vec3 value);


		public Vec3 AxisDirection
		{
			get { return getAxisDirection(scene_, entity_.entity_Id_); }
			set { setAxisDirection(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getXMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setXMotion(IntPtr scene, int cmp, int value);


		public int XMotion
		{
			get { return getXMotion(scene_, entity_.entity_Id_); }
			set { setXMotion(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getYMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setYMotion(IntPtr scene, int cmp, int value);


		public int YMotion
		{
			get { return getYMotion(scene_, entity_.entity_Id_); }
			set { setYMotion(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getZMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setZMotion(IntPtr scene, int cmp, int value);


		public int ZMotion
		{
			get { return getZMotion(scene_, entity_.entity_Id_); }
			set { setZMotion(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getSwing1(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSwing1(IntPtr scene, int cmp, int value);


		public int Swing1
		{
			get { return getSwing1(scene_, entity_.entity_Id_); }
			set { setSwing1(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getSwing2(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSwing2(IntPtr scene, int cmp, int value);


		public int Swing2
		{
			get { return getSwing2(scene_, entity_.entity_Id_); }
			set { setSwing2(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getTwist(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTwist(IntPtr scene, int cmp, int value);


		public int Twist
		{
			get { return getTwist(scene_, entity_.entity_Id_); }
			set { setTwist(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLinearLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLinearLimit(IntPtr scene, int cmp, float value);


		public float LinearLimit
		{
			get { return getLinearLimit(scene_, entity_.entity_Id_); }
			set { setLinearLimit(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getSwingLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSwingLimit(IntPtr scene, int cmp, Vec2 value);


		public Vec2 SwingLimit
		{
			get { return getSwingLimit(scene_, entity_.entity_Id_); }
			set { setSwingLimit(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getTwistLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTwistLimit(IntPtr scene, int cmp, Vec2 value);


		public Vec2 TwistLimit
		{
			get { return getTwistLimit(scene_, entity_.entity_Id_); }
			set { setTwistLimit(scene_, entity_.entity_Id_, value); }
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
		extern static float getRestitution(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRestitution(IntPtr scene, int cmp, float value);


		public float Restitution
		{
			get { return getRestitution(scene_, entity_.entity_Id_); }
			set { setRestitution(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
