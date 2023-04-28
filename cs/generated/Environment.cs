using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "environment")]
	public class Environment : Component
	{
		public Environment(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "environment" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getColor(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setColor(IntPtr module, int cmp, Vec3 value);


		public Vec3 Color
		{
			get { return getColor(module_, entity_.entity_Id_); }
			set { setColor(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getIntensity(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIntensity(IntPtr module, int cmp, float value);


		public float Intensity
		{
			get { return getIntensity(module_, entity_.entity_Id_); }
			set { setIntensity(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getIndirectIntensity(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIndirectIntensity(IntPtr module, int cmp, float value);


		public float IndirectIntensity
		{
			get { return getIndirectIntensity(module_, entity_.entity_Id_); }
			set { setIndirectIntensity(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getShadowCascades(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setShadowCascades(IntPtr module, int cmp, Vec4 value);


		public Vec4 ShadowCascades
		{
			get { return getShadowCascades(module_, entity_.entity_Id_); }
			set { setShadowCascades(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getCastShadows(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCastShadows(IntPtr module, int cmp, bool value);


		public bool IsCastShadows
		{
			get { return getCastShadows(module_, entity_.entity_Id_); }
			set { setCastShadows(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
