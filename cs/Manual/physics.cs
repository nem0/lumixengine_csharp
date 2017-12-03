using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
     [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RaycastHit
    {
        public Vec3 Position;
        public Vec3 Normal;
        public int EntityID;
    }
}
