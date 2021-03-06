using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "particle_emitter_plane")]
	public class ParticleEmitterPlane :Component
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

		public ParticleEmitterPlane(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
