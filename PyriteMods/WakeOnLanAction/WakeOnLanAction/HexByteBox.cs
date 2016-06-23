using System.Linq;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace WakeOnLanAction
{
    public partial class HexByteBox : UserControl
    {
        public HexByteBox()
        {
            InitializeComponent();

            string prevText = "";
            tbValue.TextChanged += (o, e) =>
            {
                if (string.IsNullOrEmpty(tbValue.Text))
                {
                    tbValue.Text = "00";
                    tbValue.SelectAll();
                    prevText = "";
                    return;
                }

                byte res;
                if (!byte.TryParse(tbValue.Text.ToLower(), NumberStyles.HexNumber, null, out res))
                    tbValue.Text = prevText.ToUpper();
                else
                {
                    tbValue.Text = tbValue.Text.ToUpper();
                    tbValue.SelectionStart = tbValue.Text.Length;
                    tbValue.SelectionLength = 0;
                    prevText = tbValue.Text;
                }
            };
        }

        public bool ShowSplitter
        {
            get
            {
                return lblSplitter.Visible;
            }
            set
            {
                lblSplitter.Visible = value;
            }
        }

        public string HexValue
        {
            get
            {
                return tbValue.Text.ToUpper();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    value = "00";
                tbValue.Text = Convert.ToString(byte.Parse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture), 16).ToUpper();
            }
        }

        public byte Value
        {
            get
            {
                return byte.Parse(tbValue.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            set
            {
                tbValue.Text = Convert.ToString(value, 16);
            }
        }
    }
}
