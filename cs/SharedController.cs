using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class SharedController : Component
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "shared_controller");
			scene = getScene(entity._universe, "shared_controller");
		}


		/* Parent */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setParent(IntPtr scene, int cmp, Entity source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static Entity getParent(IntPtr scene, int cmp);
		
		public Entity Parent
		{
			get{ return getParent(scene, component_id); }
			set{ setParent(scene, component_id, value); }
		}


	}

}
