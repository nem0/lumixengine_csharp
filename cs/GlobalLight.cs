using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class GlobalLight : NativeComponent
	{
		private int component_id;
		private IntPtr scene;


		public override void create()
		{
			component_id = create(entity._universe, entity._entity_id, "global_light");
			if (component_id < 0) throw new Exception("Failed to create component");
			scene = getScene(entity._universe, "global_light");
		}


		/* IndirectIntensity */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setIndirectIntensity(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getIndirectIntensity(IntPtr scene, int cmp);
		
		public float IndirectIntensity
		{
			get{ return getIndirectIntensity(scene, component_id); }
			set{ setIndirectIntensity(scene, component_id, value); }
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


	}

}