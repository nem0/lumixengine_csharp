using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class ParticleEmitterSubimage : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getParticleEmitterSubimageRows(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterSubimageRows(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getParticleEmitterSubimageCols(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParticleEmitterSubimageCols(IntPtr scene, int cmp, int value);


		public static string GetCmpType{ get { return "particle_emitter_subimage"; } }


		public ParticleEmitterSubimage(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "particle_emitter_subimage");
		}

		public ParticleEmitterSubimage(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "particle_emitter_subimage");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "particle_emitter_subimage");
		}

		/// <summary>
		/// Gets or sets the Rows
		/// </summary>
		public int Rows
		{
			get { return getParticleEmitterSubimageRows(scene_, componentId_); }
			set { setParticleEmitterSubimageRows(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Columns
		/// </summary>
		public int Columns
		{
			get { return getParticleEmitterSubimageCols(scene_, componentId_); }
			set { setParticleEmitterSubimageCols(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
