using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class PhysicsModule : IModule
	{
		public static string Type { get { return "physics"; } }

		public PhysicsModule(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(PhysicsModule _value)
		{
			return _value.instance_;
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int raycast(IntPtr instance, Vec3 a0, Vec3 a1, int a2);

		public int Raycast(Vec3 a0, Vec3 a1, int a2)
		{
			return raycast(instance_, a0, a1, a2);
		}

	}
}
