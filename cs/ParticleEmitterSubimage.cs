using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "particle_emitter_subimage")]
	public class ParticleEmitterSubimage :Component
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

		public ParticleEmitterSubimage(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
