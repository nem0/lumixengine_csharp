using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public partial class RigidActor	{
		internal IntPtr instance_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void applyForceToActor(IntPtr instance, int cmp, Vec3 force);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getActorSpeed(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void putToSleep(IntPtr instance, int cmp);


		public enum D6Motion
		{
			LOCKED,
			LIMITED,
			FREE,
		}

		public enum ActorType
		{
			BOX,
			MESH,
			CAPSULE,
			SPHERE,
		}

		public enum BoneOrientation
		{
			X,
			Y,
		}

		public enum DynamicType
		{
			STATIC,
			DYNAMIC,
			KINEMATIC,
		}

		public RigidActor(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public void ApplyForceToActor(Vec3 force)
		{
			applyForceToActor(scene_, componentId_, force);
		}

		public float ActorSpeed
		{
			get
			{
				return getActorSpeed(scene_, componentId_);
			}
		}

		public void PutToSleep()
		{
			putToSleep(scene_, componentId_);
		}

		public static implicit operator System.IntPtr(RigidActor _value)
		{
			 return _value.instance_;
		}
	}

}
