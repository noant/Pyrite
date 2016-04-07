using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WakeOnLanAction
{
    public partial class ByteBox : UserControl
    {
        public ByteBox()
        {
            InitializeComponent();
            var textPrev = "";
            tbValue.TextChanged += (o, e) => {
                if (tbValue.Text.Count() == 0)
                    tbValue.Text = "0";
                byte bt;
                if (!byte.TryParse(tbValue.Text, out bt))
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
                return byte.Parse(tbValue.Text);
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
