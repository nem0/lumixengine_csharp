using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class PointLight : NativeComponent
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "point_light");
			scene = getScene(entity._universe, "point_light");
		}


		/* SpecularIntensity */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setSpecularIntensity(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getSpecularIntensity(IntPtr scene, int cmp);
		
		public float SpecularIntensity
		{
			get{ return getSpecularIntensity(scene, component_id); }
			set{ setSpecularIntensity(scene, component_id, value); }
		}


		/* Intensity */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setIntensity(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getIntensity(IntPtr scene, int cmp);
		
		public float Intensity
		{
			get{ return getIntensity(scene, component_id); }
			set{ setIntensity(scene, component_id, value); }
		}


		/* SpecularColor */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setSpecularColor(IntPtr scene, int cmp, Vec3 source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static Vec3 getSpecularColor(IntPtr scene, int cmp);
		
		public Vec3 SpecularColor
		{
			get{ return getSpecularColor(scene, component_id); }
			set{ setSpecularColor(scene, component_id, value); }
		}


		/* Color */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setColor(IntPtr scene, int cmp, Vec3 source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static Vec3 getColor(IntPtr scene, int cmp);
		
		public Vec3 Color
		{
			get{ return getColor(scene, component_id); }
			set{ setColor(scene, component_id, value); }
		}


	}

}
