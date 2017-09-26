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
        private List<Component> components = new List<Component>();

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        private extern static string getName(IntPtr universe, int entity);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        private extern static void setName(IntPtr universe, int entity, string name);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        private extern static void destroy(IntPtr universe, int entity);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        private extern static int getComponent(IntPtr universe, int entity, string cmp_type);

        public void Destroy()
        {
            destroy(instance_, entity_Id_);
            entity_Id_ = -1;
        }


        public T GetComponent<T>() where T : Component
        {
            for (int i = 0, c = components.Count; i < c; ++i)
            {
                var cmp = components[i];
                if (cmp is T) return cmp as T;
            }

            if (typeof(T).IsSubclassOf(typeof(NativeComponent)))
            {
                var prop = typeof(T).GetProperty("GetCmpType", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                string cmp_type = (string)prop.GetValue(null, null);

                int cmp_id = getComponent(instance_, entity_Id_, cmp_type);
                if (cmp_id < 0) return null;

                var cmp = (T)Activator.CreateInstance(typeof(T), this, cmp_id);
                components.Add(cmp);
                return cmp;
            }

            return null;
        }


        public T CreateComponent<T>() where T : Component
        {
            T cmp = (T)Activator.CreateInstance(typeof(T), this);
            components.Add(cmp);
            return cmp;
        }



        public Vec3 Position
        {
            get { return getPosition(instance_, entity_Id_); }
            set { setPosition(instance_, entity_Id_, value); }
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

    }
}