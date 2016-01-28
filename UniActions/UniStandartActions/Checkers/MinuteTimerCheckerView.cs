using System.Windows.Forms;

namespace UniStandartActions.Checkers
{
    public partial class MinuteTimerCheckerView : Form
    {
        public decimal Minutes
        {
            get
            {
                return nudMinutes.Value;
            }
            set
            {
                nudMinutes.Value = value;
            }
        }

        public MinuteTimerCheckerView()
        {
            InitializeComponent();
        }
    }
}
