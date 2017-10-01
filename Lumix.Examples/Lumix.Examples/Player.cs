using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public class Player : Component
    {
        PhysicalController physicalController_;
        const int LSHIFT_KEY = 160;

        public float MOUSE_SENSITIVITY = 1.0f;

        public float Speed = 1.0f;
        float yaw_ = 0f;
        float pitch_ = 0f;

        public Entity Camera;
        public bool IsInputEnabled = true;
        void OnStartGame()
        {
            physicalController_ = CreateComponent<PhysicalController>();

            Input.AddAction(Actions.FORWARD_ACTION, Input.InputType.PRESSED, 'W', -1);
            Input.AddAction(Actions.BACK_ACTION, Input.InputType.PRESSED, 'S', -1);
            Input.AddAction(Actions.LEFT_ACTION, Input.InputType.PRESSED, 'A', -1);
            Input.AddAction(Actions.RIGHT_ACTION, Input.InputType.PRESSED, 'D', -1);


            Input.AddAction(Actions.ROT_H_ACTION, Input.InputType.MOUSE_X, 0, -1);
            Input.AddAction(Actions.ROT_V_ACTION, Input.InputType.MOUSE_Y, 0, -1);
            Input.AddAction(Actions.SPRINT_ACTION, Input.InputType.PRESSED, LSHIFT_KEY, -1);

            Input.AddAction(Actions.CONTROLLER_ROT_X, Input.InputType.RTHUMB_X, 0, 0);
            Input.AddAction(Actions.CONTROLLER_ROT_Y, Input.InputType.RTHUMB_Y, 0, 0);
            Input.AddAction(Actions.CONTROLLER_MOVE_X, Input.InputType.LTHUMB_X, 0, 0);
            Input.AddAction(Actions.CONTROLLER_MOVE_X, Input.InputType.LTHUMB_X, 0, 0);
        }

        void Update(float _deltaTime)
        {
            if (Camera == null)
                return;

            yaw_ = yaw_ + Input.GetActionValue(Actions.ROT_H_ACTION) * -0.01f * MOUSE_SENSITIVITY;
            pitch_ = pitch_ + Input.GetActionValue(Actions.ROT_V_ACTION) * -0.01f * MOUSE_SENSITIVITY;

            yaw_ = yaw_ + Input.GetActionValue(Actions.CONTROLLER_ROT_X) * -0.03f * MOUSE_SENSITIVITY;
            pitch_ = pitch_ + Input.GetActionValue(Actions.CONTROLLER_ROT_Y) * 0.03f * MOUSE_SENSITIVITY;

            pitch_ = Mathf.Clamp(pitch_, 0.7f);

            float speed = Speed;

            if (Input.GetActionValue(Actions.SPRINT_ACTION) > 0.0f)
                speed *= 3.0f;

            Camera.SetLocalRotation(Quat.FromAngleAxis(pitch_, Vec3.Right));
            entity.SetRotation(Quat.FromAngleAxis(yaw_, Vec3.Up));

            var scene = physicalController_.Scene;
            var q = new Quat(Vec3.Up, yaw_);

            var v = new Vec3(Input.GetActionValue(Actions.CONTROLLER_MOVE_X) * 0.1f, 0, 0);
            physicalController_.MoveController(q.Rotate(v) * _deltaTime);

            v.Set(0, 0, Input.GetActionValue(Actions.CONTROLLER_MOVE_Y) * 0.1f);
            physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            
            if(Input.GetActionValue(Actions.LEFT_ACTION) > 0)
            {
                v.Set(-speed, 0, 0);
                physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            }
            if (Input.GetActionValue(Actions.RIGHT_ACTION) > 0)
            {
                v.Set(speed, 0, 0);
                physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            }
            if (Input.GetActionValue(Actions.FORWARD_ACTION) > 0)
            {
                v.Set(0, 0, -speed);
                physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            }
            if (Input.GetActionValue(Actions.BACK_ACTION) > 0)
            {
                v.Set(0, 0, speed);
                physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            }
        }
    }
}
