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
    public partial class ControllerSelectForm : Form
    {
        public ControllerSelectForm(string path, ControllerInterface @interface)
        {
            InitializeComponent();
            Device = path;
            Interface = @interface;
            this.cbControllerPath.Items.AddRange(ZWGlobal.GetAllUsedControllers().ToArray());
            PrepareConnection();

            cbControllerPath.TextChanged += (o, e) =>
            {
                ChangeButtonsEnabled(false);
            };

            cbType.SelectedIndexChanged += (o, e) =>
            {
                ChangeButtonsEnabled(false);
            };
        }

        private void ChangeButtonsEnabled(bool enabled)
        {
            btCreateNewPrimary.Enabled =
                btEraseAll.Enabled =
                btOk.Enabled =
                btSoftReset.Enabled =
                btUpdateNetwork.Enabled =
                btTransferConfiguration.Enabled =
                btTransferPrimaryRole.Enabled =
                btUpdateNetwork.Enabled =
                enabled;
        }

        public ControllerInterface Interface
        {
            get
            {
                return this.cbType.SelectedIndex == 0 ? ControllerInterface.Serial : ControllerInterface.HID;
            }
            private set
            {
                cbType.SelectedIndex = value == ControllerInterface.HID ? 1 : 0;
            }
        }
        public string Device
        {
            get
            {
                return cbControllerPath.Text;
            }
            private set
            {
                cbControllerPath.Text = value;
            }
        }

        private ZWave _zwave;

        private void btConnection_Click(object sender, EventArgs e)
        {
            PrepareConnection();
        }

        private void PrepareConnection()
        {
            var buttonsEnable = false;
            if (string.IsNullOrEmpty(Device))
            {
                // do nothing
            }
            else
            {
                var zwave = ZWGlobal.PrepareZWave(Device, Interface);
                if (zwave.WaitForControllerLoaded())
                {
                    buttonsEnable = true;
                }
                else
                {
                    buttonsEnable = false;
                    MessageBox.Show("Время ожидания отклика от контроллера истекло.");
                }
            }

            ChangeButtonsEnabled(buttonsEnable);
        }
    }
}
