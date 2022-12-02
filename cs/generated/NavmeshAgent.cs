using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "navmesh_agent")]
	public class NavmeshAgent : Component
	{
		public NavmeshAgent(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "navmesh_agent" )) { }


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
		extern static float getHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHeight(IntPtr scene, int cmp, float value);


		public float Height
		{
			get { return getHeight(scene_, entity_.entity_Id_); }
			set { setHeight(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getMoveEntity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMoveEntity(IntPtr scene, int cmp, bool value);


		public bool IsMoveEntity
		{
			get { return getMoveEntity(scene_, entity_.entity_Id_); }
			set { setMoveEntity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getSpeed(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSpeed(IntPtr scene, int cmp, float value);


		public float Speed
		{
			get { return getSpeed(scene_, entity_.entity_Id_); }
			set { setSpeed(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
