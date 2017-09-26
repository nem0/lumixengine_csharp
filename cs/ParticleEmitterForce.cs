using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterForce : NativeComponent
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

	}//end class

}//end namespace
