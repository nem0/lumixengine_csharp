using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public interface IResourceType
    {
        /// <summary>
        /// descriptive name of the resoruce type, like "prefab"
        /// </summary>
        string ResourceType
        {
            get;
        }
    }
}
