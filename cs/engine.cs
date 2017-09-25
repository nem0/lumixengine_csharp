using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public static class Universe
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr createEntity(Vec3 position, System.IntPtr rotation);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getFirstEntity();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getNextEntity(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getEntityByName(string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getName();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setName(string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getScene(System.IntPtr type);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getScene(System.IntPtr hash);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void destroyEntity(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addComponent(System.IntPtr entity, System.IntPtr component_type, System.IntPtr scene, int index);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void destroyComponent(System.IntPtr entity, System.IntPtr component_type, System.IntPtr scene, int index);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool hasComponent(System.IntPtr entity, System.IntPtr component_type);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getComponent(System.IntPtr entity, System.IntPtr type);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getEntityName(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEntityName(System.IntPtr entity, string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isDescendant(System.IntPtr ancestor, System.IntPtr descendant);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getParent(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getFirstChild(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getNextSibling(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getLocalTransform(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLocalScale(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParent(System.IntPtr parent, System.IntPtr child);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLocalPosition(System.IntPtr entity, Vec3 pos);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLocalRotation(System.IntPtr entity, System.IntPtr rot);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLocalTransform(System.IntPtr entity, System.IntPtr transform);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr computeLocalTransform(System.IntPtr parent, System.IntPtr global_transform);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMatrix(System.IntPtr entity, System.IntPtr mtx);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getPositionAndRotation(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getMatrix(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTransform(System.IntPtr entity, System.IntPtr transform);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTransform(System.IntPtr entity, Vec3 pos, System.IntPtr rot, float scale);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTransformKeepChildren(System.IntPtr entity, System.IntPtr transform);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getTransform(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setScale(System.IntPtr entity, float scale);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getScale(System.IntPtr entity);


		public static System.IntPtr CreateEntity(Vec3 position, System.IntPtr rotation)
		{
			return createEntity(position, rotation);
		}

		public static System.IntPtr GetFirstEntity()
		{
			return getFirstEntity();
		}

		public static System.IntPtr GetNextEntity(System.IntPtr entity)
		{
			return getNextEntity(entity);
		}

		public static System.IntPtr GetEntityByName(string name)
		{
			return getEntityByName(name);
		}

		public static string GetName()
		{
			return getName();
		}

		public static void SetName(string name)
		{
			setName(name);
		}

		public static System.IntPtr GetScene(System.IntPtr type)
		{
			return getScene(type);
		}

		public static System.IntPtr GetScene(System.IntPtr hash)
		{
			return getScene(hash);
		}

		public static void DestroyEntity(System.IntPtr entity)
		{
			destroyEntity(entity);
		}

		public static void AddComponent(System.IntPtr entity, System.IntPtr component_type, System.IntPtr scene, )
		{
			addComponent(entity, component_type, scene, componentId_);
		}

		public static void DestroyComponent(System.IntPtr entity, System.IntPtr component_type, System.IntPtr scene, )
		{
			destroyComponent(entity, component_type, scene, componentId_);
		}

		public static bool HasComponent(System.IntPtr entity, System.IntPtr component_type)
		{
			return hasComponent(entity, component_type);
		}

		public static System.IntPtr GetComponent(System.IntPtr entity, System.IntPtr type)
		{
			return getComponent(entity, type);
		}

		public static string GetEntityName(System.IntPtr entity)
		{
			return getEntityName(entity);
		}

		public static void SetEntityName(System.IntPtr entity, string name)
		{
			setEntityName(entity, name);
		}

		public static bool IsDescendant(System.IntPtr ancestor, System.IntPtr descendant)
		{
			return isDescendant(ancestor, descendant);
		}

		public static System.IntPtr GetParent(System.IntPtr entity)
		{
			return getParent(entity);
		}

		public static System.IntPtr GetFirstChild(System.IntPtr entity)
		{
			return getFirstChild(entity);
		}

		public static System.IntPtr GetNextSibling(System.IntPtr entity)
		{
			return getNextSibling(entity);
		}

		public static System.IntPtr GetLocalTransform(System.IntPtr entity)
		{
			return getLocalTransform(entity);
		}

		public static float GetLocalScale(System.IntPtr entity)
		{
			return getLocalScale(entity);
		}

		public static void SetParent(System.IntPtr parent, System.IntPtr child)
		{
			setParent(parent, child);
		}

		public static void SetLocalPosition(System.IntPtr entity, Vec3 pos)
		{
			setLocalPosition(entity, pos);
		}

		public static void SetLocalRotation(System.IntPtr entity, System.IntPtr rot)
		{
			setLocalRotation(entity, rot);
		}

		public static void SetLocalTransform(System.IntPtr entity, System.IntPtr transform)
		{
			setLocalTransform(entity, transform);
		}

		public static System.IntPtr ComputeLocalTransform(System.IntPtr parent, System.IntPtr global_transform)
		{
			return computeLocalTransform(parent, global_transform);
		}

		public static void SetMatrix(System.IntPtr entity, System.IntPtr mtx)
		{
			setMatrix(entity, mtx);
		}

		public static System.IntPtr GetPositionAndRotation(System.IntPtr entity)
		{
			return getPositionAndRotation(entity);
		}

		public static System.IntPtr GetMatrix(System.IntPtr entity)
		{
			return getMatrix(entity);
		}

		public static void SetTransform(System.IntPtr entity, System.IntPtr transform)
		{
			setTransform(entity, transform);
		}

		public static void SetTransform(System.IntPtr entity, Vec3 pos, System.IntPtr rot, float scale)
		{
			setTransform(entity, pos, rot, scale);
		}

		public static void SetTransformKeepChildren(System.IntPtr entity, System.IntPtr transform)
		{
			setTransformKeepChildren(entity, transform);
		}

		public static System.IntPtr GetTransform(System.IntPtr entity)
		{
			return getTransform(entity);
		}

		public static void SetScale(System.IntPtr entity, float scale)
		{
			setScale(entity, scale);
		}

		public static float GetScale(System.IntPtr entity)
		{
			return getScale(entity);
		}

	}

}
