using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.Core
{
    public enum TaskStatus : long
    {
        Stopped = 0,
        Running = 1,
        Finished = 2
    }
}
