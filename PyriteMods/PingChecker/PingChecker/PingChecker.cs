using PyriteClientIntefaces;
using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PingChecker
{
    [Serializable]
    public class PingChecker : ICustomChecker
    {
        [XmlIgnore]
        public bool AllowUserSettings
        {
            get { return true; }
        }

        public bool BeginUserSettings()
        {
            var dialog = new PingCheckerView();
            dialog.Host = Host;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Host = dialog.Host;
                return true;
            }
            return false;
        }

        [HumanFriendlyName("Хост")]
        public string Host { get; set; }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                if (string.IsNullOrEmpty(Host))
                    return false;

                bool success = false;

                try
                {
                    var ping = new Ping();
                    if (ping.Send(this.Host).Status == IPStatus.Success)
                        success = true;
                }
                catch
                {
                    success = false;
                }

                return success;
            }
        }

        [XmlIgnore]
        public string Name
        {
            get { return "Устройство находится в сети"; }
        }

        public void Refresh()
        {
            //do nothing
        }
    }
}
