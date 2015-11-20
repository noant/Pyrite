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
        public string CheckState()
        {
            throw new NotImplementedException();
        }

        public void ChangeState()
        {
            SerialPort port = new SerialPort("COM1");

            // configure serial port
            port.BaudRate = 9600;
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.Open();

            // create modbus master
            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

            byte slaveId = 1;
            ushort startAddress = 1;
            ushort numRegisters = 5;

            // read five registers
            ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

            for (int i = 0; i < numRegisters; i++)
                Console.WriteLine("Register {0}={1}", startAddress + i, registers[i]);
            
            master.ReadCoils()

            // write three coils
            master.WriteMultipleCoils(slaveId, startAddress, new bool[] { true, false, true });
        }

        public string Do(string inputState)
        {
            throw new NotImplementedException();
        }

        public bool InitializeNew()
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public void SetFromString(string settings)
        {
            throw new NotImplementedException();
        }

        public string SetToString()
        {
            throw new NotImplementedException();
        }
    }
}
