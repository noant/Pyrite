using System.IO;
using System.Windows.Forms;

namespace PyriteStandartActions.Actions
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

        public string Path { 
            get { 
                return tbPath.Text; 
            }
            set
            {
                tbPath.Text = value;
            }
        }
        public string Args
        {
            get { return tbArgs.Text; }
            set
            {
                tbArgs.Text = value;
            }
        }
        public string StateOn
        {
            get { return tbStateOn.Text; }
            set
            {
                tbStateOn.Text = value;
            }
        }
        public string StateOff
        {
            get { return tbStateOff.Text; }
            set
            {
                tbStateOff.Text = value;
            }
        }
        public bool Tracking
        {
            get { return cbTracking.Checked; }
            set
            {
                cbTracking.Checked = value;
            }
        }
        public bool CloseMainWindow
        {
            get { return cbCloseMainWindow.Checked; }
            set
            {
                cbCloseMainWindow.Checked = value;
            }
        }

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
