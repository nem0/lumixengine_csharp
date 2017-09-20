using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class ModelInstance : NativeComponent
	{
		private int component_id;
		private IntPtr scene;

		public static string GetCmpType() { return "renderable"; }

		public ModelInstance(Entity _entity, int _component_id)
		{
			entity = _entity;
			component_id = _component_id;
			scene = getScene(entity._universe, "renderable");
		}

		public ModelInstance(Entity entity)
		{
			component_id = create(entity._universe, entity._entity_id, "renderable");
			if (component_id < 0) throw new Exception("Failed to create component");
			scene = getScene(entity._universe, "renderable");
		}


		/* Path */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setPath(IntPtr scene, int cmp, string source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static string getPath(IntPtr scene, int cmp);
		
		public string Path
		{
			get{ return getPath(scene, component_id); }
			set{ setPath(scene, component_id, value); }
		}


	}

}
