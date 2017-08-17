using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

 
namespace Lumix
{ 

 
public class Engine
{
	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	public extern static void logError(string message);
}


}