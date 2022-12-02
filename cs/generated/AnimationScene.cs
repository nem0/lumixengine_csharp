using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class AnimationScene : IScene
	{
		public static string Type { get { return "animation"; } }

		public AnimationScene(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(AnimationScene _value)
		{
			return _value.instance_;
		}

	}
}
