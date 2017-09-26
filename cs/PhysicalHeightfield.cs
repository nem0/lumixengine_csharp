using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class PhysicalHeightfield : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getHeightmapSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightmapSource(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeightmapXZScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightmapXZScale(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getHeightmapYScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightmapYScale(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getHeightfieldLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeightfieldLayer(IntPtr scene, int cmp, int value);


		public static string GetCmpType{ get { return "physical_heightfield"; } }


		public PhysicalHeightfield(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "physical_heightfield");
		}

		public PhysicalHeightfield(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "physical_heightfield");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "physical_heightfield");
		}

		/// <summary>
		/// Gets or sets the Heightmap
		/// </summary>
		public string Heightmap
		{
			get { return getHeightmapSource(scene_, componentId_); }
			set { setHeightmapSource(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the XZScale
		/// </summary>
		public float XZScale
		{
			get { return getHeightmapXZScale(scene_, componentId_); }
			set { setHeightmapXZScale(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the YScale
		/// </summary>
		public float YScale
		{
			get { return getHeightmapYScale(scene_, componentId_); }
			set { setHeightmapYScale(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Layer
		/// </summary>
		public int Layer
		{
			get { return getHeightfieldLayer(scene_, componentId_); }
			set { setHeightfieldLayer(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
