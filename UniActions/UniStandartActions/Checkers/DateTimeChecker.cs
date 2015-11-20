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

        private bool _onlyOnComputerStart;

        private bool _everyYear;
        private bool _everyMonth;
        private bool _everyDay;
        private bool _everyHour;
        private bool _everyMinute;

        private bool _d_monday;
        private bool _d_tuesday;
        private bool _d_wednesday;
        private bool _d_thursday;
        private bool _d_friday;
        private bool _d_saturday;
        private bool _d_sunday;

        private int _year;
        private int _month;
        private int _day;
        private int _hour;
        private int _minute;

        public bool IsCanDoNow()
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
                (DateTime.Now.Month == _month|| _everyMonth) &&
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

        public string Name
        {
            get { return "Проверка по дате"; }
        }

        public bool InitializeNew()
        {
            var form = new DateTimeCheckerView();
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

        private string _splitter = "#";

        public void SetFromString(string settings)
        {
            var strs = settings.Split(_splitter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            _everyDay = FromChar(strs[0][0]);
            _everyHour = FromChar(strs[0][1]);
            _everyMinute = FromChar(strs[0][2]);
            _everyMonth = FromChar(strs[0][3]);
            _everyYear = FromChar(strs[0][4]);
            _d_friday = FromChar(strs[0][5]);
            _d_monday = FromChar(strs[0][6]);
            _d_saturday = FromChar(strs[0][7]);
            _d_sunday = FromChar(strs[0][8]);
            _d_thursday = FromChar(strs[0][9]);
            _d_tuesday = FromChar(strs[0][10]);
            _d_wednesday = FromChar(strs[0][11]);
            
            _onlyOnComputerStart = FromChar(strs[0][12]);

            _year = int.Parse(strs[1]);
            _month = int.Parse(strs[2]);
            _day= int.Parse(strs[3]);
            _hour = int.Parse(strs[4]);
            _minute = int.Parse(strs[5]);
        }

        public string SetToString()
        {
            return
                FromBool(_everyDay)+
                FromBool(_everyHour)+
                FromBool(_everyMinute)+
                FromBool(_everyMonth)+
                FromBool(_everyYear)+
                FromBool(_d_friday)+
                FromBool(_d_monday)+
                FromBool(_d_saturday)+
                FromBool(_d_sunday)+
                FromBool(_d_thursday)+
                FromBool(_d_tuesday)+
                FromBool(_d_wednesday) +
                FromBool(_onlyOnComputerStart) +
                _splitter + _year+
                _splitter + _month+
                _splitter + _day+
                _splitter + _hour+
                _splitter + _minute;
        }

        private string FromBool(bool b)
        {
            return b ? "1" : "0";
        }

        private bool FromChar(char c)
        {
            return c != '0';
        }
    }
}
