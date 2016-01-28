using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    public class ComputerStartChecker : ICustomChecker
    {
        private bool _started;
        public bool IsCanDoNow
        {
            get
            {
                if (!_started)
                {
                    _started = true;
                    return true;
                }
                else return false;
            }
        }
        public bool AllowUserSettings { get { return false; } }

        public string Name
        {
            get { return "При включении компьютера";  }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public void Refresh() { }
    }
}
