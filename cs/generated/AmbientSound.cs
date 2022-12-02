using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "ambient_sound")]
	public class AmbientSound : Component
	{
		public AmbientSound(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "ambient_sound" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool get3D(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void set3D(IntPtr scene, int cmp, bool value);


		public bool Is3D
		{
			get { return get3D(scene_, entity_.entity_Id_); }
			set { set3D(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSound(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSound(IntPtr scene, int cmp, string value);


		public string Sound
		{
			get { return getSound(scene_, entity_.entity_Id_); }
			set { setSound(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
