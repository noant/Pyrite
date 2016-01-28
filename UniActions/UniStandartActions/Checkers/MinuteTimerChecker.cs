using HierarchicalData;
using System;
using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    public class MinuteTimerChecker : ICustomChecker
    {
        [Settings]
        private DateTime _dtFinish = DateTime.Now.AddMinutes(-5);
        private bool _shown;
        public bool IsCanDoNow
        {
            get
            {
                if (_shown) return false;
                return _shown = (DateTime.Now.Minute == _dtFinish.Minute &&
                    DateTime.Now.Hour == _dtFinish.Hour &&
                    DateTime.Now.Day == _dtFinish.Day &&
                    DateTime.Now.Month == _dtFinish.Month &&
                    DateTime.Now.Year == _dtFinish.Year);
            }
        }

        public string Name
        {
            get { return "Таймер"; }
        }

        public bool BeginUserSettings()
        {
            var form = new MinuteTimerCheckerView();
            form.Minutes = (decimal)(DateTime.Now - _dtFinish).TotalMinutes;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _dtFinish = DateTime.Now.AddMinutes((double)form.Minutes);
                return true;
            }
            return false;
        }

        public void Refresh() { }

        public bool AllowUserSettings { get { return true; } }
    }
}
