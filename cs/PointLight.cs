using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class PointLight : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getPointLightColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPointLightColor(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getPointLightSpecularColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPointLightSpecularColor(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getPointLightIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPointLightIntensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getPointLightSpecularIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPointLightSpecularIntensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLightAttenuation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLightAttenuation(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLightRange(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLightRange(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getLightCastShadows(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLightCastShadows(IntPtr scene, int cmp, bool value);


		public static string GetCmpType{ get { return "point_light"; } }


		/// <summary>
		/// Gets or sets the DiffuseColor
		/// </summary>
		public Vec3 DiffuseColor
		{
			get { return getPointLightColor(scene_, componentId_); }
			set { setPointLightColor(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the SpecularColor
		/// </summary>
		public Vec3 SpecularColor
		{
			get { return getPointLightSpecularColor(scene_, componentId_); }
			set { setPointLightSpecularColor(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the DiffuseIntensity
		/// </summary>
		public float DiffuseIntensity
		{
			get { return getPointLightIntensity(scene_, componentId_); }
			set { setPointLightIntensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the SpecularIntensity
		/// </summary>
		public float SpecularIntensity
		{
			get { return getPointLightSpecularIntensity(scene_, componentId_); }
			set { setPointLightSpecularIntensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Attenuation
		/// </summary>
		public float Attenuation
		{
			get { return getLightAttenuation(scene_, componentId_); }
			set { setLightAttenuation(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Range
		/// </summary>
		public float Range
		{
			get { return getLightRange(scene_, componentId_); }
			set { setLightRange(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the CastShadows
		/// </summary>
		public bool IsCastShadows
		{
			get { return getLightCastShadows(scene_, componentId_); }
			set { setLightCastShadows(scene_, componentId_, value); }
		}

		public PointLight(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
