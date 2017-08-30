using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Animable : NativeComponent
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "animable");
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
