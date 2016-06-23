using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class HourEqualityChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public ValueEqualityPartialImplementation Implementation { get; set; }

        public HourEqualityChecker()
        {
            Implementation = new ValueEqualityPartialImplementation();

            Implementation.Min = 0;
            Implementation.Max = 23;

            Implementation.Value = DateTime.Now.Hour;

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
                return Implementation.IsPertain(DateTime.Now.Hour);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Час"; }
        }

        public void Refresh()
        {
        }
    }
}
