using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public partial class Entity
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void emplaceEntity(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int createEntity(IntPtr instance, Vec3 position, Quat rotation);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void destroyEntity(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addComponent(IntPtr instance, int entity, int component_type, System.IntPtr scene, int index);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void destroyComponent(IntPtr instance, int entity, int component_type, System.IntPtr scene, int index);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool hasComponent(IntPtr instance, int entity, int component_type);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getComponent(IntPtr instance, int entity, int type);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getFirstComponent(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getNextComponent(IntPtr instance, System.IntPtr cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getFirstEntity(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getNextEntity(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getEntityName(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEntityName(IntPtr instance, int entity, string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool hasEntity(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isDescendant(IntPtr instance, int ancestor, int descendant);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getParent(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getFirstChild(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getNextSibling(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getLocalTransform(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLocalScale(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setParent(IntPtr instance, int parent, int child);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLocalPosition(IntPtr instance, int entity, Vec3 pos);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLocalRotation(IntPtr instance, int entity, Quat rot);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLocalTransform(IntPtr instance, int entity, System.IntPtr transform);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr computeLocalTransform(IntPtr instance, int parent, System.IntPtr global_transform);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMatrix(IntPtr instance, int entity, System.IntPtr mtx);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getPositionAndRotation(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getMatrix(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTransform(IntPtr instance, int entity, System.IntPtr transform);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTransform(IntPtr instance, int entity, Vec3 pos, Quat rot, float scale);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTransformKeepChildren(IntPtr instance, int entity, System.IntPtr transform);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getTransform(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRotation(IntPtr instance, int entity, float x, float y, float z, float w);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRotation(IntPtr instance, int entity, Quat rot);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPosition(IntPtr instance, int entity, float x, float y, float z);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPosition(IntPtr instance, int entity, Vec3 pos);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setScale(IntPtr instance, int entity, float scale);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getScale(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getPosition(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Quat getRotation(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getName(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setName(IntPtr instance, string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void serializeComponent(IntPtr instance, System.IntPtr serializer, int type, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void deserializeComponent(IntPtr instance, System.IntPtr serializer, int entity, int type, int scene_version);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void serialize(IntPtr instance, System.IntPtr serializer);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void deserialize(IntPtr instance, System.IntPtr serializer);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getScene(IntPtr instance, int type);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getScene(IntPtr instance, uint hash);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addScene(IntPtr instance, System.IntPtr scene);


		internal Entity(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public string EntityName
		{
			get { return getEntityName(instance_, entity_Id_); }
			set { setEntityName(instance_, entity_Id_, value); }
		}

		public Entity Parent
		{
			get { 
			 int x = getParent(instance_, entity_Id_);
			 if(x < 0) return null;
			  return new Entity(instance_, x);
			}
			set { setParent(instance_, entity_Id_, value); }
		}

		public System.IntPtr LocalTransform
		{
			get { return getLocalTransform(instance_, entity_Id_); }
			set { setLocalTransform(instance_, entity_Id_, value); }
		}

		public System.IntPtr Matrix
		{
			get { return getMatrix(instance_, entity_Id_); }
			set { setMatrix(instance_, entity_Id_, value); }
		}

		public float Scale
		{
			get { return getScale(instance_, entity_Id_); }
			set { setScale(instance_, entity_Id_, value); }
		}

		public string Name
		{
			get { return getName(instance_, entity_Id_); }
			set { setName(instance_, entity_Id_, value); }
		}

		public void EmplaceEntity()
		{
			emplaceEntity(instance_, entity_Id_);
		}

		public Entity CreateEntity(Vec3 position, Quat rotation)
		{
			int x = createEntity(instance_, position, rotation);
			 if(x < 0) return null;
			return new Entity(instance_, x);
		}

		public void DestroyEntity()
		{
			destroyEntity(instance_, entity_Id_);
		}

		public void AddComponent(int component_type, System.IntPtr scene, int index)
		{
			addComponent(instance_, entity_Id_, component_type, scene, index);
		}

		public void DestroyComponent(int component_type, System.IntPtr scene, int index)
		{
			destroyComponent(instance_, entity_Id_, component_type, scene, index);
		}

		public bool HasComponent(int component_type)
		{
			return hasComponent(instance_, entity_Id_, component_type);
		}

		public System.IntPtr GetComponent(int type)
		{
			return getComponent(instance_, entity_Id_, type);
		}

		public System.IntPtr GetFirstComponent()
		{
			return getFirstComponent(instance_, entity_Id_);
		}

		public System.IntPtr GetNextComponent(System.IntPtr cmp)
		{
			return getNextComponent(instance_, cmp);
		}

		public Entity GetFirstEntity()
		{
			int x = getFirstEntity(instance_);
			 if(x < 0) return null;
			return new Entity(instance_, x);
		}

		public Entity GetNextEntity()
		{
			int x = getNextEntity(instance_, entity_Id_);
			 if(x < 0) return null;
			return new Entity(instance_, x);
		}

		public bool HasEntity()
		{
			return hasEntity(instance_, entity_Id_);
		}

		public bool IsDescendant()
		{
			return isDescendant(instance_, entity_Id_, entity_Id_);
		}

		public Entity GetFirstChild()
		{
			int x = getFirstChild(instance_, entity_Id_);
			 if(x < 0) return null;
			return new Entity(instance_, x);
		}

		public Entity GetNextSibling()
		{
			int x = getNextSibling(instance_, entity_Id_);
			 if(x < 0) return null;
			return new Entity(instance_, x);
		}

		public float GetLocalScale()
		{
			return getLocalScale(instance_, entity_Id_);
		}

		public void SetLocalPosition(Vec3 pos)
		{
			setLocalPosition(instance_, entity_Id_, pos);
		}

		public void SetLocalRotation(Quat rot)
		{
			setLocalRotation(instance_, entity_Id_, rot);
		}

		public System.IntPtr ComputeLocalTransform(System.IntPtr global_transform)
		{
			return computeLocalTransform(instance_, entity_Id_, global_transform);
		}

		public System.IntPtr GetPositionAndRotation()
		{
			return getPositionAndRotation(instance_, entity_Id_);
		}

		public void SetTransform(System.IntPtr transform)
		{
			setTransform(instance_, entity_Id_, transform);
		}

		public void SetTransform(Vec3 pos, Quat rot, float scale)
		{
			setTransform(instance_, entity_Id_, pos, rot, scale);
		}

		public void SetTransformKeepChildren(System.IntPtr transform)
		{
			setTransformKeepChildren(instance_, entity_Id_, transform);
		}

		public System.IntPtr GetTransform()
		{
			return getTransform(instance_, entity_Id_);
		}

		public void SetRotation(float x, float y, float z, float w)
		{
			setRotation(instance_, entity_Id_, x, y, z, w);
		}

		public void SetRotation(Quat rot)
		{
			setRotation(instance_, entity_Id_, rot);
		}

		public void SetPosition(float x, float y, float z)
		{
			setPosition(instance_, entity_Id_, x, y, z);
		}

		public void SetPosition(Vec3 pos)
		{
			setPosition(instance_, entity_Id_, pos);
		}

		public Vec3 GetPosition()
		{
			return getPosition(instance_, entity_Id_);
		}

		public Quat GetRotation()
		{
			return getRotation(instance_, entity_Id_);
		}

		public void SerializeComponent(System.IntPtr serializer, int type, int cmp)
		{
			serializeComponent(instance_, serializer, type, cmp);
		}

		public void DeserializeComponent(System.IntPtr serializer, int type, int scene_version)
		{
			deserializeComponent(instance_, serializer, entity_Id_, type, scene_version);
		}

		public void Serialize(System.IntPtr serializer)
		{
			serialize(instance_, serializer);
		}

		public void Deserialize(System.IntPtr serializer)
		{
			deserialize(instance_, serializer);
		}

		public System.IntPtr GetScene(int type)
		{
			return getScene(instance_, type);
		}

		public System.IntPtr GetScene(uint hash)
		{
			return getScene(instance_, hash);
		}

		public void AddScene(System.IntPtr scene)
		{
			addScene(instance_, scene);
		}

	}

}
