using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveAction;
using ZWaveActionUI.ActionPanels;
using static ZWaveAction.ZWGlobal.Simplified;

namespace ZWaveActionUI
{
    public partial class CheckerForm : Form
    {
        public CheckerForm()
        {
            InitializeComponent();
        }

        private void btSelectValue_Click(object sender, EventArgs e)
        {
            var form = new TargetNodeValueSelectForm(Device, Interface, NodeId, ParameterId);
            form.HomeId = this.HomeId;
            if (form.ShowDialog() == DialogResult.OK)
            {
                Device = form.Device;
                Interface = form.Interface;
                HomeId = form.HomeId;
                NodeId = form.NodeId;
                ParameterId = form.ValueId;
            }
        }

        public string Device { get; set; }
        public ControllerInterface Interface { get; set; }
        public uint? HomeId { get; set; }
        public byte? NodeId { get; set; }

        private ulong? _parameterId { get; set; }
        public ulong? ParameterId
        {
            get
            {
                return _parameterId;
            }
            set
            {
                _parameterId = value;
                Refresh();
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            if (!string.IsNullOrEmpty(Device) && NodeId != null && ParameterId != null)
            {
                var zwave = ZWGlobal.PrepareZWave(Device, Interface);
                if (zwave.WaitForControllerLoaded())
                {
                    var valueID = ZWGlobal.GetZWValueById(ParameterId.Value);
                    if (valueID != null)
                    {
                        tbZWValue.Text = ZWGlobal.GetNodeById(NodeId.Value).Label + "/" + zwave.Manager.GetValueLabel(valueID);
                        valueSetter.ValueID = valueID;
                        btOk.Enabled = true;
                    }
                }
                else MessageBox.Show("Время ожидания отклика от контроллера истекло.");
            }
            else
                btOk.Enabled = false;
        }

        public CheckerMode Mode
        {
            get
            {
                return valueSetter.Setter.Mode;
            }
            set
            {
                valueSetter.Setter.Mode = value;
            }
        }

        public object TargetValue
        {
            get
            {
                return valueSetter.Setter.Value;
            }
            set
            {
                valueSetter.Setter.Value = value;
            }
        }
    }
}
