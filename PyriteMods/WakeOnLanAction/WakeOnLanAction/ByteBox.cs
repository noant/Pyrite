using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace WakeOnLanAction
{
    public partial class ByteBox : UserControl
    {
        public ByteBox()
        {
            InitializeComponent();
            var textPrev = "";
            tbValue.TextChanged += (o, e) =>
            {
                if (tbValue.Text.Count() == 0)
                    tbValue.Text = "0";
                byte bt;
                if (!byte.TryParse(tbValue.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out bt))
                {
                    tbValue.Text = textPrev;
                    tbValue.SelectionStart = tbValue.Text.Length;
                    tbValue.SelectionLength = 0;
                }
                else
                    textPrev = tbValue.Text;
            };
        }

        public byte Value
        {
            get
            {
                return byte.Parse(tbValue.Text, CultureInfo.InvariantCulture);
            }
            set
            {
                tbValue.Text = value.ToString();
            }
        }

        public string ValueStr
        {
            get
            {
                return tbValue.Text;
            }
            set
            {
                tbValue.Text = value;
            }
        }
    }
}
