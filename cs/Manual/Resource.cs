using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public partial class Resource
    {
        public IntPtr instance_;
        protected bool disposedValue_ = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue_)
            {
                if (disposing)
                {
                    // managed disposing stuff
                }

                IntPtr rs = getResourceManager(instance_);
                if (rs != IntPtr.Zero)
                {
                    var type = ((IResourceType)this).ResourceType;
                    new ResourceManager(rs).Get(Resources.Hash(type)).Unload(this);
                    instance_ = IntPtr.Zero;
                }
                disposedValue_ = true;
            }
        }
        ~Resource()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
