using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class ParticleEmitter : Component
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "particle_emitter");
			scene = getScene(entity._universe, "particle_emitter");
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


	}

}
