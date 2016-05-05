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
    public partial class TargetNodeValueSelectForm : Form
    {
        public TargetNodeValueSelectForm(string device, ControllerInterface @interface, byte? nodeId, ulong? parameterId)
        {
            InitializeComponent();

            _device = device;
            _interface = @interface;
            _nodeID = nodeId;
            _valueId = parameterId;

            Refresh();

            btControllerSelect.Click += (o, e) =>
            {
                var form = new ControllerSelectForm(_device, _interface);
                form.HomeId = HomeId;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Device = form.Device;
                    Interface = form.Interface;
                    HomeId = form.HomeId;
                }
            };

            btNodeSelect.Click += (o, e) =>
            {
                var form = new NodeSelectForm(_device, _interface);
                if (this.NodeId != null)
                {
                    form.SelectedNode = ZWGlobal.GetNodeById(this.NodeId.Value);
                }
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.NodeId = form.SelectedNode.ID;
                }
            };

            btParameterSelect.Click += (o, e) =>
            {
                if (_nodeID != null)
                {
                    var form = new SelectNodeValueForm(ZWGlobal.GetNodeById(_nodeID.Value));
                    if (form.ShowDialog() == DialogResult.OK && form.SelectedValue != null)
                    {
                        ValueId = form.SelectedValue.GetId();
                    }
                }
            };
        }

        private string _device;
        public string Device
        {
            get
            {
                return _device;
            }
            set
            {
                _device = value;
                Refresh();
            }
        }

        private ControllerInterface _interface;
        public ControllerInterface Interface
        {
            get
            {
                return _interface;
            }
            set
            {
                _interface = value;
                Refresh();
            }
        }

        private byte? _nodeID;
        public byte? NodeId
        {
            get
            {
                return _nodeID;
            }
            set
            {
                _nodeID = value;
                Refresh();
            }
        }

        private ulong? _valueId;
        public ulong? ValueId
        {
            get
            {
                return _valueId;
            }
            set
            {
                _valueId = value;
                Refresh();
            }
        }

        public uint? HomeId
        {
            get; set;
        }

        public override void Refresh()
        {
            base.Refresh();
            btOk.Enabled = false;
            btNodeSelect.Enabled =
                btParameterSelect.Enabled = true;
            if (!string.IsNullOrEmpty(Device))
            {
                tbControllerPath.Text = Interface.ToString() + "/" + Device;
            }
            else
            {
                tbControllerPath.Text =
                   tbNodeName.Text =
                   tbParameterName.Text = string.Empty;
                btNodeSelect.Enabled =
                    btParameterSelect.Enabled = false;
            }

            if (NodeId != null)
            {
                var node = ZWGlobal.GetNodeById(NodeId.Value);
                if (node != null)
                {
                    this.tbNodeName.Text = node.Label + "/" + node.Product + "/" + node.Manufacturer + "/" + node.Name;
                    if (_valueId != null)
                    {
                        var value = ZWGlobal.GetZWValueById(_valueId.Value);
                        if (value != null)
                        {
                            var zwave = ZWGlobal.GetZWaveByValueID(value);
                            this.tbParameterName.Text = zwave.Manager.GetValueLabel(value) + "/" + zwave.Manager.GetValueUnits(value) + "/" + zwave.Manager.GetValueHelp(value);
                            btOk.Enabled = true;
                        }
                    }
                    else
                        tbParameterName.Text = string.Empty;
                }
            }
            else
            {
                tbNodeName.Text =
                tbParameterName.Text = string.Empty;
                btParameterSelect.Enabled = false;
            }
        }

        public override string ToString()
        {
            return Interface + " / " + Device + "(" + (HomeId ?? 0) + ")" + "/" + (NodeId ?? 0) + "/" + (ValueId ?? 0);
        }
    }
}
