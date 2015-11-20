using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UniActionsCore;

namespace UniActionsUI
{
    public class V //is helper)))
    {
        public static Result<T> Process<T>(Result<T> result)
        {
            if (result.Exceptions.Count() != 0)
            {
                Thread t = new Thread(() =>
                {
                    var message = "Выброшены следующие ошибки:\r\n";
                    foreach (var e in result.Exceptions)
                        message += e.Message + "\r\n";

                    MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                });
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }

            return result;
        }
    }
}
