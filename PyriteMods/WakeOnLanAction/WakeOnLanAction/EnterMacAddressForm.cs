using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WakeOnLanAction
{
    public partial class EnterMacAddressForm : Form
    {
        public EnterMacAddressForm()
        {
            InitializeComponent();
            this.nudPort.Maximum = ushort.MaxValue;
        }

        public string MacAddress
        {
            get
            {
                return macAddressBox1.MacAddressString;
            }
            set
            {
                macAddressBox1.MacAddressString = value;
            }
        }

        public ushort TryCnt
        {
            get
            {
                return (ushort)nudTryCnt.Value;
            }
            set
            {
                nudTryCnt.Value = value;
            }
        }

        public ushort Port
        {
            get
            {
                return (ushort)nudPort.Value;
            }
            set
            {
                nudPort.Value = value;
            }
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            var form = new SelectMacAddressForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                macAddressBox1.MacAddress = form.Address;
        }
    }
}
