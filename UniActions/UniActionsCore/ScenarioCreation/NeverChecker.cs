using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreation
{
    [Serializable]
    public class NeverChecker : ICustomChecker
    {
        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                return false;
            }
        }

        [XmlIgnore]
        public bool AllowUserSettings { get { return false; } }

        [XmlIgnore]
        public string Name
        {
            get { return "Никогда"; }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public void Refresh() { }
    }
}
