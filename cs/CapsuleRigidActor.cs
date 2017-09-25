using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
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

}//end namespace
