using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "vehicle")]
	public class Vehicle : Component
	{
		public Vehicle(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "vehicle" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSpeed(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpeed(IntPtr scene, int cmp, float value);


		public float Speed
		{
			get { return getSpeed(scene_, entity_.entity_Id_); }
			set { setSpeed(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getCurrentGear(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCurrentGear(IntPtr scene, int cmp, int value);


		public int CurrentGear
		{
			get { return getCurrentGear(scene_, entity_.entity_Id_); }
			set { setCurrentGear(scene_, entity_.entity_Id_, value); }
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
		extern static Vec3 getCenterOfMass(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCenterOfMass(IntPtr scene, int cmp, Vec3 value);


		public Vec3 CenterOfMass
		{
			get { return getCenterOfMass(scene_, entity_.entity_Id_); }
			set { setCenterOfMass(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMOIMultiplier(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMOIMultiplier(IntPtr scene, int cmp, float value);


		public float MOIMultiplier
		{
			get { return getMOIMultiplier(scene_, entity_.entity_Id_); }
			set { setMOIMultiplier(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getChassis(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setChassis(IntPtr scene, int cmp, string value);


		public string Chassis
		{
			get { return getChassis(scene_, entity_.entity_Id_); }
			set { setChassis(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getChassisLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setChassisLayer(IntPtr scene, int cmp, int value);


		public int ChassisLayer
		{
			get { return getChassisLayer(scene_, entity_.entity_Id_); }
			set { setChassisLayer(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getWheelsLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setWheelsLayer(IntPtr scene, int cmp, int value);


		public int WheelsLayer
		{
			get { return getWheelsLayer(scene_, entity_.entity_Id_); }
			set { setWheelsLayer(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
