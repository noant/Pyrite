using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UniActionsCore
{
    public static class UdpClientExtensions
    {
        private static string _splitter = "#";

        public static void SendStringArrayAsync(this UdpClient client, string[] array)
        {
            string retstr = "";
            foreach (var str in array)
                retstr += ScreenString(str) + _splitter + _splitter;

            var bytes = ServerThreading.ServerThreadingSettings.Defaults.ServerEncoding.GetBytes(retstr);

            client.SendAsync(bytes, bytes.Count());
        }

        private static string ScreenString(string str)
        {
            return str.Replace(_splitter, "\"" + _splitter);
        }
    }
}
