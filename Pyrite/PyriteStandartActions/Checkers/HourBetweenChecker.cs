using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class HourBetweenChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public BetweenValuePartialImplementation Implementation { get; set; }

        public HourBetweenChecker()
        {
            Implementation = new BetweenValuePartialImplementation();

            Implementation.Max = 23;
            Implementation.Min = 0;

            Implementation.Value1 = DateTime.Now.Hour;
            Implementation.Value2 = DateTime.Now.Hour;

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
                return Implementation.IsBetween(DateTime.Now.Hour);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Час между"; }
        }

        public void Refresh()
        {
        }
    }
}
