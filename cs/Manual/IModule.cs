using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
    public class IModule
    {
        public IntPtr instance_;
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        protected extern static IntPtr getWorld(IntPtr instance);

        public World World { get { return new World(getWorld(instance_)); }}

        public IModule(IntPtr _instance)
        {
            instance_ = _instance;
        }
    }
}
