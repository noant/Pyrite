using Modbus.Device;
using System;
using System.IO.Ports;
using System.Linq;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace ModbusAction
{
    [Serializable]
    public class NModbusSingleCoilAction : ICustomAction
    {
        public string StateOn { get; set; }

        public string StateOff { get; set; }

        public string StateError { get; set; }

        public ChangeableStates ChangeableState { get; set; }

        public string PortName { get; set; }

        public int PortBaudRate { get; set; }

        public int PortDataBits { get; set; }

        public Parity PortParity { get; set; }

        public StopBits PortStopBits { get; set; }

        public byte ModbusSlaveId { get; set; }

        public ushort ModbusCoilAddress { get; set; }

        public int ModbusReadTimeout { get; set; }

        public int ModbusWriteTimeout { get; set; }

        protected SerialPort ConfigurePort()
        {
            SerialPort port = new SerialPort(PortName);
            port.BaudRate = PortBaudRate;
            port.DataBits = PortDataBits;
            port.Parity = PortParity;
            port.StopBits = PortStopBits;
            port.Open();
            return port;
        }

        protected IModbusSerialMaster ConfigureMaster()
        {
            var master = ModbusSerialMaster.CreateRtu(ConfigurePort());
            master.Transport.ReadTimeout = ModbusReadTimeout;
            master.Transport.WriteTimeout = ModbusWriteTimeout;
            return master;
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                if (ChangeableState == ChangeableStates.Off)
                    return StateOn;
                if (ChangeableState == ChangeableStates.On)
                    return StateOff;
                lock (Statics.ComPortLocker)
                {
                    try
                    {
                        using (var master = ConfigureMaster())
                        {
                            return master.ReadCoils(ModbusSlaveId, ModbusCoilAddress, 1).First() ?
                               StateOn : StateOff;
                        }
                    }
                    catch
                    {
                        return StateError;
                    }
                }
            }
        }

        public void Refresh() { }

        [XmlIgnore]
        public bool AllowUserSettings { get { return true; } }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            lock (Statics.ComPortLocker)
            {
                try
                {
                    using (var master = ConfigureMaster())
                    {
                        var state = inputState == StateOff;

                        if (ChangeableState == ChangeableStates.Off)
                            state = false;
                        else if (ChangeableState == ChangeableStates.On)
                            state = true;

                        master.WriteSingleCoil(ModbusSlaveId, ModbusCoilAddress, state);
                    }
                }
                catch { }
            }
            IsBusyNow = false;
            return State;
        }

        public virtual bool BeginUserSettings()
        {
            PortName = "COM1";
            PortBaudRate = 9600;
            PortDataBits = 8;
            PortParity = Parity.None;
            PortStopBits = StopBits.One;
            ModbusSlaveId = 1;
            ModbusCoilAddress = 0;
            ModbusReadTimeout = 2000;
            ModbusWriteTimeout = 2000;

            var form = new CreateNModbusSingleCoilActionForm();
            form.tbPortName.Text = this.PortName;
            form.tbStateOff.Text = this.StateOff;
            form.tbStateOn.Text = this.StateOn;
            form.nudBaudRate.Value = this.PortBaudRate;
            form.nudReadTimeout.Value = this.ModbusReadTimeout;
            form.nudWriteTimeout.Value = this.ModbusWriteTimeout;
            form.nudSingleCoil.Value = this.ModbusCoilAddress;
            form.nudSlaveId.Value = this.ModbusSlaveId;

            form.cbDataBits.SelectedItem = this.PortDataBits;
            form.cbParity.SelectedItem = this.PortParity;
            form.cbStopBits.SelectedItem = this.PortStopBits;

            form.rbOff.Checked = this.ChangeableState == ChangeableStates.Off;
            form.rbOn.Checked = this.ChangeableState == ChangeableStates.On;
            form.rbOnOff.Checked = this.ChangeableState == ChangeableStates.Both;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.PortName = form.tbPortName.Text;
                this.StateOff = form.tbStateOff.Text;
                this.StateOn = form.tbStateOn.Text;
                this.PortBaudRate = (int)form.nudBaudRate.Value;
                this.ModbusReadTimeout = (int)form.nudReadTimeout.Value;
                this.ModbusWriteTimeout = (int)form.nudWriteTimeout.Value;
                this.ModbusCoilAddress = (ushort)form.nudSingleCoil.Value;
                this.ModbusSlaveId = (byte)form.nudSlaveId.Value;

                this.PortDataBits = (int)form.cbDataBits.SelectedItem;
                this.PortParity = (Parity)form.cbParity.SelectedItem;
                this.PortStopBits = (StopBits)form.cbStopBits.SelectedItem;

                this.ChangeableState = form.rbOff.Checked ? ChangeableStates.Off : form.rbOn.Checked ? ChangeableStates.On : ChangeableStates.Both;

                return true;
            }

            return false;
        }

        [XmlIgnore]
        public virtual string Name
        {
            get { return "Действие Modbus RTU (запись в одну ячейку)"; }
        }

        [XmlIgnore]
        public bool IsBusyNow { get; private set; }

        public enum ChangeableStates
        {
            Off,
            On,
            Both
        }
    }
}
