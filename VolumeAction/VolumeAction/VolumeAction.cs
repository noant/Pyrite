using System;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace VolumeAction
{
    [Serializable]
    public class VolumeSetAction : ICustomAction
    {
        private VolumeChangeMode _mode = VolumeChangeMode.Set;
        private byte _value = 50;

        [XmlIgnore]
        public string State
        {
            get
            {
                if (Mode == VolumeChangeMode.Minus)
                    return "- Убавить звук";
                if (Mode == VolumeChangeMode.Plus)
                    return "+ Прибавить звук";

                if (Value == 0)
                    return "Убрать звук";
                if (Value == 100)
                    return "Звук на максимум";

                return "Выставить звук на " + Value + " единиц";
            }
        }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            if (Mode == VolumeChangeMode.Set)
                Nircmd.SetSoundVolume(Value);
            else if (Mode == VolumeChangeMode.Plus)
                Nircmd.ChangeSoundVolume(Value);
            else if (Mode == VolumeChangeMode.Minus)
                Nircmd.ChangeSoundVolume(-Value);
            IsBusyNow = false;

            return State;
        }

        public bool BeginUserSettings()
        {
            var form = new FormVolumeAction();
            form.rbMinus.Checked = Mode == VolumeChangeMode.Minus;
            form.rbPlus.Checked = Mode == VolumeChangeMode.Plus;
            form.rbSet.Checked = Mode == VolumeChangeMode.Set;
            form.nudVolume.Value = this.Value;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Mode =
                    form.rbMinus.Checked ? VolumeChangeMode.Minus :
                    form.rbPlus.Checked ? VolumeChangeMode.Plus :
                    VolumeChangeMode.Set;
                Value = (byte)form.nudVolume.Value;
                return true;
            }
            else
                return false;
        }

        public void Refresh() { }

        [XmlIgnore]
        public bool AllowUserSettings { get { return true; } }

        [XmlIgnore]
        public string Name
        {
            get { return "Уровень звука"; }
        }

        [XmlIgnore]
        public bool IsBusyNow { get; private set; }

        public VolumeChangeMode Mode
        {
            get
            {
                return _mode;
            }

            set
            {
                _mode = value;
            }
        }

        public byte Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }
    }

    public enum VolumeChangeMode
    {
        Set,
        Plus,
        Minus
    }
}
