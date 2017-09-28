using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public partial class Engine
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getResourceManager();


		public static System.IntPtr GetResourceManager()
		{
			return getResourceManager();
		}

	}

}
