using HierarchicalData;
using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace UniStandartActions.Actions
{
    [Serializable]
    public class MessageShowAction : ICustomAction
    {
        [HumanFriendlyName("Сообщение")]
        public string Message { get; set; }

        [XmlIgnore]
        public bool AllowUserSettings { get { return true; } }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            MessageShow.SetMessage(inputState);
            IsBusyNow = false;
            return Message;
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                return Message;
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Показать сообщение"; }
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get;
            private set;
        }

        public bool BeginUserSettings()
        {
            var form = new MessageShowActionView();
            form.Message = Message;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Message = form.Message;
                return true;
            }
            else return false;
        }

        public void Refresh() {
        }
    }
}
