using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreating
{
    [Serializable]
    public class ComplexAction : ICustomAction, IHasChecker
    {
        public string Do(string inputState)
        {
            IsBusyNow = true;

            foreach (var action in Actions)
                action.Do(action.State);

            IsBusyNow = false;

            return State;
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                return "";
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

        public bool HasChecker(Type checker)
        {
            foreach (var action in Actions)
            {
                if (action is IHasChecker)
                    if (((IHasChecker)action).HasChecker(checker))
                        return true;
            }
            return false;
        }

        public List<ICustomAction> Actions { get; set; }
    }
}
