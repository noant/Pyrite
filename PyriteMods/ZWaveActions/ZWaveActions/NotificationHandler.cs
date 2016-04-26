using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZWaveActions
{
    public static class NotificationHandler
    {
        public static void Execute(ZWave zWave, ZWNotification notification, Action<ZWManager, ZWaveEventArgs> zWaveEvent)
        {
            var nodeId = notification.GetNodeId();
            var homeId = notification.GetHomeId();
            var node = zWave.Nodes.SingleOrDefault(x => x.ID == nodeId);

            switch (notification.GetType())
            {
                case ZWNotification.Type.ValueChanged:
                    {
                        var valueId = notification.GetValueID();
                        zWaveEvent(zWave.Manager, new ZWaveEventArgs(
                            zWave,
                            zWave.Nodes.Single(x => x.ID == nodeId),
                            valueId,
                            valueId.GetValue<object>(zWave.Manager)
                            ));
                        break;
                    }

                case ZWNotification.Type.NodeAdded:
                    {
                        if (!zWave.Nodes.Any(x => x.HomeID == homeId && x.ID == notification.GetNodeId()))
                        {
                            node = new Node();
                            node.ID = notification.GetNodeId();
                            node.HomeID = homeId;
                            zWave.Nodes.Add(node);
                        }
                        break;
                    }

                case ZWNotification.Type.NodeNew:
                    {
                        node = new Node();
                        node.ID = notification.GetNodeId();
                        node.HomeID = homeId;
                        zWave.Nodes.Add(node);
                        break;
                    }

                case ZWNotification.Type.NodeRemoved:
                    {
                        zWave.Nodes
                        .Remove(
                             zWave.Nodes.SingleOrDefault(x => x.ID == notification.GetNodeId())
                        );
                        break;
                    }

                case ZWNotification.Type.ValueAdded:
                    {
                        node.AddValue(notification.GetValueID());
                        break;
                    }

                case ZWNotification.Type.ValueRemoved:
                    {
                        node.RemoveValue(notification.GetValueID());
                        break;
                    }

                case ZWNotification.Type.NodeProtocolInfo:
                    {
                        node.Label = zWave.Manager.GetNodeType(homeId, node.ID);
                        break;
                    }

                case ZWNotification.Type.NodeNaming:
                    {
                        node.Manufacturer = zWave.Manager.GetNodeManufacturerName(homeId, node.ID);
                        node.Product = zWave.Manager.GetNodeProductName(homeId, node.ID);
                        node.Location = zWave.Manager.GetNodeLocation(homeId, node.ID);
                        node.Name = zWave.Manager.GetNodeName(homeId, node.ID);
                        break;
                    }

                case ZWNotification.Type.NodeQueriesComplete:
                    {
                        node.Loaded = true;
                        if (!zWave.Nodes.Where(x => !x.Loaded).Any() || !zWave.Nodes.Any())
                        {
                            zWave.NodesLoaded = true;
                            if (!ZWGlobal.GetAllZWaveControllersNames().Where(x => !x.NodesLoaded).Any())
                                ZWGlobal.ControllersLoaded = true;
                        }
                        break;
                    }

                case ZWNotification.Type.Group:
                    {
                        break;
                    }

                case ZWNotification.Type.NodeEvent:
                    {
                        break;
                    }

                case ZWNotification.Type.PollingDisabled:
                    {
                        break;
                    }

                case ZWNotification.Type.PollingEnabled:
                    {
                        break;
                    }

                case ZWNotification.Type.DriverReady:
                    {
                        BeginRequestParams(zWave, homeId);
                        break;
                    }

                case ZWNotification.Type.EssentialNodeQueriesComplete:
                    {
                        BeginRequestParams(zWave, homeId);
                        break;
                    }

                case ZWNotification.Type.AllNodesQueried:
                    {
                        BeginRequestParams(zWave, homeId);
                        break;
                    }

                case ZWNotification.Type.AllNodesQueriedSomeDead:
                    {
                        zWave.NodesLoaded = true;
                        if (!ZWGlobal.GetAllZWaveControllersNames().Where(x => !x.NodesLoaded).Any())
                            ZWGlobal.ControllersLoaded = true;
                        BeginRequestParams(zWave, homeId);
                        break;
                    }

                case ZWNotification.Type.AwakeNodesQueried:
                    {
                        break;
                    }
            }
        }

        private static void BeginRequestParams(ZWave zWave, uint homeId)
        {
            foreach (var curnode in zWave.Nodes.Where(x => !x.RequestingValuesBegan))
            {
                zWave.Manager.RequestAllConfigParams(homeId, curnode.ID);
                curnode.RequestingValuesBegan = true;
            }
        }
    }
}
