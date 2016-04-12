using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PyriteStandartActions.Checkers
{
    public partial class PyritePingCheckerView : Form
    {
        public PyritePingCheckerView()
        {
            InitializeComponent();
            nudPort.Minimum = 0;
            nudPort.Maximum = ushort.MaxValue;
        }

        public string Host
        {
            get
            {
                return tbHost.Text;
            }
            set
            {
                tbHost.Text = value;
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
    }
}
