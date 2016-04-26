using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveActions;

namespace ZWaveActionsUI
{
    public partial class TargetNodeValueSelectForm : Form
    {
        public TargetNodeValueSelectForm(string device, ControllerInterface @interface)
        {
            InitializeComponent();
            Device = device;
            Interface = @interface;

            btControllerSelect.Click += (o, e) =>
            {
                var form = new ControllerSelectForm(_device, _interface);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Device = form.Device;
                    Interface = form.Interface;
                }
            };

            btNodeSelect.Click += (o, e) =>
            {
                var form = new NodeSelectForm(_device, _interface);
                form.ShowDialog();
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

        public override void Refresh()
        {
            base.Refresh();
            tbControllerPath.Text = Interface.ToString() + " " + Device;
        }
    }
}
