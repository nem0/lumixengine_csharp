using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public class Rotator : Component
    {
        Vec3 rotate_;
        void OnStartGame()
        {
            rotate_ = new Vec3(15f, 30f, 45f);
        }
        void Update(float _deltaTime)
        {
            entity.Rotate(rotate_ * _deltaTime);
        }
    }
}
