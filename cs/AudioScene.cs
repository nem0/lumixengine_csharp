using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class AudioScene : IScene
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEcho(IntPtr instance, int sound_id, float wet_dry_mix, float feedback, float left_delay, float right_delay);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr play(IntPtr instance, int entity, System.IntPtr clip, bool is_3d);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void stop(IntPtr instance, int sound_id);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setVolume(IntPtr instance, int sound_id, float volume);


		public AudioScene(IntPtr _instance)
			:base(_instance){ }

		public void SetEcho(int sound_id, float wet_dry_mix, float feedback, float left_delay, float right_delay)
		{
			setEcho(instance_, sound_id, wet_dry_mix, feedback, left_delay, right_delay);
		}

		public System.IntPtr Play(Entity entity, System.IntPtr clip, bool is_3d)
		{
			return play(instance_, entity, clip, is_3d);
		}

		public void Stop(int sound_id)
		{
			stop(instance_, sound_id);
		}

		public void SetVolume(int sound_id, float volume)
		{
			setVolume(instance_, sound_id, volume);
		}

		public static implicit operator System.IntPtr(AudioScene _value)
		{
			 return _value.instance_;
		}
	}

}
