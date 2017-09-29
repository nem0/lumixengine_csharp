using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public partial class Engine	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getResourceManager(IntPtr instance);


		public Engine(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public ResourceManager GetResourceManager()
		{
			return new ResourceManager(getResourceManager(instance_));
		}

	}

}
