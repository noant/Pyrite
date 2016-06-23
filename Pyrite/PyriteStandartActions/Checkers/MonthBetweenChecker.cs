using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class MonthBetweenChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public BetweenValuePartialImplementation Implementation { get; set; }

        public MonthBetweenChecker()
        {
            Implementation = new BetweenValuePartialImplementation();

            Implementation.Max = 12;
            Implementation.Min = 1;

            Implementation.Value1 = DateTime.Now.Month;
            Implementation.Value2 = DateTime.Now.Month;

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
                return Implementation.IsBetween(DateTime.Now.Month);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Месяц между"; }
        }

        public void Refresh()
        {
        }
    }
}
