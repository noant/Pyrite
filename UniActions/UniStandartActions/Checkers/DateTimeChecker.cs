using HierarchicalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    public class DateTimeChecker : ICustomChecker
    {
        private bool _fWasStarted;
        private DateTime _lastUpdate;

        [Settings]
        private bool _onlyOnComputerStart;

        [Settings]
        private bool _everyYear;

        [Settings]
        private bool _everyMonth;

        [Settings]
        private bool _everyDay;

        [Settings]
        private bool _everyHour;

        [Settings]
        private bool _everyMinute;

        [Settings]
        private bool _d_monday;

        [Settings]
        private bool _d_tuesday;

        [Settings]
        private bool _d_wednesday;

        [Settings]
        private bool _d_thursday;

        [Settings]
        private bool _d_friday;

        [Settings]
        private bool _d_saturday;

        [Settings]
        private bool _d_sunday;

        [Settings]
        private int _year;

        [Settings]
        private int _month;

        [Settings]
        private int _day;

        [Settings]
        private int _hour;

        [Settings]
        private int _minute;

        public bool IsCanDoNow
        {
            get
            {
                if (_fWasStarted && _onlyOnComputerStart)
                    return false;
                if ((DateTime.Now - _lastUpdate).TotalSeconds < 59)
                    return false;
                else _lastUpdate = DateTime.Now;

                var dayOfWeekFlag =
                    (DateTime.Now.DayOfWeek == DayOfWeek.Monday && _d_monday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday && _d_tuesday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday && _d_wednesday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && _d_thursday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Friday && _d_friday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Saturday && _d_saturday) ||
                    (DateTime.Now.DayOfWeek == DayOfWeek.Sunday && _d_sunday);

                if (dayOfWeekFlag == false)
                    return false;

                var dateFlag =
                    (DateTime.Now.Year == _year || _everyYear) &&
                    (DateTime.Now.Month == _month || _everyMonth) &&
                    (DateTime.Now.Day == _day || _everyDay) &&
                    (DateTime.Now.Hour == _hour || _everyHour) &&
                    (DateTime.Now.Minute == _minute || _everyMinute);

                if (dateFlag)
                {
                    _fWasStarted = true;
                    return true;
                }

                return false;
            }
        }

        public bool AllowUserSettings { get { return true; } }

        public string Name
        {
            get { return "Проверка по дате"; }
        }

        public bool BeginUserSettings()
        {
            var form = new DateTimeCheckerView();

            form.cbFriday.Checked = this._d_friday;
            form.cbMonday.Checked = this._d_monday;
            form.cbSaturday.Checked = this._d_saturday;
            form.cbSunday.Checked = this._d_sunday;
            form.cbThursday.Checked = this._d_thursday;
            form.cbTuesday.Checked = this._d_tuesday;
            form.cbWednesday.Checked = this._d_wednesday;

            form.cbEveryDay.Checked = this._everyDay;
            form.cbEveryHour.Checked = this._everyHour;
            form.cbEveryMinute.Checked = this._everyMinute;
            form.cbEveryMonth.Checked = this._everyMonth;
            form.cbEveryYear.Checked = this._everyYear;

            form.dtPicker.Value = new DateTime(this._year, this._month, this._day, this._hour, this._minute, 0);

            form.cbOnlyWhenCompStart.Checked = this._onlyOnComputerStart;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._d_friday = form.cbFriday.Checked;
                this._d_monday = form.cbMonday.Checked;
                this._d_saturday = form.cbSaturday.Checked;
                this._d_sunday = form.cbSunday.Checked;
                this._d_thursday = form.cbThursday.Checked;
                this._d_tuesday = form.cbTuesday.Checked;
                this._d_wednesday = form.cbWednesday.Checked;

                this._everyDay = form.cbEveryDay.Checked;
                this._everyHour = form.cbEveryHour.Checked;
                this._everyMinute = form.cbEveryMinute.Checked;
                this._everyMonth = form.cbEveryMonth.Checked;
                this._everyYear = form.cbEveryYear.Checked;

                this._year = form.dtPicker.Value.Year;
                this._month = form.dtPicker.Value.Month;
                this._day = form.dtPicker.Value.Day;
                this._hour = (int)form.nudHour.Value;
                this._minute = (int)form.nudMinute.Value;

                this._onlyOnComputerStart = form.cbOnlyWhenCompStart.Checked;

                return true;
            }
            return false;
        }

        public void Refresh() { }
    }
}
