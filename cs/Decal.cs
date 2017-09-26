using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class Decal : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getDecalMaterialPath(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDecalMaterialPath(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getDecalScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDecalScale(IntPtr scene, int cmp, Vec3 value);


		public static string GetCmpType{ get { return "decal"; } }


		public Decal(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "decal");
		}

		public Decal(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "decal");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "decal");
		}

		/// <summary>
		/// Gets or sets the Material
		/// </summary>
		public string Material
		{
			get { return getDecalMaterialPath(scene_, componentId_); }
			set { setDecalMaterialPath(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Scale
		/// </summary>
		public Vec3 Scale
		{
			get { return getDecalScale(scene_, componentId_); }
			set { setDecalScale(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
