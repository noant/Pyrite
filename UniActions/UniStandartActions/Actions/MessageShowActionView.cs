using System.Windows.Forms;

namespace UniStandartActions.Actions
{
    public partial class MessageShowActionView : Form
    {
        public MessageShowActionView()
        {
            InitializeComponent();
        }

        public string Message
        {
            get
            {
                return tbMessage.Text;
            }
            set {
                tbMessage.Text = value;
            }
        }
    }
}
