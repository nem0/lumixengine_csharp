using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterSpawnShape : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getParticleEmitterShapeRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterShapeRadius(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "particle_emitter_spawn_shape"; } }


		public ParticleEmitterSpawnShape(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_spawn_shape");
		}

		public ParticleEmitterSpawnShape(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_spawn_shape");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_spawn_shape");
		}

		/// <summary>
		/// Gets or sets the Radius
		/// </summary>
		public float Radius
		{
			get { return getParticleEmitterShapeRadius(scene_, componentId_); }
			set { setParticleEmitterShapeRadius(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
