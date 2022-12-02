using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "navmesh_zone")]
	public class NavmeshZone : Component
	{
		public NavmeshZone(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "navmesh_zone" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getExtents(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setExtents(IntPtr scene, int cmp, Vec3 value);


		public Vec3 Extents
		{
			get { return getExtents(scene_, entity_.entity_Id_); }
			set { setExtents(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAgentHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAgentHeight(IntPtr scene, int cmp, float value);


		public float AgentHeight
		{
			get { return getAgentHeight(scene_, entity_.entity_Id_); }
			set { setAgentHeight(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAgentRadius(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAgentRadius(IntPtr scene, int cmp, float value);


		public float AgentRadius
		{
			get { return getAgentRadius(scene_, entity_.entity_Id_); }
			set { setAgentRadius(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCellSize(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCellSize(IntPtr scene, int cmp, float value);


		public float CellSize
		{
			get { return getCellSize(scene_, entity_.entity_Id_); }
			set { setCellSize(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCellHeight(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCellHeight(IntPtr scene, int cmp, float value);


		public float CellHeight
		{
			get { return getCellHeight(scene_, entity_.entity_Id_); }
			set { setCellHeight(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getWalkableSlopeAngle(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setWalkableSlopeAngle(IntPtr scene, int cmp, float value);


		public float WalkableSlopeAngle
		{
			get { return getWalkableSlopeAngle(scene_, entity_.entity_Id_); }
			set { setWalkableSlopeAngle(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMaxClimb(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaxClimb(IntPtr scene, int cmp, float value);


		public float MaxClimb
		{
			get { return getMaxClimb(scene_, entity_.entity_Id_); }
			set { setMaxClimb(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getAutoload(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAutoload(IntPtr scene, int cmp, bool value);


		public bool IsAutoload
		{
			get { return getAutoload(scene_, entity_.entity_Id_); }
			set { setAutoload(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getDetailed(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDetailed(IntPtr scene, int cmp, bool value);


		public bool IsDetailed
		{
			get { return getDetailed(scene_, entity_.entity_Id_); }
			set { setDetailed(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
