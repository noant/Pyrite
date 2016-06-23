using PyriteClientIntefaces;
using System;
using System.Threading;
using System.Xml.Serialization;

namespace PyriteCore.ScenarioCreation
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
                {
                    Action.Do("");
                    Thread.Sleep(1);
                }
            return "";
        }

        public void Refresh()
        {

        }

        public bool RemoveChecker(Type checkerType)
        {
            bool result = false;
            if (Checker.GetType().Equals(checkerType))
                Checker = null;
            else if (Checker != null && Checker is IHasCheckerAction)
                if (((IHasCheckerAction)Checker).RemoveChecker(checkerType))
                    result = true;

            if (Action != null && Action is IHasCheckerAction)
                if (((IHasCheckerAction)Action).RemoveChecker(checkerType))
                    result = true;
            return result;
        }


        public bool RemoveAction(Type actionType)
        {
            if (Action != null && Action is IHasCheckerAction)
                if (((IHasCheckerAction)Action).RemoveAction(actionType))
                    return true;
            return false;
        }

        public void ForAllActionAndChecker(Action<object> action)
        {
            if (Checker != null && Checker is IHasCheckerAction)
                ((IHasCheckerAction)Checker).ForAllActionAndChecker(action);

            if (Action != null && Action is IHasCheckerAction)
                ((IHasCheckerAction)Action).ForAllActionAndChecker(action);
        }
    }
}
