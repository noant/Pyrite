using HierarchicalData;
using UniActionsClientIntefaces;

namespace UniStandartActions.Actions
{
    public class MessageShowAction : ICustomAction
    {
        [Settings]
        private string _message = "";

        public bool AllowUserSettings { get { return true; } }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            MessageShow.SetMessage(inputState);
            IsBusyNow = false;
            return _message;
        }

        public string State
        {
            get
            {
                return _message;
            }
        }

        public string Name
        {
            get { return "Показать сообщение"; }
        }

        public bool IsBusyNow
        {
            get;
            private set;
        }

        public bool BeginUserSettings()
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

        public void Refresh() { }
    }
}
