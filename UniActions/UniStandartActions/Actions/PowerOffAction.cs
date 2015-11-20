using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace UniStandartActions.Actions
{
    public class PowerOffAction : ICustomAction
    {
        private int _timeout = 0;
        private bool _canCancel;
        private bool _restart;
        private string _state = "Выключить компьютер";
        private string _stateRestart = "Перезагрузить компьютер";
        public string Do(string inputState)
        {
            var form = new PowerOffForm() { 
                Timer = _timeout, 
                CanCancel = _canCancel, 
                Restart = _restart 
            };

            form.Show();
            form.Start();

            return CheckState();
        }

        public string CheckState()
        {
            return _restart ? _stateRestart : _state;
        }

        public string Name
        {
            get { return _state; }
        }

        public bool InitializeNew()
        {
            var form = new PowerOffActionView();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._timeout = form.Timer;
                this._canCancel = form.CanCancel;
                this._restart = form.Restart;
                return true;
            }
            return false;
        }

        string _splitter = "#";
        public void SetFromString(string settings)
        {
            var strs = settings.Split(_splitter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            _canCancel = strs[0][0] == '1';
            _restart = strs[0][1] == '1';

            _timeout = int.Parse(strs[1]);
        }

        public string SetToString()
        {
            return
                (_canCancel ? "1" : "0") +
                (_restart ? "1" : "0") +
                _splitter + _timeout; 
        }
    }
}
