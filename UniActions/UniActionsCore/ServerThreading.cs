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
        public class ServerThreadingSettings
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

            public ushort DistributionPort { get; set; }
            public bool ResolveAllIp { get; set; }

            public List<IPAddress> ResolvedIp { get; internal set; }
            public List<ushort> ActionsPorts { get; internal set; } 
        }
                
        private class ThreadPortOccupation{
            public ThreadPortOccupation(Thread t, TcpListenerEx listener, ushort port)
            {
                Thread = t;
                Port = port;
                Listener = listener;
            }

            public Thread Thread { get; private set; }
            public TcpListenerEx Listener { get; private set; }
            public ushort Port { get; private set; }
            public bool IsOccupiedByClient { get; set; }
        }

        public Uni Uni { get; internal set; }

        public ServerThreadingSettings Settings {get; private set;}

        private volatile List<ThreadPortOccupation> _threadPortOccupations;

        public event Action WhenStopped;

        private volatile bool _prepareToStop;
        private Action _whenStoppedCallback;
        public VoidResult BeginStop(Action callback)
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
                        x.Listener.Stop();
                    }
                });
            }
            catch (Exception e)
            {
                result.AddException(e);
            }

            return result;
        }
        private bool _isStopped;
        public bool IsStopped
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

        private void SendString(NetworkStream stream, string str)
        {
            var bytesToSend = Encoding.UTF8.GetBytes(str);
            stream.WriteByte((byte)bytesToSend.Length);
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        private string GetNextString(NetworkStream stream)
        {
            var len = stream.ReadByte();
            var buff = new byte[len];
            stream.Read(buff, 0, buff.Length);
            return Encoding.UTF8.GetString(buff);
        }
        
        public void Initialize()
        {
            this.Settings = new ServerThreadingSettings();
            this.Settings.ResolvedIp = new List<IPAddress>();
            this.Settings.ActionsPorts = ServerThreadingSettings.Defaults.ActionPorts.ToList();
            IsStopped = true;
        }

        private TcpListenerEx _listenerPortDistribution;
        private Thread _threadPortDistribution;

        public VoidResult BeginStart()
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
                _listenerPortDistribution = new TcpListenerEx(Settings.DistributionPort);
                _listenerPortDistribution.Start();
                while (!_prepareToStop)
                {
                    try
                    {
                        using (var client = _listenerPortDistribution.AcceptTcpClient())
                        {
                            ThreadPortOccupation occupation = null;

                            while (occupation == null)
                            {
                                occupation = _threadPortOccupations.FirstOrDefault(x => !x.IsOccupiedByClient);
                                if (occupation == null)
                                    Thread.Sleep(10);
                            }

                            SendString(client.GetStream(), occupation.Port.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is SocketException && e.Message.Contains("WSACancelBlockingCall"))
                        {
                            //do nothing
                        }
                        else
                            Log.Write(e);
                    }
                }
            });

            for (int i = 0; i < Settings.ActionsPorts.Count; i++)
            {
                try
                {
                    var port = Settings.ActionsPorts[i];

                    var listener = new TcpListenerEx(port);

                    listener.Server.SendTimeout = ServerThreadingSettings.Defaults.SendTimout;
                    listener.Server.ReceiveTimeout = ServerThreadingSettings.Defaults.ReceiveTimout;
                    ThreadPortOccupation portOccupation = null;
                    Thread t = new Thread(() => {
                        while (true)
                        {
                            try
                            {
                                using (TcpClient client = listener.AcceptTcpClient())
                                {
                                    portOccupation.IsOccupiedByClient = true;

                                    if (Settings.ResolvedIp != null && Settings.ResolvedIp.Any() && !Settings.ResolveAllIp)
                                    {
                                        lock (Settings.ResolvedIp)
                                        {
                                            var ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                                            if (!Settings.ResolvedIp.Where(x => x.Equals(ip)).Any())
                                            {
                                                client.Close();
                                                throw new Exception("Not resolved ip: " + ip);
                                            }
                                        }
                                    }

                                    var stream = client.GetStream();
                                    var command = GetNextString(stream);

                                    CommandHandling(stream, command);
                                }
                            }
                            catch (Exception e)
                            {
                                if (e is SocketException && e.Message.Contains("WSACancelBlockingCall"))
                                {
                                    //do nothing
                                }
                                else
                                    Log.Write(e);
                            }

                            portOccupation.IsOccupiedByClient = false;

                            if (!listener.IsActive)
                            {
                                lock (_threadPortOccupations)
                                {
                                    _threadPortOccupations.RemoveAll(x => x.Port == port);

                                    if (!_threadPortOccupations.Any())
                                        IsStopped = true;
                                }
                                break;
                            }
                        }
                    });

                    _threadPortOccupations.Add(portOccupation = new ThreadPortOccupation(t, listener, port));
                    t.IsBackground = false;
                    listener.Start();
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

        private void CommandHandling(NetworkStream stream, string command)
        {
            if (command == VAC.ServerCommands.Command_GetStartCommands)
            {
                var fastActions = Uni.TasksPool.ActionItems.Where(x => string.IsNullOrEmpty(x.Category) && x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand));
                var categories = Uni.TasksPool.ActionItems.Where(x => !string.IsNullOrEmpty(x.Category) && !string.IsNullOrEmpty(x.ServerCommand) && x.UseServerThreading).Select(x => x.Category).Distinct().OrderBy(x => x);
                stream.WriteByte((byte)(fastActions.Count() + categories.Count()));
                foreach (var action in fastActions)
                {
                    SendString(stream, action.CheckState());
                    SendString(stream, action.ServerCommand);}

                SendString(stream, VAC.ServerCommands.Command_EndFastActions);

                foreach (var category in categories)
                {
                    SendString(stream, category);
                }
            }
            else if (command == VAC.ServerCommands.Command_GetCategoryCommands)
            {
                var category = GetNextString(stream);

                var actions = Uni.TasksPool.ActionItems.Where(x => x.Category == category && x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand));

                stream.WriteByte((byte)actions.Count());

                foreach (var action in actions)
                {
                    SendString(stream, action.CheckState());
                    SendString(stream, action.ServerCommand);
                }
            }
            else
            {
                var remoteAction = Uni.TasksPool.ActionItems.Where(x => x.ServerCommand == command).FirstOrDefault();
                if (remoteAction != null)
                {
                    var state = GetNextString(stream);
                    SendString(stream, remoteAction.Execute(state));
                }
            }
        }
    }
}
