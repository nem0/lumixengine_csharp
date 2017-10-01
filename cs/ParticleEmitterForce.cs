using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "particle_emitter_force")]
	public class ParticleEmitterForce :Component
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getParticleEmitterAcceleration(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterAcceleration(IntPtr scene, int cmp, Vec3 value);


		public static string GetCmpType{ get { return "particle_emitter_force"; } }


		/// <summary>
		/// Gets or sets the Acceleration
		/// </summary>
		public Vec3 Acceleration
		{
			get { return getParticleEmitterAcceleration(scene_, componentId_); }
			set { setParticleEmitterAcceleration(scene_, componentId_, value); }
		}

		public ParticleEmitterForce(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
