using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class CoreScene : IScene
	{
		public static string Type { get { return "core"; } }

		public CoreScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(CoreScene _value)
		{
			return _value.instance_;
		}

	}
}
