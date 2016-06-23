using PyriteClientIntefaces;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class DayOfWeekChecker : ICustomChecker
    {
        public DayOfWeekChecker()
        {
            this.DayOfWeek = DateTime.Now.DayOfWeek;
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                return DateTime.Now.DayOfWeek.Equals(DayOfWeek);
            }
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "День недели";
            }
        }

        public bool BeginUserSettings()
        {
            var form = new DayOfWeekCheckerView();
            form.DayOfWeek = this.DayOfWeek;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DayOfWeek = form.DayOfWeek;
                return true;
            }
            return false;
        }

        [HumanFriendlyName("Значение")]
        public DayOfWeek DayOfWeek { get; set; }

        public void Refresh()
        {
        }
    }
}
