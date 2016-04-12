using PyriteClientIntefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PyriteStandartActions.Actions
{
    [Serializable]
    public class RunRemoteScenarioAction : ICustomAction
    {
        public static class Strings
        {
            public static readonly string CannotConnect = "[нет соединения]";
            public static readonly string Command_EndFastActions = "endfastactions";
            public static readonly string Command_GetStartCommands = "getallcommands";
            public static readonly string Command_GetCategoryCommands = "getcategorycommands";
            public static readonly string Command_GetCommandStatus = "getcommandstate";
        }

        public RunRemoteScenarioAction()
        {
            Host = "127.0.0.1";
            Port = 6001;
        }

        public bool BypassStatus { get; set; }

        [HumanFriendlyName("GUID комманды")]
        public string ServerCommand { get; set; }

        [HumanFriendlyName("Хост")]
        public string Host { get; set; }

        [HumanFriendlyName("Порт")]
        public ushort Port { get; set; }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            try
            {
                using (var client = new TcpClient())
                {
                    client.Connect(this.Host, GetNextPyritePort());

                    var stream = client.GetStream();
                    SendString(stream, ServerCommand);
                    SendString(stream, string.Empty);
                    var state = GetNextString(stream);

                    return state;
                }
            }
            catch
            {
                return Strings.CannotConnect;
            }
            finally
            {
                IsBusyNow = false;
            }
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                if (BypassStatus)
                {
                    try
                    {
                        using (var client = new TcpClient())
                        {
                            client.Connect(this.Host, GetNextPyritePort());

                            var stream = client.GetStream();

                            SendString(stream, Strings.Command_GetCommandStatus);
                            SendString(stream, ServerCommand);
                            var state = GetNextString(stream);

                            return state;
                        }
                    }
                    catch
                    {
                        return Strings.CannotConnect;
                    }
                }
                else
                    return "Запуск удаленного сценария на " + Host + " " + Port + " (" + ServerCommand + ")";
            }
        }

        private ushort GetNextPyritePort()
        {
            return GetNextPyritePort(this.Host, this.Port);
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Запуск удаленного сценария";
            }
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        public bool BeginUserSettings()
        {
            var form = new RunRemoteScenarioActionView();
            form.Host = this.Host;
            form.Port = this.Port;
            form.ServerCommand = this.ServerCommand;
            form.BypassStatus = this.BypassStatus;
            form.RefreshHosts();

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Host = form.Host;
                this.Port = form.Port;
                this.ServerCommand = form.ServerCommand;
                this.BypassStatus = form.BypassStatus;
                return true;
            }
            return false;
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get;
            set;
        }

        public void Refresh()
        { }

        public static ushort GetNextPyritePort(string host, ushort port)
        {
            using (var client = new TcpClient())
            {
                client.Connect(host, port);

                var stream = client.GetStream();
                var value = GetNextString(stream);

                return ushort.Parse(value);
            }
        }

        public static void SendString(NetworkStream stream, string str)
        {
            var bytesToSend = ServerEncoding.GetBytes(str);
            stream.WriteByte((byte)bytesToSend.Length);
            if (!string.IsNullOrEmpty(str))
                stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public static string GetNextString(NetworkStream stream)
        {
            var len = stream.ReadByte();
            if (len <= 0)
                return string.Empty;
            var buff = new byte[len];
            stream.Read(buff, 0, buff.Length);
            return ServerEncoding.GetString(buff);
        }

        public static readonly Encoding ServerEncoding = Encoding.UTF8;
    }
}
