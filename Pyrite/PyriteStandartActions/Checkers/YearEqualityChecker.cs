using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class YearEqualityChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public ValueEqualityPartialImplementation Implementation { get; set; }

        public YearEqualityChecker()
        {
            Implementation = new ValueEqualityPartialImplementation();

            Implementation.Max = 9999;
            Implementation.Min = 1800;

            Implementation.Value = DateTime.Now.Year;

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
                return Implementation.IsPertain(DateTime.Now.Year);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Год"; }
        }

        public void Refresh()
        {
        }
    }
}
