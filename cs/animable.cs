using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Animable : NativeComponent
	{
		private int component_id;
		private IntPtr scene;

		public static string GetCmpType() { return "animable"; }

		public Animable(Entity _entity, int _component_id)
		{
			entity = _entity;
			component_id = _component_id;
			scene = getScene(entity._universe, "animable");
		}

		public Animable(Entity entity)
		{
			component_id = create(entity._universe, entity._entity_id, "animable");
			if (component_id < 0) throw new Exception("Failed to create component");
			scene = getScene(entity._universe, "animable");
		}


		/* Time */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setTime(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getTime(IntPtr scene, int cmp);
		
		public float Time
		{
			get{ return getTime(scene, component_id); }
			set{ setTime(scene, component_id, value); }
		}


	}

}
