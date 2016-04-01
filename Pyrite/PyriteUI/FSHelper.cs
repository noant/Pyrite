using System;
using System.IO;

namespace PyriteUI
{
    public static class FSHelper
    {
        public static string GetImgLocation(string imgName)
        {
            var uri = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(uri)+@"\"+imgName;
        }
    }
}
