using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public static class InputSystem
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isMouseDown(int button);


		public static bool IsMouseDown(int button)
		{
			return isMouseDown(button);
		}

	}

}
