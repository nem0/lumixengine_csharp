using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "particle_emitter_size")]
	public class ParticleEmitterSize :Component
	{

		public static string GetCmpType{ get { return "particle_emitter_size"; } }


		public ParticleEmitterSize(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
