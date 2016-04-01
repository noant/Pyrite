using System;
using System.Threading;

namespace PyriteCore
{
    internal static class ThreadHelper
    {
        public static Thread AlterThread(Action action, bool isBackground, ApartmentState apartmentState)
        {
            Thread t = new Thread(() =>
            {
                action();
            });
            t.SetApartmentState(apartmentState);
            t.IsBackground = isBackground;
            t.Start();
            return t;
        }

        public static Thread AlterThread(Action action)
        {
            return AlterThread(action, true, ApartmentState.MTA);
        }

        public static Thread AlterHardThread(Action action)
        {
            return AlterThread(action, false, ApartmentState.MTA);
        }
    }
}
