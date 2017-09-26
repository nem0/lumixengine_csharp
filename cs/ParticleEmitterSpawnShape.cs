using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterSpawnShape : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getParticleEmitterShapeRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterShapeRadius(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "particle_emitter_spawn_shape"; } }


		/// <summary>
		/// Gets or sets the Radius
		/// </summary>
		public float Radius
		{
			get { return getParticleEmitterShapeRadius(scene_, componentId_); }
			set { setParticleEmitterShapeRadius(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
