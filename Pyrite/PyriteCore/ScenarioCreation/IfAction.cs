using PyriteClientIntefaces;
using System;
using System.Xml.Serialization;

namespace PyriteCore.ScenarioCreation
{
    [Serializable]
    public class IfAction : ICustomAction, IHasCheckerAction
    {
        public string Do(string inputState)
        {
            if (Checker != null)
            {
                if (Checker.IsCanDoNow)
                {
                    if (ActionIf != null)
                        ActionIf.Do(ActionIf.State);
                }
                else if (ActionElse != null)
                {
                    ActionElse.Do(ActionElse.State);
                }
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

        public bool RemoveChecker(Type checkerType)
        {
            var result = false;
            if (Checker.GetType().Equals(checkerType))
                Checker = null;
            else if (Checker != null && Checker is IHasCheckerAction)
                if (((IHasCheckerAction)Checker).RemoveChecker(checkerType))
                    result = true;

            if (ActionIf != null && ActionIf is IHasCheckerAction)
                if (((IHasCheckerAction)ActionIf).RemoveChecker(checkerType))
                    result = true;

            if (ActionElse != null && ActionElse is IHasCheckerAction)
                if (((IHasCheckerAction)ActionElse).RemoveChecker(checkerType))
                    result = true;

            return result;
        }

        public bool RemoveAction(Type actionType)
        {
            var result = false;

            if (ActionIf != null && ActionIf is IHasCheckerAction)
                if (((IHasCheckerAction)ActionIf).RemoveAction(actionType))
                    result = true;

            if (ActionElse != null && ActionElse is IHasCheckerAction)
                if (((IHasCheckerAction)ActionElse).RemoveAction(actionType))
                    result = true;

            return result;
        }

        public void ForAllActionAndChecker(Action<object> action)
        {
            if (Checker != null && Checker is IHasCheckerAction)
                ((IHasCheckerAction)Checker).ForAllActionAndChecker(action);

            if (ActionIf != null && ActionIf is IHasCheckerAction)
                ((IHasCheckerAction)ActionIf).ForAllActionAndChecker(action);

            if (ActionElse != null && ActionElse is IHasCheckerAction)
                ((IHasCheckerAction)ActionElse).ForAllActionAndChecker(action);
        }
    }
}
