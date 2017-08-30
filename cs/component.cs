using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

 
namespace Lumix
{ 

 
public class Component
{
	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	protected extern static int create(IntPtr universe, int entity, string cmp_type);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	protected extern static IntPtr getScene(IntPtr universe, string cmp_type);
	
	public Entity entity;
	
	public virtual void create() {}
}


public class NativeComponent : Component
{
	
}


}