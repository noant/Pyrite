using System.Linq;
using System.Collections.Generic;
using PyriteClientIntefaces;
using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

namespace PyriteStandartActions.Checkers
{
    [Serializable]
    public class PyritePingChecker : ICustomChecker
    {
        public PyritePingChecker()
        {
            Host = "127.0.0.1";
            Port = 6001;
        }

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        [HumanFriendlyName("Хост")]
        public string Host { get; set; }

        [XmlIgnore]
        public bool IsCanDoNow
        {
            get
            {
                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        IAsyncResult ar = client.BeginConnect(this.Host, GetNextPyritePort(this.Host, this.Port), null, null);
                        System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                        if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(0.5), false))
                        {
                            client.Close();
                            throw new TimeoutException();
                        }
                        var stream = client.GetStream();

                        SendString(stream, "isYouPyrite?");

                        return GetNextString(stream).Equals("yesIAm");
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Другой Pyrite сервер в сети";
            }
        }

        [HumanFriendlyName("Порт")]
        public ushort Port { get; set; }

        public bool BeginUserSettings()
        {
            var form = new PyritePingCheckerView();
            form.Host = this.Host;
            form.Port = this.Port;

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Host = form.Host;
                this.Port = form.Port;
                return true;
            }
            return false;
        }

        public void Refresh()
        {
        }

        public static ushort GetNextPyritePort(string host, ushort port)
        {
            using (TcpClient client = new TcpClient())
            {
                IAsyncResult ar = client.BeginConnect(host, port, null, null);
                System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(0.5), false))
                {
                    client.Close();
                    throw new TimeoutException();
                }
                var stream = client.GetStream();
                var value = GetNextString(stream);
                return ushort.Parse(value, CultureInfo.InvariantCulture);
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
