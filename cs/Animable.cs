using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class Animable : NativeComponent
	{
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


		public AnimationScene Scene
		{
			 get { return new AnimationScene(scene_); }
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

		public Animable(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
