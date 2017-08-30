using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Sphere : Component
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "sphere");
			scene = getScene(entity._universe, "sphere");
		}


		/* Radius */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setRadius(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getRadius(IntPtr scene, int cmp);
		
		public float Radius
		{
			get{ return getRadius(scene, component_id); }
			set{ setRadius(scene, component_id, value); }
		}


	}

}
