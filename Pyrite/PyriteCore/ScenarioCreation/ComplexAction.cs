using PyriteClientIntefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace PyriteCore.ScenarioCreation
{
    [Serializable]
    public class ComplexAction : ICustomAction, IHasCheckerAction
    {
        public ComplexAction()
        {
            ActionBags = new List<ActionBag>();
        }

        public string Do(string inputState)
        {
            IsBusyNow = true;

            if (ActionBags != null)
                foreach (var bag in ActionBags)
                    bag.Action.Do(bag.Action.State);

            IsBusyNow = false;

            return State;
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                if (this.ActionBags.Count == 1)
                    return this.ActionBags.First().Action.State;
                return string.Empty;
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Комплексная операция"; }
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
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

        public void ForAllActionAndChecker(Action<object> action)
        {
            if (ActionBags != null)
                foreach (var bag in ActionBags)
                {
                    if (bag.Action is IHasCheckerAction)
                    {
                        ((IHasCheckerAction)bag.Action).ForAllActionAndChecker(action);
                    }
                    else
                        action(bag.Action);
                }
        }

        public bool RemoveChecker(Type checkerType)
        {
            bool result = false;
            if (ActionBags != null)
                foreach (var bag in ActionBags)
                {
                    if (bag.Action is IHasCheckerAction)
                    {
                        if (((IHasCheckerAction)bag.Action).RemoveChecker(checkerType))
                            result = true;
                    }
                }
            return result;
        }

        public bool RemoveAction(Type actionType)
        {
            bool result = false;
            if (ActionBags != null)
                foreach (var bag in ActionBags.ToArray())
                {
                    if (bag.Action is IHasCheckerAction)
                    {
                        if (((IHasCheckerAction)bag.Action).RemoveAction(actionType))
                            result = true;
                    }
                    else if (bag.Action.GetType().Equals(actionType))
                    {
                        ActionBags.Remove(bag);
                        result = true;
                    }
                }
            return result;
        }

        public List<ActionBag> ActionBags { get; set; }
    }

    [Serializable]
    public class ActionBag
    {
        [XmlIgnore]
        public ICustomAction Action { get; set; }

        //for xml serialization
        public object ActionObj
        {
            get
            {
                return Action;
            }
            set
            {
                Action = (ICustomAction)value;
            }
        }
    }
}
