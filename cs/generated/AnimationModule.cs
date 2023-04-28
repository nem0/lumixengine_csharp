using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public unsafe partial class AnimationModule : IModule
	{
		public static string Type { get { return "animation"; } }

		public AnimationModule(IntPtr _instance)
			: base(_instance) { }

		public static implicit operator System.IntPtr(AnimationModule _value)
		{
			return _value.instance_;
		}

	}
}
