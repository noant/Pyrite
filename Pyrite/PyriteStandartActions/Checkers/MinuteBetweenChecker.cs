using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class MinuteBetweenChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public BetweenValuePartialImplementation Implementation { get; set; }

        public MinuteBetweenChecker()
        {
            Implementation = new BetweenValuePartialImplementation();

            Implementation.Max = 59;
            Implementation.Min = 0;

            Implementation.Value1 = DateTime.Now.Minute;
            Implementation.Value2 = DateTime.Now.Minute;

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
                return Implementation.IsBetween(DateTime.Now.Minute);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Минута между"; }
        }

        public void Refresh()
        {
        }
    }
}
