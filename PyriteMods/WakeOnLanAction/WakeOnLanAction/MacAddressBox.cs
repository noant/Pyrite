using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace WakeOnLanAction
{
    public partial class MacAddressBox : UserControl
    {
        public MacAddressBox()
        {
            InitializeComponent();
        }

        public byte[] MacAddress
        {
            get
            {
                return new byte[] { 
                    hexByteBox1.Value,
                    hexByteBox2.Value,
                    hexByteBox3.Value,
                    hexByteBox4.Value,
                    hexByteBox5.Value,
                    hexByteBox6.Value,
                };
            }
            set
            {
                hexByteBox1.Value = value[0];
                hexByteBox2.Value = value[1];
                hexByteBox3.Value = value[2];
                hexByteBox4.Value = value[3];
                hexByteBox5.Value = value[4];
                hexByteBox6.Value = value[5];
            }
        }

        public string MacAddressString
        {
            get
            {
                return
                    hexByteBox1.HexValue + ":" +
                    hexByteBox2.HexValue + ":" +
                    hexByteBox3.HexValue + ":" +
                    hexByteBox4.HexValue + ":" +
                    hexByteBox5.HexValue + ":" +
                    hexByteBox6.HexValue;
            }
            set
            {
                MacAddress = value
                .Split("-:".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(x => byte.Parse(x, NumberStyles.HexNumber))
                .ToArray();
            }
        }
    }
}
