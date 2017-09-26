using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class PhysicalController : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getControllerLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerLayer(IntPtr scene, int cmp, int value);


		public static string GetCmpType{ get { return "physical_controller"; } }


		/// <summary>
		/// Gets or sets the Layer
		/// </summary>
		public int Layer
		{
			get { return getControllerLayer(scene_, componentId_); }
			set { setControllerLayer(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
