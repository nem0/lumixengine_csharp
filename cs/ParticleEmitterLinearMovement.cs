using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterLinearMovement : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getParticleEmitterLinearMovementX(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterLinearMovementX(IntPtr scene, int cmp, Vec2 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getParticleEmitterLinearMovementY(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterLinearMovementY(IntPtr scene, int cmp, Vec2 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getParticleEmitterLinearMovementZ(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterLinearMovementZ(IntPtr scene, int cmp, Vec2 value);


		public static string GetCmpType{ get { return "particle_emitter_linear_movement"; } }


		public ParticleEmitterLinearMovement(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "particle_emitter_linear_movement");
		}

		public ParticleEmitterLinearMovement(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "particle_emitter_linear_movement");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "particle_emitter_linear_movement");
		}

		/// <summary>
		/// Gets or sets the X
		/// </summary>
		public Vec2 X
		{
			get { return getParticleEmitterLinearMovementX(scene_, componentId_); }
			set { setParticleEmitterLinearMovementX(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Y
		/// </summary>
		public Vec2 Y
		{
			get { return getParticleEmitterLinearMovementY(scene_, componentId_); }
			set { setParticleEmitterLinearMovementY(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Z
		/// </summary>
		public Vec2 Z
		{
			get { return getParticleEmitterLinearMovementZ(scene_, componentId_); }
			set { setParticleEmitterLinearMovementZ(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
