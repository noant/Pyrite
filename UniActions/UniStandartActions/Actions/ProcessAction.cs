using HierarchicalData;
using System.Diagnostics;
using System.IO;
using UniActionsClientIntefaces;

namespace UniStandartActions.Actions
{
    public class ProcessAction : ICustomAction
    {
        public bool AllowUserSettings { get { return true; } }

        [Settings]
        private string _stateOn;
        [Settings]
        private string _stateOff;
        [Settings]
        private string _path;
        [Settings]
        private string _args;
        [Settings]
        private bool _closeMainWindow;
        [Settings]
        private bool _processTracking;
        public string Do(string inputState)
        {
            IsBusyNow = true;
            if (inputState == _stateOff)
            {
                if (StartProcess(_path, _args) && _processTracking)
                    return _stateOn;
                else return _stateOff;
            }
            else if (inputState == _stateOn)
            {
                if (StopProcess() && _processTracking)
                    return _stateOff;
                else return _stateOn;
            }
            IsBusyNow = false;
            return _stateOff;
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
                finally {
                    if (!_processTracking)
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
                    if (!_closeMainWindow)
                        _currentProcess.Kill();
                    else _currentProcess.CloseMainWindow();
                    _currentProcess.WaitForExit();
                }
                catch{
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

        public string State
        {
            get
            {
                if (_currentProcess == null || !_processTracking) return _stateOff;
                if (_currentProcess.HasExited) return _stateOff;
                else return _stateOn;
            }
        }

        public string Name
        {
            get { return "Запуск процесса"; }
        }

        public bool BeginUserSettings()
        {
            var form = new ProcessActionView();
            form.Path = this._path;
            form.Args = this._args;
            form.StateOn = this._stateOn;
            form.StateOff = this._stateOff;
            form.Tracking = this._processTracking;
            form.CloseMainWindow = this._closeMainWindow;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._path = form.Path;
                this._args = form.Args;
                this._stateOn = form.StateOn;
                this._stateOff = form.StateOff;
                this._processTracking = form.Tracking;
                this._closeMainWindow = form.CloseMainWindow;
                return true;
            }
            else return false;
        }

        public bool IsBusyNow
        {
            get;
            private set;
        }
        public void Refresh() { }
    }
}
