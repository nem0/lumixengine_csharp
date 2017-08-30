using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Controller : Component
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "controller");
			scene = getScene(entity._universe, "controller");
		}


		/* Layer */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setLayer(IntPtr scene, int cmp, int source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static int getLayer(IntPtr scene, int cmp);
		
		public int Layer
		{
			get{ return getLayer(scene, component_id); }
			set{ setLayer(scene, component_id, value); }
		}


	}

}
