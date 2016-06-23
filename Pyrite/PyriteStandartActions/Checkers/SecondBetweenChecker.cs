using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class SecondBetweenChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public BetweenValuePartialImplementation Implementation { get; set; }

        public SecondBetweenChecker()
        {
            Implementation = new BetweenValuePartialImplementation();

            Implementation.Max = 59;
            Implementation.Min = 0;

            Implementation.Value1 = DateTime.Now.Second;
            Implementation.Value2 = DateTime.Now.Second;

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
                return Implementation.IsBetween(DateTime.Now.Second);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Секунда между"; }
        }

        public void Refresh()
        {
        }
    }
}
