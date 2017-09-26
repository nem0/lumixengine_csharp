using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class GlobalLight : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getGlobalLightColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGlobalLightColor(IntPtr scene, int cmp, Vec3 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getGlobalLightIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGlobalLightIntensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getGlobalLightIndirectIntensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGlobalLightIndirectIntensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec4 getShadowmapCascades(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setShadowmapCascades(IntPtr scene, int cmp, Vec4 value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFogDensity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogDensity(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFogBottom(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogBottom(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getFogHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogHeight(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getFogColor(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setFogColor(IntPtr scene, int cmp, Vec3 value);


		public static string GetCmpType{ get { return "global_light"; } }


		public GlobalLight(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "global_light");
		}

		public GlobalLight(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "global_light");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "global_light");
		}

		/// <summary>
		/// Gets or sets the Color
		/// </summary>
		public Vec3 Color
		{
			get { return getGlobalLightColor(scene_, componentId_); }
			set { setGlobalLightColor(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Intensity
		/// </summary>
		public float Intensity
		{
			get { return getGlobalLightIntensity(scene_, componentId_); }
			set { setGlobalLightIntensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the IndirectIntensity
		/// </summary>
		public float IndirectIntensity
		{
			get { return getGlobalLightIndirectIntensity(scene_, componentId_); }
			set { setGlobalLightIndirectIntensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the ShadowCascades
		/// </summary>
		public Vec4 ShadowCascades
		{
			get { return getShadowmapCascades(scene_, componentId_); }
			set { setShadowmapCascades(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the FogDensity
		/// </summary>
		public float FogDensity
		{
			get { return getFogDensity(scene_, componentId_); }
			set { setFogDensity(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the FogBottom
		/// </summary>
		public float FogBottom
		{
			get { return getFogBottom(scene_, componentId_); }
			set { setFogBottom(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the FogHeight
		/// </summary>
		public float FogHeight
		{
			get { return getFogHeight(scene_, componentId_); }
			set { setFogHeight(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the FogColor
		/// </summary>
		public Vec3 FogColor
		{
			get { return getFogColor(scene_, componentId_); }
			set { setFogColor(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
