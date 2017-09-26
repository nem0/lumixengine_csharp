using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterAttractor : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getParticleEmitterAttractorForce(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterAttractorForce(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "particle_emitter_attractor"; } }


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
