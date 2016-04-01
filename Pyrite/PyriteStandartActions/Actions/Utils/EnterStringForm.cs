using System.Windows.Forms;

namespace PyriteStandartActions.Actions.Utils
{
    public partial class EnterStringForm : Form
    {
        public EnterStringForm()
        {
            InitializeComponent();
        }

        public string Value
        {
            get
            {
                return tbValue.Text;
            }
            set
            {
                tbValue.Text = value;
            }
        }
    }
}
