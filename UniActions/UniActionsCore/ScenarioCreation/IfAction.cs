using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreation
{
    [Serializable]
    public class IfAction : ICustomAction, IHasCheckerAction
    {
        public string Do(string inputState)
        {
            if (Checker != null)
            {
                if (Checker.IsCanDoNow)
                    if (ActionIf != null)
                        ActionIf.Do(ActionIf.State);
                else
                    if (ActionElse != null)
                        ActionElse.Do(ActionElse.State);
            }
            return State;
        }

        public ComplexAction ActionIf { get; set; }
        public ComplexAction ActionElse { get; set; }
        public ComplexChecker Checker { get; set; }

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

        public void RemoveChecker(Type checkerType)
        {
            if (Checker.GetType().Equals(checkerType))
                Checker = null;
            else if (Checker != null && Checker is IHasCheckerAction)
                ((IHasCheckerAction)Checker).RemoveChecker(checkerType);

            if (ActionIf != null && ActionIf is IHasCheckerAction)
                ((IHasCheckerAction)ActionIf).RemoveChecker(checkerType);

            if (ActionElse != null && ActionElse is IHasCheckerAction)
                ((IHasCheckerAction)ActionElse).RemoveChecker(checkerType);
        }

        public void RemoveAction(Type actionType)
        {
            if (ActionIf != null && ActionIf is IHasCheckerAction)
                ((IHasCheckerAction)ActionIf).RemoveAction(actionType);

            if (ActionElse != null && ActionElse is IHasCheckerAction)
                ((IHasCheckerAction)ActionElse).RemoveAction(actionType);
        }
    }
}
