using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterAlpha : NativeComponent
	{
		int componentId_;
		IntPtr scene_;


		public static string GetCmpType{ get { return "particle_emitter_alpha"; } }


		public ParticleEmitterAlpha(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_alpha");
		}

		public ParticleEmitterAlpha(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_alpha");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_alpha");
		}

	}//end class

}//end namespace
