using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreation
{
    [Serializable]
    public class WhileAction : ICustomAction, IHasCheckerAction
    {
        public ComplexAction Action { get; set; }

        public ComplexChecker Checker { get; set; }

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
            if (Checker != null)
                while (Checker.IsCanDoNow)
                    Action.Do("");
            return "";
        }

        public void Refresh()
        {

        }

        public void RemoveChecker(Type checkerType)
        {
            if (Checker.GetType().Equals(checkerType))
                Checker = null;
            else if (Checker != null && Checker is IHasCheckerAction)
                ((IHasCheckerAction)Checker).RemoveChecker(checkerType);

            if (Action != null && Action is IHasCheckerAction)
                ((IHasCheckerAction)Action).RemoveChecker(checkerType);
        }


        public void RemoveAction(Type actionType)
        {
            if (Action != null && Action is IHasCheckerAction)
                ((IHasCheckerAction)Action).RemoveAction(actionType);
        }
    }
}
