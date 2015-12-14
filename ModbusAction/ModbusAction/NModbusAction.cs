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

        protected string _stateOn = "off";
        protected string _stateOff = "on";
        protected string _stateError = "Ошибка связи с устройством Modbus";

        protected ChangeableState _changeableState = ChangeableState.Both;

        protected string _portName = "COM1";
        protected int _portBaudRate = 9600;
        protected int _portDataBits = 8;
        protected Parity _portParity = Parity.None;
        protected StopBits _portStopBits = StopBits.One;

        protected byte _modbusSlaveId = 1;
        protected ushort _modbusCoilAddress = 0;
        protected int _modbusReadTimeout = 2000;
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

        public string CheckState()
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

        public string Do(string inputState)
        {
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
            return CheckState();
        }

        public virtual bool InitializeNew()
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
                this._portName = form.tbPortName.Text.Replace(_splitter, "?");
                this._stateOff = form.tbStateOff.Text.Replace(_splitter, "?");
                this._stateOn = form.tbStateOn.Text.Replace(_splitter, "?");
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

        protected string _splitter = "#";

        public void SetFromString(string settings)
        {
            var strs = settings.Split(_splitter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            _stateOn = strs[0];
            _stateOff = strs[1];
            _portName = strs[2];
            _portBaudRate = int.Parse(strs[3]);
            _portDataBits = int.Parse(strs[4]);
            _portParity = (Parity)int.Parse(strs[5]);
            _portStopBits = (StopBits)int.Parse(strs[6]);
            _modbusSlaveId = byte.Parse(strs[7]);
            _modbusCoilAddress = byte.Parse(strs[8]);
            _modbusReadTimeout = int.Parse(strs[9]);
            _modbusWriteTimeout = int.Parse(strs[10]);
            _changeableState = (ChangeableState)int.Parse(strs[11]);
        }

        public string SetToString()
        {
            return
                _stateOn + _splitter +
                _stateOff + _splitter +
                _portName + _splitter +
                _portBaudRate + _splitter +
                _portDataBits + _splitter +
                ((int)_portParity) + _splitter +
                ((int)_portStopBits) + _splitter +
                _modbusSlaveId + _splitter +
                _modbusCoilAddress + _splitter +
                _modbusReadTimeout + _splitter +
                _modbusWriteTimeout + _splitter +
                (int)_changeableState;
        }

        public enum ChangeableState
        {
            Off,
            On,
            Both
        }
    }
}
