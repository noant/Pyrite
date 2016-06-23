using PyriteClientIntefaces;
using System;
using System.Xml.Serialization;

namespace PyriteCore.ScenarioCreation
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
