using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZWaveAction
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
        }

        public static IEnumerable<string> GetAllUsedControllers()
        {
            return _zwManagers.Values.Select(x => x.Device.Path).ToArray();
        }

        private static ZWManager _mainManager;

        private static Dictionary<DeviceKey, ZWave> _zwManagers = new Dictionary<DeviceKey, ZWave>();

        public static ZWave GetZWaveByValueID(ZWValueID id)
        {
            return _zwManagers.Values.FirstOrDefault(x => x.Nodes.Any(z => z.Values.Any(v => v.Equals(id))));
        }

        public static Node GetNodeById(byte id)
        {
            return _zwManagers.SelectMany(x => x.Value.Nodes).FirstOrDefault(x => x.ID.Equals(id));
        }

        public static bool IsValueBoolAndTrue(ulong id)
        {
            var valueId = GetZWValueById(id);
            if (valueId != null && valueId.GetType() == ZWValueID.ValueType.Bool)
            {
                bool isTrue;
                _mainManager.GetValueAsBool(valueId, out isTrue);
                return isTrue;
            }
            return false;
        }

        public static string GetNodeLabel(byte id)
        {
            var node = GetNodeById(id);
            if (node == null)
                return null;
            return node.Label;
        }

        public static string GetValueIDLabel(ulong id)
        {
            var value = _zwManagers.SelectMany(x => x.Value.Nodes).SelectMany(x => x.Values).Where(x => x.GetId().Equals(id)).FirstOrDefault();
            if (value == null)
                return null;
            return _mainManager.GetValueLabel(value);
        }

        public static ZWValueID GetZWValueById(ulong id)
        {
            return _zwManagers.SelectMany(x => x.Value.Nodes).SelectMany(x => x.Values).FirstOrDefault(x => x.GetId().Equals(id));
        }

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
#if DEBUG
                m_options.AddOptionInt("SaveLogLevel", (int)ZWLogLevel.Detail);
                m_options.AddOptionInt("QueueLogLevel", (int)ZWLogLevel.Debug);
                m_options.AddOptionInt("DumpTriggerLevel", (int)ZWLogLevel.Error);
#endif
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

            return _zwManagers[key];
        }

        public static void RemoveZWave(string device)
        {
            _zwManagers.Remove(_zwManagers.Keys.Single(x => x.Path.Equals(device)));
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
            private static bool WaitForValueChanged(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, int maxWaiting)
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
                var totalWaiting = 0;
                while (!flagContinue)
                {
                    Thread.Sleep(Constants.IterationWaitingInterval);
                    totalWaiting += Constants.IterationWaitingInterval;
                    if (totalWaiting >= maxWaiting) return false;
                }
                ZWGlobal.ZWaveEvent -= handler;
                return true;
            }

            public static void WaitForValueChanged(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, object targetValue, CheckerMode checkerMode)
            {
                var zwave = PrepareZWave(device, @interface);
                zwave.WaitForControllerLoaded();

                bool flagContinue = false;
                var handler = new Action<ZWManager, ZWaveEventArgs>(delegate (ZWManager manager, ZWaveEventArgs e)
                {
                    try
                    {
                        if (e.ValueID.GetId() == valueIDID
                        && e.ZWave.Device.Interface.Equals(@interface)
                        && e.ZWave.Device.Path.Equals(device))
                        {
                            if ((checkerMode == CheckerMode.Equals && e.CurrentValue.Equals(targetValue)) ||
                                (checkerMode == CheckerMode.Less && Convert.ToDecimal(e.CurrentValue) < Convert.ToDecimal(targetValue)) ||
                                (checkerMode == CheckerMode.LessOrEquals && Convert.ToDecimal(e.CurrentValue) <= Convert.ToDecimal(targetValue)) ||
                                (checkerMode == CheckerMode.More && Convert.ToDecimal(e.CurrentValue) > Convert.ToDecimal(targetValue)) ||
                                (checkerMode == CheckerMode.MoreOrEquals && Convert.ToDecimal(e.CurrentValue) >= Convert.ToDecimal(targetValue))
                            )
                                flagContinue = true;
                        }
                    }
                    catch
                    {
                        // do nothing // converting to decimal crutch
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
                if (!zwave.WaitForControllerLoaded())
                    return false;

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
                if (!zwave.WaitForControllerLoaded())
                    return false;

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
                if (!zwave.WaitForControllerLoaded())
                    return false;

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
                if (!zwave.WaitForControllerLoaded())
                    return false;

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
                if (!zwave.WaitForControllerLoaded())
                    return false;

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

            public static bool SetValue(string device, ControllerInterface @interface, uint homeId, byte nodeId, ulong valueIDID, object value, bool invertValueIfBool, AppendType mode)
            {
                var zwave = PrepareZWave(device, @interface);
                if (!zwave.WaitForControllerLoaded())
                    return false;

                var node = zwave.Nodes.SingleOrDefault(x =>
                    x.ID == nodeId
                    && x.HomeID == homeId);

                if (node == null)
                    return false;

                var valueId = node.Values.SingleOrDefault(x => x.GetId() == valueIDID);

                if (valueId == null)
                    return false;

                var type = valueId.GetType();

                if (invertValueIfBool && type == ZWValueID.ValueType.Bool)
                {
                    value = !(bool)Helper.GetValue(valueId, zwave.Manager);
                }

                if (mode != AppendType.Equalize &&
                    (type == ZWValueID.ValueType.Byte ||
                    type == ZWValueID.ValueType.Decimal ||
                    type == ZWValueID.ValueType.Int ||
                    type == ZWValueID.ValueType.Short))
                {
                    if (mode == AppendType.Decrement)
                        value = 0 - Convert.ToDouble(value);
                    else value = Convert.ToDouble(value);

                    value = Convert.ToDouble(Helper.GetValue(valueId, zwave.Manager)) + Convert.ToDouble(value);
                }

                if (_mainManager.IsNodeFailed(homeId, nodeId))
                    return false;

                if (!_mainManager.IsNodeAwake(homeId, nodeId))
                    return false;

                if (Helper.SetValue(zwave.Manager, valueId, value))
                {
                    return WaitForValueChanged(device, @interface, homeId, nodeId, valueIDID, 3000);
                }
                return false;
            }

            public static bool SetNodeOn(bool on, string device, ControllerInterface @interface, uint homeId, byte nodeId)
            {
                var zwave = PrepareZWave(device, @interface);
                if (!zwave.WaitForControllerLoaded())
                    return false;

                if (on)
                    zwave.Manager.SetNodeOn(homeId, nodeId);
                else zwave.Manager.SetNodeOff(homeId, nodeId);

                return true;
            }

            public enum AppendType
            {
                Increment,
                Equalize,
                Decrement
            }

            public enum CheckerMode
            {
                Equals,
                More,
                MoreOrEquals,
                Less,
                LessOrEquals
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
            Nodes = new List<Node>();
            Device = device;
        }

        public ZWManager Manager { get; private set; }
        public List<Node> Nodes { get; private set; }
        public DeviceKey Device { get; private set; }
        public uint? HomeId { get; internal set; }
        public bool NodesLoaded { get; internal set; }

        public bool? Failed { get; internal set; }

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
            while (!NodesLoaded && Failed == null)
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
