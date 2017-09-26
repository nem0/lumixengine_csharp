using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class SharedAnimController : NativeComponent
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

	}//end class

}//end namespace
