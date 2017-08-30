using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Decal : NativeComponent
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "decal");
			scene = getScene(entity._universe, "decal");
		}


		/* Scale */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setScale(IntPtr scene, int cmp, Vec3 source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static Vec3 getScale(IntPtr scene, int cmp);
		
		public Vec3 Scale
		{
			get{ return getScale(scene, component_id); }
			set{ setScale(scene, component_id, value); }
		}


		/* MaterialPath */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setMaterialPath(IntPtr scene, int cmp, string source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static string getMaterialPath(IntPtr scene, int cmp);
		
		public string MaterialPath
		{
			get{ return getMaterialPath(scene, component_id); }
			set{ setMaterialPath(scene, component_id, value); }
		}


	}

}
