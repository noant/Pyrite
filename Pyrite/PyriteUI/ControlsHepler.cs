using System;
using System.Globalization;
using System.Windows.Controls;

namespace PyriteUI
{
    public static class ControlsHelper
    {
        public interface IRefreshable
        {
            void Refresh();
        }

        public interface IConfirm
        {
            void Confirm();
        }

        public static void AppendOnlyInteger(TextBox tb, int min, int max)
        {
            tb.PreviewTextInput += (o, e) =>
            {
                var futureText = tb.Text.Insert(tb.CaretIndex, e.Text);
                int r;
                var handled = !int.TryParse(futureText, NumberStyles.Number, CultureInfo.InvariantCulture, out r);

                if (!handled)
                {
                    if (r < min || r > max)
                        handled = true;
                }

                e.Handled = handled;
            };
        }
    }

    public static class ControlsExtensions
    {
        public static int GetInt(this TextBox tb)
        {
            if (tb.Text == "")
                return 0;

            int res;
            if (!int.TryParse(tb.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out res))
                throw new Exception("Not int");
            else return res;
        }

        public static ushort GetUShort(this TextBox tb)
        {
            if (tb.Text == "")
                return 0;

            ushort res;
            if (!ushort.TryParse(tb.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out res))
                throw new Exception("Not short");
            else return res;
        }
    }
}
