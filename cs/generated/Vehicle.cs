using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "vehicle")]
	public class Vehicle : Component
	{
		public Vehicle(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "vehicle" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSpeed(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpeed(IntPtr module, int cmp, float value);


		public float Speed
		{
			get { return getSpeed(module_, entity_.entity_Id_); }
			set { setSpeed(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getCurrentGear(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCurrentGear(IntPtr module, int cmp, int value);


		public int CurrentGear
		{
			get { return getCurrentGear(module_, entity_.entity_Id_); }
			set { setCurrentGear(module_, entity_.entity_Id_, value); }
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
		extern static Vec3 getCenterOfMass(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCenterOfMass(IntPtr module, int cmp, Vec3 value);


		public Vec3 CenterOfMass
		{
			get { return getCenterOfMass(module_, entity_.entity_Id_); }
			set { setCenterOfMass(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMOIMultiplier(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMOIMultiplier(IntPtr module, int cmp, float value);


		public float MOIMultiplier
		{
			get { return getMOIMultiplier(module_, entity_.entity_Id_); }
			set { setMOIMultiplier(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getChassis(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setChassis(IntPtr module, int cmp, string value);


		public string Chassis
		{
			get { return getChassis(module_, entity_.entity_Id_); }
			set { setChassis(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getChassisLayer(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setChassisLayer(IntPtr module, int cmp, int value);


		public int ChassisLayer
		{
			get { return getChassisLayer(module_, entity_.entity_Id_); }
			set { setChassisLayer(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getWheelsLayer(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setWheelsLayer(IntPtr module, int cmp, int value);


		public int WheelsLayer
		{
			get { return getWheelsLayer(module_, entity_.entity_Id_); }
			set { setWheelsLayer(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setVehicleAccel(IntPtr instance, int cmp, float a0);

		public void SetVehicleAccel(float a0)
		{
			setVehicleAccel(module_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setVehicleSteer(IntPtr instance, int cmp, float a0);

		public void SetVehicleSteer(float a0)
		{
			setVehicleSteer(module_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setVehicleBrake(IntPtr instance, int cmp, float a0);

		public void SetVehicleBrake(float a0)
		{
			setVehicleBrake(module_, entity_.entity_Id_, a0);
		}

	} // class
} // namespace
