using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class RendererModule : IModule
	{
		public static string Type { get { return "renderer"; } }

		public RendererModule(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(RendererModule _value)
		{
			return _value.instance_;
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCross(IntPtr instance, DVec3 a0, float a1, Vec4 a2);

		public void AddDebugCross(DVec3 a0, float a1, Vec4 a2)
		{
			 addDebugCross(instance_, a0, a1, a2);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugLine(IntPtr instance, DVec3 a0, DVec3 a1, Vec4 a2);

		public void AddDebugLine(DVec3 a0, DVec3 a1, Vec4 a2)
		{
			 addDebugLine(instance_, a0, a1, a2);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugTriangle(IntPtr instance, DVec3 a0, DVec3 a1, DVec3 a2, Vec4 a3);

		public void AddDebugTriangle(DVec3 a0, DVec3 a1, DVec3 a2, Vec4 a3)
		{
			 addDebugTriangle(instance_, a0, a1, a2, a3);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setActiveCamera(IntPtr instance, int a0);

		public void SetActiveCamera(int a0)
		{
			 setActiveCamera(instance_, a0);
		}

	}
}
