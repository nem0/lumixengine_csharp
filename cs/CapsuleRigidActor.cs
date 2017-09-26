using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class CapsuleRigidActor : NativeComponent
	{
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

		public CapsuleRigidActor(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
