using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

 
namespace Lumix
{ 

 
public partial class Universe
{
	private IntPtr _universe;
	
	public Universe(IntPtr ptr)
	{
		_universe = ptr;
	}
}


}