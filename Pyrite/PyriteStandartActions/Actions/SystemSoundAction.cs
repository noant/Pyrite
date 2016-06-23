using PyriteClientIntefaces;
using System;
using System.Media;
using System.Xml.Serialization;

namespace PyriteStandartActions.Actions
{
    [Serializable]
    public class SystemSoundAction : ICustomAction
    {
        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return false;
            }
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get
            {
                return false;
            }
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Звуковой сигнал";
            }
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                return string.Empty;
            }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public string Do(string inputState)
        {
            SystemSounds.Hand.Play();
            return State;
        }

        public void Refresh()
        {

        }
    }
}
