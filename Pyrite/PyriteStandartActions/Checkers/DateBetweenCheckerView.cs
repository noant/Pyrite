using System;
using System.Windows.Forms;

namespace PyriteStandartActions.Checkers
{
    public partial class DateBetweenCheckerView : Form
    {
        public DateBetweenCheckerView()
        {
            InitializeComponent();
        }

        public DateTime DateTime1
        {
            get
            {
                return dtPicker1.Value;
            }
            set
            {
                dtPicker1.Value = value;
            }
        }

        public DateTime DateTime2
        {
            get
            {
                return dtPicker2.Value;
            }
            set
            {
                dtPicker2.Value = value;
            }
        }

        public bool FirstMoreOrEqual
        {
            get
            {
                return cbOrEqual1.Checked;
            }
            set
            {
                cbOrEqual1.Checked = value;
            }
        }

        public bool SecondLessOrEqual
        {
            get
            {
                return cbOrEqual2.Checked;
            }
            set
            {
                cbOrEqual2.Checked = value;
            }
        }
    }
}
