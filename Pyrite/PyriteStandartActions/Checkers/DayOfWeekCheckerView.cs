using System;
using System.Windows.Forms;

namespace PyriteStandartActions.Checkers
{
    public partial class DayOfWeekCheckerView : Form
    {
        public DayOfWeekCheckerView()
        {
            InitializeComponent();

            DayOfWeek = DateTime.Now.DayOfWeek;
        }

        public DayOfWeek DayOfWeek
        {
            get
            {
                if (cbDayOfWeek.SelectedIndex == 0)
                    return DayOfWeek.Monday;
                if (cbDayOfWeek.SelectedIndex == 1)
                    return DayOfWeek.Tuesday;
                if (cbDayOfWeek.SelectedIndex == 2)
                    return DayOfWeek.Wednesday;
                if (cbDayOfWeek.SelectedIndex == 3)
                    return DayOfWeek.Thursday;
                if (cbDayOfWeek.SelectedIndex == 4)
                    return DayOfWeek.Friday;
                if (cbDayOfWeek.SelectedIndex == 5)
                    return DayOfWeek.Saturday;
                if (cbDayOfWeek.SelectedIndex == 6)
                    return DayOfWeek.Sunday;
                throw new Exception();
            }
            set
            {
                if (value.Equals(DayOfWeek.Monday))
                    cbDayOfWeek.SelectedIndex = 0;
                else if (value.Equals(DayOfWeek.Tuesday))
                    cbDayOfWeek.SelectedIndex = 1;
                else if (value.Equals(DayOfWeek.Wednesday))
                    cbDayOfWeek.SelectedIndex = 2;
                else if (value.Equals(DayOfWeek.Thursday))
                    cbDayOfWeek.SelectedIndex = 3;
                else if (value.Equals(DayOfWeek.Friday))
                    cbDayOfWeek.SelectedIndex = 4;
                else if (value.Equals(DayOfWeek.Saturday))
                    cbDayOfWeek.SelectedIndex = 5;
                else if (value.Equals(DayOfWeek.Sunday))
                    cbDayOfWeek.SelectedIndex = 6;
            }
        }
    }
}
