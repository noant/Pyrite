using PyriteClientIntefaces;
using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace PyriteStandartActions.Actions
{
    [Serializable]
    public class ExecuteCommandAction : ICustomAction
    {
        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get; private set;
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Команда консоли";
            }
        }

        [HumanFriendlyName("Команда")]
        public string Command { get; set; }

        [HumanFriendlyName("Название")]
        public string ViewName { get; set; }

        [XmlIgnore]
        public string State
        {
            get
            {
                return string.IsNullOrWhiteSpace(ViewName) ? Command : ViewName;
            }
        }

        public bool BeginUserSettings()
        {
            var form = new ExecuteCommandActionView();
            form.Command = Command;
            form.ViewName = ViewName;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Command = form.Command;
                this.ViewName = form.ViewName;
                return true;
            }
            else return false;
        }

        public string Do(string inputState)
        {
            var processStartInfo =
                        new ProcessStartInfo("cmd", "/c " + Command);

            processStartInfo.CreateNoWindow = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(processStartInfo);

            return State;
        }

        public void Refresh()
        {
        }
    }
}
