using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "ambient_sound")]
	public class AmbientSound : Component
	{
		public AmbientSound(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "ambient_sound" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool get3D(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void set3D(IntPtr module, int cmp, bool value);


		public bool Is3D
		{
			get { return get3D(module_, entity_.entity_Id_); }
			set { set3D(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSound(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSound(IntPtr module, int cmp, string value);


		public string Sound
		{
			get { return getSound(module_, entity_.entity_Id_); }
			set { setSound(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void pauseAmbientSound(IntPtr instance, int cmp);

		public void PauseAmbientSound()
		{
			pauseAmbientSound(module_, entity_.entity_Id_);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void resumeAmbientSound(IntPtr instance, int cmp);

		public void ResumeAmbientSound()
		{
			resumeAmbientSound(module_, entity_.entity_Id_);
		}

	} // class
} // namespace
