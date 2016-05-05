using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZWaveAction;

namespace ZWaveActionImplementations
{
    public static class Helper
    {
        public static void PrepareController(string path, ControllerInterface @interface)
        {
            if (!string.IsNullOrEmpty(path))
                ZWGlobal.PrepareZWave(path, @interface).WaitForControllerLoaded();
        }
    }
}
