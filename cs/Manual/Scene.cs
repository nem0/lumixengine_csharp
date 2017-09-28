using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
    public partial class Scene
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        protected extern static IntPtr getUniverse(IntPtr instance);
    }
}
