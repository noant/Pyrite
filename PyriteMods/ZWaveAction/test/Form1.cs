using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveAction;
using ZWaveActionUI;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            //var form = new TargetNodeValueSelectForm("", ControllerInterface.Serial, null, null);
            //form.ShowDialog();
            //var vals = form.ToString();
            //vals = form.ToString();

            //var form = new ActionForm();
            //form.Interface = ControllerInterface.Serial;
            //form.Device = "COM4";
            //form.NodeId = 4;
            //form.ParameterId = 72057594109837312;
            //form.TargetValue = false;
            //form.ShowDialog();
            //form.ShowDialog();

            while (true)
            {
                var zwaction = new ZWaveActionImplementations.ZWaveAction();
                zwaction.BeginUserSettings();

                //while (true)
                zwaction.Do(zwaction.State);
                MessageBox.Show("done");
            }
            //var zwchecker = new ZWaveActionImplementations.ZWaveChecker();
            //while (true)
            //    zwchecker.BeginUserSettings();
        }
    }
}
