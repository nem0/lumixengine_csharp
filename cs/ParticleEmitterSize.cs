using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterSize : NativeComponent
	{

		public static string GetCmpType{ get { return "particle_emitter_size"; } }


		public ParticleEmitterSize(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "particle_emitter_size");
		}

		public ParticleEmitterSize(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "particle_emitter_size");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "particle_emitter_size");
		}

	}//end class

}//end namespace
