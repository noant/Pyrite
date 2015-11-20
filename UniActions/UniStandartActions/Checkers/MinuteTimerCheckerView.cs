using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
