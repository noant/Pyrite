using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniActionsUI
{
    public static class FSHelper
    {
        public static string GetImgLocation(string imgName)
        {
            var uri = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(uri)+@"\img\"+imgName;
        }
    }
}
