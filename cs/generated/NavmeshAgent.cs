using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "navmesh_agent")]
	public class NavmeshAgent : Component
	{
		public NavmeshAgent(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "navmesh_agent" )) { }


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
		extern static float getHeight(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeight(IntPtr module, int cmp, float value);


		public float Height
		{
			get { return getHeight(module_, entity_.entity_Id_); }
			set { setHeight(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getMoveEntity(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMoveEntity(IntPtr module, int cmp, bool value);


		public bool IsMoveEntity
		{
			get { return getMoveEntity(module_, entity_.entity_Id_); }
			set { setMoveEntity(module_, entity_.entity_Id_, value); }
		}

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
		extern static void setActorActive(IntPtr instance, int cmp, bool a0);

		public void SetActorActive(bool a0)
		{
			setActorActive(module_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool navigate(IntPtr instance, int cmp, DVec3 a0, float a1, float a2);

		public bool Navigate(DVec3 a0, float a1, float a2)
		{
			return navigate(module_, entity_.entity_Id_, a0, a1, a2);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void cancelNavigation(IntPtr instance, int cmp);

		public void CancelNavigation()
		{
			cancelNavigation(module_, entity_.entity_Id_);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void debugDrawPath(IntPtr instance, int cmp);

		public void DebugDrawPath()
		{
			debugDrawPath(module_, entity_.entity_Id_);
		}

	} // class
} // namespace
