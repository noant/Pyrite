using PyriteClientIntefaces;
using System;
using System.Xml.Serialization;

namespace PyriteCore.ScenarioCreation
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
