using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class AmbientSound : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getAmbientSoundClipIndex(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAmbientSoundClipIndex(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isAmbientSound3D(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAmbientSound3D(IntPtr scene, int cmp, bool value);


		public static string GetCmpType{ get { return "ambient_sound"; } }


		public AmbientSound(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "ambient_sound");
		}

		public AmbientSound(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "ambient_sound");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "ambient_sound");
		}

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

	}//end class

}//end namespace
