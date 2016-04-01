using System;
using System.Windows.Forms;

namespace PyriteStandartActions.Checkers
{
    public partial class DateTimeCheckerView : Form
    {
        public DateTimeCheckerView()
        {
            InitializeComponent();

            ProcessDateView();
            
            this.dtPicker.ValueChanged += (o, e) => {
                ProcessDateView();
            };

            this.dtPicker.Value = DateTime.Now;

            this.cbEveryDay.CheckedChanged += (o, e) => ProcessDateView();
            this.cbEveryHour.CheckedChanged += (o, e) => ProcessDateView();
            this.cbEveryMinute.CheckedChanged += (o, e) => ProcessDateView();
            this.cbEveryMonth.CheckedChanged += (o, e) => ProcessDateView();
            this.cbEveryYear.CheckedChanged += (o, e) => ProcessDateView();
            this.nudHour.ValueChanged += (o, e) => ProcessDateView();
            this.nudMinute.ValueChanged += (o, e) => ProcessDateView();
        }

        private void ProcessDateView()
        {
            string all = "*";
            string c = ".";
            var year = dtPicker.Value.Year.ToString();
            var month = dtPicker.Value.Month.ToString();
            var day = dtPicker.Value.Day.ToString();
            var hour = nudHour.Value.ToString();
            var minute = nudMinute.Value.ToString();
            if (cbEveryYear.Checked) year = all;
            if (cbEveryMonth.Checked) month = all;
            if (cbEveryDay.Checked) day = all;
            if (cbEveryHour.Checked) hour = all;
            if (cbEveryMinute.Checked) minute = all;

            tbDateView.Text = year + c + month + c + day + " " + hour + ":" + minute;
        }
    }
}
