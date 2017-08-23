using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class PointLight : Component
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "point_light");
			scene = getScene(entity._universe, "point_light");
		}


		/* Intensity */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setIntensity(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getIntensity(IntPtr scene, int cmp);
		
		public float Intensity
		{
			get{ return getIntensity(scene, component_id); }
			set{ setIntensity(scene, component_id, value); }
		}


	}

}
