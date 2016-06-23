using PyriteClientIntefaces;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class EveryIterationChecker : ICustomChecker
    {
        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                return true;
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Всегда"; }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        [XmlIgnore]
        public bool AllowUserSettings { get { return false; } }

        public void Refresh() { }
    }
}
