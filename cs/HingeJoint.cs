using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class HingeJoint : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getJointConnectedBody(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setJointConnectedBody(IntPtr scene, int cmp, Entity value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHingeJointDamping(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHingeJointDamping(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHingeJointStiffness(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHingeJointStiffness(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getJointAxisPosition(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setJointAxisPosition(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getJointAxisDirection(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setJointAxisDirection(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getHingeJointUseLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHingeJointUseLimit(IntPtr scene, int cmp, bool value);


		public static string GetCmpType{ get { return "hinge_joint"; } }


		/// <summary>
		/// Gets or sets the ConnectedBody
		/// </summary>
		public Entity ConnectedBody
		{
			get { return getJointConnectedBody(scene_, componentId_); }
			set { setJointConnectedBody(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Damping
		/// </summary>
		public float Damping
		{
			get { return getHingeJointDamping(scene_, componentId_); }
			set { setHingeJointDamping(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Stiffness
		/// </summary>
		public float Stiffness
		{
			get { return getHingeJointStiffness(scene_, componentId_); }
			set { setHingeJointStiffness(scene_, componentId_, value); }
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
		/// Gets or sets the UseLimit
		/// </summary>
		public bool IsUseLimit
		{
			get { return getHingeJointUseLimit(scene_, componentId_); }
			set { setHingeJointUseLimit(scene_, componentId_, value); }
		}

		public HingeJoint(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
