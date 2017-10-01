using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public partial class RigidActor : Component
    {
        public RigidActor(Entity _entity, int _cmpId, IntPtr _scene)
            : base(_entity, _cmpId, _scene) { }
    }
}
