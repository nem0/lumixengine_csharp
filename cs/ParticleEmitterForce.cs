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


		public ParticleEmitterForce(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "particle_emitter_force");
		}

		public ParticleEmitterForce(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "particle_emitter_force");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "particle_emitter_force");
		}

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
