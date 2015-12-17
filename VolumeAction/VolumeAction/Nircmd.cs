using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VolumeAction
{
    public static class Nircmd
    {
        private static readonly string _nircmd = "nircmdc.exe";
        private static readonly string _setsysvolume = "setsysvolume ";
        private static readonly string _changesysvolume = "changesysvolume ";

        public static void SetSoundVolume(int value)
        {
            StartNircmd(_setsysvolume + CorrectValue(value));
        }

        public static void ChangeSoundVolume(int value)
        {            
            StartNircmd(_changesysvolume + CorrectValue(value));
        }

        private static int CorrectValue(int value)
        {
            var perc100 = 256 * 256;
            return (int)(perc100 * (float)((float)value / 100.0));
        }

        private static string GetCurrentAssemblyFolder()
        {
            return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\";
        }

        private static void StartNircmd(string args)
        {
            var processInfo = new ProcessStartInfo(GetCurrentAssemblyFolder() + _nircmd, args);
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(processInfo);
        }
    }
}
