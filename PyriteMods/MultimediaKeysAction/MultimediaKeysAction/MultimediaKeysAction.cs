using PyriteClientIntefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MultimediaKeysAction
{
    [Serializable]
    public class MultimediaKeysAction : ICustomAction
    {
        public MultimediaKeysAction()
        {
            this.Key = KeysNames.Keys.First();
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get; private set;
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Мультимедиа";
            }
        }

        [XmlIgnore]
        [HumanFriendlyName("Действие")]
        public string State
        {
            get
            {
                return KeysNames[Key];
            }
        }

        public Keys Key
        {
            get; set;
        }

        public bool BeginUserSettings()
        {
            var form = new MultimediaKeysActionView(this.Key);
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.Key = form.Key;
                return true;
            }
            return false;
        }

        public string Do(string inputState)
        {
            KeyPressWrapper.Press(Key);
            return State;
        }

        public void Refresh()
        {
            //do nothing
        }

        public static readonly Dictionary<Keys, string> KeysNames = new Dictionary<Keys, string>() {
            { Keys.VolumeDown, "Убавить звук" },
            { Keys.VolumeUp, "Прибавить звук" },
            { Keys.VolumeMute, "Убрать звук" },
            { Keys.MediaNextTrack, "Следующий трек" },
            { Keys.MediaPreviousTrack, "Предыдущий трек" },
            { Keys.MediaStop, "Стоп" },
            { Keys.MediaPlayPause, "Плей/пауза" },
        };
    }
}
