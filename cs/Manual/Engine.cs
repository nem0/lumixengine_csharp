using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
    public partial class Engine
    {
        static IntPtr instance_;
        static Engine managedInstance_;
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        public extern static void logError(string msg);

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

        public static implicit operator System.IntPtr(Engine _value)
        {
            return instance_;
        }
    }
}
