using PyriteClientIntefaces;
using System;
using System.Threading;
using System.Xml.Serialization;

namespace PyriteStandartActions.Actions
{
    [Serializable]
    public class WaitAction : ICustomAction
    {
        [HumanFriendlyName("Минут")]
        public decimal Minutes { get; set; }

        public WaitAction()
        {
            Minutes = 5;
        }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            Thread.Sleep((int)(Minutes * 1000 * 60));
            IsBusyNow = false;
            return State;
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                return "Ожидать " + Minutes + (Minutes != 1 ? " минут(ы)" : " минуту");
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Ожидание"; }
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get { return true; }
        }

        public bool BeginUserSettings()
        {
            WaitActionView form = new WaitActionView();
            form.nudMinutes.Value = Minutes;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Minutes = form.nudMinutes.Value;
            else return false;
            return true;
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get;
            private set;
        }

        public void Refresh()
        { }
    }
}
