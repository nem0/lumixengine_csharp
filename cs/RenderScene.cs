using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class RenderScene : IScene
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugTriangle(IntPtr instance, Vec3 p0, Vec3 p1, Vec3 p2, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugPoint(IntPtr instance, Vec3 pos, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCone(IntPtr instance, Vec3 vertex, Vec3 dir, Vec3 axis0, Vec3 axis1, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugLine(IntPtr instance, Vec3 from, Vec3 to, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCross(IntPtr instance, Vec3 center, float size, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCube(IntPtr instance, Vec3 pos, Vec3 dir, Vec3 up, Vec3 right, uint color, float life);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCube(IntPtr instance, Vec3 from, Vec3 max, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCubeSolid(IntPtr instance, Vec3 from, Vec3 max, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCircle(IntPtr instance, Vec3 center, Vec3 up, float radius, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugSphere(IntPtr instance, Vec3 center, float radius, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugFrustum(IntPtr instance, Vec3 position, Vec3 direction, Vec3 up, float fov, float ratio, float near_distance, float far_distance, uint color, float life);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugFrustum(IntPtr instance, System.IntPtr frustum, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCapsule(IntPtr instance, Vec3 position, float height, float radius, uint color, float life);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCapsule(IntPtr instance, System.IntPtr transform, float height, float radius, uint color, float life);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addDebugCylinder(IntPtr instance, Vec3 position, Vec3 up, float radius, uint color, float life);


		internal RenderScene(IntPtr _instance)
			:base(_instance){ }

		public void AddDebugTriangle(Vec3 p0, Vec3 p1, Vec3 p2, uint color, float life)
		{
			addDebugTriangle(instance_, p0, p1, p2, color, life);
		}

		public void AddDebugPoint(Vec3 pos, uint color, float life)
		{
			addDebugPoint(instance_, pos, color, life);
		}

		public void AddDebugCone(Vec3 vertex, Vec3 dir, Vec3 axis0, Vec3 axis1, uint color, float life)
		{
			addDebugCone(instance_, vertex, dir, axis0, axis1, color, life);
		}

		public void AddDebugLine(Vec3 from, Vec3 to, uint color, float life)
		{
			addDebugLine(instance_, from, to, color, life);
		}

		public void AddDebugCross(Vec3 center, float size, uint color, float life)
		{
			addDebugCross(instance_, center, size, color, life);
		}

		public void AddDebugCube(Vec3 pos, Vec3 dir, Vec3 up, Vec3 right, uint color, float life)
		{
			addDebugCube(instance_, pos, dir, up, right, color, life);
		}

		public void AddDebugCube(Vec3 from, Vec3 max, uint color, float life)
		{
			addDebugCube(instance_, from, max, color, life);
		}

		public void AddDebugCubeSolid(Vec3 from, Vec3 max, uint color, float life)
		{
			addDebugCubeSolid(instance_, from, max, color, life);
		}

		public void AddDebugCircle(Vec3 center, Vec3 up, float radius, uint color, float life)
		{
			addDebugCircle(instance_, center, up, radius, color, life);
		}

		public void AddDebugSphere(Vec3 center, float radius, uint color, float life)
		{
			addDebugSphere(instance_, center, radius, color, life);
		}

		public void AddDebugFrustum(Vec3 position, Vec3 direction, Vec3 up, float fov, float ratio, float near_distance, float far_distance, uint color, float life)
		{
			addDebugFrustum(instance_, position, direction, up, fov, ratio, near_distance, far_distance, color, life);
		}

		public void AddDebugFrustum(System.IntPtr frustum, uint color, float life)
		{
			addDebugFrustum(instance_, frustum, color, life);
		}

		public void AddDebugCapsule(Vec3 position, float height, float radius, uint color, float life)
		{
			addDebugCapsule(instance_, position, height, radius, color, life);
		}

		public void AddDebugCapsule(System.IntPtr transform, float height, float radius, uint color, float life)
		{
			addDebugCapsule(instance_, transform, height, radius, color, life);
		}

		public void AddDebugCylinder(Vec3 position, Vec3 up, float radius, uint color, float life)
		{
			addDebugCylinder(instance_, position, up, radius, color, life);
		}

		public static implicit operator System.IntPtr(RenderScene _value)
		{
			 return _value.instance_;
		}
	}

}
