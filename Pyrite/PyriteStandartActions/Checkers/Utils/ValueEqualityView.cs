using System;
using System.Windows.Forms;

namespace PyriteStandartActions.Checkers.Utils
{
    public partial class ValueEqualityView : Form
    {
        public ValueEqualityView()
        {
            InitializeComponent();
            cbEquality.SelectedIndex = 0;
        }

        public Equality Equality
        {
            get
            {
                if (cbEquality.SelectedIndex == 0)
                    return Utils.Equality.Equal;
                if (cbEquality.SelectedIndex == 1)
                    return Utils.Equality.MoreThan;
                if (cbEquality.SelectedIndex == 2)
                    return Utils.Equality.MoreOrEqualThan;
                if (cbEquality.SelectedIndex == 3)
                    return Utils.Equality.LessThan;
                if (cbEquality.SelectedIndex == 4)
                    return Utils.Equality.LessOrEqualThan;
                throw new Exception();
            }
            set
            {
                if (value.Equals(Utils.Equality.Equal))
                    cbEquality.SelectedIndex = 0;
                if (value.Equals(Utils.Equality.MoreThan))
                    cbEquality.SelectedIndex = 1;
                if (value.Equals(Utils.Equality.MoreOrEqualThan))
                    cbEquality.SelectedIndex = 2;
                if (value.Equals(Utils.Equality.LessThan))
                    cbEquality.SelectedIndex = 3;
                if (value.Equals(Utils.Equality.LessOrEqualThan))
                    cbEquality.SelectedIndex = 4;
            }
        }

        public decimal Value
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

        public decimal Min
        {
            get
            {
                return nud1.Minimum;
            }
            set
            {
                nud1.Minimum = value;
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
                nud1.Maximum = value;
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
                this.Text = labelValueName.Text = value;
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
                nud1.DecimalPlaces = value;
            }
        }
    }
}
