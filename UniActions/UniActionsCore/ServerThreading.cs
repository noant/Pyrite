using Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
                public static readonly ushort DistributionPort = 600;
                public static readonly bool ResolveAll = true;
                public static readonly int ReceiveTimout = 10000;
                public static readonly int SendTimout = 10000;

                public static readonly IEnumerable<ushort> ActionPorts = new List<ushort>() { 
                    601,602,603,604,605,606,607,608,609,610
                };
            }

            public static ushort DistributionPort { get; set; }
            public static bool ResolveAllIp { get; set; }

            public static List<IPAddress> ResolvedIp { get; internal set; }
            public static List<ushort> ActionsPorts { get; internal set; } 
        }

        private class ThreadPortOccupation {
            public ThreadPortOccupation(Thread t, TcpListener listener, ushort port)
            {
                Thread = t;
                Port = port;
                Listener = listener;
            }

            public Thread Thread { get; private set; }
            public TcpListener Listener { get; private set; }
            public ushort Port { get; private set; }
            public bool IsOccupiedByClient { get; set; }
        }

        private static volatile List<ThreadPortOccupation> _threadPortOccupations;

        public static event Action WhenStopped;

        private static volatile bool _prepareToStop;
        private static Action _whenStoppedCallback;
        public static VoidResult BeginStop(Action callback)
        {
            _whenStoppedCallback = callback;

            var result = new VoidResult();
            try
            {
                _prepareToStop = true;

                _listenerPortDistribution.Stop();

                _threadPortOccupations.ForEach(x =>
                {
                    if (!x.IsOccupiedByClient)
                    {
                        //x.Thread.Abort();
                        x.Listener.Stop();
                    }
                });

                _threadPortOccupations.RemoveAll(x => !x.IsOccupiedByClient);
            }
            catch (Exception e)
            {
                result.AddException(e);
            }

            return result;
        }
        private static bool _isStopped;
        public static bool IsStopped
        {
            get
            {
                return _isStopped;
            }
            private set {
                _isStopped = value;
                if (value && _whenStoppedCallback != null)
                {
                    _whenStoppedCallback();
                    _whenStoppedCallback = null;
                }
                if (value && WhenStopped != null)
                    WhenStopped();
            }
        }

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
            Settings.ResolvedIp = new List<IPAddress>();
            Settings.ActionsPorts = Settings.Defaults.ActionPorts.ToList();
            IsStopped = true;
        }

        private static TcpListener _listenerPortDistribution;
        private static Thread _threadPortDistribution;

        public static VoidResult BeginStart()
        {
            VoidResult result = new VoidResult();

            if (!IsStopped)
            {
                result.AddException(new Exception("Is started now"));
                return result;
            }

            if (_threadPortOccupations == null)
                _threadPortOccupations = new List<ThreadPortOccupation>();

            IsStopped = false;
            _prepareToStop = false;

            _threadPortDistribution = Helper.AlterThread(() => { 
                _listenerPortDistribution = new TcpListener(Settings.DistributionPort);
                _listenerPortDistribution.Start();
                while (!_prepareToStop)
                {
                    try
                    {
                        var client = _listenerPortDistribution.AcceptTcpClient();
                        ThreadPortOccupation occupation = null;

                        while (occupation == null)
                            occupation = _threadPortOccupations.FirstOrDefault(x => !x.IsOccupiedByClient);

                        SendString(client.GetStream(), occupation.Port.ToString());
                        client.Close();
                    }
                    catch (Exception e)
                    {
                        Log.Write(e);
                    }
                }
            });

            for (int i = 0; i < Settings.ActionsPorts.Count; i++)
            {
                try
                {
                    var port = Settings.ActionsPorts[i];

                    var listener = new TcpListener(port);
                    listener.Server.SendTimeout = Settings.Defaults.SendTimout;
                    listener.Server.ReceiveTimeout = Settings.Defaults.ReceiveTimout;
                    listener.Start();
                    Thread t = new Thread(() => {
                        while (!_prepareToStop)
                        {
                            try
                            {
                                TcpClient client = listener.AcceptTcpClient();

                                _threadPortOccupations.Single(x => x.Port == port).IsOccupiedByClient = true;

                                if (Settings.ResolvedIp != null && Settings.ResolvedIp.Any() && !Settings.ResolveAllIp)
                                {
                                    lock (Settings.ResolvedIp)
                                    {
                                        var ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                                        if (!Settings.ResolvedIp.Where(x=>x.Equals(ip)).Any())
                                        {
                                            client.Close();
                                            throw new Exception("Not resolved ip: " + ip);
                                        }
                                    }
                                }

                                var stream = client.GetStream();
                                var command = GetNextString(stream);

                                lock (Pool.ActionItems)
                                {
                                    CommandHandling(stream, command);
                                }

                                client.Close();
                            }
                            catch (Exception e)
                            {
                                Log.Write(e);
                            }
                            finally {
                                if (_threadPortOccupations.Any(x => x.Port == port))
                                    _threadPortOccupations.Single(x => x.Port == port).IsOccupiedByClient = false;                              
                            }

                            if (_prepareToStop)
                            {
                                listener.Stop();
                                _threadPortOccupations.RemoveAll(x => x.Port == port);

                                if (!_threadPortOccupations.Any())
                                    IsStopped = true;
                            }
                        }
                    });
                    _threadPortOccupations.Add(new ThreadPortOccupation(t, listener, port));
                    t.IsBackground = false;
                    t.Start();
                }
                catch (Exception e)
                {
                    Log.Write(e);
                    result.AddException(e);
                }
            }

            return result;
        }

        private static void CommandHandling(NetworkStream stream, string command)
        {
            if (command == VAC.ServerCommands.Command_GetStartCommands)
            {
                var fastActions = Pool.ActionItems.Where(x => string.IsNullOrEmpty(x.Category) && x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand));
                var categories = Pool.ActionItems.Where(x => !string.IsNullOrEmpty(x.Category) && !string.IsNullOrEmpty(x.ServerCommand) && x.UseServerThreading).Select(x => x.Category).Distinct().OrderBy(x => x);
                stream.WriteByte((byte)(fastActions.Count() + categories.Count()));
                foreach (var action in fastActions)
                {
                    SendString(stream, action.CheckState());
                    SendString(stream, action.ServerCommand);}

                SendString(stream, VAC.ServerCommands.Command_EndFastActions);

                foreach (var category in categories)
                {
                    SendString(stream, category);}
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
                }
            }
            else
            {
                var remoteAction = Pool.ActionItems.Where(x => x.ServerCommand == command).FirstOrDefault();
                if (remoteAction != null)
                {
                    var state = GetNextString(stream);

                    SendString(stream, remoteAction.Execute(state));
                }
            }
        }
    }
}
