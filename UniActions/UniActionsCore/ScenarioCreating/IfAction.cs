using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreating
{
    [Serializable]
    public class IfAction : ICustomAction, IHasChecker
    {
        public string Do(string inputState)
        {
            if (Checker.IsCanDoNow)
                ActionIf.Do(ActionIf.State);
            else
                ActionElse.Do(ActionElse.State);

            return State;
        }

        public ICustomAction ActionIf { get; set; }
        public ICustomAction ActionElse { get; set; }
        public ICustomChecker Checker { get; set; }

        [XmlIgnore]
        public string State
        {
            get { return ""; }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Если/Иначе операция"; }
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get { return true; }
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

        public bool HasChecker(Type checkerType)
        {
            if (Checker.GetType().Equals(checkerType))
                return true;

            if (Checker is IHasChecker && ((IHasChecker)Checker).HasChecker(checkerType))
                return true;

            if (ActionIf is IHasChecker && ((IHasChecker)ActionIf).HasChecker(checkerType))
                return true;

            if (ActionElse is IHasChecker && ((IHasChecker)ActionElse).HasChecker(checkerType))
                return true;

            return false;
        }
    }
}
