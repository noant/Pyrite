using PyriteClientIntefaces;
using System;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class DateBetweenChecker : ICustomChecker
    {
        [HumanFriendlyName("Дата 1")]
        public DateTime DateFirst { get; set; }
        [HumanFriendlyName("Дата 2")]
        public DateTime DateSecond { get; set; }

        public bool MoreThanOrEqualFirst { get; set; }
        public bool LessThanOrEqualSecond { get; set; }

        public DateBetweenChecker()
        {
            DateFirst = DateTime.Now;
            DateSecond = DateTime.Now;
        }

        private bool IsFirstDateLessOrEqualNow()
        {
            if (MoreThanOrEqualFirst)
                return DateFirst <= DateTime.Now;
            else return DateFirst < DateTime.Now;
        }

        private bool IsSecondDateMoreOrEqualNow()
        {
            if (MoreThanOrEqualFirst)
                return DateSecond >= DateTime.Now;
            else return DateSecond > DateTime.Now;
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get { return true; }
        }

        public bool BeginUserSettings()
        {
            var form = new DateBetweenCheckerView();
            form.FirstMoreOrEqual = MoreThanOrEqualFirst;
            form.SecondLessOrEqual = LessThanOrEqualSecond;
            form.DateTime1 = DateFirst;
            form.DateTime2 = DateSecond;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DateFirst = form.DateTime1;
                this.DateSecond = form.DateTime2;
                this.MoreThanOrEqualFirst = form.FirstMoreOrEqual;
                this.LessThanOrEqualSecond = form.SecondLessOrEqual;
                return true;
            }
            return false;
        }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                return IsFirstDateLessOrEqualNow() && IsSecondDateMoreOrEqualNow();
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Дата между"; }
        }

        public void Refresh()
        {
        }
    }
}
