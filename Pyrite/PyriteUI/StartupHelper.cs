using System;
using System.IO;

namespace PyriteUI
{
    public static class StartupHelper
    {
        public static bool IsAppInStartup
        {
            get
            {
                return IsShortcutExist();
            }
            set
            {
                if (value)
                    CreateStartupShortcut();
                else RemoveStartupLocation();
            }
        }

        private static string ShortcutPath
        {
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "Pyrite.lnk");
            }
        }


        public static void CreateStartupShortcut()
        {
            string shortcutLocation = ShortcutPath;
            IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = "Pyrite";
            shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            shortcut.Save();
        }

        public static void RemoveStartupLocation()
        {
            if (File.Exists(ShortcutPath))
                File.Delete(ShortcutPath);
        }

        public static bool IsShortcutExist()
        {
            return File.Exists(ShortcutPath);
        }
    }
}
