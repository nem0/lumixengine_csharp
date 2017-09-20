using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Sphere : NativeComponent
	{
		private int component_id;
		private IntPtr scene;

		public static string GetCmpType() { return "sphere"; }

		public Sphere(Entity _entity, int _component_id)
		{
			entity = _entity;
			component_id = _component_id;
			scene = getScene(entity._universe, "sphere");
		}

		public Sphere(Entity entity)
		{
			component_id = create(entity._universe, entity._entity_id, "sphere");
			if (component_id < 0) throw new Exception("Failed to create component");
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
