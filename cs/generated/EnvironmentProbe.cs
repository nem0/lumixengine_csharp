using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "environment_probe")]
	public class EnvironmentProbe : Component
	{
		public EnvironmentProbe(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "environment_probe" )) { }


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
		extern static Vec3 getInnerRange(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setInnerRange(IntPtr module, int cmp, Vec3 value);


		public Vec3 InnerRange
		{
			get { return getInnerRange(module_, entity_.entity_Id_); }
			set { setInnerRange(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getOuterRange(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOuterRange(IntPtr module, int cmp, Vec3 value);


		public Vec3 OuterRange
		{
			get { return getOuterRange(module_, entity_.entity_Id_); }
			set { setOuterRange(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
