using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Controller : NativeComponent
	{
		private int component_id;
		private IntPtr scene;

		public static string GetCmpType() { return "controller"; }

		public Controller(Entity _entity, int _component_id)
		{
			entity = _entity;
			component_id = _component_id;
			scene = getScene(entity._universe, "controller");
		}

		public Controller(Entity entity)
		{
			component_id = create(entity._universe, entity._entity_id, "controller");
			if (component_id < 0) throw new Exception("Failed to create component");
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
