using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

            cbControllerPath.TextChanged += (o, e) =>
                ChangeButtonsEnabled(false);

            cbType.SelectedIndexChanged += (o, e) =>
                ChangeButtonsEnabled(false);

            this.Load += (o, e) => PrepareConnection();
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
            var device = Device;
            var @interface = Interface;
            progressBar.Visible =
                lblStatus.Visible = true;

            this.Enabled = false;

            new Thread(() =>
            {
                var buttonsEnable = false;
                if (string.IsNullOrEmpty(device))
                {
                    // do nothing
                }
                else
                {
                    _zwave = ZWGlobal.PrepareZWave(device, @interface);
                    if (_zwave.WaitForControllerLoaded())
                    {
                        buttonsEnable = true;
                    }
                    else
                    {
                        buttonsEnable = false;
                        BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show("Время ожидания отклика от контроллера истекло.");
                        }));
                    }
                }
                BeginInvoke(new Action(() =>
                {
                    ChangeButtonsEnabled(buttonsEnable);
                    progressBar.Visible =
                        lblStatus.Visible = false;
                    this.Enabled = true;
                }));
            })
            {
                IsBackground = true
            }.Start();
        }
    }
}
