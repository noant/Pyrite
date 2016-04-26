using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveActions;
using ZWaveActionsUI;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //while (true)
            //{
            //    ZWGlobal.Simplified.WaitForValueChanged(@"\\.\COM4", ControllerInterface.Serial, 3364711304, 3, 72057594093060096, false);
            //    MessageBox.Show("pressed");
            //}

            //while (true)
            //{
            //    if (ZWGlobal.Simplified.IsValueEquals(@"\\.\COM4", ControllerInterface.Serial, 3364711304, 3, 72057594093060096, false))
            //        MessageBox.Show("pressed");
            //    Thread.Sleep(100);
            //}

            var form = new TargetNodeValueSelectForm("", ControllerInterface.Serial);
            form.ShowDialog();
        }
    }
}
