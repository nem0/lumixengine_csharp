using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "point_light")]
	public class PointLight : Component
	{
		public PointLight(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "point_light" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getCastShadows(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCastShadows(IntPtr scene, int cmp, bool value);


		public bool IsCastShadows
		{
			get { return getCastShadows(scene_, entity_.entity_Id_); }
			set { setCastShadows(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getDynamic(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDynamic(IntPtr scene, int cmp, bool value);


		public bool IsDynamic
		{
			get { return getDynamic(scene_, entity_.entity_Id_); }
			set { setDynamic(scene_, entity_.entity_Id_, value); }
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
		extern static float getFOV(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFOV(IntPtr scene, int cmp, float value);


		public float FOV
		{
			get { return getFOV(scene_, entity_.entity_Id_); }
			set { setFOV(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAttenuation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAttenuation(IntPtr scene, int cmp, float value);


		public float Attenuation
		{
			get { return getAttenuation(scene_, entity_.entity_Id_); }
			set { setAttenuation(scene_, entity_.entity_Id_, value); }
		}

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
		extern static float getRange(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRange(IntPtr scene, int cmp, float value);


		public float Range
		{
			get { return getRange(scene_, entity_.entity_Id_); }
			set { setRange(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
