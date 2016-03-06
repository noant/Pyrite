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
    public class DoNothingAction : ICustomAction
    {
        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return false;
            }
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get
            {
                return false;
            }
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Ничего не делать";
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
            return "";
        }

        public void Refresh()
        {
        }
    }
}
