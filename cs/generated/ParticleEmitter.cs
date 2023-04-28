using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "particle_emitter")]
	public class ParticleEmitter : Component
	{
		public ParticleEmitter(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "particle_emitter" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getEmitRate(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEmitRate(IntPtr module, int cmp, uint value);


		public uint EmitRate
		{
			get { return getEmitRate(module_, entity_.entity_Id_); }
			set { setEmitRate(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getAutodestroy(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAutodestroy(IntPtr module, int cmp, bool value);


		public bool IsAutodestroy
		{
			get { return getAutodestroy(module_, entity_.entity_Id_); }
			set { setAutodestroy(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSource(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr module, int cmp, string value);


		public string Source
		{
			get { return getSource(module_, entity_.entity_Id_); }
			set { setSource(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
