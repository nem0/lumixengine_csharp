using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "point_light")]
	public class PointLight : Component
	{
		public PointLight(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "point_light" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getCastShadows(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCastShadows(IntPtr module, int cmp, bool value);


		public bool IsCastShadows
		{
			get { return getCastShadows(module_, entity_.entity_Id_); }
			set { setCastShadows(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getDynamic(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDynamic(IntPtr module, int cmp, bool value);


		public bool IsDynamic
		{
			get { return getDynamic(module_, entity_.entity_Id_); }
			set { setDynamic(module_, entity_.entity_Id_, value); }
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
		extern static float getFOV(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFOV(IntPtr module, int cmp, float value);


		public float FOV
		{
			get { return getFOV(module_, entity_.entity_Id_); }
			set { setFOV(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAttenuation(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAttenuation(IntPtr module, int cmp, float value);


		public float Attenuation
		{
			get { return getAttenuation(module_, entity_.entity_Id_); }
			set { setAttenuation(module_, entity_.entity_Id_, value); }
		}

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
		extern static float getRange(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRange(IntPtr module, int cmp, float value);


		public float Range
		{
			get { return getRange(module_, entity_.entity_Id_); }
			set { setRange(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
