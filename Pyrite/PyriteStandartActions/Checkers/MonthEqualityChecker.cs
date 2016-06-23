using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class MonthEqualityChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public ValueEqualityPartialImplementation Implementation { get; set; }

        public MonthEqualityChecker()
        {
            Implementation = new ValueEqualityPartialImplementation();

            Implementation.Min = 1;
            Implementation.Max = 12;

            Implementation.Value = DateTime.Now.Month;

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
                return Implementation.IsPertain(DateTime.Now.Month);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Месяц"; }
        }

        public void Refresh()
        {
        }
    }
}
