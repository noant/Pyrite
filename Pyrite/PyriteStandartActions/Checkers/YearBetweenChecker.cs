using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class YearBetweenChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public BetweenValuePartialImplementation Implementation { get; set; }

        public YearBetweenChecker()
        {
            Implementation = new BetweenValuePartialImplementation();

            Implementation.Max = 9999;
            Implementation.Min = 1800;

            Implementation.Value1 = DateTime.Now.Year;
            Implementation.Value2 = DateTime.Now.Year;

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
                return Implementation.IsBetween(DateTime.Now.Year);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Год между"; }
        }

        public void Refresh()
        {
        }
    }
}
