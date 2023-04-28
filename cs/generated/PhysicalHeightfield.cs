using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "physical_heightfield")]
	public class PhysicalHeightfield : Component
	{
		public PhysicalHeightfield(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "physical_heightfield" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr module, int cmp, int value);


		public int Layer
		{
			get { return getLayer(module_, entity_.entity_Id_); }
			set { setLayer(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getHeightmap(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightmap(IntPtr module, int cmp, string value);


		public string Heightmap
		{
			get { return getHeightmap(module_, entity_.entity_Id_); }
			set { setHeightmap(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getYScale(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setYScale(IntPtr module, int cmp, float value);


		public float YScale
		{
			get { return getYScale(module_, entity_.entity_Id_); }
			set { setYScale(module_, entity_.entity_Id_, value); }
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

	} // class
} // namespace
