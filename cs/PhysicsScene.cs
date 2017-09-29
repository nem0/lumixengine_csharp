using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class PhysicsScene : IScene
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int raycast(IntPtr instance, Vec3 origin, Vec3 dir, int ignore_entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool raycastEx(IntPtr instance, Vec3 origin, Vec3 dir, float distance, System.IntPtr result, int ignored);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getCollisionLayerName(IntPtr instance, int index);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setCollisionLayerName(IntPtr instance, int index, string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool canLayersCollide(IntPtr instance, int layer1, int layer2);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayersCanCollide(IntPtr instance, int layer1, int layer2, bool can_collide);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getCollisionsLayersCount(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addCollisionLayer(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void removeCollisionLayer(IntPtr instance);


		public PhysicsScene(IntPtr _instance)
			:base(_instance){ }

		public Entity Raycast(Vec3 origin, Vec3 dir, Entity ignore_entity)
		{
			int x = raycast(instance_, origin, dir, ignore_entity);
			 if(x < 0) return null;
			return new Entity(getUniverse(instance_), x);
		}

		public bool RaycastEx(Vec3 origin, Vec3 dir, float distance, System.IntPtr result, Entity ignored)
		{
			return raycastEx(instance_, origin, dir, distance, result, ignored);
		}

		public string GetCollisionLayerName(int index)
		{
			return getCollisionLayerName(instance_, index);
		}

		public void SetCollisionLayerName(int index, string name)
		{
			setCollisionLayerName(instance_, index, name);
		}

		public bool CanLayersCollide(int layer1, int layer2)
		{
			return canLayersCollide(instance_, layer1, layer2);
		}

		public void SetLayersCanCollide(int layer1, int layer2, bool can_collide)
		{
			setLayersCanCollide(instance_, layer1, layer2, can_collide);
		}

		public int GetCollisionsLayersCount()
		{
			return getCollisionsLayersCount(instance_);
		}

		public void AddCollisionLayer()
		{
			addCollisionLayer(instance_);
		}

		public void RemoveCollisionLayer()
		{
			removeCollisionLayer(instance_);
		}

		public static implicit operator System.IntPtr(PhysicsScene _value)
		{
			 return _value.instance_;
		}
	}

}
