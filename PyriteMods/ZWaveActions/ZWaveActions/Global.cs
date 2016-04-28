using HierarchicalData;
using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZWaveActions
{
    public static class ZWGlobal
    {
        internal static class Constants
        {
            public static readonly ushort IterationWaitingInterval = 10;
            public static readonly int MaxWaitingInterval = 200000;
        }

        static ZWGlobal()
        {
            _data = new HierarchicalObject("zwave_data.xml");
        }

        private static HierarchicalObject _data;

        private static void SetAnotherOneController(string id)
        {
            _data[GetAllUsedControllers().Count()] = id;
            _data.SaveToFile();
        }

        public static IEnumerable<string> GetAllUsedControllers()
        {
            for (int i = 0; _data.ContainsKey(i); i++)
                yield return _data[i].ToString();
        }

        private static ZWManager _mainManager;

        private static Dictionary<DeviceKey, ZWave> _zwManagers = new Dictionary<DeviceKey, ZWave>();

        public static ZWave PrepareZWave(string device, ControllerInterface @interface)
        {
            var key = new DeviceKey()
            {
                Path = device,
                Interface = @interface
            };
            if (_mainManager == null)
            {
                var m_options = new ZWOptions();
                var location = Path.GetDirectoryName(typeof(ZWGlobal).Assembly.Location) + @"\config\";
                m_options.Create(location, @"", @"");
                //m_options.AddOptionInt("SaveLogLevel", (int)ZWLogLevel.Detail);
                //m_options.AddOptionInt("QueueLogLevel", (int)ZWLogLevel.Debug);
                //m_options.AddOptionInt("DumpTriggerLevel", (int)ZWLogLevel.Error);
                m_options.Lock();
                _mainManager = new ZWManager();
                _mainManager.OnNotification = (notification) =>
                {
                    NotificationHandler.Execute(
                        _zwManagers[key],
                        notification,
                        new Action<ZWManager, ZWaveEventArgs>((o, e) =>
                        {
                            if (ZWaveEvent != null)
                                ZWaveEvent(o, e);
                        }
                        )
                    );
                };
                _mainManager.Create();
            }

            if (!_zwManagers.ContainsKey(key))
            {
                _zwManagers.Add(key, new ZWave(_mainManager, key));
                if (!_mainManager.AddDriver(key.Path, key.Interface.Equals(ControllerInterface.Serial) ? ZWControllerInterface.Serial : ZWControllerInterface.Hid))
                    throw new Exception("Cannot connect to device " + key.Path + " (" + key.Interface + ")");
            }

            if (!GetAllUsedControllers().Contains(device))
                SetAnotherOneController(device);

            return _zwManagers[key];
        }

        public static bool ControllersLoaded { get; internal set; }

        public static void WaitForControllersLoaded()
        {
            var sum = 0;
            while (!ControllersLoaded)
            {
                Thread.Sleep(Constants.IterationWaitingInterval);
                sum += Constants.IterationWaitingInterval;
                if (sum >= Constants.MaxWaitingInterval)
                    break;
            }
        }

        internal static ZWave[] GetAllZWaveControllersNames()
        {
            return _zwManagers.Values.ToArray();
        }

        public static event Action<ZWManager, ZWaveEventArgs> ZWaveEvent;

        public static class Simplified
        {
            public static void WaitForValueChanged(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID)
            {
                var zwave = PrepareZWave(device, @interface);
                zwave.WaitForControllerLoaded();
                bool flagContinue = false;
                var handler = new Action<ZWManager, ZWaveEventArgs>(delegate (ZWManager manager, ZWaveEventArgs e)
                {
                    if (e.ValueID.GetId() == valueIDID && e.ZWave.Device.Interface.Equals(@interface) && e.ZWave.Device.Path.Equals(device))
                    {
                        flagContinue = true;
                    }
                });
                ZWGlobal.ZWaveEvent += handler;
                while (!flagContinue)
                    Thread.Sleep(Constants.IterationWaitingInterval);
                ZWGlobal.ZWaveEvent -= handler;
            }

            public static void WaitForValueChanged(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, object targetValue)
            {
                var zwave = PrepareZWave(device, @interface);

                zwave.WaitForControllerLoaded();
                bool flagContinue = false;
                var handler = new Action<ZWManager, ZWaveEventArgs>(delegate (ZWManager manager, ZWaveEventArgs e)
                {
                    if (e.ValueID.GetId() == valueIDID
                    && e.ZWave.Device.Interface.Equals(@interface)
                    && e.ZWave.Device.Path.Equals(device)
                    && e.CurrentValue.Equals(targetValue))
                    {
                        flagContinue = true;
                    }
                });
                ZWGlobal.ZWaveEvent += handler;
                while (!flagContinue)
                    Thread.Sleep(Constants.IterationWaitingInterval);
                ZWGlobal.ZWaveEvent -= handler;
            }

            public static bool IsValueEquals(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, object targetValue)
            {
                var zwave = PrepareZWave(device, @interface);
                zwave.WaitForControllerLoaded();

                var node = zwave.Nodes.SingleOrDefault(x =>
                    x.ID == nodeId
                    && x.HomeID == homeId);

                if (node == null)
                    return false;

                var valueId = node.Values.SingleOrDefault(x => x.GetId() == valueIDID);

                if (valueId == null)
                    return false;

                var valCurrent = Convert.ToDecimal(valueId.GetValue(zwave.Manager));

                return valCurrent.Equals(Convert.ToDecimal(targetValue));
            }

            public static bool IsValueMoreThan(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, object targetValue)
            {
                var zwave = PrepareZWave(device, @interface);
                zwave.WaitForControllerLoaded();

                var node = zwave.Nodes.SingleOrDefault(x =>
                    x.ID == nodeId
                    && x.HomeID == homeId);

                if (node == null)
                    return false;

                var valueId = node.Values.SingleOrDefault(x => x.GetId() == valueIDID);

                if (valueId == null)
                    return false;

                var valCurrent = Convert.ToDecimal(valueId.GetValue(zwave.Manager));

                return valCurrent > Convert.ToDecimal(targetValue);
            }

            public static bool IsValueMoreThanOrEqual(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, object targetValue)
            {
                var zwave = PrepareZWave(device, @interface);
                zwave.WaitForControllerLoaded();

                var node = zwave.Nodes.SingleOrDefault(x =>
                    x.ID == nodeId
                    && x.HomeID == homeId);

                if (node == null)
                    return false;

                var valueId = node.Values.SingleOrDefault(x => x.GetId() == valueIDID);

                if (valueId == null)
                    return false;

                var valCurrent = Convert.ToDecimal(valueId.GetValue(zwave.Manager));

                return valCurrent >= Convert.ToDecimal(targetValue);
            }

            public static bool IsValueLessThanOrEqual(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, object targetValue)
            {
                var zwave = PrepareZWave(device, @interface);
                zwave.WaitForControllerLoaded();

                var node = zwave.Nodes.SingleOrDefault(x =>
                    x.ID == nodeId
                    && x.HomeID == homeId);

                if (node == null)
                    return false;

                var valueId = node.Values.SingleOrDefault(x => x.GetId() == valueIDID);

                if (valueId == null)
                    return false;

                var valCurrent = Convert.ToDecimal(valueId.GetValue(zwave.Manager));

                return valCurrent <= Convert.ToDecimal(targetValue);
            }

            public static bool IsValueLessThan(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, object targetValue)
            {
                var zwave = PrepareZWave(device, @interface);
                zwave.WaitForControllerLoaded();

                var node = zwave.Nodes.SingleOrDefault(x =>
                    x.ID == nodeId
                    && x.HomeID == homeId);

                if (node == null)
                    return false;

                var valueId = node.Values.SingleOrDefault(x => x.GetId() == valueIDID);

                if (valueId == null)
                    return false;

                var valCurrent = Convert.ToDecimal(valueId.GetValue(zwave.Manager));

                return valCurrent < Convert.ToDecimal(targetValue);
            }

            public static bool SetValue(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, object value)
            {
                var zwave = PrepareZWave(device, @interface);
                zwave.WaitForControllerLoaded();

                var node = zwave.Nodes.SingleOrDefault(x =>
                    x.ID == nodeId
                    && x.HomeID == homeId);

                if (node == null)
                    return false;

                var valueId = node.Values.SingleOrDefault(x => x.GetId() == valueIDID);

                if (valueId == null)
                    return false;

                return Helper.SetValue(zwave.Manager, valueId, value);
            }
        }
    }

    public struct DeviceKey
    {
        public string Path { get; set; }
        public ControllerInterface Interface { get; set; }
    }

    public class ZWave
    {
        public ZWave(ZWManager manager, DeviceKey device)
        {
            Manager = manager;
            Nodes = new BindingList<Node>();
            Device = device;
        }

        public ZWManager Manager { get; private set; }
        public BindingList<Node> Nodes { get; private set; }
        public DeviceKey Device { get; private set; }
        public bool NodesLoaded { get; internal set; }

        public T GetValue<T>(ZWValueID valueId)
        {
            return Helper.GetValue<T>(valueId, Manager);
        }

        public bool SetValue<T>(ZWValueID valueId, T value)
        {
            return Helper.SetValue<T>(valueId, Manager, value);
        }

        public bool WaitForControllerLoaded()
        {
            var sum = 0;
            while (!NodesLoaded)
            {
                Thread.Sleep(ZWGlobal.Constants.IterationWaitingInterval);
                sum += ZWGlobal.Constants.IterationWaitingInterval;
                if (sum >= ZWGlobal.Constants.MaxWaitingInterval)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class ZWaveEventArgs : EventArgs
    {
        public ZWaveEventArgs(ZWave zWave, Node node, ZWValueID valueID, object currentValue)
        {
            ZWave = zWave;
            Node = node;
            ValueID = valueID;
            CurrentValue = currentValue;
        }

        public ZWave ZWave { get; private set; }
        public Node Node { get; private set; }
        public ZWValueID ValueID { get; private set; }
        public object CurrentValue { get; private set; }
    }

    public enum ControllerInterface
    {
        Serial = 1,
        HID = 2
    }
}
