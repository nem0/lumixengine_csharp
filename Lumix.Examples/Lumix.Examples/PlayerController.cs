using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public class PlayerController : Component
    {
        RigidActor rigidActor_;

        public float Speed = 20.0f;

        void OnStartGame()
        {
            rigidActor_ = GetComponent<RigidActor>();

            Input.AddAction(Actions.FORWARD_ACTION, Input.InputType.PRESSED, 'W', -1);
            Input.AddAction(Actions.BACK_ACTION, Input.InputType.PRESSED, 'S', -1);
            Input.AddAction(Actions.LEFT_ACTION, Input.InputType.PRESSED, 'A', -1);
            Input.AddAction(Actions.RIGHT_ACTION, Input.InputType.PRESSED, 'D', -1);
        }

        void Update()
        {
            if (rigidActor_ == null)
                return;

            float left = Input.GetActionValue(Actions.LEFT_ACTION);
            float right = Input.GetActionValue(Actions.RIGHT_ACTION);
            float up  = Input.GetActionValue(Actions.FORWARD_ACTION);
            float down = Input.GetActionValue(Actions.BACK_ACTION);

            float horizontal = left > 0 ? -left : right > 0 ? right : 0;
            float vertical = up > 0 ? -up : down > 0 ? down : 0;

            Vec3 movement = new Vec3(horizontal, 0, vertical);

            rigidActor_.ApplyForceToActor(movement * Speed);
        }

       
    }
}
