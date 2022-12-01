using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


    public partial class Entity
    {
        internal int entity_Id_;
        internal IntPtr instance_;

        public List<Component> components_ = new List<Component>();

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        private extern static string getName(IntPtr universe, int entity);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        private extern static void setName(IntPtr universe, int entity, string name);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        private extern static void destroy(IntPtr universe, int entity);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        private extern static bool hasComponent(IntPtr universe, int entity, string cmp_type);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static IntPtr getScene(IntPtr universe, string cmp_type);
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
		extern static void setLocalPosition(IntPtr instance, int entity, DVec3 pos);


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
		extern static void setPosition(IntPtr instance, int entity, DVec3 pos);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setScale(IntPtr instance, int entity, float scale);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getScale(IntPtr instance, int entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static DVec3 getPosition(IntPtr instance, int entity);


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
			set { setParent(instance_, value == null ? -1 : value.entity_Id_, entity_Id_); }
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

		public Entity FirstEntity
		{
			get
			{
				int x= getFirstEntity(instance_);
				if(x < 0) return null;
				return new Entity(instance_, x);
			}
		}

		public Entity NextEntity
		{
			get
			{
				int x= getNextEntity(instance_, entity_Id_);
				if(x < 0) return null;
				return new Entity(instance_, x);
			}
		}

		public bool IsDescent
		{
			get
			{
				return isDescendant(instance_, entity_Id_, entity_Id_);
			}
		}

		public Entity FirstChild
		{
			get
			{
				int x= getFirstChild(instance_, entity_Id_);
				if(x < 0) return null;
				return new Entity(instance_, x);
			}
		}

		public Entity NextSibling
		{
			get
			{
				int x= getNextSibling(instance_, entity_Id_);
				if(x < 0) return null;
				return new Entity(instance_, x);
			}
		}

		public float LocalScale
		{
			get
			{
				return getLocalScale(instance_, entity_Id_);
			}
		}

		public void SetLocalPosition(DVec3 pos)
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

		public System.IntPtr PositionAndRoatation
		{
			get
			{
				return getPositionAndRotation(instance_, entity_Id_);
			}
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

		public void SetPosition(DVec3 pos)
		{
			setPosition(instance_, entity_Id_, pos);
		}

		public DVec3 GetPosition()
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
        
        public void Destroy()
        {
            destroy(instance_, entity_Id_);
            entity_Id_ = -1;
        }

        public Entity(IntPtr _universe, int _id)
        {
            instance_ = _universe;
            entity_Id_ = _id;
        }
        
		public bool IsNativeComponent<T>(out NativeComponent _attrib)
		{
            _attrib = null;
			System.Attribute[] attrs = System.Attribute.GetCustomAttributes(typeof(T));
			foreach(var attr in attrs)
			{
                if (attr is NativeComponent)
                {
                    _attrib = attr as NativeComponent;
                    return true;
                }
			}
			return false;
		}
		public bool IsNativeComponentBase<T>(out NativeComponentBase _attrib)
        {
            _attrib = null;
            var attrs = Attribute.GetCustomAttributes(typeof(T));
            foreach (var attr in attrs)
            {
                if (attr is NativeComponentBase)
                {
                    _attrib = attr as NativeComponentBase;
                    return true;
                }
            }
            return false;
        }
		public T GetComponent<T>() where T : Component
        {
            for (int i = 0, c = components_.Count; i < c; ++i)
            {
                var cmp = components_[i];
                if (cmp is T) return cmp as T;
            }
            NativeComponent prop;
            NativeComponentBase ncb;
			if (IsNativeComponent<T>(out prop))
            {
                string cmp_type = prop.Type;

                if (!hasComponent(instance_, entity_Id_, cmp_type)) return null;

                var cmp = (T)System.Activator.CreateInstance(typeof(T), new object[] { this });
                components_.Add(cmp);
                return cmp;
            }
            else if(IsNativeComponentBase<T>(out ncb))
            {
                var types = ncb.SupportedTypes;
                for (int k = 0; k < types.Length; k++)
                {
                    if (!hasComponent(instance_, entity_Id_, types[k])) continue;

                    Type t = Type.GetType("Lumix." + types[k].Capitalize('_'));
                    var cmp = (T)System.Activator.CreateInstance(t, new object[] { this });
                    components_.Add(cmp);
                    return cmp;
                }
            }
            return null;
        }


        public T CreateComponent<T>() where T : Component
        {
            T cmp = (T)Activator.CreateInstance(typeof(T), this);
            components_.Add(cmp);
            return cmp;
        }

        public DVec3 Position
        {
            get { return getPosition(instance_, entity_Id_); }
            set { setPosition(instance_, entity_Id_, value); }
        }

		public Vec3 Direction
		{
			get { return Rotation.Rotate(new Vec3(0, 0, 1)); }
		}


        public Quat Rotation
        {
            get { return getRotation(instance_, entity_Id_); }
            set { setRotation(instance_, entity_Id_, value); }
        }

        public Universe Universe
        {
            get { return new Universe(instance_); }
        }

        public static implicit operator int (Entity value)
        {
            return value.entity_Id_;
        }

        public void Rotate(Vec3 _angles)
        {
            _angles.x = _angles.x.ToRadians();
            _angles.y = _angles.y.ToRadians();
            _angles.z = _angles.z.ToRadians();
            Quat rot = Quat.FromEuler(_angles);
            this.Rotation = Rotation * rot;
        }
    }
}