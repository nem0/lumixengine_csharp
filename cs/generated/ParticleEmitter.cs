using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter")]
	public class ParticleEmitter : Component
	{
		public ParticleEmitter(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "particle_emitter" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getEmitRate(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEmitRate(IntPtr scene, int cmp, uint value);


		public uint EmitRate
		{
			get { return getEmitRate(scene_, entity_.entity_Id_); }
			set { setEmitRate(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getAutodestroy(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAutodestroy(IntPtr scene, int cmp, bool value);


		public bool IsAutodestroy
		{
			get { return getAutodestroy(scene_, entity_.entity_Id_); }
			set { setAutodestroy(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr scene, int cmp, string value);


		public string Source
		{
			get { return getSource(scene_, entity_.entity_Id_); }
			set { setSource(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
