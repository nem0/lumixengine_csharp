using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "wheel")]
	public class Wheel : Component
	{
		public Wheel(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "wheel" )) { }


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
		extern static float getWidth(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setWidth(IntPtr module, int cmp, float value);


		public float Width
		{
			get { return getWidth(module_, entity_.entity_Id_); }
			set { setWidth(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMass(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMass(IntPtr module, int cmp, float value);


		public float Mass
		{
			get { return getMass(module_, entity_.entity_Id_); }
			set { setMass(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMOI(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMOI(IntPtr module, int cmp, float value);


		public float MOI
		{
			get { return getMOI(module_, entity_.entity_Id_); }
			set { setMOI(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMaxCompression(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaxCompression(IntPtr module, int cmp, float value);


		public float MaxCompression
		{
			get { return getMaxCompression(module_, entity_.entity_Id_); }
			set { setMaxCompression(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMaxDroop(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaxDroop(IntPtr module, int cmp, float value);


		public float MaxDroop
		{
			get { return getMaxDroop(module_, entity_.entity_Id_); }
			set { setMaxDroop(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSpringStrength(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpringStrength(IntPtr module, int cmp, float value);


		public float SpringStrength
		{
			get { return getSpringStrength(module_, entity_.entity_Id_); }
			set { setSpringStrength(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSpringDamperRate(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpringDamperRate(IntPtr module, int cmp, float value);


		public float SpringDamperRate
		{
			get { return getSpringDamperRate(module_, entity_.entity_Id_); }
			set { setSpringDamperRate(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getSlot(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSlot(IntPtr module, int cmp, int value);


		public int Slot
		{
			get { return getSlot(module_, entity_.entity_Id_); }
			set { setSlot(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRPM(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRPM(IntPtr module, int cmp, float value);


		public float RPM
		{
			get { return getRPM(module_, entity_.entity_Id_); }
			set { setRPM(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
