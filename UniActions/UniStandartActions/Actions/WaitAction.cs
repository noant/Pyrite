using HierarchicalData;
using System.Threading;
using UniActionsClientIntefaces;

namespace UniStandartActions.Actions
{
#if DEBUG
    public class WaitAction : ICustomAction
    {
        [Settings]
        private decimal _minutes = 5;

        public string Do(string inputState)
        {
            IsBusyNow = true;
            Thread.Sleep((int)(_minutes * 1000 * 60));
            IsBusyNow = false;
            return State;
        }

        public string State
        {
            get {
                return "Ожидать " + _minutes + (_minutes != 1 ? " минут(ы)" : " минуту"); 
            }
        }

        public string Name
        {
            get { return "Ожидание"; }
        }

        public bool AllowUserSettings
        {
            get { return true; }
        }

        public bool BeginUserSettings()
        {
            WaitActionView form = new WaitActionView();
            form.nudMinutes.Value = _minutes;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                _minutes = form.nudMinutes.Value;
            else return false;
            return true;
        }

        public bool IsBusyNow
        {
            get;
            private set;
        }

        public void Refresh()
        {}
    }
#endif
}
