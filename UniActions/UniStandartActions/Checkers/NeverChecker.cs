using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    public class NeverChecker : ICustomChecker
    {
        public bool IsCanDoNow
        {
            get
            {
                return false;
            }
        }

        public bool AllowUserSettings { get { return false; } }

        public string Name
        {
            get { return "Никогда"; }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public void Refresh() { }
    }
}
