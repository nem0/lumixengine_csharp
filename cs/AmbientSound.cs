using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class AmbientSound : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getAmbientSoundClipIndex(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAmbientSoundClipIndex(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isAmbientSound3D(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAmbientSound3D(IntPtr scene, int cmp, bool value);


		public static string GetCmpType{ get { return "ambient_sound"; } }


		/// <summary>
		/// Gets or sets the Sound
		/// </summary>
		public int Sound
		{
			get { return getAmbientSoundClipIndex(scene_, componentId_); }
			set { setAmbientSoundClipIndex(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the 3D
		/// </summary>
		public bool Is3D
		{
			get { return isAmbientSound3D(scene_, componentId_); }
			set { setAmbientSound3D(scene_, componentId_, value); }
		}

		public AmbientSound(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
