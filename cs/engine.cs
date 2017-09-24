using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public static class Universe
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getAllocator();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void emplaceEntity(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr createEntity(Vec3 position, System.IntPtr rotation);


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
		extern static System.IntPtr getFirstComponent(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getNextComponent(System.IntPtr cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr registerComponentType(System.IntPtr type);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getFirstEntity();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getNextEntity(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getEntityName(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getEntityByName(string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEntityName(System.IntPtr entity, string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool hasEntity(System.IntPtr entity);


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
		extern static void setRotation(System.IntPtr entity, float x, float y, float z, float w);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRotation(System.IntPtr entity, System.IntPtr rot);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPosition(System.IntPtr entity, float x, float y, float z);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPosition(System.IntPtr entity, Vec3 pos);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setScale(System.IntPtr entity, float scale);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr instantiatePrefab(System.IntPtr prefab, Vec3 pos, System.IntPtr rot, float scale);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getScale(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getPosition(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getRotation(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getName();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setName(string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr entityTransformed(System.IntPtr );


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr entityCreated(System.IntPtr );


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr entityDestroyed(System.IntPtr );


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr componentDestroyed(System.IntPtr );


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr componentAdded(System.IntPtr );


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void serializeComponent(System.IntPtr serializer, System.IntPtr type, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void deserializeComponent(System.IntPtr serializer, System.IntPtr entity, System.IntPtr type, int scene_version);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void serialize(System.IntPtr serializer);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void deserialize(System.IntPtr serializer);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getScene(System.IntPtr type);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getScene(System.IntPtr hash);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getScenes();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addScene(System.IntPtr scene);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void transformEntity(System.IntPtr entity, bool update_local);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void updateGlobalTransform(System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void EntityData();


		public static System.IntPtr GetAllocator()
		{
			return getAllocator();
		}

		public static void EmplaceEntity(System.IntPtr entity)
		{
			emplaceEntity(entity);
		}

		public static System.IntPtr CreateEntity(Vec3 position, System.IntPtr rotation)
		{
			return createEntity(position, rotation);
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

		public static System.IntPtr GetFirstComponent(System.IntPtr entity)
		{
			return getFirstComponent(entity);
		}

		public static System.IntPtr GetNextComponent(System.IntPtr cmp)
		{
			return getNextComponent(cmp);
		}

		public static System.IntPtr RegisterComponentType(System.IntPtr type)
		{
			return registerComponentType(type);
		}

		public static System.IntPtr GetFirstEntity()
		{
			return getFirstEntity();
		}

		public static System.IntPtr GetNextEntity(System.IntPtr entity)
		{
			return getNextEntity(entity);
		}

		public static string GetEntityName(System.IntPtr entity)
		{
			return getEntityName(entity);
		}

		public static System.IntPtr GetEntityByName(string name)
		{
			return getEntityByName(name);
		}

		public static void SetEntityName(System.IntPtr entity, string name)
		{
			setEntityName(entity, name);
		}

		public static bool HasEntity(System.IntPtr entity)
		{
			return hasEntity(entity);
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

		public static void SetRotation(System.IntPtr entity, float x, float y, float z, float w)
		{
			setRotation(entity, x, y, z, w);
		}

		public static void SetRotation(System.IntPtr entity, System.IntPtr rot)
		{
			setRotation(entity, rot);
		}

		public static void SetPosition(System.IntPtr entity, float x, float y, float z)
		{
			setPosition(entity, x, y, z);
		}

		public static void SetPosition(System.IntPtr entity, Vec3 pos)
		{
			setPosition(entity, pos);
		}

		public static void SetScale(System.IntPtr entity, float scale)
		{
			setScale(entity, scale);
		}

		public static System.IntPtr InstantiatePrefab(System.IntPtr prefab, Vec3 pos, System.IntPtr rot, float scale)
		{
			return instantiatePrefab(prefab, pos, rot, scale);
		}

		public static float GetScale(System.IntPtr entity)
		{
			return getScale(entity);
		}

		public static System.IntPtr GetPosition(System.IntPtr entity)
		{
			return getPosition(entity);
		}

		public static System.IntPtr GetRotation(System.IntPtr entity)
		{
			return getRotation(entity);
		}

		public static string GetName()
		{
			return getName();
		}

		public static void SetName(string name)
		{
			setName(name);
		}

		public static System.IntPtr EntityTransformed(System.IntPtr )
		{
			return entityTransformed();
		}

		public static System.IntPtr EntityCreated(System.IntPtr )
		{
			return entityCreated();
		}

		public static System.IntPtr EntityDestroyed(System.IntPtr )
		{
			return entityDestroyed();
		}

		public static System.IntPtr ComponentDestroyed(System.IntPtr )
		{
			return componentDestroyed();
		}

		public static System.IntPtr ComponentAdded(System.IntPtr )
		{
			return componentAdded();
		}

		public static void SerializeComponent(System.IntPtr serializer, System.IntPtr type, )
		{
			serializeComponent(serializer, type, componentId_);
		}

		public static void DeserializeComponent(System.IntPtr serializer, System.IntPtr entity, System.IntPtr type, int scene_version)
		{
			deserializeComponent(serializer, entity, type, scene_version);
		}

		public static void Serialize(System.IntPtr serializer)
		{
			serialize(serializer);
		}

		public static void Deserialize(System.IntPtr serializer)
		{
			deserialize(serializer);
		}

		public static System.IntPtr GetScene(System.IntPtr type)
		{
			return getScene(type);
		}

		public static System.IntPtr GetScene(System.IntPtr hash)
		{
			return getScene(hash);
		}

		public static System.IntPtr GetScenes()
		{
			return getScenes();
		}

		public static void AddScene(System.IntPtr scene)
		{
			addScene(scene);
		}

		public static void TransformEntity(System.IntPtr entity, bool update_local)
		{
			transformEntity(entity, update_local);
		}

		public static void UpdateGlobalTransform(System.IntPtr entity)
		{
			updateGlobalTransform(entity);
		}

		public static void EntityData()
		{
			return EntityData();
		}

	}

}
