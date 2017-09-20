using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Decal : NativeComponent
	{
		private int component_id;
		private IntPtr scene;

		public static string GetCmpType() { return "decal"; }

		public Decal(Entity _entity, int _component_id)
		{
			entity = _entity;
			component_id = _component_id;
			scene = getScene(entity._universe, "decal");
		}

		public Decal(Entity entity)
		{
			component_id = create(entity._universe, entity._entity_id, "decal");
			if (component_id < 0) throw new Exception("Failed to create component");
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
