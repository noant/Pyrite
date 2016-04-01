using System.Windows.Forms;

namespace PyriteStandartActions.Actions
{
    public partial class PowerOffActionView : Form
    {
        public PowerOffActionView()
        {
            InitializeComponent();
        }

        public int Timer
        {
            get
            {
                return (int)nudTimer.Value;
            }
            set
            {
                nudTimer.Value = value;
            }
        }

        public bool CanCancel
        {
            get
            {
                return cbCanCancel.Checked;
            }
            set
            {
                cbCanCancel.Checked = value;
            }
        }

        public bool Restart
        {
            get
            {
                return cbRestart.Checked;
            }
            set
            {
                cbRestart.Checked = value;
            }
        }
    }
}
