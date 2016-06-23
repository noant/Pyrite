using System;
namespace PyriteStandartActions.Actions
{
#if DEBUG
    [Serializable]
    public class _Debug_ChangeTestStatus : ICustomAction
    {
        private static string _lastState = "off";
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
        [XmlIgnore]
        public string State
        {
            get
            {
                return _lastState;
            }
        }
        [XmlIgnore]
        public string Name
        {
            get { return "Проверка тест"; }
        }
        [XmlIgnore]
        public bool AllowUserSettings
        {
            get { return false; }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        [XmlIgnore]
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
