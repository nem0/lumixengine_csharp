using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class Terrain : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getTerrainMaterialPath(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTerrainMaterialPath(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTerrainXZScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTerrainXZScale(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTerrainYScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTerrainYScale(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "terrain"; } }


		public Terrain(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "terrain");
		}

		public Terrain(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "terrain");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "terrain");
		}

		/// <summary>
		/// Gets or sets the Material
		/// </summary>
		public string Material
		{
			get { return getTerrainMaterialPath(scene_, componentId_); }
			set { setTerrainMaterialPath(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the XZScale
		/// </summary>
		public float XZScale
		{
			get { return getTerrainXZScale(scene_, componentId_); }
			set { setTerrainXZScale(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the HeightScale
		/// </summary>
		public float HeightScale
		{
			get { return getTerrainYScale(scene_, componentId_); }
			set { setTerrainYScale(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
