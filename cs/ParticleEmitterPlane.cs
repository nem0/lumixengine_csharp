using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class ParticleEmitterPlane : Component
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "particle_emitter_plane");
			scene = getScene(entity._universe, "particle_emitter_plane");
		}


		/* Bounce */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setBounce(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getBounce(IntPtr scene, int cmp);
		
		public float Bounce
		{
			get{ return getBounce(scene, component_id); }
			set{ setBounce(scene, component_id, value); }
		}


	}

}
