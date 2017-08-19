using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

 
namespace Lumix
{ 

 
public class Entity
{
	public int native;
	public IntPtr universe;
	
	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void native_setPosition(IntPtr universe, int entity, float x, float y, float z);
	
	public void setPosition(float x, float y, float z)
	{
		native_setPosition(universe, native, x, y, z);
	}
}


}