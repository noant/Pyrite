using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusAction
{
    public class FastModbusActionOn0 : NModbusAction
    {
        public FastModbusActionOn0()
        {
            base._changeableState = ChangeableState.On;
            base._modbusCoilAddress = 0;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOn1 : NModbusAction
    {
        public FastModbusActionOn1()
        {
            base._changeableState = ChangeableState.On;
            base._modbusCoilAddress = 1;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOn2 : NModbusAction
    {
        public FastModbusActionOn2()
        {
            base._changeableState = ChangeableState.On;
            base._modbusCoilAddress = 2;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOn3 : NModbusAction
    {
        public FastModbusActionOn3()
        {
            base._changeableState = ChangeableState.On;
            base._modbusCoilAddress = 3;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOn4 : NModbusAction
    {
        public FastModbusActionOn4()
        {
            base._changeableState = ChangeableState.On;
            base._modbusCoilAddress = 4;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOn5 : NModbusAction
    {
        public FastModbusActionOn5()
        {
            base._changeableState = ChangeableState.On;
            base._modbusCoilAddress = 5;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOn6 : NModbusAction
    {
        public FastModbusActionOn6()
        {
            base._changeableState = ChangeableState.On;
            base._modbusCoilAddress = 6;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOn7 : NModbusAction
    {
        public FastModbusActionOn7()
        {
            base._changeableState = ChangeableState.On;
            base._modbusCoilAddress = 7;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }

    public class FastModbusActionOff0 : NModbusAction
    {
        public FastModbusActionOff0()
        {
            base._changeableState = ChangeableState.Off;
            base._modbusCoilAddress = 0;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOff1 : NModbusAction
    {
        public FastModbusActionOff1()
        {
            base._changeableState = ChangeableState.Off;
            base._modbusCoilAddress = 1;
            
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOff2 : NModbusAction
    {
        public FastModbusActionOff2()
        {
            base._changeableState = ChangeableState.Off;
            base._modbusCoilAddress = 2;
            
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOff3 : NModbusAction
    {
        public FastModbusActionOff3()
        {
            base._changeableState = ChangeableState.Off;
            base._modbusCoilAddress = 3;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOff4 : NModbusAction
    {
        public FastModbusActionOff4()
        {
            base._changeableState = ChangeableState.Off;
            base._modbusCoilAddress = 4;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOff5 : NModbusAction
    {
        public FastModbusActionOff5()
        {
            base._changeableState = ChangeableState.Off;
            base._modbusCoilAddress = 5;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOff6 : NModbusAction
    {
        public FastModbusActionOff6()
        {
            base._changeableState = ChangeableState.Off;
            base._modbusCoilAddress = 6;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusActionOff7 : NModbusAction
    {
        public FastModbusActionOff7()
        {
            base._changeableState = ChangeableState.Off;
            base._modbusCoilAddress = 7;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    
    public class FastModbusAcionOnOff0 : NModbusAction
    {
        public FastModbusAcionOnOff0()
        {
            base._changeableState = ChangeableState.Both;
            base._modbusCoilAddress = 0;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusAcionOnOff1 : NModbusAction
    {
        public FastModbusAcionOnOff1()
        {
            base._changeableState = ChangeableState.Both;
            base._modbusCoilAddress = 1;

        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusAcionOnOff2 : NModbusAction
    {
        public FastModbusAcionOnOff2()
        {
            base._changeableState = ChangeableState.Both;
            base._modbusCoilAddress = 2;

        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusAcionOnOff3 : NModbusAction
    {
        public FastModbusAcionOnOff3()
        {
            base._changeableState = ChangeableState.Both;
            base._modbusCoilAddress = 3;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusAcionOnOff4 : NModbusAction
    {
        public FastModbusAcionOnOff4()
        {
            base._changeableState = ChangeableState.Both;
            base._modbusCoilAddress = 4;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusAcionOnOff5 : NModbusAction
    {
        public FastModbusAcionOnOff5()
        {
            base._changeableState = ChangeableState.Both;
            base._modbusCoilAddress = 5;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusAcionOnOff6 : NModbusAction
    {
        public FastModbusAcionOnOff6()
        {
            base._changeableState = ChangeableState.Both;
            base._modbusCoilAddress = 6;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
    public class FastModbusAcionOnOff7 : NModbusAction
    {
        public FastModbusAcionOnOff7()
        {
            base._changeableState = ChangeableState.Both;
            base._modbusCoilAddress = 7;
        }

        public override bool InitializeNew() { base._stateOff="Включить устройство "+base._modbusCoilAddress; base._stateOn="Выключить устройство "+base._modbusCoilAddress; return true; }

        public override string Name
        {
            get
            {
                var strCState = base._changeableState == ChangeableState.Both ? "включить или выключить" : base._changeableState == ChangeableState.Off ? "выключить" : "включить";
                return base.Name + " (быстрое действие: " + strCState + " устройство под номером " + base._modbusCoilAddress + " у modbus устройства " + base._modbusSlaveId + ")";
            }
        }
    }
}
