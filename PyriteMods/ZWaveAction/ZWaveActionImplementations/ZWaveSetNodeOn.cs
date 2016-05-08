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
    public class ZWaveSetNodeOn : ICustomAction
    {
        public ZWaveSetNodeOn()
        {
            Interface = ControllerInterface.Serial;
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
                return "ZWave запустить узел";
            }
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                return DeviceName;
            }
        }

        public bool BeginUserSettings()
        {
            var form = new TargetNodeValueSelectForm("", ControllerInterface.Serial, null, null);
            form.HideParameterSelect();
            form.Device = this.Device;
            form.Interface = this.Interface;
            form.NodeId = this.NodeId;
            form.HomeId = this.HomeId;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _tempDeviceName = null;
                this.Device = form.Device;
                this.Interface = form.Interface;
                this.NodeId = form.NodeId.Value;
                this.HomeId = form.HomeId.Value;
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
                {
                    ZWGlobal.Simplified.SetNodeOn(true, Device, Interface, HomeId, NodeId);
                }
            }
            catch
            {
                // do nothing
            }
            IsBusyNow = false;
            return State;
        }

        public void Refresh()
        {
            Helper.PrepareController(Device, Interface);
        }

        [XmlIgnore]
        [HumanFriendlyName("Устройство")]
        public string DeviceName
        {
            get
            {
                Helper.PrepareController(Device, Interface);
                if (string.IsNullOrEmpty(_tempDeviceName))
                    _tempDeviceName = (ZWGlobal.GetNodeLabel(NodeId) ?? "[пусто]");

                return _tempDeviceName;
            }
        }
        private string _tempDeviceName;

        public string Device { get; set; }
        public ControllerInterface Interface { get; set; }
        public uint HomeId { get; set; }
        public byte NodeId { get; set; }
    }
}
