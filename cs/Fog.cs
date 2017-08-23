using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Fog : Component
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "fog");
			scene = getScene(entity._universe, "fog");
		}


		/* Height */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setHeight(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getHeight(IntPtr scene, int cmp);
		
		public float Height
		{
			get{ return getHeight(scene, component_id); }
			set{ setHeight(scene, component_id, value); }
		}


		/* Density */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setDensity(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getDensity(IntPtr scene, int cmp);
		
		public float Density
		{
			get{ return getDensity(scene, component_id); }
			set{ setDensity(scene, component_id, value); }
		}


		/* Bottom */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setBottom(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getBottom(IntPtr scene, int cmp);
		
		public float Bottom
		{
			get{ return getBottom(scene, component_id); }
			set{ setBottom(scene, component_id, value); }
		}


	}

}
