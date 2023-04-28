using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "rigid_actor")]
	public class RigidActor : Component
	{
		public RigidActor(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "rigid_actor" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr module, int cmp, int value);


		public int Layer
		{
			get { return getLayer(module_, entity_.entity_Id_); }
			set { setLayer(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getDynamic(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDynamic(IntPtr module, int cmp, int value);


		public int Dynamic
		{
			get { return getDynamic(module_, entity_.entity_Id_); }
			set { setDynamic(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getTrigger(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTrigger(IntPtr module, int cmp, bool value);


		public bool IsTrigger
		{
			get { return getTrigger(module_, entity_.entity_Id_); }
			set { setTrigger(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMesh(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMesh(IntPtr module, int cmp, string value);


		public string Mesh
		{
			get { return getMesh(module_, entity_.entity_Id_); }
			set { setMesh(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMaterial(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaterial(IntPtr module, int cmp, string value);


		public string Material
		{
			get { return getMaterial(module_, entity_.entity_Id_); }
			set { setMaterial(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void putToSleep(IntPtr instance, int cmp);

		public void PutToSleep()
		{
			putToSleep(module_, entity_.entity_Id_);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getActorSpeed(IntPtr instance, int cmp);

		public float GetActorSpeed()
		{
			return getActorSpeed(module_, entity_.entity_Id_);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getActorVelocity(IntPtr instance, int cmp);

		public Vec3 GetActorVelocity()
		{
			return getActorVelocity(module_, entity_.entity_Id_);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void applyForceToActor(IntPtr instance, int cmp, Vec3 a0);

		public void ApplyForceToActor(Vec3 a0)
		{
			applyForceToActor(module_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void applyImpulseToActor(IntPtr instance, int cmp, Vec3 a0);

		public void ApplyImpulseToActor(Vec3 a0)
		{
			applyImpulseToActor(module_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addForceAtPos(IntPtr instance, int cmp, Vec3 a0, Vec3 a1);

		public void AddForceAtPos(Vec3 a0, Vec3 a1)
		{
			addForceAtPos(module_, entity_.entity_Id_, a0, a1);
		}

	} // class
} // namespace
