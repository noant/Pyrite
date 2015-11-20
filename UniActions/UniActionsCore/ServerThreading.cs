using Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UniActionsCore
{
    public class ServerThreading
    {
        public static class Settings
        {
            public static class Defaults
            {
                public static readonly int ServerThreadCount = 4;
                public static readonly int ServerListenerPort = 600;
                public static readonly bool ResolveAll = true;
            }

            public static int ServerThreadCount { get; set; }
            public static int ServerListenerPort { get; set; }
            public static bool ResolveAll { get; set; }

            public static List<string> ResolvedIp { get; internal set; }
        }

        private static TcpListener _listener;
        private static List<Thread> _threads;

        private static void SendString(NetworkStream stream, string str)
        {
            var bytesToSend = Encoding.UTF8.GetBytes(str);
            stream.WriteByte((byte)bytesToSend.Length);
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        private static string GetNextString(NetworkStream stream)
        {
            var len = stream.ReadByte();
            var buff = new byte[len];
            stream.Read(buff, 0, buff.Length);
            return Encoding.UTF8.GetString(buff);
        }
        
        public static void Initialize()
        {
            Settings.ResolvedIp = new List<string>();
        }

        public static VoidResult Start()
        {
            VoidResult result = new VoidResult();

            try
            {
                _listener = new TcpListener(Settings.ServerListenerPort);
                _listener.Start();
            }
            catch (Exception e)
            {
                Log.Write(e);
                result.AddException(e);
            }
            
            _threads = new List<Thread>();

            for (int i = 0; i < Settings.ServerThreadCount; i++)
            {
                int threadNumber = i;
                Thread t = new Thread(() => {
                    while (true)
                    {
                        try
                        {
                            lock (_listener)
                            {
                                TcpClient client = null;
                                try
                                {
                                    client = _listener.AcceptTcpClient();
                                }
                                catch (Exception e)
                                {
                                    Log.Write(e);
                                    break;
                                }

                                if (Settings.ResolvedIp != null && !Settings.ResolveAll)
                                {
                                    lock (Settings.ResolvedIp)
                                    {
                                        var ip = client.Client.RemoteEndPoint.ToString().Split(":".ToCharArray())[0];
                                        if (Settings.ResolvedIp.Contains(ip))
                                        {
                                            throw new Exception("Not resolved ip: " + ip);
                                        }
                                    }
                                }
                                
                                var stream = client.GetStream();

                                var command = GetNextString(stream);

                                lock (Pool.ActionItems)
                                {
                                    if (command == VAC.ServerCommands.Command_GetStartCommands)
                                    {
                                        var fastActions = Pool.ActionItems.Where(x => string.IsNullOrEmpty(x.Category) && x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand));
                                        var categories = Pool.ActionItems.Where(x=>!string.IsNullOrEmpty(x.Category) && !string.IsNullOrEmpty(x.ServerCommand) && x.UseServerThreading).Select(x => x.Category).Distinct().OrderBy(x=>x);
                                        stream.WriteByte((byte)(fastActions.Count() + categories.Count()));
                                        foreach (var action in fastActions)
                                        {
                                            SendString(stream, action.CheckState());
                                            SendString(stream, action.ServerCommand);

                                            Debug.WriteLine("thread " + threadNumber + " send " + action.Name + " " + action.ServerCommand + " to " + client.Client.RemoteEndPoint.ToString());
                                        }

                                        SendString(stream, VAC.ServerCommands.Command_EndFastActions);

                                        foreach (var category in categories)
                                        {
                                            SendString(stream, category);

                                            Debug.WriteLine("thread " + threadNumber + " send category " + category);
                                        }
                                    }
                                    else if (command == VAC.ServerCommands.Command_GetCategoryCommands)
                                    {
                                        var category = GetNextString(stream);

                                        var actions = Pool.ActionItems.Where(x => x.Category == category && x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand));

                                        stream.WriteByte((byte)actions.Count());

                                        foreach (var action in actions)
                                        {
                                            SendString(stream, action.CheckState());
                                            SendString(stream, action.ServerCommand);

                                            Debug.WriteLine("thread " + threadNumber + " send " + action.Name + " " + action.ServerCommand + " to " + client.Client.RemoteEndPoint.ToString());
                                        }
                                    }
                                    else
                                    {
                                        var remoteAction = Pool.ActionItems.Where(x => x.ServerCommand == command).FirstOrDefault();
                                        if (remoteAction != null)
                                        {
                                            var state = GetNextString(stream);

                                            SendString(stream, remoteAction.BeginExecute(state));

                                            Debug.WriteLine("thread " + threadNumber + " action '" + command + "' from " + client.Client.RemoteEndPoint.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Write(e);
                        }
                    }
                });

                _threads.Add(t);
                t.IsBackground = true;
                t.Start();
            }

            return result;
        }

        public static VoidResult Restart()
        {
            var result = new VoidResult();
            var currentThread = Thread.CurrentThread;
            new Thread(() => {
                try
                {
                    _listener.Stop();
                    _threads.ForEach(x => x.Abort());
                    _threads.Clear();
                    Start();
                }
                catch (Exception e)
                {
                    result.AddException(e);
                }
                currentThread.Resume();
            }).Start();

            currentThread.Suspend();

            return result;
        }
    }
}
