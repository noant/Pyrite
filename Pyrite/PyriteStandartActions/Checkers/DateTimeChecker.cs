using PyriteClientIntefaces;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class DateTimeChecker : ICustomChecker
    {
        public DateTimeChecker()
        {
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            Day = DateTime.Now.Day;
            Hour = DateTime.Now.Hour;
            Minute = DateTime.Now.Minute;

            D_friday =
                D_monday =
                D_saturday =
                D_sunday =
                D_thursday =
                D_tuesday =
                D_wednesday = true;
        }

        [HumanFriendlyName("Год")]
        public int Year { get; set; }

        [HumanFriendlyName("Месяц")]
        public int Month { get; set; }

        [HumanFriendlyName("День")]
        public int Day { get; set; }

        [HumanFriendlyName("Час")]
        public int Hour { get; set; }

        [HumanFriendlyName("Минута")]
        public int Minute { get; set; }

        [HumanFriendlyName("Каждый год")]
        public bool EveryYear { get; set; }

        [HumanFriendlyName("Каждый месяц")]
        public bool EveryMonth { get; set; }

        [HumanFriendlyName("Каждый день")]
        public bool EveryDay { get; set; }

        [HumanFriendlyName("Каждый час")]
        public bool EveryHour { get; set; }

        [HumanFriendlyName("Каждую минуту")]
        public bool EveryMinute { get; set; }

        [HumanFriendlyName("Понедельник")]
        public bool D_monday { get; set; }

        [HumanFriendlyName("Вторник")]
        public bool D_tuesday { get; set; }

        [HumanFriendlyName("Среда")]
        public bool D_wednesday { get; set; }

        [HumanFriendlyName("Четверг")]
        public bool D_thursday { get; set; }

        [HumanFriendlyName("Пятница")]
        public bool D_friday { get; set; }

        [HumanFriendlyName("Суббота")]
        public bool D_saturday { get; set; }

        [HumanFriendlyName("Воскресенье")]
        public bool D_sunday { get; set; }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                var dayOfWeekFlag =
                    (DateTime.Now.DayOfWeek == DayOfWeek.Monday && D_monday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && D_tuesday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && D_wednesday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && D_thursday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Friday && D_friday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Saturday && D_saturday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && D_sunday);

                if (dayOfWeekFlag == false)
                    return false;

                var dateFlag =
                    (DateTime.Now.Year == Year || EveryYear) &&
                    (DateTime.Now.Month == Month || EveryMonth) &&
                    (DateTime.Now.Day == Day || EveryDay) &&
                    (DateTime.Now.Hour == Hour || EveryHour) &&
                    (DateTime.Now.Minute == Minute || EveryMinute);

                return dateFlag;
            }
        }

        [XmlIgnore]
        public bool AllowUserSettings { get { return true; } }

        [XmlIgnore]
        public string Name
        {
            get { return "Проверка по дате"; }
        }

        public bool BeginUserSettings()
        {
            var form = new DateTimeCheckerView();

            form.cbFriday.Checked = this.D_friday;
            form.cbMonday.Checked = this.D_monday;
            form.cbSaturday.Checked = this.D_saturday;
            form.cbSunday.Checked = this.D_sunday;
            form.cbThursday.Checked = this.D_thursday;
            form.cbTuesday.Checked = this.D_tuesday;
            form.cbWednesday.Checked = this.D_wednesday;

            form.cbEveryDay.Checked = this.EveryDay;
            form.cbEveryHour.Checked = this.EveryHour;
            form.cbEveryMinute.Checked = this.EveryMinute;
            form.cbEveryMonth.Checked = this.EveryMonth;
            form.cbEveryYear.Checked = this.EveryYear;

            form.dtPicker.Value = new DateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, 0);

            form.nudHour.Value = this.Hour;
            form.nudMinute.Value = this.Minute;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.D_friday = form.cbFriday.Checked;
                this.D_monday = form.cbMonday.Checked;
                this.D_saturday = form.cbSaturday.Checked;
                this.D_sunday = form.cbSunday.Checked;
                this.D_thursday = form.cbThursday.Checked;
                this.D_tuesday = form.cbTuesday.Checked;
                this.D_wednesday = form.cbWednesday.Checked;

                this.EveryDay = form.cbEveryDay.Checked;
                this.EveryHour = form.cbEveryHour.Checked;
                this.EveryMinute = form.cbEveryMinute.Checked;
                this.EveryMonth = form.cbEveryMonth.Checked;
                this.EveryYear = form.cbEveryYear.Checked;

                this.Year = form.dtPicker.Value.Year;
                this.Month = form.dtPicker.Value.Month;
                this.Day = form.dtPicker.Value.Day;
                this.Hour = (int)form.nudHour.Value;
                this.Minute = (int)form.nudMinute.Value;

                return true;
            }
            return false;
        }

        public void Refresh() { }
    }
}
