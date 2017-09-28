using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class AnimationScene
	{
		IntPtr instance_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getAnimableAnimation(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getAnimation(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimation(IntPtr instance, int cmp, System.IntPtr path);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAnimableTime(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimableTime(IntPtr instance, int cmp, float time);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void updateAnimable(IntPtr instance, int cmp, float time_delta);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void updateController(IntPtr instance, int cmp, float time_delta);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getControllerEntity(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAnimableTimeScale(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimableTimeScale(IntPtr instance, int cmp, float time_scale);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAnimableStartTime(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimableStartTime(IntPtr instance, int cmp, float time);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getControllerInput(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerInput(IntPtr instance, int cmp, int input_idx, int value);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerInput(IntPtr instance, int cmp, int input_idx, float value);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerInput(IntPtr instance, int cmp, int input_idx, bool value);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getControllerRootMotion(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerSource(IntPtr instance, int cmp, System.IntPtr path);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getControllerSource(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getControllerRoot(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getControllerInputIndex(IntPtr instance, int cmp, string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getSharedControllerParent(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSharedControllerParent(IntPtr instance, int cmp, int parent);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void applyControllerSet(IntPtr instance, int cmp, string set_name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerDefaultSet(IntPtr instance, int cmp, int set);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getControllerDefaultSet(IntPtr instance, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getControllerResource(IntPtr instance, int cmp);


		internal AnimationScene(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public System.IntPtr GetAnimableAnimation(int cmp)
		{
			return getAnimableAnimation(instance_, cmp);
		}

		public System.IntPtr GetAnimation(int cmp)
		{
			return getAnimation(instance_, cmp);
		}

		public void SetAnimation(int cmp, System.IntPtr path)
		{
			setAnimation(instance_, cmp, path);
		}

		public float GetAnimableTime(int cmp)
		{
			return getAnimableTime(instance_, cmp);
		}

		public void SetAnimableTime(int cmp, float time)
		{
			setAnimableTime(instance_, cmp, time);
		}

		public void UpdateAnimable(int cmp, float time_delta)
		{
			updateAnimable(instance_, cmp, time_delta);
		}

		public void UpdateController(int cmp, float time_delta)
		{
			updateController(instance_, cmp, time_delta);
		}

		public Entity GetControllerEntity(int cmp)
		{
			int x = getControllerEntity(instance_, cmp);
			 if(x < 0) return null;
			return new Entity(instance_, x);
		}

		public float GetAnimableTimeScale(int cmp)
		{
			return getAnimableTimeScale(instance_, cmp);
		}

		public void SetAnimableTimeScale(int cmp, float time_scale)
		{
			setAnimableTimeScale(instance_, cmp, time_scale);
		}

		public float GetAnimableStartTime(int cmp)
		{
			return getAnimableStartTime(instance_, cmp);
		}

		public void SetAnimableStartTime(int cmp, float time)
		{
			setAnimableStartTime(instance_, cmp, time);
		}

		public System.IntPtr GetControllerInput(int cmp)
		{
			return getControllerInput(instance_, cmp);
		}

		public void SetControllerInput(int cmp, int input_idx, int value)
		{
			setControllerInput(instance_, cmp, input_idx, value);
		}

		public void SetControllerInput(int cmp, int input_idx, float value)
		{
			setControllerInput(instance_, cmp, input_idx, value);
		}

		public void SetControllerInput(int cmp, int input_idx, bool value)
		{
			setControllerInput(instance_, cmp, input_idx, value);
		}

		public System.IntPtr GetControllerRootMotion(int cmp)
		{
			return getControllerRootMotion(instance_, cmp);
		}

		public void SetControllerSource(int cmp, System.IntPtr path)
		{
			setControllerSource(instance_, cmp, path);
		}

		public System.IntPtr GetControllerSource(int cmp)
		{
			return getControllerSource(instance_, cmp);
		}

		public System.IntPtr GetControllerRoot(int cmp)
		{
			return getControllerRoot(instance_, cmp);
		}

		public int GetControllerInputIndex(int cmp, string name)
		{
			return getControllerInputIndex(instance_, cmp, name);
		}

		public Entity GetSharedControllerParent(int cmp)
		{
			int x = getSharedControllerParent(instance_, cmp);
			 if(x < 0) return null;
			return new Entity(instance_, x);
		}

		public void SetSharedControllerParent(int cmp, Entity parent)
		{
			setSharedControllerParent(instance_, cmp, parent);
		}

		public void ApplyControllerSet(int cmp, string set_name)
		{
			applyControllerSet(instance_, cmp, set_name);
		}

		public void SetControllerDefaultSet(int cmp, int set)
		{
			setControllerDefaultSet(instance_, cmp, set);
		}

		public int GetControllerDefaultSet(int cmp)
		{
			return getControllerDefaultSet(instance_, cmp);
		}

		public System.IntPtr GetControllerResource(int cmp)
		{
			return getControllerResource(instance_, cmp);
		}

	}

}
