using OpenZWaveDotNet;
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
using ZWaveAction;

namespace ZWaveActionUI
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

        public uint? HomeId
        {
            get; set;
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
                        HomeId = _zwave.HomeId;
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

        private void DoCommand(ZWControllerCommand command)
        {
            ControllerCommandDlg dlg = new ControllerCommandDlg(_zwave.Manager, _zwave.HomeId.Value, command, null, false);
            DialogResult d = dlg.ShowDialog(this);
            dlg.Dispose();
        }

        private void btCreateNewPrimary_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.CreateNewPrimary);
        }

        private void btTransferPrimaryRole_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.TransferPrimaryRole);
        }

        private void btTransferConfiguration_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.ReceiveConfiguration);
        }

        private void btUpdateNetwork_Click(object sender, EventArgs e)
        {
            DoCommand(ZWControllerCommand.RequestNetworkUpdate);
        }

        private void btSoftReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Совершить \"мягкий\" сброс устройства?", "Сброс устройства", MessageBoxButtons.YesNo) == DialogResult.Yes)
                _zwave.Manager.SoftReset(_zwave.HomeId.Value);
        }

        private void btEraseAll_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Сбросить устройство?", "Сброс устройства", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _zwave.Manager.ResetController(_zwave.HomeId.Value);
                _zwave.Manager.RemoveDriver(_zwave.Device.Path);
                ZWGlobal.RemoveZWave(_zwave.Device.Path);
                ZWGlobal.PrepareZWave(_zwave.Device.Path, _zwave.Device.Interface);
            }
        }
    }
}
