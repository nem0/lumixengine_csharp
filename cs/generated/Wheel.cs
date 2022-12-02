using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "wheel")]
	public class Wheel : Component
	{
		public Wheel(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "wheel" )) { }


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
		extern static float getWidth(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setWidth(IntPtr scene, int cmp, float value);


		public float Width
		{
			get { return getWidth(scene_, entity_.entity_Id_); }
			set { setWidth(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMass(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMass(IntPtr scene, int cmp, float value);


		public float Mass
		{
			get { return getMass(scene_, entity_.entity_Id_); }
			set { setMass(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMOI(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMOI(IntPtr scene, int cmp, float value);


		public float MOI
		{
			get { return getMOI(scene_, entity_.entity_Id_); }
			set { setMOI(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMaxCompression(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaxCompression(IntPtr scene, int cmp, float value);


		public float MaxCompression
		{
			get { return getMaxCompression(scene_, entity_.entity_Id_); }
			set { setMaxCompression(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMaxDroop(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaxDroop(IntPtr scene, int cmp, float value);


		public float MaxDroop
		{
			get { return getMaxDroop(scene_, entity_.entity_Id_); }
			set { setMaxDroop(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSpringStrength(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpringStrength(IntPtr scene, int cmp, float value);


		public float SpringStrength
		{
			get { return getSpringStrength(scene_, entity_.entity_Id_); }
			set { setSpringStrength(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSpringDamperRate(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpringDamperRate(IntPtr scene, int cmp, float value);


		public float SpringDamperRate
		{
			get { return getSpringDamperRate(scene_, entity_.entity_Id_); }
			set { setSpringDamperRate(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getSlot(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSlot(IntPtr scene, int cmp, int value);


		public int Slot
		{
			get { return getSlot(scene_, entity_.entity_Id_); }
			set { setSlot(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRPM(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRPM(IntPtr scene, int cmp, float value);


		public float RPM
		{
			get { return getRPM(scene_, entity_.entity_Id_); }
			set { setRPM(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
