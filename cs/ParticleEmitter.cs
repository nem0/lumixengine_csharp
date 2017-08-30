using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class ParticleEmitter : NativeComponent
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "particle_emitter");
			scene = getScene(entity._universe, "particle_emitter");
		}


		/* Autoemit */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setAutoemit(IntPtr scene, int cmp, bool source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static bool getAutoemit(IntPtr scene, int cmp);
		
		public bool Autoemit
		{
			get{ return getAutoemit(scene, component_id); }
			set{ setAutoemit(scene, component_id, value); }
		}


		/* Acceleration */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setAcceleration(IntPtr scene, int cmp, Vec3 source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static Vec3 getAcceleration(IntPtr scene, int cmp);
		
		public Vec3 Acceleration
		{
			get{ return getAcceleration(scene, component_id); }
			set{ setAcceleration(scene, component_id, value); }
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
