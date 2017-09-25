using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterAttractor : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getParticleEmitterAttractorForce(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterAttractorForce(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "particle_emitter_attractor"; } }


		public ParticleEmitterAttractor(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_attractor");
		}

		public ParticleEmitterAttractor(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_attractor");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_attractor");
		}

		/// <summary>
		/// Gets or sets the Force
		/// </summary>
		public float Force
		{
			get { return getParticleEmitterAttractorForce(scene_, componentId_); }
			set { setParticleEmitterAttractorForce(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
