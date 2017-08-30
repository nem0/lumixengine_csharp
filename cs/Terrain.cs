using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Terrain : Component
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "terrain");
			scene = getScene(entity._universe, "terrain");
		}


		/* XZScale */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setXZScale(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getXZScale(IntPtr scene, int cmp);
		
		public float XZScale
		{
			get{ return getXZScale(scene, component_id); }
			set{ setXZScale(scene, component_id, value); }
		}


		/* YScale */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setYScale(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getYScale(IntPtr scene, int cmp);
		
		public float YScale
		{
			get{ return getYScale(scene, component_id); }
			set{ setYScale(scene, component_id, value); }
		}


	}

}
