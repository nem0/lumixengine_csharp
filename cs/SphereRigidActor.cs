using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class SphereRigidActor : NativeComponent
	{
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

		public SphereRigidActor(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
