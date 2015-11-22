using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniActionsCore
{
    internal static class Helper
    {
        public static string MassToString(IEnumerable<object> objs, string splitter)
        {
            return objs.Aggregate<object>((x0, x1) => x0.ToString() + splitter + x1.ToString()).ToString();
        }

        public static Thread AlterThread(Action action, bool isBackground, ApartmentState apartmentState)
        {
            Thread t = new Thread(() => {
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
