using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class D6Joint : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getJointConnectedBody(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setJointConnectedBody(IntPtr scene, int cmp, Entity value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getJointAxisPosition(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setJointAxisPosition(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getJointAxisDirection(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setJointAxisDirection(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getD6JointXMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setD6JointXMotion(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getD6JointYMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setD6JointYMotion(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getD6JointZMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setD6JointZMotion(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getD6JointSwing1Motion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setD6JointSwing1Motion(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getD6JointSwing2Motion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setD6JointSwing2Motion(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getD6JointTwistMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setD6JointTwistMotion(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getD6JointLinearLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setD6JointLinearLimit(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "d6_joint"; } }


		/// <summary>
		/// Gets or sets the ConnectedBody
		/// </summary>
		public Entity ConnectedBody
		{
			get { return getJointConnectedBody(scene_, componentId_); }
			set { setJointConnectedBody(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the AxisPosition
		/// </summary>
		public Vec3 AxisPosition
		{
			get { return getJointAxisPosition(scene_, componentId_); }
			set { setJointAxisPosition(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the AxisDirection
		/// </summary>
		public Vec3 AxisDirection
		{
			get { return getJointAxisDirection(scene_, componentId_); }
			set { setJointAxisDirection(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the XMotion
		/// </summary>
		public int XMotion
		{
			get { return getD6JointXMotion(scene_, componentId_); }
			set { setD6JointXMotion(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the YMotion
		/// </summary>
		public int YMotion
		{
			get { return getD6JointYMotion(scene_, componentId_); }
			set { setD6JointYMotion(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the ZMotion
		/// </summary>
		public int ZMotion
		{
			get { return getD6JointZMotion(scene_, componentId_); }
			set { setD6JointZMotion(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Swing1
		/// </summary>
		public int Swing1
		{
			get { return getD6JointSwing1Motion(scene_, componentId_); }
			set { setD6JointSwing1Motion(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Swing2
		/// </summary>
		public int Swing2
		{
			get { return getD6JointSwing2Motion(scene_, componentId_); }
			set { setD6JointSwing2Motion(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Twist
		/// </summary>
		public int Twist
		{
			get { return getD6JointTwistMotion(scene_, componentId_); }
			set { setD6JointTwistMotion(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the LinearLimit
		/// </summary>
		public float LinearLimit
		{
			get { return getD6JointLinearLimit(scene_, componentId_); }
			set { setD6JointLinearLimit(scene_, componentId_, value); }
		}

		public D6Joint(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
