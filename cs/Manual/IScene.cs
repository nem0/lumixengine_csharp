using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
    public class IScene
    {
        public IntPtr instance_;
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        protected extern static IntPtr getUniverse(IntPtr instance);

        public Universe Universe { get { return new Universe(getUniverse(instance_)); }}

        public IScene(IntPtr _instance)
        {
            instance_ = _instance;
        }
    }
}
