using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class ModelInstance : NativeComponent
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "model_instance");
			if (component_id < 0) throw new Exception("Failed to create component");
			scene = getScene(entity._universe, "model_instance");
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
