using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    [Flags]
    public enum MouseButton : int
    {
        Left = 0x01,
        Middle = 0x2,
        Right = 0x4,
        None = 0x0
    }

    public enum Keyboard : int
    {

    }
}
