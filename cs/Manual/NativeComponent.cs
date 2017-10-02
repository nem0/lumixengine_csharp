using System;

namespace Lumix
{
    [AttributeUsage(AttributeTargets.All)]
    public class NativeComponentBase : Attribute
    {
        public string[] SupportedTypes
        {
            get;
            set;
        }
        public NativeComponentBase(params string[] _supportedTypes)
        {
            SupportedTypes = _supportedTypes;
        }
    }

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
