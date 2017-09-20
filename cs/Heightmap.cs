using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Heightmap : NativeComponent
	{
		private int component_id;
		private IntPtr scene;

		public static string GetCmpType() { return "heightmap"; }

		public Heightmap(Entity _entity, int _component_id)
		{
			entity = _entity;
			component_id = _component_id;
			scene = getScene(entity._universe, "heightmap");
		}

		public Heightmap(Entity entity)
		{
			component_id = create(entity._universe, entity._entity_id, "heightmap");
			if (component_id < 0) throw new Exception("Failed to create component");
			scene = getScene(entity._universe, "heightmap");
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
