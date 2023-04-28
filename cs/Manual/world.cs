using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{
    public class World
    {
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getEntity(IntPtr world, int entity_id);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static IntPtr getModuleByName(IntPtr world, string name);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int instantiatePrefab(IntPtr world, IntPtr resource, Vec3 pos, Quat rot, float scale);

		public World(IntPtr _instance)
		{
			instance_ = _instance;
		}

        public IntPtr instance_;

        public Entity InstantiatePrefab(PrefabResource prefab, Vec3 pos, Quat rot, float scale)
        {
            int entity_id = instantiatePrefab(instance_, prefab.__Instance, pos, rot, scale);
            return getEntity(instance_, entity_id);
        }

        public Entity GetEntity(int entity_id)
        {
            return getEntity(instance_, entity_id);
        }

        public T GetModule<T>() where T : IModule
        {
            var prop = typeof(T).GetProperty("Type", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            string module_type_name = (string)prop.GetValue(null, null);
            IntPtr native_module = getModuleByName(instance_, module_type_name);
            return (T)System.Activator.CreateInstance(typeof(T), new object[] { native_module });
        }

    }
}