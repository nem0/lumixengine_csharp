using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class EchoZone : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getEchoZoneRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEchoZoneRadius(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getEchoZoneDelay(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEchoZoneDelay(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "echo_zone"; } }


		/// <summary>
		/// Gets or sets the Radius
		/// </summary>
		public float Radius
		{
			get { return getEchoZoneRadius(scene_, componentId_); }
			set { setEchoZoneRadius(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the Delay(ms)
		/// </summary>
		public float Delayms
		{
			get { return getEchoZoneDelay(scene_, componentId_); }
			set { setEchoZoneDelay(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
