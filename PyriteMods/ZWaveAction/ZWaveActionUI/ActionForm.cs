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

namespace ZWaveActionUI
{
    public partial class ActionForm : Form
    {
        public ActionForm()
        {
            InitializeComponent();
        }

        private void btSelectValue_Click(object sender, EventArgs e)
        {
            var form = new TargetNodeValueSelectForm(Device, Interface, NodeId, ParameterId);
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
        public uint? HomeId { get; private set; }
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
            if (NodeId != null && ParameterId != null)
            {
                var zwave = ZWGlobal.PrepareZWave(Device, Interface);
                if (zwave.WaitForControllerLoaded())
                {
                    var valueID = ZWGlobal.GetZWValueById(ParameterId.Value);
                    tbZWValue.Text = ZWGlobal.GetNodeById(NodeId.Value).Label + "/" + zwave.Manager.GetValueLabel(valueID);
                    valueSetter.ValueID = valueID;
                    btOk.Enabled = true;
                }
                else MessageBox.Show("Время ожидания отклика от контроллера истекло.");
            }
            else
                btOk.Enabled = false;
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
