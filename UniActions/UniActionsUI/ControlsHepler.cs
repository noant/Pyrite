using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using UniActionsCore;

namespace UniActionsUI
{
    public static class ControlsHelper
    {
        public interface IRefreshable
        {
            void Refresh();
        }

        public static void AppendOnlyInteger(TextBox tb, int min, int max)
        {
            tb.PreviewTextInput += (o, e) => {
                var futureText = tb.Text.Insert(tb.CaretIndex, e.Text);
                int r;
                var handled = !int.TryParse(futureText, out r);

                if (!handled)
                {
                    if (r < min || r > max)
                        handled = true;
                }

                e.Handled = handled;
            };
        }
    }

    public static class ControlsExtensions {
        public static int GetInt(this TextBox tb)
        {
            if (tb.Text == "")
                return 0;

            int res;
            if (!int.TryParse(tb.Text, out res))
                throw new Exception("Not an int");
            else return res;
        }
        public static ushort GetUShort(this TextBox tb)
        {
            if (tb.Text == "")
                return 0;

            ushort res;
            if (!ushort.TryParse(tb.Text, out res))
                throw new Exception("Not an short");
            else return res;   
        }
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class YesNoBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = value is bool && (bool)value;

            return boolValue ? "Да" : "Нет";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.ToString() == "Да";
        }
    }
}
