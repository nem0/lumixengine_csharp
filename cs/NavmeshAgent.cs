using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
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

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void cancelNavigation(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool navigate(IntPtr instance, int cmp, Vec3 dest, float speed, float stop_distance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAgentSpeed(IntPtr instance, int cmp);



		public static string GetCmpType{ get { return "navmesh_agent"; } }


		public NavmeshAgent(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "navmesh_agent");
		}

		public NavmeshAgent(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "navmesh_agent");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "navmesh_agent");
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
