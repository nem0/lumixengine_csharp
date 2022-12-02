using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "environment")]
	public class Environment : Component
	{
		public Environment(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "environment" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setColor(IntPtr scene, int cmp, Vec3 value);


		public Vec3 Color
		{
			get { return getColor(scene_, entity_.entity_Id_); }
			set { setColor(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIntensity(IntPtr scene, int cmp, float value);


		public float Intensity
		{
			get { return getIntensity(scene_, entity_.entity_Id_); }
			set { setIntensity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getIndirectIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setIndirectIntensity(IntPtr scene, int cmp, float value);


		public float IndirectIntensity
		{
			get { return getIndirectIntensity(scene_, entity_.entity_Id_); }
			set { setIndirectIntensity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getShadowCascades(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setShadowCascades(IntPtr scene, int cmp, Vec4 value);


		public Vec4 ShadowCascades
		{
			get { return getShadowCascades(scene_, entity_.entity_Id_); }
			set { setShadowCascades(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getCastShadows(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCastShadows(IntPtr scene, int cmp, bool value);


		public bool IsCastShadows
		{
			get { return getCastShadows(scene_, entity_.entity_Id_); }
			set { setCastShadows(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
