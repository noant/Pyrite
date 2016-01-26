using HierarchicalData;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace ModbusAction
{
    public class NModbusAction : ICustomAction
    {
        internal static object _comPortsLocker = new object();

        [Settings]
        protected string _stateOn = "off";
        [Settings]
        protected string _stateOff = "on";
        [Settings]
        protected string _stateError = "Ошибка связи с устройством Modbus";

        [Settings]
        protected ChangeableState _changeableState = ChangeableState.Both;

        [Settings]
        protected string _portName = "COM1";
        [Settings]
        protected int _portBaudRate = 9600;
        [Settings]
        protected int _portDataBits = 8;
        [Settings]
        protected Parity _portParity = Parity.None;
        [Settings]
        protected StopBits _portStopBits = StopBits.One;

        [Settings]
        protected byte _modbusSlaveId = 1;
        [Settings]
        protected ushort _modbusCoilAddress = 0;
        [Settings]
        protected int _modbusReadTimeout = 2000;
        [Settings]
        protected int _modbusWriteTimeout = 2000;

        protected SerialPort ConfigurePort() {
            
            SerialPort port = new SerialPort(_portName);
            port.BaudRate = _portBaudRate;
            port.DataBits = _portDataBits;
            port.Parity = _portParity;
            port.StopBits = _portStopBits;
            port.Open();            
            return port;
        }

        protected IModbusSerialMaster ConfigureMaster()
        {
            var master = ModbusSerialMaster.CreateRtu(ConfigurePort());
            master.Transport.ReadTimeout = _modbusReadTimeout;
            master.Transport.WriteTimeout = _modbusWriteTimeout;
            return master;
        }

        public string State
        {
            get
            {
                if (_changeableState == ChangeableState.Off)
                    return _stateOn;
                if (_changeableState == ChangeableState.On)
                    return _stateOff;
                lock (_comPortsLocker)
                {
                    try
                    {
                        using (var master = ConfigureMaster())
                        {
                            return master.ReadCoils(_modbusSlaveId, _modbusCoilAddress, 1).First() ?
                               _stateOn : _stateOff;
                        }
                    }
                    catch
                    {
                        return _stateError;
                    }
                }
            }
        }

        public void Refresh() { }

        public bool AllowUserSettings { get { return true; } }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            lock (_comPortsLocker)
            {
                try
                {
                    using (var master = ConfigureMaster())
                    {
                        var state = inputState == _stateOff;

                        if (_changeableState == ChangeableState.Off)
                            state = false;
                        else if (_changeableState == ChangeableState.On)
                            state = true;

                        master.WriteSingleCoil(_modbusSlaveId, _modbusCoilAddress, state);
                    }
                }
                catch { }
            }
            IsBusyNow = false;
            return State;
        }

        public virtual bool BeginUserSettings()
        {
            var form = new CreateForm();
            form.tbPortName.Text = this._portName;
            form.tbStateOff.Text = this._stateOff; 
            form.tbStateOn.Text = this._stateOn;
            form.nudBaudRate.Value = this._portBaudRate;
            form.nudReadTimeout.Value = this._modbusReadTimeout;
            form.nudWriteTimeout.Value = this._modbusWriteTimeout;
            form.nudSingleCoil.Value = this._modbusCoilAddress;
            form.nudSlaveId.Value = this._modbusSlaveId;

            form.cbDataBits.SelectedItem = this._portDataBits;
            form.cbParity.SelectedItem = this._portParity;
            form.cbStopBits.SelectedItem = this._portStopBits;

            form.rbOff.Checked = this._changeableState == ChangeableState.Off;
            form.rbOn.Checked = this._changeableState == ChangeableState.On;
            form.rbOnOff.Checked = this._changeableState == ChangeableState.Both;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._portName = form.tbPortName.Text;
                this._stateOff = form.tbStateOff.Text;
                this._stateOn = form.tbStateOn.Text;
                this._portBaudRate = (int)form.nudBaudRate.Value;
                this._modbusReadTimeout = (int)form.nudReadTimeout.Value;
                this._modbusWriteTimeout = (int)form.nudWriteTimeout.Value;
                this._modbusCoilAddress = (ushort)form.nudSingleCoil.Value;
                this._modbusSlaveId = (byte)form.nudSlaveId.Value;

                this._portDataBits = (int)form.cbDataBits.SelectedItem;
                this._portParity = (Parity)form.cbParity.SelectedItem;
                this._portStopBits = (StopBits)form.cbStopBits.SelectedItem;

                this._changeableState = form.rbOff.Checked ? ChangeableState.Off : form.rbOn.Checked ? ChangeableState.On : ChangeableState.Both;

                return true;
            }

            return false;
        }

        public virtual string Name
        {
            get { return "Действие Modbus RTU (запись в одну ячейку)"; }
        }

        public bool IsBusyNow { get; private set; }
        
        public enum ChangeableState
        {
            Off,
            On,
            Both
        }
    }
}
