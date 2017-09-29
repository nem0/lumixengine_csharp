using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
    public class IScene
    {
        internal IntPtr instance_;
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        protected extern static IntPtr getUniverse(IntPtr instance);

        public IScene(IntPtr _instance)
        {
            instance_ = _instance;
        }
    }
}
