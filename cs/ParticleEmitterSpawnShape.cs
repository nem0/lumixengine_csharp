using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "particle_emitter_spawn_shape")]
	public class ParticleEmitterSpawnShape :Component
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

		public ParticleEmitterSpawnShape(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
