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
    public class ZWaveChecker : ICustomChecker
    {
        public ZWaveChecker()
        {
            Value = 0;
            Interface = ControllerInterface.Serial;
            Mode = CheckerMode.Equals;
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
        public string Name
        {
            get
            {
                return "ZWave проверка";
            }
        }

        public bool BeginUserSettings()
        {
            var form = new CheckerForm();
            form.Device = this.Device;
            form.Interface = this.Interface;
            form.NodeId = this.NodeId;
            form.HomeId = this.HomeId;
            form.ParameterId = this.ParameterId;
            form.Mode = this.Mode; // set only if ParameterId not null
            form.TargetValue = this.Value;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _tempDeviceName = null;
                this.Device = form.Device;
                this.Interface = form.Interface;
                this.NodeId = form.NodeId.Value;
                this.HomeId = form.HomeId.Value;
                this.Value = form.TargetValue;
                this.ParameterId = form.ParameterId.Value;
                this.Mode = form.Mode; // set only if ParameterId not null
                return true;
            }
            return false;
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

        public CheckerMode Mode { get; set; }

        public string Device { get; set; }
        public ControllerInterface Interface { get; set; }
        public uint HomeId { get; set; }
        public byte NodeId { get; set; }
        public ulong ParameterId { get; set; }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                try
                {
                    if (string.IsNullOrEmpty(Device))
                        return false;
                    if (Mode == CheckerMode.Equals)
                        return ZWGlobal.Simplified.IsValueEquals(Device, Interface, HomeId, NodeId, ParameterId, Value);
                    else if (Mode == CheckerMode.Less)
                        return ZWGlobal.Simplified.IsValueLessThan(Device, Interface, HomeId, NodeId, ParameterId, Value);
                    else if (Mode == CheckerMode.LessOrEquals)
                        return ZWGlobal.Simplified.IsValueLessThanOrEqual(Device, Interface, HomeId, NodeId, ParameterId, Value);
                    else if (Mode == CheckerMode.More)
                        return ZWGlobal.Simplified.IsValueMoreThan(Device, Interface, HomeId, NodeId, ParameterId, Value);
                    else if (Mode == CheckerMode.MoreOrEquals)
                        return ZWGlobal.Simplified.IsValueMoreThanOrEqual(Device, Interface, HomeId, NodeId, ParameterId, Value);
                }
                catch
                {
                    // do nothing
                }
                return false;
            }
        }
    }
}
