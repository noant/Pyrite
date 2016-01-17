using HierarchicalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace VolumeAction
{
    public class VolumeSetAction : ICustomAction
    {
        [Settings]
        VolumeChangeMode _mode = VolumeChangeMode.Set;
        [Settings]
        byte _value = 50;
        public string State
        {
            get
            {
                if (_mode == VolumeChangeMode.Minus)
                    return "- Убавить звук";
                if (_mode == VolumeChangeMode.Plus)
                    return "+ Прибавить звук";

                if (_value == 0)
                    return "Убрать звук";
                if (_value == 100)
                    return "Звук на максимум";

                return "Выставить звук на " + _value + " единиц";
            }
        }

        public string Do(string inputState)
        {
            if (_mode == VolumeChangeMode.Set)
                Nircmd.SetSoundVolume(_value);
            else if (_mode == VolumeChangeMode.Plus)
                Nircmd.ChangeSoundVolume(_value);
            else if (_mode == VolumeChangeMode.Minus)
                Nircmd.ChangeSoundVolume(-_value);

            return State;
        }

        public bool BeginUserSettings()
        {
            var form = new FormVolumeAction();
            form.rbMinus.Checked = _mode == VolumeChangeMode.Minus;
            form.rbPlus.Checked = _mode == VolumeChangeMode.Plus;
            form.rbSet.Checked = _mode == VolumeChangeMode.Set;
            form.nudVolume.Value = this._value;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _mode =
                    form.rbMinus.Checked ? VolumeChangeMode.Minus :
                    form.rbPlus.Checked ? VolumeChangeMode.Plus :
                    VolumeChangeMode.Set;
                _value = (byte)form.nudVolume.Value;
                return true;
            }
            else
                return false;
        }

        public void Refresh() { }

        public bool AllowUserSettings { get { return true; } }

        public string Name
        {
            get { return "Уровень звука"; }
        }
    }

    public enum VolumeChangeMode
    { 
        Set,
        Plus,
        Minus
    }
}
