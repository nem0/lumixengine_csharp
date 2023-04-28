using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "reflection_probe")]
	public class ReflectionProbe : Component
	{
		public ReflectionProbe(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "reflection_probe" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabled(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabled(IntPtr module, int cmp, bool value);


		public bool IsEnabled
		{
			get { return getEnabled(module_, entity_.entity_Id_); }
			set { setEnabled(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getSize(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSize(IntPtr module, int cmp, uint value);


		public uint Size
		{
			get { return getSize(module_, entity_.entity_Id_); }
			set { setSize(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getHalfExtents(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHalfExtents(IntPtr module, int cmp, Vec3 value);


		public Vec3 HalfExtents
		{
			get { return getHalfExtents(module_, entity_.entity_Id_); }
			set { setHalfExtents(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
