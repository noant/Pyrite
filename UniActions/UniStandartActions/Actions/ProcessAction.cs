using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace UniStandartActions.Actions
{
    public class ProcessAction : ICustomAction
    {
        private string _stateOn;
        private string _stateOff;
        private string _path;
        private string _args;
        private bool _closeMainWindow;
        private bool _processTracking;
        public string Do(string inputState)
        {
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

        public string CheckState()
        {
            if (_currentProcess == null || !_processTracking) return _stateOff;
            if (_currentProcess.HasExited) return _stateOff;
            else return _stateOn;
        }

        public string Name
        {
            get { return "Запуск процесса"; }
        }

        public bool InitializeNew()
        {
            var form = new ProcessActionView();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._path = Validate(form.Path);
                this._args = Validate(form.Args);
                this._stateOn = Validate(form.StateOn);
                this._stateOff = Validate(form.StateOff);
                this._processTracking = form.Tracking;
                this._closeMainWindow = form.CloseMainWindow;
                return true;
            }
            else return false;
        }

        private string Validate(string str)
        {
            return str.Replace(_splitter, "???");
        }

        private string _splitter = "###";

        public void SetFromString(string settings)
        {
            var strs = settings.Split(new string[] { _splitter }, StringSplitOptions.RemoveEmptyEntries);
            this._stateOff = strs[0];
            this._stateOn = strs[1];
            this._path = strs[2];
            this._args = strs[3];
            this._processTracking = strs[4] == "1";
            this._closeMainWindow = strs[5] == "1";
        }

        public string SetToString()
        {
            return _stateOff + _splitter + _stateOn + _splitter + _path + _splitter + (string.IsNullOrEmpty(_args) ? " " : _args) + _splitter + (this._processTracking ? "1" : "0") + _splitter + (this._closeMainWindow ? "1" : "0");
        }
    }
}
