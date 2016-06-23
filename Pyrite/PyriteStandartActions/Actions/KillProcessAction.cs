using PyriteClientIntefaces;
using PyriteStandartActions.Actions.Utils;
using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace PyriteStandartActions.Actions
{
    [Serializable]
    public class KillProcessAction : ICustomAction
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
            get; set;
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Убить процесс";
            }
        }

        [HumanFriendlyName("Процесс")]
        public string ProcessName { get; set; }

        [XmlIgnore]
        public string State
        {
            get
            {
                return "Убить процесс \"" + ProcessName + "\"";
            }
        }

        public bool BeginUserSettings()
        {
            var form = new EnterStringForm()
            {
                Text = "Ввод названия процесса",
                Value = ProcessName
            };
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProcessName = form.Value;
                return true;
            }
            return false;
        }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            foreach (Process proc in Process.GetProcessesByName(ProcessName))
                try
                {
                    proc.Kill();
                }
                catch { }
            IsBusyNow = false;
            return State;
        }

        public void Refresh()
        {
        }
    }
}
