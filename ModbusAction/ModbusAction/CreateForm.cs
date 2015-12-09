using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusAction
{
    public partial class CreateForm : Form
    {
        public CreateForm()
        {
            InitializeComponent();

            this.tbPortName.TextChanged += (o, e) => ProcessOkEnable();
            this.tbStateOff.TextChanged += (o, e) => ProcessOkEnable();
            this.tbStateOn.TextChanged += (o, e) => ProcessOkEnable();
            
            Refresh();
        }

        public void ProcessOkEnable()
        {
            btOk.Enabled = this.tbPortName.Text.Any() && this.tbStateOff.Text.Any() && this.tbStateOn.Text.Any();
        }

        public new void Refresh()
        {
            this.cbDataBits.Items.Add(8);
            this.cbDataBits.Items.Add(7);

            this.cbParity.Items.Add(Parity.None);
            this.cbParity.Items.Add(Parity.Mark);
            this.cbParity.Items.Add(Parity.Even);
            this.cbParity.Items.Add(Parity.Odd);
            this.cbParity.Items.Add(Parity.Space);

            this.cbStopBits.Items.Add(StopBits.One);
            this.cbStopBits.Items.Add(StopBits.OnePointFive);
            this.cbStopBits.Items.Add(StopBits.Two);
            this.cbStopBits.Items.Add(StopBits.None);

            nudReadTimeout.Maximum = int.MaxValue;
            nudWriteTimeout.Maximum = int.MaxValue;
            nudSingleCoil.Maximum = ushort.MaxValue;
        }
    }
}
