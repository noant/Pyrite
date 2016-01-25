using HierarchicalData;
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
        public bool AllowUserSettings { get { return true; } }

        [Settings]
        private int _timeout = 0;
        [Settings]
        private bool _canCancel;
        [Settings]
        private bool _restart;

        private string _state = "Выключить компьютер";
        private string _stateRestart = "Перезагрузить компьютер";
        public string Do(string inputState)
        {
            IsBusyNow = true;
            var form = new PowerOffForm() { 
                Timer = _timeout, 
                CanCancel = _canCancel, 
                Restart = _restart 
            };

            form.Show();
            form.Start();
            IsBusyNow = false;

            return State;
        }

        public string State
        {
            get
            {
                return _restart ? _stateRestart : _state;
            }
        }

        public string Name
        {
            get { return _state; }
        }

        public bool BeginUserSettings()
        {
            var form = new PowerOffActionView();
            form.Timer = this._timeout;
            form.CanCancel = this._canCancel;
            form.Restart = this._restart;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._timeout = form.Timer;
                this._canCancel = form.CanCancel;
                this._restart = form.Restart;
                return true;
            }
            return false;
        }

        public bool IsBusyNow
        {
            get;
            private set;
        }
        public void Refresh() { }
    }
}
