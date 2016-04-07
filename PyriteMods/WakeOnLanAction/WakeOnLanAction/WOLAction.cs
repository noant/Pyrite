using PyriteClientIntefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WakeOnLanAction
{
    [Serializable]
    public class WOLAction : ICustomAction
    {
        public WOLAction()
        {
            MacAddress = "00:00:00:00:00:00";
            Port = UdpClientExt.StandartWakeOnLanPort;
            TryCount = UdpClientExt.StandartWOLTryCount;
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get { return true; }
        }

        public bool BeginUserSettings()
        {
            var form = new EnterMacAddressForm()
            {
                MacAddress = this.MacAddress,
                Port = this.Port,
                TryCnt = this.TryCount
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MacAddress = form.MacAddress;
                Port = form.Port;
                TryCount = form.TryCnt;
                return true;
            }

            return false;
        }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            UdpClientExt.SendWakeOnLan(MacAddress, TryCount, Port);
            IsBusyNow = false;
            return State;
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get;
            set;
        }

        [XmlIgnore]
        public string Name
        {
            get { return "WakeOnLan"; }
        }

        public void Refresh()
        {
        }

        [HumanFriendlyName("MAC")]
        public string MacAddress { get; set; }

        [HumanFriendlyName("Порт")]
        public ushort Port { get; set; }

        [HumanFriendlyName("Попыток")]
        public ushort TryCount { get; set; }

        [XmlIgnore]
        public string State
        {
            get
            {
                return "Разбудить [" + MacAddress + "]";
            }
        }
    }
}
