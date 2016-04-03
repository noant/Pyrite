using PyriteClientIntefaces;
using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ModsExample
{
    /// <summary>
    /// Всегда помечается Serializable
    /// </summary>
    [Serializable]
    public class ShowMessageAction : ICustomAction
    {
        /// <summary>
        /// Возвращать true, если можно изменять свойства из UI. Всегда помечается [XmlIgnore]
        /// </summary>
        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Необходимо выставлять true в начале тела метода Do, false - в конце. Всегда помечать атрибутом [XmlIgnore]
        /// </summary>
        [XmlIgnore]
        public bool IsBusyNow
        {
            get; set;
        }

        /// <summary>
        /// Возвращает имя, которое будет использоваться в UI. Всегда помечать атрибутом [XmlIgnore]
        /// </summary>
        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Показать сообщение";
            }
        }

        /// <summary>
        /// Возвращает текущий статус задачи, который будет отображаться в UI и затем передаваться в метод Do. Всегда помечать атрибутом [XmlIgnore]
        /// </summary>
        [XmlIgnore]
        public string State
        {
            get
            {
                return "Показать сообщение: " + this.Message;
            }
        }

        /// <summary>
        /// Вызывает диалог настройки экземпляра
        /// </summary>
        /// <returns></returns>
        public bool BeginUserSettings()
        {
            var settingsForm = new ShowMessageSettingsForm();
            settingsForm.Message = this.Message;
            if (settingsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Message = settingsForm.Message;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Кастомное свойство
        /// </summary>
        [HumanFriendlyName("Текст сообщения")]
        public string Message { get; set; }

        /// <summary>
        /// Метод вызывает целевое действие
        /// </summary>
        /// <param name="inputState">
        /// Входящий статус, в зависимости от него следует 
        /// корректировать действие и возвращаемое значение
        /// </param>
        /// <returns></returns>
        public string Do(string inputState)
        {
            IsBusyNow = true;
            MessageBox.Show(this.Message);
            IsBusyNow = false;
            return State;
        }

        /// <summary>
        /// В ядре метод вызывается после метода BeginUserSettings
        /// </summary>
        public void Refresh()
        {
            // do nothing
        }
    }
}
