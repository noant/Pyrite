using System.Threading;
using UniActionsClientIntefaces;

namespace UniStandartActions.Actions
{
#if DEBUG
    public class ChangeTestStatus : ICustomAction
    {
        private string _lastState = "off";
        public string Do(string inputState)
        {
            IsBusyNow = true;
            Thread.Sleep(500);
            if (inputState == "on")
                _lastState = "off";
            else _lastState = "on";
            IsBusyNow = false;
            return State;
        }

        public string State
        {
            get {
                return _lastState;
            }
        }

        public string Name
        {
            get { return "Проверка тест"; }
        }

        public bool AllowUserSettings
        {
            get { return false; }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public bool IsBusyNow
        {
            get;
            private set;
        }

        public void Refresh()
        {
        }
    }
#endif
}
