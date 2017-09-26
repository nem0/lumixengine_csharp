using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterSize : NativeComponent
	{

		public static string GetCmpType{ get { return "particle_emitter_size"; } }


		public ParticleEmitterSize(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
