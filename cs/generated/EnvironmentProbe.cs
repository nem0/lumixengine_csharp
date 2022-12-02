using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "environment_probe")]
	public class EnvironmentProbe : Component
	{
		public EnvironmentProbe(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "environment_probe" )) { }


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
		extern static Vec3 getInnerRange(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setInnerRange(IntPtr scene, int cmp, Vec3 value);


		public Vec3 InnerRange
		{
			get { return getInnerRange(scene_, entity_.entity_Id_); }
			set { setInnerRange(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getOuterRange(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setOuterRange(IntPtr scene, int cmp, Vec3 value);


		public Vec3 OuterRange
		{
			get { return getOuterRange(scene_, entity_.entity_Id_); }
			set { setOuterRange(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
