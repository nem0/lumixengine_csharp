using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class AnimController : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getControllerSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerSource(IntPtr scene, int cmp, string value);

		public static string GetCmpType{ get { return "anim_controller"; } }


		public AnimController(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "anim_controller");
		}

		public AnimController(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "anim_controller");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "anim_controller");
		}

		/// <summary>
		/// Gets or sets the Source
		/// </summary>
		public string Source
		{
			get { return getControllerSource(scene_, componentId_); }
			set { setControllerSource(scene_, componentId_, value); }
		}

	}//end class

	public class Animable : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getAnimation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimation(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAnimableStartTime(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimableStartTime(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAnimableTimeScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimableTimeScale(IntPtr scene, int cmp, float value);

		public static string GetCmpType{ get { return "animable"; } }


		public Animable(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "animable");
		}

		public Animable(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "animable");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "animable");
		}

		/// <summary>
		/// Gets or sets the Animation
		/// </summary>
		public string Animation
		{
			get { return getAnimation(scene_, componentId_); }
			set { setAnimation(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the StartTime
		/// </summary>
		public float StartTime
		{
			get { return getAnimableStartTime(scene_, componentId_); }
			set { setAnimableStartTime(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the TimeScale
		/// </summary>
		public float TimeScale
		{
			get { return getAnimableTimeScale(scene_, componentId_); }
			set { setAnimableTimeScale(scene_, componentId_, value); }
		}

	}//end class

	public class SharedAnimController : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getSharedControllerParent(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSharedControllerParent(IntPtr scene, int cmp, Entity value);

		public static string GetCmpType{ get { return "shared_anim_controller"; } }


		public SharedAnimController(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "shared_anim_controller");
		}

		public SharedAnimController(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "shared_anim_controller");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "shared_anim_controller");
		}

		/// <summary>
		/// Gets or sets the Parent
		/// </summary>
		public Entity Parent
		{
			get { return getSharedControllerParent(scene_, componentId_); }
			set { setSharedControllerParent(scene_, componentId_, value); }
		}

	}//end class

	public class AmbientSound : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getAmbientSoundClipIndex(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAmbientSoundClipIndex(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isAmbientSound3D(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAmbientSound3D(IntPtr scene, int cmp, bool value);

		public static string GetCmpType{ get { return "ambient_sound"; } }


		public AmbientSound(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "ambient_sound");
		}

		public AmbientSound(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "ambient_sound");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "ambient_sound");
		}

		/// <summary>
		/// Gets or sets the Sound
		/// </summary>
		public int Sound
		{
			get { return getAmbientSoundClipIndex(scene_, componentId_); }
			set { setAmbientSoundClipIndex(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the 3D
		/// </summary>
		public bool Is3D
		{
			get { return isAmbientSound3D(scene_, componentId_); }
			set { setAmbientSound3D(scene_, componentId_, value); }
		}

	}//end class

	public class EchoZone : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getEchoZoneRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEchoZoneRadius(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getEchoZoneDelay(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEchoZoneDelay(IntPtr scene, int cmp, float value);

		public static string GetCmpType{ get { return "echo_zone"; } }


		public EchoZone(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "echo_zone");
		}

		public EchoZone(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "echo_zone");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "echo_zone");
		}

		/// <summary>
		/// Gets or sets the Radius
		/// </summary>
		public float Radius
		{
			get { return getEchoZoneRadius(scene_, componentId_); }
			set { setEchoZoneRadius(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Delay(ms)
		/// </summary>
		public float Delayms
		{
			get { return getEchoZoneDelay(scene_, componentId_); }
			set { setEchoZoneDelay(scene_, componentId_, value); }
		}

	}//end class

	public class LuaScript : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		public static string GetCmpType{ get { return "lua_script"; } }


		public LuaScript(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "lua_script");
		}

		public LuaScript(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "lua_script");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "lua_script");
		}

	}//end class

	public class NavmeshAgent : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAgentRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAgentRadius(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAgentHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAgentHeight(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool useAgentRootMotion(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUseAgentRootMotion(IntPtr scene, int cmp, bool value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isGettingRootMotionFromAnim(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIsGettingRootMotionFromAnim(IntPtr scene, int cmp, bool value);

		public static string GetCmpType{ get { return "navmesh_agent"; } }


		public NavmeshAgent(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "navmesh_agent");
		}

		public NavmeshAgent(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "navmesh_agent");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "navmesh_agent");
		}

		/// <summary>
		/// Gets or sets the Radius
		/// </summary>
		public float Radius
		{
			get { return getAgentRadius(scene_, componentId_); }
			set { setAgentRadius(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Height
		/// </summary>
		public float Height
		{
			get { return getAgentHeight(scene_, componentId_); }
			set { setAgentHeight(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the UseRootMotion
		/// </summary>
		public bool IsUseRootMotion
		{
			get { return useAgentRootMotion(scene_, componentId_); }
			set { setUseAgentRootMotion(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the RootMotionFromAnim
		/// </summary>
		public bool IsRootMotionFromAnim
		{
			get { return isGettingRootMotionFromAnim(scene_, componentId_); }
			set { setIsGettingRootMotionFromAnim(scene_, componentId_, value); }
		}

	}//end class

	public class Ragdoll : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getRagdollLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRagdollLayer(IntPtr scene, int cmp, int value);

		public static string GetCmpType{ get { return "ragdoll"; } }


		public Ragdoll(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "ragdoll");
		}

		public Ragdoll(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "ragdoll");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "ragdoll");
		}

		/// <summary>
		/// Gets or sets the Layer
		/// </summary>
		public int Layer
		{
			get { return getRagdollLayer(scene_, componentId_); }
			set { setRagdollLayer(scene_, componentId_, value); }
		}

	}//end class

	public class SphereRigidActor : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSphereRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSphereRadius(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getDynamicType(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDynamicType(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isTrigger(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIsTrigger(IntPtr scene, int cmp, bool value);

		public static string GetCmpType{ get { return "sphere_rigid_actor"; } }


		public SphereRigidActor(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "sphere_rigid_actor");
		}

		public SphereRigidActor(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "sphere_rigid_actor");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "sphere_rigid_actor");
		}

		/// <summary>
		/// Gets or sets the Radius
		/// </summary>
		public float Radius
		{
			get { return getSphereRadius(scene_, componentId_); }
			set { setSphereRadius(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Dynamic
		/// </summary>
		public int Dynamic
		{
			get { return getDynamicType(scene_, componentId_); }
			set { setDynamicType(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Trigger
		/// </summary>
		public bool IsTrigger
		{
			get { return isTrigger(scene_, componentId_); }
			set { setIsTrigger(scene_, componentId_, value); }
		}

	}//end class

	public class CapsuleRigidActor : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCapsuleRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCapsuleRadius(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCapsuleHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCapsuleHeight(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getDynamicType(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDynamicType(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isTrigger(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIsTrigger(IntPtr scene, int cmp, bool value);

		public static string GetCmpType{ get { return "capsule_rigid_actor"; } }


		public CapsuleRigidActor(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "capsule_rigid_actor");
		}

		public CapsuleRigidActor(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "capsule_rigid_actor");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "capsule_rigid_actor");
		}

		/// <summary>
		/// Gets or sets the Radius
		/// </summary>
		public float Radius
		{
			get { return getCapsuleRadius(scene_, componentId_); }
			set { setCapsuleRadius(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Height
		/// </summary>
		public float Height
		{
			get { return getCapsuleHeight(scene_, componentId_); }
			set { setCapsuleHeight(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Dynamic
		/// </summary>
		public int Dynamic
		{
			get { return getDynamicType(scene_, componentId_); }
			set { setDynamicType(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Trigger
		/// </summary>
		public bool IsTrigger
		{
			get { return isTrigger(scene_, componentId_); }
			set { setIsTrigger(scene_, componentId_, value); }
		}

	}//end class

	public class D6Joint : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

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


		public D6Joint(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "d6_joint");
		}

		public D6Joint(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "d6_joint");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "d6_joint");
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

	}//end class

	public class SphericalJoint : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

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


		public SphericalJoint(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "spherical_joint");
		}

		public SphericalJoint(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "spherical_joint");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "spherical_joint");
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

	}//end class

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
			scene_ = getScene(entity_._universe, "distance_joint");
		}

		public DistanceJoint(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "distance_joint");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "distance_joint");
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

	public class HingeJoint : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

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


		public HingeJoint(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "hinge_joint");
		}

		public HingeJoint(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "hinge_joint");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "hinge_joint");
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

	}//end class

	public class PhysicalController : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getControllerLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerLayer(IntPtr scene, int cmp, int value);

		public static string GetCmpType{ get { return "physical_controller"; } }


		public PhysicalController(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "physical_controller");
		}

		public PhysicalController(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "physical_controller");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "physical_controller");
		}

		/// <summary>
		/// Gets or sets the Layer
		/// </summary>
		public int Layer
		{
			get { return getControllerLayer(scene_, componentId_); }
			set { setControllerLayer(scene_, componentId_, value); }
		}

	}//end class

	public class BoxRigidActor : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getDynamicType(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDynamicType(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isTrigger(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIsTrigger(IntPtr scene, int cmp, bool value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getHalfExtents(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHalfExtents(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getActorLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setActorLayer(IntPtr scene, int cmp, int value);

		public static string GetCmpType{ get { return "box_rigid_actor"; } }


		public BoxRigidActor(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "box_rigid_actor");
		}

		public BoxRigidActor(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "box_rigid_actor");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "box_rigid_actor");
		}

		/// <summary>
		/// Gets or sets the Dynamic
		/// </summary>
		public int Dynamic
		{
			get { return getDynamicType(scene_, componentId_); }
			set { setDynamicType(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Trigger
		/// </summary>
		public bool IsTrigger
		{
			get { return isTrigger(scene_, componentId_); }
			set { setIsTrigger(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Size
		/// </summary>
		public Vec3 Size
		{
			get { return getHalfExtents(scene_, componentId_); }
			set { setHalfExtents(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Layer
		/// </summary>
		public int Layer
		{
			get { return getActorLayer(scene_, componentId_); }
			set { setActorLayer(scene_, componentId_, value); }
		}

	}//end class

	public class MeshRigidActor : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getShapeSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setShapeSource(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getDynamicType(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDynamicType(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getActorLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setActorLayer(IntPtr scene, int cmp, int value);

		public static string GetCmpType{ get { return "mesh_rigid_actor"; } }


		public MeshRigidActor(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "mesh_rigid_actor");
		}

		public MeshRigidActor(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "mesh_rigid_actor");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "mesh_rigid_actor");
		}

		/// <summary>
		/// Gets or sets the Source
		/// </summary>
		public string Source
		{
			get { return getShapeSource(scene_, componentId_); }
			set { setShapeSource(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Dynamic
		/// </summary>
		public int Dynamic
		{
			get { return getDynamicType(scene_, componentId_); }
			set { setDynamicType(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Layer
		/// </summary>
		public int Layer
		{
			get { return getActorLayer(scene_, componentId_); }
			set { setActorLayer(scene_, componentId_, value); }
		}

	}//end class

	public class PhysicalHeightfield : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getHeightmapSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightmapSource(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeightmapXZScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightmapXZScale(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeightmapYScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightmapYScale(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getHeightfieldLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightfieldLayer(IntPtr scene, int cmp, int value);

		public static string GetCmpType{ get { return "physical_heightfield"; } }


		public PhysicalHeightfield(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "physical_heightfield");
		}

		public PhysicalHeightfield(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "physical_heightfield");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "physical_heightfield");
		}

		/// <summary>
		/// Gets or sets the Heightmap
		/// </summary>
		public string Heightmap
		{
			get { return getHeightmapSource(scene_, componentId_); }
			set { setHeightmapSource(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the XZScale
		/// </summary>
		public float XZScale
		{
			get { return getHeightmapXZScale(scene_, componentId_); }
			set { setHeightmapXZScale(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the YScale
		/// </summary>
		public float YScale
		{
			get { return getHeightmapYScale(scene_, componentId_); }
			set { setHeightmapYScale(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Layer
		/// </summary>
		public int Layer
		{
			get { return getHeightfieldLayer(scene_, componentId_); }
			set { setHeightfieldLayer(scene_, componentId_, value); }
		}

	}//end class

	public class BoneAttachment : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getBoneAttachmentParent(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBoneAttachmentParent(IntPtr scene, int cmp, Entity value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getBoneAttachmentPosition(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBoneAttachmentPosition(IntPtr scene, int cmp, Vec3 value);

		public static string GetCmpType{ get { return "bone_attachment"; } }


		public BoneAttachment(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "bone_attachment");
		}

		public BoneAttachment(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "bone_attachment");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "bone_attachment");
		}

		/// <summary>
		/// Gets or sets the Parent
		/// </summary>
		public Entity Parent
		{
			get { return getBoneAttachmentParent(scene_, componentId_); }
			set { setBoneAttachmentParent(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the RelativePosition
		/// </summary>
		public Vec3 RelativePosition
		{
			get { return getBoneAttachmentPosition(scene_, componentId_); }
			set { setBoneAttachmentPosition(scene_, componentId_, value); }
		}

	}//end class

	public class ParticleEmitterSpawnShape : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getParticleEmitterShapeRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterShapeRadius(IntPtr scene, int cmp, float value);

		public static string GetCmpType{ get { return "particle_emitter_spawn_shape"; } }


		public ParticleEmitterSpawnShape(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_spawn_shape");
		}

		public ParticleEmitterSpawnShape(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_spawn_shape");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_spawn_shape");
		}

		/// <summary>
		/// Gets or sets the Radius
		/// </summary>
		public float Radius
		{
			get { return getParticleEmitterShapeRadius(scene_, componentId_); }
			set { setParticleEmitterShapeRadius(scene_, componentId_, value); }
		}

	}//end class

	public class ParticleEmitterPlane : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getParticleEmitterPlaneBounce(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterPlaneBounce(IntPtr scene, int cmp, float value);

		public static string GetCmpType{ get { return "particle_emitter_plane"; } }


		public ParticleEmitterPlane(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_plane");
		}

		public ParticleEmitterPlane(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_plane");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_plane");
		}

		/// <summary>
		/// Gets or sets the Bounce
		/// </summary>
		public float Bounce
		{
			get { return getParticleEmitterPlaneBounce(scene_, componentId_); }
			set { setParticleEmitterPlaneBounce(scene_, componentId_, value); }
		}

	}//end class

	public class ParticleEmitterAttractor : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getParticleEmitterAttractorForce(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterAttractorForce(IntPtr scene, int cmp, float value);

		public static string GetCmpType{ get { return "particle_emitter_attractor"; } }


		public ParticleEmitterAttractor(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_attractor");
		}

		public ParticleEmitterAttractor(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_attractor");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_attractor");
		}

		/// <summary>
		/// Gets or sets the Force
		/// </summary>
		public float Force
		{
			get { return getParticleEmitterAttractorForce(scene_, componentId_); }
			set { setParticleEmitterAttractorForce(scene_, componentId_, value); }
		}

	}//end class

	public class ParticleEmitterAlpha : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		public static string GetCmpType{ get { return "particle_emitter_alpha"; } }


		public ParticleEmitterAlpha(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_alpha");
		}

		public ParticleEmitterAlpha(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_alpha");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_alpha");
		}

	}//end class

	public class ParticleEmitterForce : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getParticleEmitterAcceleration(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterAcceleration(IntPtr scene, int cmp, Vec3 value);

		public static string GetCmpType{ get { return "particle_emitter_force"; } }


		public ParticleEmitterForce(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_force");
		}

		public ParticleEmitterForce(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_force");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_force");
		}

		/// <summary>
		/// Gets or sets the Acceleration
		/// </summary>
		public Vec3 Acceleration
		{
			get { return getParticleEmitterAcceleration(scene_, componentId_); }
			set { setParticleEmitterAcceleration(scene_, componentId_, value); }
		}

	}//end class

	public class ParticleEmitterSubimage : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getParticleEmitterSubimageRows(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterSubimageRows(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getParticleEmitterSubimageCols(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterSubimageCols(IntPtr scene, int cmp, int value);

		public static string GetCmpType{ get { return "particle_emitter_subimage"; } }


		public ParticleEmitterSubimage(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_subimage");
		}

		public ParticleEmitterSubimage(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_subimage");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_subimage");
		}

		/// <summary>
		/// Gets or sets the Rows
		/// </summary>
		public int Rows
		{
			get { return getParticleEmitterSubimageRows(scene_, componentId_); }
			set { setParticleEmitterSubimageRows(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Columns
		/// </summary>
		public int Columns
		{
			get { return getParticleEmitterSubimageCols(scene_, componentId_); }
			set { setParticleEmitterSubimageCols(scene_, componentId_, value); }
		}

	}//end class

	public class ParticleEmitterSize : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		public static string GetCmpType{ get { return "particle_emitter_size"; } }


		public ParticleEmitterSize(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_size");
		}

		public ParticleEmitterSize(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_size");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_size");
		}

	}//end class

	public class ParticleEmitterLinearMovement : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getParticleEmitterLinearMovementX(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterLinearMovementX(IntPtr scene, int cmp, Vec2 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getParticleEmitterLinearMovementY(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterLinearMovementY(IntPtr scene, int cmp, Vec2 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getParticleEmitterLinearMovementZ(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterLinearMovementZ(IntPtr scene, int cmp, Vec2 value);

		public static string GetCmpType{ get { return "particle_emitter_linear_movement"; } }


		public ParticleEmitterLinearMovement(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_linear_movement");
		}

		public ParticleEmitterLinearMovement(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_linear_movement");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_linear_movement");
		}

		/// <summary>
		/// Gets or sets the X
		/// </summary>
		public Vec2 X
		{
			get { return getParticleEmitterLinearMovementX(scene_, componentId_); }
			set { setParticleEmitterLinearMovementX(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Y
		/// </summary>
		public Vec2 Y
		{
			get { return getParticleEmitterLinearMovementY(scene_, componentId_); }
			set { setParticleEmitterLinearMovementY(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Z
		/// </summary>
		public Vec2 Z
		{
			get { return getParticleEmitterLinearMovementZ(scene_, componentId_); }
			set { setParticleEmitterLinearMovementZ(scene_, componentId_, value); }
		}

	}//end class

	public class ParticleEmitter : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getParticleEmitterInitialLife(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterInitialLife(IntPtr scene, int cmp, Vec2 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getParticleEmitterInitialSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterInitialSize(IntPtr scene, int cmp, Vec2 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getParticleEmitterSpawnPeriod(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterSpawnPeriod(IntPtr scene, int cmp, Vec2 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Int2 getParticleEmitterSpawnCount(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterSpawnCount(IntPtr scene, int cmp, Int2 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getParticleEmitterAutoemit(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterAutoemit(IntPtr scene, int cmp, bool value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getParticleEmitterLocalSpace(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterLocalSpace(IntPtr scene, int cmp, bool value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getParticleEmitterMaterialPath(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterMaterialPath(IntPtr scene, int cmp, string value);

		public static string GetCmpType{ get { return "particle_emitter"; } }


		public ParticleEmitter(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter");
		}

		public ParticleEmitter(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter");
		}

		/// <summary>
		/// Gets or sets the Life
		/// </summary>
		public Vec2 Life
		{
			get { return getParticleEmitterInitialLife(scene_, componentId_); }
			set { setParticleEmitterInitialLife(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the InitialSize
		/// </summary>
		public Vec2 InitialSize
		{
			get { return getParticleEmitterInitialSize(scene_, componentId_); }
			set { setParticleEmitterInitialSize(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the SpawnPeriod
		/// </summary>
		public Vec2 SpawnPeriod
		{
			get { return getParticleEmitterSpawnPeriod(scene_, componentId_); }
			set { setParticleEmitterSpawnPeriod(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the SpawnCount
		/// </summary>
		public Int2 SpawnCount
		{
			get { return getParticleEmitterSpawnCount(scene_, componentId_); }
			set { setParticleEmitterSpawnCount(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Autoemit
		/// </summary>
		public bool IsAutoemit
		{
			get { return getParticleEmitterAutoemit(scene_, componentId_); }
			set { setParticleEmitterAutoemit(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the LocalSpace
		/// </summary>
		public bool IsLocalSpace
		{
			get { return getParticleEmitterLocalSpace(scene_, componentId_); }
			set { setParticleEmitterLocalSpace(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Material
		/// </summary>
		public string Material
		{
			get { return getParticleEmitterMaterialPath(scene_, componentId_); }
			set { setParticleEmitterMaterialPath(scene_, componentId_, value); }
		}

	}//end class

	public class Camera : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getCameraSlot(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCameraSlot(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCameraOrthoSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCameraOrthoSize(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isCameraOrtho(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCameraOrtho(IntPtr scene, int cmp, bool value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCameraNearPlane(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCameraNearPlane(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCameraFarPlane(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCameraFarPlane(IntPtr scene, int cmp, float value);

		public static string GetCmpType{ get { return "camera"; } }


		public Camera(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "camera");
		}

		public Camera(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "camera");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "camera");
		}

		/// <summary>
		/// Gets or sets the Slot
		/// </summary>
		public string Slot
		{
			get { return getCameraSlot(scene_, componentId_); }
			set { setCameraSlot(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the OrthographicSize
		/// </summary>
		public float OrthographicSize
		{
			get { return getCameraOrthoSize(scene_, componentId_); }
			set { setCameraOrthoSize(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Orthographic
		/// </summary>
		public bool IsOrthographic
		{
			get { return isCameraOrtho(scene_, componentId_); }
			set { setCameraOrtho(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Near
		/// </summary>
		public float Near
		{
			get { return getCameraNearPlane(scene_, componentId_); }
			set { setCameraNearPlane(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Far
		/// </summary>
		public float Far
		{
			get { return getCameraFarPlane(scene_, componentId_); }
			set { setCameraFarPlane(scene_, componentId_, value); }
		}

	}//end class

	public class ModelInstance : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getModelInstancePath(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setModelInstancePath(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getModelInstanceKeepSkin(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setModelInstanceKeepSkin(IntPtr scene, int cmp, bool value);

		public static string GetCmpType{ get { return "renderable"; } }


		public ModelInstance(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "renderable");
		}

		public ModelInstance(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "renderable");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "renderable");
		}

		/// <summary>
		/// Gets or sets the Source
		/// </summary>
		public string Source
		{
			get { return getModelInstancePath(scene_, componentId_); }
			set { setModelInstancePath(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the KeepSkin
		/// </summary>
		public bool IsKeepSkin
		{
			get { return getModelInstanceKeepSkin(scene_, componentId_); }
			set { setModelInstanceKeepSkin(scene_, componentId_, value); }
		}

	}//end class

	public class GlobalLight : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getGlobalLightColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGlobalLightColor(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getGlobalLightIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGlobalLightIntensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getGlobalLightIndirectIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGlobalLightIndirectIntensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getShadowmapCascades(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setShadowmapCascades(IntPtr scene, int cmp, Vec4 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFogDensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogDensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFogBottom(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogBottom(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFogHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogHeight(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getFogColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogColor(IntPtr scene, int cmp, Vec3 value);

		public static string GetCmpType{ get { return "global_light"; } }


		public GlobalLight(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "global_light");
		}

		public GlobalLight(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "global_light");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "global_light");
		}

		/// <summary>
		/// Gets or sets the Color
		/// </summary>
		public Vec3 Color
		{
			get { return getGlobalLightColor(scene_, componentId_); }
			set { setGlobalLightColor(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Intensity
		/// </summary>
		public float Intensity
		{
			get { return getGlobalLightIntensity(scene_, componentId_); }
			set { setGlobalLightIntensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the IndirectIntensity
		/// </summary>
		public float IndirectIntensity
		{
			get { return getGlobalLightIndirectIntensity(scene_, componentId_); }
			set { setGlobalLightIndirectIntensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the ShadowCascades
		/// </summary>
		public Vec4 ShadowCascades
		{
			get { return getShadowmapCascades(scene_, componentId_); }
			set { setShadowmapCascades(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the FogDensity
		/// </summary>
		public float FogDensity
		{
			get { return getFogDensity(scene_, componentId_); }
			set { setFogDensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the FogBottom
		/// </summary>
		public float FogBottom
		{
			get { return getFogBottom(scene_, componentId_); }
			set { setFogBottom(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the FogHeight
		/// </summary>
		public float FogHeight
		{
			get { return getFogHeight(scene_, componentId_); }
			set { setFogHeight(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the FogColor
		/// </summary>
		public Vec3 FogColor
		{
			get { return getFogColor(scene_, componentId_); }
			set { setFogColor(scene_, componentId_, value); }
		}

	}//end class

	public class PointLight : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getPointLightColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPointLightColor(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getPointLightSpecularColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPointLightSpecularColor(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getPointLightIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPointLightIntensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getPointLightSpecularIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPointLightSpecularIntensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLightAttenuation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLightAttenuation(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLightRange(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLightRange(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getLightCastShadows(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLightCastShadows(IntPtr scene, int cmp, bool value);

		public static string GetCmpType{ get { return "point_light"; } }


		public PointLight(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "point_light");
		}

		public PointLight(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "point_light");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "point_light");
		}

		/// <summary>
		/// Gets or sets the DiffuseColor
		/// </summary>
		public Vec3 DiffuseColor
		{
			get { return getPointLightColor(scene_, componentId_); }
			set { setPointLightColor(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the SpecularColor
		/// </summary>
		public Vec3 SpecularColor
		{
			get { return getPointLightSpecularColor(scene_, componentId_); }
			set { setPointLightSpecularColor(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the DiffuseIntensity
		/// </summary>
		public float DiffuseIntensity
		{
			get { return getPointLightIntensity(scene_, componentId_); }
			set { setPointLightIntensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the SpecularIntensity
		/// </summary>
		public float SpecularIntensity
		{
			get { return getPointLightSpecularIntensity(scene_, componentId_); }
			set { setPointLightSpecularIntensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Attenuation
		/// </summary>
		public float Attenuation
		{
			get { return getLightAttenuation(scene_, componentId_); }
			set { setLightAttenuation(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Range
		/// </summary>
		public float Range
		{
			get { return getLightRange(scene_, componentId_); }
			set { setLightRange(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the CastShadows
		/// </summary>
		public bool IsCastShadows
		{
			get { return getLightCastShadows(scene_, componentId_); }
			set { setLightCastShadows(scene_, componentId_, value); }
		}

	}//end class

	public class Decal : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getDecalMaterialPath(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDecalMaterialPath(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getDecalScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDecalScale(IntPtr scene, int cmp, Vec3 value);

		public static string GetCmpType{ get { return "decal"; } }


		public Decal(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "decal");
		}

		public Decal(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "decal");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "decal");
		}

		/// <summary>
		/// Gets or sets the Material
		/// </summary>
		public string Material
		{
			get { return getDecalMaterialPath(scene_, componentId_); }
			set { setDecalMaterialPath(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Scale
		/// </summary>
		public Vec3 Scale
		{
			get { return getDecalScale(scene_, componentId_); }
			set { setDecalScale(scene_, componentId_, value); }
		}

	}//end class

	public class Terrain : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getTerrainMaterialPath(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTerrainMaterialPath(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTerrainXZScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTerrainXZScale(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTerrainYScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTerrainYScale(IntPtr scene, int cmp, float value);

		public static string GetCmpType{ get { return "terrain"; } }


		public Terrain(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "terrain");
		}

		public Terrain(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "terrain");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "terrain");
		}

		/// <summary>
		/// Gets or sets the Material
		/// </summary>
		public string Material
		{
			get { return getTerrainMaterialPath(scene_, componentId_); }
			set { setTerrainMaterialPath(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the XZScale
		/// </summary>
		public float XZScale
		{
			get { return getTerrainXZScale(scene_, componentId_); }
			set { setTerrainXZScale(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the HeightScale
		/// </summary>
		public float HeightScale
		{
			get { return getTerrainYScale(scene_, componentId_); }
			set { setTerrainYScale(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
