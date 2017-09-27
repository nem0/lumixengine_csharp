using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class NavmeshAgent : NativeComponent
	{
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

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void cancelNavigation(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool navigate(IntPtr instance, int cmp, Vec3 dest, float speed, float stop_distance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAgentSpeed(IntPtr instance, int cmp);



		public static string GetCmpType{ get { return "navmesh_agent"; } }


		public NavigationScene Scene
		{
			 get { return new NavigationScene(scene_); }
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

		public NavmeshAgent(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

		public void CancelNavigation()
		{
			cancelNavigation(scene_, componentId_);
		}

		public bool Navigate(Vec3 dest, float speed, float stop_distance)
		{
			return navigate(scene_, componentId_, dest, speed, stop_distance);
		}

		public float GetAgentSpeed()
		{
			return getAgentSpeed(scene_, componentId_);
		}

	}//end class

}//end namespace
