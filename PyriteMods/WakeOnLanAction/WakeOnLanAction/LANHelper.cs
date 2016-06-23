using System.Linq;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace WakeOnLanAction
{
    public static class LANHelper
    {
        public static readonly byte[] StandartIpBase = new byte[] { 192, 168, 0 };

        public static void ListAllHosts(byte[] ipBase, Action<AddressStruct> itemCallback)
        {
            for (int b4 = 0; b4 <= 255; b4++)
            {
                var ip = ipBase[0] + "." + ipBase[1] + "." + ipBase[2] + "." + b4;

                var ping = new Ping();
                ping.PingCompleted += (o, e) =>
                {
                    if (e.Error == null && e.Reply.Status == IPStatus.Success)
                        if (itemCallback != null)
                        {
                            GetMacAddress(
                                e.Reply.Address,
                                (mac) =>
                                {
                                    itemCallback(new AddressStruct()
                                    {
                                        IPAddress = e.Reply.Address,
                                        MacAddress = mac
                                    });
                                });
                        }
                };
                ping.SendAsync(ip, null);
            }
        }

        private static void GetMacAddress(System.Net.IPAddress address, Action<PhysicalAddress> callback)
        {
            new Thread(() =>
            {
                try
                {
                    var destAddr = BitConverter.ToInt32(address.GetAddressBytes(), 0);

                    var srcAddr = BitConverter.ToInt32(System.Net.IPAddress.Any.GetAddressBytes(), 0);

                    var macAddress = new byte[6];

                    var macAddrLen = macAddress.Length;

                    var ret = SendArp(destAddr, srcAddr, macAddress, ref macAddrLen);

                    if (ret != 0)
                    {
                        throw new System.ComponentModel.Win32Exception(ret);
                    }

                    var mac = new PhysicalAddress(macAddress);

                    if (callback != null)
                        callback(mac);
                }
                catch
                {
                    //do nothing
                }
            })
            {
                IsBackground = true
            }.Start();
        }

        [System.Runtime.InteropServices.DllImport("Iphlpapi.dll", EntryPoint = "SendARP")]

        private extern static Int32 SendArp(Int32 destIpAddress, Int32 srcIpAddress, byte[] macAddress, ref Int32 macAddressLength);

        public struct AddressStruct
        {
            public IPAddress IPAddress;

            public PhysicalAddress MacAddress;
        }
    }
}
