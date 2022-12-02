using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "reflection_probe")]
	public class ReflectionProbe : Component
	{
		public ReflectionProbe(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "reflection_probe" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabled(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabled(IntPtr scene, int cmp, bool value);


		public bool IsEnabled
		{
			get { return getEnabled(scene_, entity_.entity_Id_); }
			set { setEnabled(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSize(IntPtr scene, int cmp, uint value);


		public uint Size
		{
			get { return getSize(scene_, entity_.entity_Id_); }
			set { setSize(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getHalfExtents(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHalfExtents(IntPtr scene, int cmp, Vec3 value);


		public Vec3 HalfExtents
		{
			get { return getHalfExtents(scene_, entity_.entity_Id_); }
			set { setHalfExtents(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
