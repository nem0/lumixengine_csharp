using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterLinearMovement : NativeComponent
	{
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

		public ParticleEmitterLinearMovement(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
