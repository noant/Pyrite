using Modbus.Device;
using System;
using System.IO.Ports;
using System.Xml.Serialization;
using UniActionsClientIntefaces;

namespace ModbusAction
{
    [Serializable]
    public class NModbusRtuSingleCoilChecker : ICustomChecker
    {
        public NModbusRtuSingleCoilChecker()
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
        }

        [HumanFriendlyName("Порт")]
        public string PortName { get; set; }

        public int PortBaudRate { get; set; }

        public int PortDataBits { get; set; }

        public Parity PortParity { get; set; }

        public StopBits PortStopBits { get; set; }

        [HumanFriendlyName("Идентификатор устройства")]
        public byte ModbusSlaveId { get; set; }

        [HumanFriendlyName("Адрес ячейки")]
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
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                lock (Statics.ComPortLocker)
                {
                    try
                    {
                        using (var master = ConfigureMaster())
                        {
                            return master.ReadCoils(ModbusSlaveId, ModbusCoilAddress, 1)[0];
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Проверка Modbus RTU (чтение из одной ячейки)"; }
        }

        public bool BeginUserSettings()
        {
            var form = new CreateNModbusSingleCoilCheckerForm();
            form.tbPortName.Text = this.PortName;
            form.nudBaudRate.Value = this.PortBaudRate;
            form.nudReadTimeout.Value = this.ModbusReadTimeout;
            form.nudWriteTimeout.Value = this.ModbusWriteTimeout;
            form.nudSingleCoil.Value = this.ModbusCoilAddress;
            form.nudSlaveId.Value = this.ModbusSlaveId;

            form.cbDataBits.SelectedItem = this.PortDataBits;
            form.cbParity.SelectedItem = this.PortParity;
            form.cbStopBits.SelectedItem = this.PortStopBits;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.PortName = form.tbPortName.Text;
                this.PortBaudRate = (int)form.nudBaudRate.Value;
                this.ModbusReadTimeout = (int)form.nudReadTimeout.Value;
                this.ModbusWriteTimeout = (int)form.nudWriteTimeout.Value;
                this.ModbusCoilAddress = (ushort)form.nudSingleCoil.Value;
                this.ModbusSlaveId = (byte)form.nudSlaveId.Value;

                this.PortDataBits = (int)form.cbDataBits.SelectedItem;
                this.PortParity = (Parity)form.cbParity.SelectedItem;
                this.PortStopBits = (StopBits)form.cbStopBits.SelectedItem;

                return true;
            }

            return false;
        }

        public void Refresh()
        {

        }
    }
}
