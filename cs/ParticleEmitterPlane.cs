using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterPlane : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getParticleEmitterPlaneBounce(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterPlaneBounce(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "particle_emitter_plane"; } }


		public ParticleEmitterPlane(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "particle_emitter_plane");
		}

		public ParticleEmitterPlane(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "particle_emitter_plane");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "particle_emitter_plane");
		}

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
