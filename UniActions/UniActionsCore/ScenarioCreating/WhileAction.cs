using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreating
{
    [Serializable]
    public class WhileAction : ICustomAction, IHasChecker
    {
        public ICustomAction Action { get; set; }

        public ICustomChecker Checker { get; set; }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get; set;
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                return "";
            }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public string Do(string inputState)
        {
            while (Checker.IsCanDoNow)
            {
                Action.Do("");
            }
            return "";
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

            if (Action is IHasChecker && ((IHasChecker)Action).HasChecker(checkerType))
                return true;

            return false;
        }
    }
}
