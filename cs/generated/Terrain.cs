using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "terrain")]
	public class Terrain : Component
	{
		public Terrain(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "terrain" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMaterial(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaterial(IntPtr module, int cmp, string value);


		public string Material
		{
			get { return getMaterial(module_, entity_.entity_Id_); }
			set { setMaterial(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getXZScale(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setXZScale(IntPtr module, int cmp, float value);


		public float XZScale
		{
			get { return getXZScale(module_, entity_.entity_Id_); }
			set { setXZScale(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeightScale(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightScale(IntPtr module, int cmp, float value);


		public float HeightScale
		{
			get { return getHeightScale(module_, entity_.entity_Id_); }
			set { setHeightScale(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getTesselation(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTesselation(IntPtr module, int cmp, uint value);


		public uint Tesselation
		{
			get { return getTesselation(module_, entity_.entity_Id_); }
			set { setTesselation(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getGridResolution(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGridResolution(IntPtr module, int cmp, uint value);


		public uint GridResolution
		{
			get { return getGridResolution(module_, entity_.entity_Id_); }
			set { setGridResolution(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getTerrainNormalAt(IntPtr instance, int cmp, float a0, float a1);

		public Vec3 GetTerrainNormalAt(float a0, float a1)
		{
			return getTerrainNormalAt(module_, entity_.entity_Id_, a0, a1);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTerrainHeightAt(IntPtr instance, int cmp, float a0, float a1);

		public float GetTerrainHeightAt(float a0, float a1)
		{
			return getTerrainHeightAt(module_, entity_.entity_Id_, a0, a1);
		}

	} // class
} // namespace
