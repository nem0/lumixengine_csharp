using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "shared_anim_controller")]
	public class SharedAnimController :Component
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getSharedControllerParent(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSharedControllerParent(IntPtr scene, int cmp, Entity value);


		public static string GetCmpType{ get { return "shared_anim_controller"; } }


		/// <summary>
		/// Gets or sets the Parent
		/// </summary>
		public Entity Parent
		{
			get { return getSharedControllerParent(scene_, componentId_); }
			set { setSharedControllerParent(scene_, componentId_, value); }
		}

		public SharedAnimController(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
