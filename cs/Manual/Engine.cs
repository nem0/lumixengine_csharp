using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
    public partial class Engine
    {
        static public IntPtr instance_;
        static Engine managedInstance_;
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void logError(string msg);

        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static IntPtr loadResource(IntPtr engine, string path, string type);

        public static Engine Instance
        {
            get
            {
                if(managedInstance_ == null)
                {
                    managedInstance_ = new Engine(instance_);
                }
                return managedInstance_;
            }
        }

        public T LoadResource<T>(string path) where T : Resource, new()
        {
            var ret = new T();
            ret.__Instance = loadResource(instance_, path, ret.GetResourceType());
            return ret;
        }

	    public Engine(IntPtr _instance)
		{
			instance_ = _instance;
		}
    }
}
