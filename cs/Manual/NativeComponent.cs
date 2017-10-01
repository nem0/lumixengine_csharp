using System;

namespace Lumix
{
    [AttributeUsage(AttributeTargets.All)]
    public class NativeComponent : Attribute
    {
        public string Type
        {
            get;
            set;
        }
    }
}
