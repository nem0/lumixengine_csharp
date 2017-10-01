using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "decal")]
	public class Decal :Component
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

		public Decal(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
