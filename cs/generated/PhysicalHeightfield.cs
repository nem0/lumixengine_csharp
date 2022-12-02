using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "physical_heightfield")]
	public class PhysicalHeightfield : Component
	{
		public PhysicalHeightfield(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "physical_heightfield" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr scene, int cmp, int value);


		public int Layer
		{
			get { return getLayer(scene_, entity_.entity_Id_); }
			set { setLayer(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getHeightmap(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightmap(IntPtr scene, int cmp, string value);


		public string Heightmap
		{
			get { return getHeightmap(scene_, entity_.entity_Id_); }
			set { setHeightmap(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getYScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setYScale(IntPtr scene, int cmp, float value);


		public float YScale
		{
			get { return getYScale(scene_, entity_.entity_Id_); }
			set { setYScale(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getXZScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setXZScale(IntPtr scene, int cmp, float value);


		public float XZScale
		{
			get { return getXZScale(scene_, entity_.entity_Id_); }
			set { setXZScale(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
