using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterPlane : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getParticleEmitterPlaneBounce(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterPlaneBounce(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "particle_emitter_plane"; } }


		/// <summary>
		/// Gets or sets the Bounce
		/// </summary>
		public float Bounce
		{
			get { return getParticleEmitterPlaneBounce(scene_, componentId_); }
			set { setParticleEmitterPlaneBounce(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
