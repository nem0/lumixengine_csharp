using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterAlpha : NativeComponent
	{

		public static string GetCmpType{ get { return "particle_emitter_alpha"; } }


		public ParticleEmitterAlpha(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "particle_emitter_alpha");
		}

		public ParticleEmitterAlpha(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "particle_emitter_alpha");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "particle_emitter_alpha");
		}

	}//end class

}//end namespace
