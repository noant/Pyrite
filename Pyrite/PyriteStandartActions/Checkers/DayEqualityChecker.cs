using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class DayEqualityChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public ValueEqualityPartialImplementation Implementation { get; set; }

        public DayEqualityChecker()
        {
            Implementation = new ValueEqualityPartialImplementation();

            Implementation.Max = 31;
            Implementation.Min = 1;

            Implementation.Value = DateTime.Now.Day;

            Implementation.ValueName = this.Name;
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get { return true; }
        }

        public bool BeginUserSettings()
        {
            return Implementation.BeginSettings();
        }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                return Implementation.IsPertain(DateTime.Now.Day);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "День"; }
        }

        public void Refresh()
        {
        }
    }
}
