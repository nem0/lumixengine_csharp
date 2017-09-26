using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class DistanceJoint : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDistanceJointDamping(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDistanceJointDamping(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDistanceJointStiffness(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDistanceJointStiffness(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDistanceJointTolerance(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDistanceJointTolerance(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getJointConnectedBody(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setJointConnectedBody(IntPtr scene, int cmp, Entity value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getDistanceJointLimits(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDistanceJointLimits(IntPtr scene, int cmp, Vec2 value);


		public static string GetCmpType{ get { return "distance_joint"; } }


		public DistanceJoint(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "distance_joint");
		}

		public DistanceJoint(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "distance_joint");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "distance_joint");
		}

		/// <summary>
		/// Gets or sets the Damping
		/// </summary>
		public float Damping
		{
			get { return getDistanceJointDamping(scene_, componentId_); }
			set { setDistanceJointDamping(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Stiffness
		/// </summary>
		public float Stiffness
		{
			get { return getDistanceJointStiffness(scene_, componentId_); }
			set { setDistanceJointStiffness(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Tolerance
		/// </summary>
		public float Tolerance
		{
			get { return getDistanceJointTolerance(scene_, componentId_); }
			set { setDistanceJointTolerance(scene_, componentId_, value); }
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
		/// Gets or sets the Limits
		/// </summary>
		public Vec2 Limits
		{
			get { return getDistanceJointLimits(scene_, componentId_); }
			set { setDistanceJointLimits(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
