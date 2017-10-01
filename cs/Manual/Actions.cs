using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public static class Actions
    {
        public static readonly uint LEFT_ACTION = 0;
        public static readonly uint RIGHT_ACTION = 1;
        public static readonly uint FORWARD_ACTION = 2;
        public static readonly uint BACK_ACTION = 3;
        public static readonly uint ROT_H_ACTION = 4;
        public static readonly uint SPRINT_ACTION = 5;
        public static readonly uint ROT_V_ACTION = 6;

        public static readonly uint CONTROLLER_ROT_X = 7;
        public static readonly uint CONTROLLER_ROT_Y = 8;
        public static readonly uint CONTROLLER_MOVE_X = 9;
        public static readonly uint CONTROLLER_MOVE_Y = 10;
    }
}
