using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "renderable")]
	public class ModelInstance :Component
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getModelInstancePath(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setModelInstancePath(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getModelInstanceKeepSkin(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setModelInstanceKeepSkin(IntPtr scene, int cmp, bool value);


		public static string GetCmpType{ get { return "renderable"; } }


		/// <summary>
		/// Gets or sets the Source
		/// </summary>
		public string Source
		{
			get { return getModelInstancePath(scene_, componentId_); }
			set { setModelInstancePath(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the KeepSkin
		/// </summary>
		public bool IsKeepSkin
		{
			get { return getModelInstanceKeepSkin(scene_, componentId_); }
			set { setModelInstanceKeepSkin(scene_, componentId_, value); }
		}

		public ModelInstance(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
