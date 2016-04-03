using PyriteClientIntefaces;
using System;
using System.Xml.Serialization;

namespace ModsExample
{
    /// <summary>
    /// Всегда помечается Serializable
    /// </summary>
    [Serializable]
    public class TimeOfDayChecker : ICustomChecker
    {
        /// <summary>
        /// Возвращать true, если можно изменять свойства из UI. Всегда помечать атрибутом [XmlIgnore]
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
        /// Кастомное свойство
        /// </summary>
        [HumanFriendlyName("Время суток")]
        public TimeOfDay TimeOfDay { get; set; }

        /// <summary>
        /// Указывает, верно ли целевое утверждение сейчас. Всегда помечать атрибутом [XmlIgnore]
        /// </summary>
        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18 && TimeOfDay == TimeOfDay.Day)
                    return true;
                if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 24 && TimeOfDay == TimeOfDay.Evening)
                    return true;
                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 6 && TimeOfDay == TimeOfDay.Night)
                    return true;
                if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12 && TimeOfDay == TimeOfDay.Morning)
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Возвращает имя, которое будет использоваться в UI. Всегда помечать атрибутом [XmlIgnore]
        /// </summary>
        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Время дня";
            }
        }

        /// <summary>
        /// Вызывает диалог настройки экземпляра
        /// </summary>
        /// <returns></returns>
        public bool BeginUserSettings()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// В ядре метод вызывается после метода BeginUserSettings
        /// </summary>
        public void Refresh()
        {
            // do nothing
        }
    }

    public enum TimeOfDay
    {
        Morning = 0,
        Day = 1,
        Evening = 2,
        Night = 4
    }
}
