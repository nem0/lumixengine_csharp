using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public class CameraOffset : Component
    {
        public Entity Player;
        Vec3 offset_;
        void OnStartGame()
        {
            if (Player == null)
            {
                Engine.logError("Player not set :(");
                return;
            }
            offset_ = entity.Position - Player.Position;
        }

        void Update(float _deltaTime)
        {
            if (Player == null)
                return;

            entity.Position  = Player.Position + offset_;
        }
    }
}
