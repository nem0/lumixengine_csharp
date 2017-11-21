using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{
    public class Universe
    {
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getEntity(IntPtr universe, int entity_id);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static IntPtr getSceneByName(IntPtr universe, string name);

		public Universe(IntPtr _instance)
		{
			instance_ = _instance;
		}

        public IntPtr instance_;

        public Entity GetEntity(int entity_id)
        {
            return getEntity(instance_, entity_id);
        }

        public T GetScene<T>() where T : IScene
        {
            var prop = typeof(T).GetProperty("Type", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            string scene_type_name = (string)prop.GetValue(null, null);
            IntPtr native_scene = getSceneByName(instance_, scene_type_name);
            return (T)System.Activator.CreateInstance(typeof(T), new object[] { native_scene });
        }

    }
}