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

        const uint LEFT_ACTION = 0;
        const uint RIGHT_ACTION = 1;
        const uint FORWARD_ACTION = 2;
        const uint BACK_ACTION = 3;
        const uint ROT_H_ACTION = 4;
        const uint SPRINT_ACTION = 5;
        const uint ROT_V_ACTION = 6;

        const uint CONTROLLER_ROT_X = 7;
        const uint CONTROLLER_ROT_Y = 8;
        const uint CONTROLLER_MOVE_X = 9;
        const uint CONTROLLER_MOVE_Y = 10;

        public float MOUSE_SENSITIVITY = 1.0f;

        public float Speed = 1.0f;
        float yaw_ = 0f;
        float pitch_ = 0f;

        public Entity Camera;
        public bool IsInputEnabled = true;
        void OnStartGame()
        {
            physicalController_ = CreateComponent<PhysicalController>();

            Input.AddAction(FORWARD_ACTION, Input.InputType.PRESSED, 'W', -1);
            Input.AddAction(BACK_ACTION, Input.InputType.PRESSED, 'S', -1);
            Input.AddAction(LEFT_ACTION, Input.InputType.PRESSED, 'A', -1);
            Input.AddAction(RIGHT_ACTION, Input.InputType.PRESSED, 'D', -1);


            Input.AddAction(ROT_H_ACTION, Input.InputType.MOUSE_X, 0, -1);
            Input.AddAction(ROT_V_ACTION, Input.InputType.MOUSE_Y, 0, -1);
            Input.AddAction(SPRINT_ACTION, Input.InputType.PRESSED, LSHIFT_KEY, -1);

            Input.AddAction(CONTROLLER_ROT_X, Input.InputType.RTHUMB_X, 0, 0);
            Input.AddAction(CONTROLLER_ROT_Y, Input.InputType.RTHUMB_Y, 0, 0);
            Input.AddAction(CONTROLLER_MOVE_X, Input.InputType.LTHUMB_X, 0, 0);
            Input.AddAction(CONTROLLER_MOVE_X, Input.InputType.LTHUMB_X, 0, 0);
        }

        void Update(float _deltaTime)
        {
            if (Camera == null)
                return;

            yaw_ = yaw_ + Input.GetActionValue(ROT_H_ACTION) * -0.01f * MOUSE_SENSITIVITY;
            pitch_ = pitch_ + Input.GetActionValue(ROT_V_ACTION) * -0.01f * MOUSE_SENSITIVITY;

            yaw_ = yaw_ + Input.GetActionValue(CONTROLLER_ROT_X) * -0.03f * MOUSE_SENSITIVITY;
            pitch_ = pitch_ + Input.GetActionValue(CONTROLLER_ROT_Y) * 0.03f * MOUSE_SENSITIVITY;

            pitch_ = Mathf.Clamp(pitch_, 0.7f);

            float speed = Speed;

            if (Input.GetActionValue(SPRINT_ACTION) > 0.0f)
                speed *= 3.0f;

            Camera.SetLocalRotation(Quat.FromAngleAxis(pitch_, Vec3.Right));
            entity.SetRotation(Quat.FromAngleAxis(yaw_, Vec3.Up));

            var scene = physicalController_.Scene;
            var q = new Quat(Vec3.Up, yaw_);

            var v = new Vec3(Input.GetActionValue(CONTROLLER_MOVE_X) * 0.1f, 0, 0);
            physicalController_.MoveController(q.Rotate(v) * _deltaTime);

            v.Set(0, 0, Input.GetActionValue(CONTROLLER_MOVE_Y) * 0.1f);
            physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            
            if(Input.GetActionValue(LEFT_ACTION) > 0)
            {
                v.Set(-speed, 0, 0);
                physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            }
            if (Input.GetActionValue(RIGHT_ACTION) > 0)
            {
                v.Set(speed, 0, 0);
                physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            }
            if (Input.GetActionValue(FORWARD_ACTION) > 0)
            {
                v.Set(0, 0, -speed);
                physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            }
            if (Input.GetActionValue(BACK_ACTION) > 0)
            {
                v.Set(0, 0, speed);
                physicalController_.MoveController(q.Rotate(v) * _deltaTime);
            }
        }
    }
}
