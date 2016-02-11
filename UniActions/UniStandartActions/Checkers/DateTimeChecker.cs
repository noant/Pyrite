using HierarchicalData;
using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    public class DateTimeChecker : ICustomChecker
    {
        public DateTimeChecker() { 
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            Day = DateTime.Now.Day;
            Hour = DateTime.Now.Hour;
            Minute = DateTime.Now.Minute;
        }

        private bool _fWasStarted;
        private DateTime _lastUpdate;

        public bool OnlyOnComputerStart { get; set; }

        public bool EveryYear { get; set; }

        public bool EveryMonth { get; set; }

        public bool EveryDay { get; set; }

        public bool EveryHour { get; set; }

        public bool EveryMinute { get; set; }

        public bool D_monday { get; set; }

        public bool D_tuesday { get; set; }

        public bool D_wednesday { get; set; }

        public bool D_thursday { get; set; }

        public bool D_friday { get; set; }

        public bool D_saturday { get; set; }

        public bool D_sunday { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                if (_fWasStarted && OnlyOnComputerStart)
                    return false;
                if ((DateTime.Now - _lastUpdate).TotalSeconds < 59)
                    return false;
                else _lastUpdate = DateTime.Now;

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

                if (dateFlag)
                {
                    _fWasStarted = true;
                    return true;
                }

                return false;
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

            form.cbOnlyWhenCompStart.Checked = this.OnlyOnComputerStart;

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

                this.OnlyOnComputerStart = form.cbOnlyWhenCompStart.Checked;

                return true;
            }
            return false;
        }

        public void Refresh() { }
    }
}
