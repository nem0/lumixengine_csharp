using System.Collections.Generic;

namespace Lumix
{


    public class InputEvent
    {

    }


    public class KeyboardInputEvent : InputEvent
    {
        public uint scancode;
        public uint key_id;
        public bool is_down;
    }


    public class MouseAxisInputEvent : InputEvent
    {
        public float x;
        public float y;
        public float x_abs;
        public float y_abs;
    }


    public class MouseButtonInputEvent : InputEvent
    {
        public uint key_id;
        public bool is_down;
        public float x_abs;
        public float y_abs;
    }
}