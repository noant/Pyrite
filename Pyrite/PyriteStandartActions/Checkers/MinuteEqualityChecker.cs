using PyriteClientIntefaces;
using PyriteStandartActions.Checkers.Utils;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class MinuteEqualityChecker : ICustomChecker
    {
        [HumanFriendlyName("Выражение")]
        public ValueEqualityPartialImplementation Implementation { get; set; }

        public MinuteEqualityChecker()
        {
            Implementation = new ValueEqualityPartialImplementation();

            Implementation.Min = 0;
            Implementation.Max = 59;

            Implementation.Value = DateTime.Now.Minute;

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
                return Implementation.IsPertain(DateTime.Now.Minute);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Минута"; }
        }

        public void Refresh()
        {
        }
    }
}
