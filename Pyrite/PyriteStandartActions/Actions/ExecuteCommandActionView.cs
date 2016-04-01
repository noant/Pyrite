using System.Windows.Forms;

namespace PyriteStandartActions.Actions
{
    public partial class ExecuteCommandActionView : Form
    {
        public ExecuteCommandActionView()
        {
            InitializeComponent();
        }

        public string Command
        {
            get
            {
                return tbCommand.Text;
            }
            set
            {
                tbCommand.Text = value;
            }
        }

        public string ViewName
        {
            get
            {
                return tbName.Text;
            }
            set
            {
                tbName.Text = value;
            }
        }
    }
}
