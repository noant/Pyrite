using HierarchicalData;
using System;
using System.IO;
using System.Linq;
using System.Net;
using UniActionsCore.ScenarioCreation;

namespace UniActionsCore
{
    public class SaveAndLoad
    {
        public static class Defaults
        {
            public static readonly string FileName = "coreSettings.xml";
            public static readonly string PluginsFileName = "plugins.xml";
        }

        private string _fileName = Defaults.FileName;
        private string _pluginsFileName = Defaults.PluginsFileName;

        public SaveAndLoad(string fileName, string pluginsFileName)
        {
            _fileName = fileName;
            _pluginsFileName = pluginsFileName;
        }

        public SaveAndLoad() : this(Defaults.FileName, Defaults.PluginsFileName) { }

        public HierarchicalObject Savior { get; private set; }
        public HierarchicalObject PluginsSavior { get; private set; }

        internal Uni Uni { get; set; }

        public VoidResult Save()
        {
            var result = new VoidResult();
            try
            {
                PluginsSavior.Clear();
                PluginsSavior[VAC.AppSettingsNames.CheckerModule] = Uni.ModulesControl.CustomCheckers.Where(x => !Uni.ModulesControl.IsStandart(x)).Select(x => x.Assembly.Location).ToHierarchicalObject();
                PluginsSavior[VAC.AppSettingsNames.ActionModule] = Uni.ModulesControl.CustomActions.Where(x => !Uni.ModulesControl.IsStandart(x)).Select(x => x.Assembly.Location).ToHierarchicalObject();

                Savior.Clear();
                Savior[VAC.AppSettingsNames.DistributionPort] = Uni.ServerThreading.Settings.DistributionPort;
                Savior[VAC.AppSettingsNames.ResolveAll] = Uni.ServerThreading.Settings.ResolveAllIp;
                Savior[VAC.AppSettingsNames.ActionsPorts] = Uni.ServerThreading.Settings.ActionsPorts.ToHierarchicalObject();
                Savior[VAC.AppSettingsNames.ResolvedIp] = Uni.ServerThreading.Settings.ResolvedIp.ToHierarchicalObject();

                for (int i = 0; i < Uni.TasksPool.Scenarios.Count(); i++)
                {
                    try
                    {
                        var item = Uni.TasksPool.Scenarios.ElementAt(i);

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionCustomSettings] = new SerializedObject(item.ActionBag);

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionName] = item.Name;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionServerCommand] = item.ServerCommand;

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedCategory] = item.Category;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedCustomAction] = item.ActionBag.GetType().FullName;

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedUseServerCommand] = item.UseServerThreading;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedIsActive] = item.IsActive;
                    }
                    catch (Exception e)
                    {
                        result.AddException(e);
                    }
                }
                Savior.SaveToFile();
            }
            catch (Exception e)
            {
                result.AddException(e);
            }
            return result;
        }

        public void SetDefaults()
        {
            Uni.ServerThreading.Settings.ResolveAllIp = ServerThreading.ServerThreadingSettings.Defaults.ResolveAll;
            Uni.ServerThreading.Settings.DistributionPort = ServerThreading.ServerThreadingSettings.Defaults.DistributionPort;
            Uni.ServerThreading.Settings.ActionsPorts = SkeddedList<ushort>.Create(ServerThreading.ServerThreadingSettings.Defaults.ActionPorts);
        }

        public VoidResult Load()
        {
            var result = new VoidResult();
            //#if !DEBUG
            try
            {
                //#endif
                //plugins load
                if (!File.Exists(_pluginsFileName))
                {
                    new HierarchicalObject(_pluginsFileName).SaveToFile();
                }
                PluginsSavior = HierarchicalObject.FromFile(_pluginsFileName);
                PluginsSavior.ThrowsExceptionIfParameterNotExist = true;

                Uni.ModulesControl.Clear();

                if (PluginsSavior.ContainsKey(VAC.AppSettingsNames.ActionModule))
                    foreach (var item in PluginsSavior[VAC.AppSettingsNames.ActionModule].Values)
                    {
                        Uni.ModulesControl.RegisterAction(item);
                    }
                if (PluginsSavior.ContainsKey(VAC.AppSettingsNames.CheckerModule))
                    foreach (var item in PluginsSavior[VAC.AppSettingsNames.CheckerModule].Values)
                    {
                        Uni.ModulesControl.RegisterChecker(item);
                    }

                //other settings load
                if (!File.Exists(_fileName))
                {
                    new HierarchicalObject(_fileName).SaveToFile();
                }
                Savior = HierarchicalObject.FromFile(_fileName);
                Savior.ThrowsExceptionIfParameterNotExist = true;

                Uni.ServerThreading.Settings.DistributionPort = Savior[VAC.AppSettingsNames.DistributionPort];
                Uni.ServerThreading.Settings.ResolveAllIp = Savior[VAC.AppSettingsNames.ResolveAll];

                if (Savior.ContainsKey(VAC.AppSettingsNames.ActionsPorts))
                    Uni.ServerThreading.Settings.ActionsPorts = SkeddedList<ushort>.Create(Savior[VAC.AppSettingsNames.ActionsPorts].ToList<ushort>());

                if (Savior.ContainsKey(VAC.AppSettingsNames.ResolvedIp))
                    Uni.ServerThreading.Settings.ResolvedIp = SkeddedList<IPAddress>.Create(Savior[VAC.AppSettingsNames.ResolvedIp].ToList<IPAddress>());

                Uni.TasksPool.Clear();
                if (Savior.ContainsKey(VAC.AppSettingsNames.Action))
                    foreach (var hobject in Savior[VAC.AppSettingsNames.Action].Values)
                    {
                        var actionItem = new Scenario();
#if !DEBUG
                    try
                    {
#endif
                        actionItem.Category = hobject[VAC.AppSettingsNames.UsedCategory];
                        actionItem.Name = hobject[VAC.AppSettingsNames.UsedActionName];
                        actionItem.ServerCommand = hobject[VAC.AppSettingsNames.UsedActionServerCommand];
                        actionItem.UseServerThreading = hobject[VAC.AppSettingsNames.UsedUseServerCommand];

                        var actionTypeName = hobject[VAC.AppSettingsNames.UsedCustomAction];
                        var actionType = Uni.ModulesControl.GetActionTypeByName(actionTypeName);
                        if (hobject.ContainsKey(VAC.AppSettingsNames.UsedActionCustomSettings))
                            actionItem.ActionBag = hobject[VAC.AppSettingsNames.UsedActionCustomSettings].Value;

                        actionItem.IsActive = hobject[VAC.AppSettingsNames.UsedIsActive];

                        actionItem.Action.Refresh();

                        Uni.TasksPool.Add(actionItem);
#if !DEBUG
                    }
                    catch (Exception e)
                    {
                        result.AddException(e);
                    }
#endif
                    }
                //#if !DEBUG
            }
            catch (Exception e)
            {
                if (e is ParameterNotExistException)
                    SetDefaults();
                else
                {
                    result.AddException(e);
                }
            }
            finally
            {
                //#endif
                Savior.ThrowsExceptionIfParameterNotExist = false;
                PluginsSavior.ThrowsExceptionIfParameterNotExist = false;
                //#if !DEBUG
            }
            //#endif
            return result;
        }
    }
}
