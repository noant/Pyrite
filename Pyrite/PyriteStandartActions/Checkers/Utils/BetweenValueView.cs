using System.Windows.Forms;

namespace PyriteStandartActions.Checkers.Utils
{
    public partial class BetweenValueView : Form
    {
        public BetweenValueView()
        {
            InitializeComponent();
        }

        public decimal Value1
        {
            get
            {
                return nud1.Value;
            }
            set
            {
                nud1.Value = value;
            }
        }

        public decimal Value2
        {
            get
            {
                return nud2.Value;
            }
            set
            {
                nud2.Value = value;
            }
        }

        public decimal Min
        {
            get
            {
                return nud1.Minimum;
            }
            set
            {
                nud1.Minimum = nud2.Minimum = value;
            }
        }

        public decimal Max
        {
            get
            {
                return nud1.Maximum;
            }
            set
            {
                nud1.Maximum = nud2.Maximum = value;
            }
        }

        public bool FirstMoreOrEqual
        {
            get
            {
                return rbMoreOrEqual.Checked;
            }
            set
            {
                rbMoreOrEqual.Checked = value;
                rbMore.Checked = !value;
            }
        }

        public bool SecondLessOrEqual
        {
            get
            {
                return rbLessOrEqual.Checked;
            }
            set
            {
                rbLessOrEqual.Checked = value;
                rbLess.Checked = !value;
            }
        }

        public string ValueName
        {
            get
            {
                return this.Text;
            }
            set
            {
                lblParName.Text = Text = value;
            }
        }

        public int DecimalPlaces
        {
            get
            {
                return nud1.DecimalPlaces;
            }
            set
            {
                nud1.DecimalPlaces = nud2.DecimalPlaces = value;
            }
        }
    }
}
