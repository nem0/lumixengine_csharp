using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class GuiScene : IScene
	{
		public static string Type { get { return "gui"; } }

		public GuiScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(GuiScene _value)
		{
			return _value.instance_;
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getRectAt(IntPtr instance, Vec2 a0);

		public int GetRectAt(Vec2 a0)
		{
			return getRectAt(instance_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isOver(IntPtr instance, Vec2 a0, int a1);

		public bool IsOver(Vec2 a0, int a1)
		{
			return isOver(instance_, a0, a1);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static IntPtr getSystem(IntPtr instance);

		public IntPtr GetSystem()
		{
			return getSystem(instance_);
		}

	}
}
