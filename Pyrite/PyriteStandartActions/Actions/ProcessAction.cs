using PyriteClientIntefaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace PyriteStandartActions.Actions
{
    [Serializable]
    public class ProcessAction : ICustomAction
    {
        [XmlIgnore]
        public bool AllowUserSettings { get { return true; } }

        public string StateOn;
        public string StateOff;

        [HumanFriendlyName("Файл")]
        public string Path;
        [HumanFriendlyName("Аргументы")]
        public string Args;
        [HumanFriendlyName("Закрывать главное окно")]
        public bool CloseMainWindow;
        [HumanFriendlyName("Отслеживание состояния")]
        public bool ProcessTracking;
        public string Do(string inputState)
        {
            IsBusyNow = true;
            if (inputState == StateOff)
            {
                if (StartProcess(Path, Args) && ProcessTracking)
                    return StateOn;
                else return StateOff;
            }
            else if (inputState == StateOn)
            {
                if (StopProcess() && ProcessTracking)
                    return StateOff;
                else return StateOn;
            }
            IsBusyNow = false;
            return StateOff;
        }

        private Process _currentProcess;
        private bool StartProcess(string path, string arguments)
        {
            if (File.Exists(path))
            {
                try
                {
                    _currentProcess = new Process();
                    _currentProcess.StartInfo = new ProcessStartInfo(path, arguments);
                    return _currentProcess.Start();
                }
                catch
                {
                    _currentProcess = null;
                    return false;
                }
                finally
                {
                    if (!ProcessTracking)
                        _currentProcess = null;
                }
            }
            else
                return false;
        }

        private bool StopProcess()
        {
            if (_currentProcess != null)
            {
                try
                {
                    if (!CloseMainWindow)
                        _currentProcess.Kill();
                    else _currentProcess.CloseMainWindow();
                    _currentProcess.WaitForExit();
                }
                catch
                {
                }
                try
                {
                    return _currentProcess.HasExited;
                }
                catch
                {
                    return true;
                }
            }
            _currentProcess = null;
            return true;
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                if (_currentProcess == null || !ProcessTracking) return StateOff;
                if (_currentProcess.HasExited) return StateOff;
                else return StateOn;
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Запуск процесса"; }
        }

        public bool BeginUserSettings()
        {
            var form = new ProcessActionView();
            form.Path = this.Path;
            form.Args = this.Args;
            form.StateOn = this.StateOn;
            form.StateOff = this.StateOff;
            form.Tracking = this.ProcessTracking;
            form.CloseMainWindow = this.CloseMainWindow;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Path = form.Path;
                this.Args = form.Args;
                this.StateOn = form.StateOn;
                this.StateOff = form.StateOff;
                this.ProcessTracking = form.Tracking;
                this.CloseMainWindow = form.CloseMainWindow;
                return true;
            }
            else return false;
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get;
            private set;
        }
        public void Refresh() { }
    }
}
