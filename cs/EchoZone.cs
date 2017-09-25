using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class EchoZone : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getEchoZoneRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEchoZoneRadius(IntPtr scene, int cmp, float value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getEchoZoneDelay(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEchoZoneDelay(IntPtr scene, int cmp, float value);


		public static string GetCmpType{ get { return "echo_zone"; } }


		public EchoZone(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "echo_zone");
		}

		public EchoZone(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "echo_zone");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "echo_zone");
		}

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
