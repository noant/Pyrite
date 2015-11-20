using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniStandartActions.Actions
{
    public partial class ProcessActionView : Form
    {
        public ProcessActionView()
        {
            InitializeComponent();
            ProcessOkEnable();

            this.btBrowse.Click += (o, e) => {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                if (ofd.ShowDialog()== System.Windows.Forms.DialogResult.OK)
                {
                    tbPath.Text = ofd.FileName;
                }
                ProcessOkEnable();
            };

            this.tbStateOn.TextChanged += (o, e) => ProcessOkEnable();
            this.tbStateOff.TextChanged += (o, e) => ProcessOkEnable();
        }

        public string Path { get { return tbPath.Text; } }
        public string Args { get { return tbArgs.Text; } }
        public string StateOn { get { return tbStateOn.Text; } }
        public string StateOff { get { return tbStateOff.Text; } }
        public bool Tracking { get { return cbTracking.Checked; } }
        public bool CloseMainWindow { get { return cbCloseMainWindow.Checked; } }

        private void ProcessOkEnable()
        {
            if (!string.IsNullOrWhiteSpace(tbStateOn.Text) && !string.IsNullOrWhiteSpace(tbStateOff.Text) && File.Exists(tbPath.Text))
            {
                btOk.Enabled = true;
            }
            else btOk.Enabled = false;
        }
    }
}
