using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreation
{
    [Serializable]
    public class DoubleComplexAction : ICustomAction
    {
        public static readonly string BeginState = "begin";
        public static readonly string EndState = "end";

        public DoubleComplexAction()
        {
            ActionBagBegin = new ActionBag()
            {
                Action = new ComplexAction()
            };
            ActionBagEnd = new ActionBag()
            {
                Action = new ComplexAction()
            };
            CurrentState = CurrentDCActionState.Ended;
        }

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
                return "";
            }
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                if (CurrentState == CurrentDCActionState.Ended)
                    return BeginState;
                else
                    return EndState;
            }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public string Do(string inputState)
        {
            try
            {
                IsBusyNow = true;
                if (inputState == BeginState)
                {
                    CurrentState = CurrentDCActionState.Ended;
                    ActionEnd.Do(string.Empty);
                }
                else
                {
                    CurrentState = CurrentDCActionState.Began;
                    ActionBegin.Do(string.Empty);
                    CurrentState = CurrentDCActionState.Ended;
                    ActionEnd.Do(string.Empty);
                }
            }
            catch { }
            finally
            {
                IsBusyNow = false;
            }
            return inputState;
        }

        [XmlIgnore]
        public CurrentDCActionState CurrentState { get; private set; }

        public ActionBag ActionBagBegin { get; set; }
        public ActionBag ActionBagEnd { get; set; }

        [XmlIgnore]
        public ComplexAction ActionBegin
        {
            get
            {
                return (ComplexAction)ActionBagBegin.Action;
            }
        }

        [XmlIgnore]
        public ComplexAction ActionEnd
        {
            get
            {
                return (ComplexAction)ActionBagEnd.Action;
            }
        }

        public void Refresh()
        {
        }

        public enum CurrentDCActionState
        {
            Began = 1,
            Ended = 2
        }
    }
}
