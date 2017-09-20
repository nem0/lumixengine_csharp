using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class SharedController : NativeComponent
	{
		private int component_id;
		private IntPtr scene;

		public static string GetCmpType() { return "shared_controller"; }

		public SharedController(Entity _entity, int _component_id)
		{
			entity = _entity;
			component_id = _component_id;
			scene = getScene(entity._universe, "shared_controller");
		}

		public SharedController(Entity entity)
		{
			component_id = create(entity._universe, entity._entity_id, "shared_controller");
			if (component_id < 0) throw new Exception("Failed to create component");
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
