using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using UniActionsClientIntefaces;

namespace UniStandartActions.Actions
{
    public class MessageShowAction : ICustomAction
    {
        private string _message = "";

        public string Do(string inputState)
        {
            MessageShow.SetMessage(inputState);
            return _message;
        }

        public string CheckState()
        {
            return _message;
        }

        public string Name
        {
            get { return "Показать сообщение"; }
        }

        public bool InitializeNew()
        {
            var form = new MessageShowActionView();
            form.Message = _message;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._message = form.Message;
                return true;
            }
            else return false;
        }

        public void SetFromString(string settings)
        {
            _message = settings;
        }

        public string SetToString()
        {
            return _message;
        }
    }
}
