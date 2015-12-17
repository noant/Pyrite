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
        VolumeChangeMode _mode = VolumeChangeMode.Set;
        byte _value = 50;
        public string CheckState()
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

        public string Do(string inputState)
        {
            if (_mode == VolumeChangeMode.Set)
                Nircmd.SetSoundVolume(_value);
            else if (_mode == VolumeChangeMode.Plus)
                Nircmd.ChangeSoundVolume(_value);
            else if (_mode == VolumeChangeMode.Minus)
                Nircmd.ChangeSoundVolume(-_value);

            return CheckState();
        }

        public bool InitializeNew()
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

        public string Name
        {
            get { return "Уровень звука"; }
        }

        string _splitter = "#";
        public void SetFromString(string settings)
        {
            var strs = settings.Split(_splitter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            _value = byte.Parse(strs[0]);
            _mode = strs[1] == "0" ? VolumeChangeMode.Minus :
                    strs[1] == "1" ? VolumeChangeMode.Plus :
                                       VolumeChangeMode.Set;
        }

        public string SetToString()
        {
            return
                _value.ToString() + _splitter +
                    (_mode == VolumeChangeMode.Minus ? "0" :
                    _mode == VolumeChangeMode.Plus ? "1" :
                    "2");
        }
    }

    public enum VolumeChangeMode
    { 
        Set,
        Plus,
        Minus
    }
}
