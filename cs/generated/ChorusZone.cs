using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "chorus_zone")]
	public class ChorusZone : Component
	{
		public ChorusZone(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "chorus_zone" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadius(IntPtr scene, int cmp, float value);


		public float Radius
		{
			get { return getRadius(scene_, entity_.entity_Id_); }
			set { setRadius(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDelay(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDelay(IntPtr scene, int cmp, float value);


		public float Delay
		{
			get { return getDelay(scene_, entity_.entity_Id_); }
			set { setDelay(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
