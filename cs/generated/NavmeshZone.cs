using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "navmesh_zone")]
	public class NavmeshZone : Component
	{
		public NavmeshZone(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "navmesh_zone" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getExtents(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setExtents(IntPtr module, int cmp, Vec3 value);


		public Vec3 Extents
		{
			get { return getExtents(module_, entity_.entity_Id_); }
			set { setExtents(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAgentHeight(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAgentHeight(IntPtr module, int cmp, float value);


		public float AgentHeight
		{
			get { return getAgentHeight(module_, entity_.entity_Id_); }
			set { setAgentHeight(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getAgentRadius(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAgentRadius(IntPtr module, int cmp, float value);


		public float AgentRadius
		{
			get { return getAgentRadius(module_, entity_.entity_Id_); }
			set { setAgentRadius(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCellSize(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCellSize(IntPtr module, int cmp, float value);


		public float CellSize
		{
			get { return getCellSize(module_, entity_.entity_Id_); }
			set { setCellSize(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getCellHeight(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCellHeight(IntPtr module, int cmp, float value);


		public float CellHeight
		{
			get { return getCellHeight(module_, entity_.entity_Id_); }
			set { setCellHeight(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getWalkableSlopeAngle(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setWalkableSlopeAngle(IntPtr module, int cmp, float value);


		public float WalkableSlopeAngle
		{
			get { return getWalkableSlopeAngle(module_, entity_.entity_Id_); }
			set { setWalkableSlopeAngle(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMaxClimb(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaxClimb(IntPtr module, int cmp, float value);


		public float MaxClimb
		{
			get { return getMaxClimb(module_, entity_.entity_Id_); }
			set { setMaxClimb(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getAutoload(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAutoload(IntPtr module, int cmp, bool value);


		public bool IsAutoload
		{
			get { return getAutoload(module_, entity_.entity_Id_); }
			set { setAutoload(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getDetailed(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDetailed(IntPtr module, int cmp, bool value);


		public bool IsDetailed
		{
			get { return getDetailed(module_, entity_.entity_Id_); }
			set { setDetailed(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool loadZone(IntPtr instance, int cmp);

		public bool LoadZone()
		{
			return loadZone(module_, entity_.entity_Id_);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void debugDrawContours(IntPtr instance, int cmp);

		public void DebugDrawContours()
		{
			debugDrawContours(module_, entity_.entity_Id_);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void debugDrawNavmesh(IntPtr instance, int cmp, DVec3 a0, bool a1, bool a2, bool a3);

		public void DebugDrawNavmesh(DVec3 a0, bool a1, bool a2, bool a3)
		{
			debugDrawNavmesh(module_, entity_.entity_Id_, a0, a1, a2, a3);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void debugDrawCompactHeightfield(IntPtr instance, int cmp);

		public void DebugDrawCompactHeightfield()
		{
			debugDrawCompactHeightfield(module_, entity_.entity_Id_);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void debugDrawHeightfield(IntPtr instance, int cmp);

		public void DebugDrawHeightfield()
		{
			debugDrawHeightfield(module_, entity_.entity_Id_);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static IntPtr generateNavmesh(IntPtr instance, int cmp);

		public IntPtr GenerateNavmesh()
		{
			return generateNavmesh(module_, entity_.entity_Id_);
		}

	} // class
} // namespace
