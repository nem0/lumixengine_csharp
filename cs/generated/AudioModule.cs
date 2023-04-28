using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class AudioModule : IModule
	{
		public static string Type { get { return "audio"; } }

		public AudioModule(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(AudioModule _value)
		{
			return _value.instance_;
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMasterVolume(IntPtr instance, float a0);

		public void SetMasterVolume(float a0)
		{
			 setMasterVolume(instance_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int play(IntPtr instance, int a0, string a1, bool a2);

		public int Play(int a0, string a1, bool a2)
		{
			return play(instance_, a0, a1, a2);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void stop(IntPtr instance, int a0);

		public void Stop(int a0)
		{
			 stop(instance_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isEnd(IntPtr instance, int a0);

		public bool IsEnd(int a0)
		{
			return isEnd(instance_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFrequency(IntPtr instance, int a0, uint a1);

		public void SetFrequency(int a0, uint a1)
		{
			 setFrequency(instance_, a0, a1);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setVolume(IntPtr instance, int a0, float a1);

		public void SetVolume(int a0, float a1)
		{
			 setVolume(instance_, a0, a1);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEcho(IntPtr instance, int a0, float a1, float a2, float a3, float a4);

		public void SetEcho(int a0, float a1, float a2, float a3, float a4)
		{
			 setEcho(instance_, a0, a1, a2, a3, a4);
		}

	}
}
