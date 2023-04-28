using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "chorus_zone")]
	public class ChorusZone : Component
	{
		public ChorusZone(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "chorus_zone" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRadius(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRadius(IntPtr module, int cmp, float value);


		public float Radius
		{
			get { return getRadius(module_, entity_.entity_Id_); }
			set { setRadius(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getDelay(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDelay(IntPtr module, int cmp, float value);


		public float Delay
		{
			get { return getDelay(module_, entity_.entity_Id_); }
			set { setDelay(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
