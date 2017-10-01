using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "spherical_joint")]
	public class SphericalJoint :Component
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
		extern static bool getSphericalJointUseLimit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSphericalJointUseLimit(IntPtr scene, int cmp, bool value);


		public static string GetCmpType{ get { return "spherical_joint"; } }


		public PhysicsScene Scene
		{
			 get { return new PhysicsScene(scene_); }
		}
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
		/// Gets or sets the UseLimit
		/// </summary>
		public bool IsUseLimit
		{
			get { return getSphericalJointUseLimit(scene_, componentId_); }
			set { setSphericalJointUseLimit(scene_, componentId_, value); }
		}

		public SphericalJoint(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
