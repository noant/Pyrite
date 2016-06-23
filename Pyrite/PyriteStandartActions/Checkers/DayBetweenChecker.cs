using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class DayBetweenChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public BetweenValuePartialImplementation Implementation { get; set; }

        public DayBetweenChecker()
        {
            Implementation = new BetweenValuePartialImplementation();

            Implementation.Max = 31;
            Implementation.Min = 1;

            Implementation.Value1 = DateTime.Now.Day;
            Implementation.Value2 = DateTime.Now.Day;

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
                return Implementation.IsBetween(DateTime.Now.Day);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "День между"; }
        }

        public void Refresh()
        {
        }
    }
}
