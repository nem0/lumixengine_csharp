using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public partial class Entity	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getFirstEntity(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getNextEntity(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getEntityName(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEntityName(IntPtr instance, int entity, string name);


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
		extern static System.IntPtr getScene(IntPtr instance, int type);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getScene(IntPtr instance, uint hash);


		public Entity(IntPtr _instance)
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

		public System.IntPtr GetScene(int type)
		{
			return getScene(instance_, type);
		}

		public System.IntPtr GetScene(uint hash)
		{
			return getScene(instance_, hash);
		}

		public static implicit operator System.IntPtr(Entity _value)
		{
			 return _value.instance_;
		}
	}

}
