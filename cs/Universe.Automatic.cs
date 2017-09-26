using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public partial class Universe
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getEntityByName(IntPtr instance, string name);


		internal Universe(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public Entity GetEntityByName(string name)
		{
			return new Entity(instance_, getEntityByName(instance_, name));
		}

	}

}
