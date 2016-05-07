using PyriteClientIntefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ZWaveAction;
using ZWaveActionUI;
using ZWaveActionUI.ActionPanels;
using static ZWaveAction.ZWGlobal.Simplified;

namespace ZWaveActionImplementations
{
    [Serializable]
    public class ZWaveAction : ICustomAction
    {
        public ZWaveAction()
        {
            Value = 0;
            Interface = ControllerInterface.Serial;
            Mode = AppendType.Equalize;
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
                return "ZWave действие";
            }
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    if (ZWGlobal.IsValueBoolAndTrue(this.ParameterId))
                    {
                        return "Выключить: " + Text.ToLower();
                    }
                    return Text;
                }

                if (Value == null)
                    return DeviceName;

                var mode = "Выставить";
                var operation = "=";
                if (Mode == AppendType.Increment)
                {
                    mode = "Увеличить";
                    operation = "на";
                }
                if (Mode == AppendType.Decrement)
                {
                    mode = "Уменьшить";
                    operation = "на";
                }

                return string.Format("{0} {1} {2} {3}", mode, DeviceName, operation, Value);
            }
        }

        public bool BeginUserSettings()
        {
            var form = new ActionForm();
            form.Device = this.Device;
            form.Interface = this.Interface;
            form.NodeId = this.NodeId;
            form.HomeId = this.HomeId;
            form.ParameterId = this.ParameterId;
            form.Mode = this.Mode; // set only if ParameterId not null
            form.TargetValue = this.Value;
            form.ButtonText = this.Text;
            form.InvertValueIfBool = this.InvertValueIfBool; // set only if ParameterId not null

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _tempDeviceName = null;
                this.Device = form.Device;
                this.Interface = form.Interface;
                this.NodeId = form.NodeId.Value;
                this.HomeId = form.HomeId.Value;
                this.Value = form.TargetValue;
                this.ParameterId = form.ParameterId.Value;
                this.InvertValueIfBool = form.InvertValueIfBool; // set only if ParameterId not null
                this.Mode = form.Mode; // set only if ParameterId not null
                this.Text = form.ButtonText;
                return true;
            }
            return false;
        }

        public string Do(string inputState)
        {
            Helper.PrepareController(Device, Interface);
            IsBusyNow = true;
            try
            {
                if (!string.IsNullOrEmpty(Device))
                    ZWGlobal.Simplified.SetValue(Device, Interface, HomeId, NodeId, ParameterId, Value, InvertValueIfBool, Mode);
            }
            catch (Exception e)
            {
                throw e;
                // do nothing
            }
            IsBusyNow = false;
            return State;
        }

        public void Refresh()
        {
            Helper.PrepareController(Device, Interface);
        }

        [HumanFriendlyName("Значение")]
        public object Value { get; set; }

        [XmlIgnore]
        [HumanFriendlyName("Устройство")]
        public string DeviceName
        {
            get
            {
                Helper.PrepareController(Device, Interface);
                if (string.IsNullOrEmpty(_tempDeviceName))
                    _tempDeviceName = (ZWGlobal.GetNodeLabel(NodeId) ?? "[пусто]") + "/" + (ZWGlobal.GetValueIDLabel(ParameterId) ?? "[пусто]");

                return _tempDeviceName;
            }
        }
        private string _tempDeviceName;

        public AppendType Mode { get; set; }

        public bool InvertValueIfBool { get; set; }

        public string Device { get; set; }
        public ControllerInterface Interface { get; set; }
        public uint HomeId { get; set; }
        public byte NodeId { get; set; }
        public ulong ParameterId { get; set; }

        public string Text { get; set; }
    }
}
