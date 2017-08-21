using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

 
namespace Lumix
{ 

 
public class Animable : Component
{
	private int component_id;
	private IntPtr scene;
	
	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static void setSource(IntPtr scene, int cmp, string source);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static string getSource(IntPtr scene, int cmp);
	
	
	public override void create()
	{
		component_id = create(entity._universe, entity._entity_id, "animable");
		scene = getScene(entity._universe, "animable");
	}
	
	
	public string source
	{
		get { return getSource(scene, component_id); }
		set { setSource(scene, component_id, value); }
	}
}


}