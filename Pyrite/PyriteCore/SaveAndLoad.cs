using HierarchicalData;
using PyriteCore.CoreStandartActions;
using PyriteCore.ScenarioCreation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace PyriteCore
{
    public class SaveAndLoad
    {
        public static class Defaults
        {
            static Defaults()
            {
                FileName = Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName, FileName);
                PluginsFileName = Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName, PluginsFileName);
            }
            public static readonly string FileName = "coreSettings.xml";
            public static readonly string PluginsFileName = "plugins.xml";
        }

        private string _fileName;
        private string _pluginsFileName;

        public SaveAndLoad(string fileName, string pluginsFileName)
        {
            _fileName = fileName;
            _pluginsFileName = pluginsFileName;
        }

        public SaveAndLoad() : this(Defaults.FileName, Defaults.PluginsFileName) { }

        public HierarchicalObject Savior { get; private set; }
        public HierarchicalObject PluginsSavior { get; private set; }

        internal Pyrite Pyrite { get; set; }

        public VoidResult Save()
        {
            var result = new VoidResult();
            try
            {
                PluginsSavior.Clear();
                PluginsSavior[VAC.AppSettingsNames.CheckerModule] = Pyrite.ModulesControl.CustomCheckers.Where(x => !Pyrite.ModulesControl.IsStandart(x)).Select(x => x.Assembly.Location).ToHierarchicalObject();
                PluginsSavior[VAC.AppSettingsNames.ActionModule] = Pyrite.ModulesControl.CustomActions.Where(x => !Pyrite.ModulesControl.IsStandart(x)).Select(x => x.Assembly.Location).ToHierarchicalObject();
                PluginsSavior.SaveToFile();

                Savior.Clear();
                Savior[VAC.AppSettingsNames.DistributionPort] = Pyrite.ServerThreading.Settings.DistributionPort;
                Savior[VAC.AppSettingsNames.ResolveAll] = Pyrite.ServerThreading.Settings.ResolveAllIp;
                Savior[VAC.AppSettingsNames.ActionsPorts] = Pyrite.ServerThreading.Settings.ActionsPorts.ToHierarchicalObject();
                Savior[VAC.AppSettingsNames.ResolvedIp] = Pyrite.ServerThreading.Settings.ResolvedIp.Select(x => x.ToString()).ToHierarchicalObject();

                for (int i = 0; i < Pyrite.ScenariosPool.Scenarios.Count(); i++)
                {
                    try
                    {
                        var item = Pyrite.ScenariosPool.Scenarios.ElementAt(i);

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionCustomSettings] = new SerializedObject(item.ActionBag);

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionName] = item.Name;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionServerCommand] = item.ServerCommand;

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedCategory] = item.Category;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedCustomAction] = item.Action.GetType().FullName;

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedUseServerCommand] = item.UseServerThreading;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedIsActive] = item.IsActive;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedIndex] = item.Index;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedOffOnState] = item.UseOnOffState;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.ActionGuid] = item.Guid;
                    }
                    catch (Exception e)
                    {
                        result.AddWarning(new Warning(e.Message), true);
                    }
                }
                Savior.SaveToFile();
            }
            catch (Exception e)
            {
                result.AddWarning(new Warning(e.Message), true);
#if DEBUG
                throw e;
#endif
            }
            return result;
        }

        public void SetDefaults()
        {
            Pyrite.ServerThreading.Settings.ResolveAllIp = ServerThreading.ServerThreadingSettings.Defaults.ResolveAll;
            Pyrite.ServerThreading.Settings.DistributionPort = ServerThreading.ServerThreadingSettings.Defaults.DistributionPort;
            Pyrite.ServerThreading.Settings.ActionsPorts = SkeddedList<ushort>.Create(ServerThreading.ServerThreadingSettings.Defaults.ActionPorts);
        }

        public VoidResult Load()
        {
            var result = new VoidResult();
            try
            {
                //plugins load
                if (!File.Exists(_pluginsFileName))
                {
                    new HierarchicalObject(_pluginsFileName).SaveToFile();
                }
                PluginsSavior = HierarchicalObject.FromFile(_pluginsFileName);
                PluginsSavior.ThrowsExceptionIfParameterNotExist = true;

                Pyrite.ModulesControl.Clear();

                if (PluginsSavior.ContainsKey(VAC.AppSettingsNames.ActionModule))
                    foreach (var item in PluginsSavior[VAC.AppSettingsNames.ActionModule].Values)
                    {
                        try
                        {
                            Pyrite.ModulesControl.RegisterAction(item);
                        }
                        catch (Exception e)
                        {
                            result.AddWarning(new Warning(e.Message), true);
                        }
                    }
                if (PluginsSavior.ContainsKey(VAC.AppSettingsNames.CheckerModule))
                    foreach (var item in PluginsSavior[VAC.AppSettingsNames.CheckerModule].Values)
                    {
                        try
                        {
                            Pyrite.ModulesControl.RegisterChecker(item);
                        }
                        catch (Exception e)
                        {
                            result.AddWarning(new Warning(e.Message), true);
                        }
                    }

            }
            catch (Exception e)
            {
                result.AddWarning(new Warning(e.Message));
            }
            finally
            {
                if (PluginsSavior != null)
                    PluginsSavior.ThrowsExceptionIfParameterNotExist = false;
            }
            try
            {
                //other settings load
                if (!File.Exists(_fileName))
                {
                    new HierarchicalObject(_fileName).SaveToFile();
                }
                Savior = HierarchicalObject.FromFile(_fileName);
                Savior.ThrowsExceptionIfParameterNotExist = true;

                Pyrite.ServerThreading.Settings.DistributionPort = Savior[VAC.AppSettingsNames.DistributionPort];
                Pyrite.ServerThreading.Settings.ResolveAllIp = Savior[VAC.AppSettingsNames.ResolveAll];

                if (Savior.ContainsKey(VAC.AppSettingsNames.ActionsPorts))
                    Pyrite.ServerThreading.Settings.ActionsPorts = SkeddedList<ushort>.Create(Savior[VAC.AppSettingsNames.ActionsPorts].ToList<ushort>());

                if (Savior.ContainsKey(VAC.AppSettingsNames.ResolvedIp))
                    Pyrite.ServerThreading.Settings.ResolvedIp = SkeddedList<IPAddress>.Create(((List<string>)Savior[VAC.AppSettingsNames.ResolvedIp].ToList<string>()).Select(x => IPAddress.Parse(x)));

                Pyrite.ScenariosPool.Clear();
                if (Savior.ContainsKey(VAC.AppSettingsNames.Action))
                {
                    foreach (var hobject in Savior[VAC.AppSettingsNames.Action].Values)
                    {
                        var actionItem = new Scenario();
                        try
                        {
                            var actionTypeName = hobject[VAC.AppSettingsNames.UsedCustomAction];

                            if (Pyrite.ModulesControl.CustomActions.Any(x => x.FullName.Equals(actionTypeName)) ||
                                actionTypeName.Equals(typeof(DoubleComplexAction).FullName))
                            {
                                actionItem.Category = hobject[VAC.AppSettingsNames.UsedCategory];
                                actionItem.Name = hobject[VAC.AppSettingsNames.UsedActionName];
                                actionItem.ServerCommand = hobject[VAC.AppSettingsNames.UsedActionServerCommand];
                                actionItem.UseServerThreading = hobject[VAC.AppSettingsNames.UsedUseServerCommand];
                                actionItem.Index = hobject[VAC.AppSettingsNames.UsedIndex];
                                actionItem.UseOnOffState = hobject[VAC.AppSettingsNames.UsedOffOnState];
                                actionItem.Guid = hobject[VAC.AppSettingsNames.ActionGuid];

                                if (hobject.ContainsKey(VAC.AppSettingsNames.UsedActionCustomSettings))
                                {
                                    actionItem.ActionBag = hobject[VAC.AppSettingsNames.UsedActionCustomSettings].Value;
                                }

                                actionItem.IsActive = hobject[VAC.AppSettingsNames.UsedIsActive];

                                Pyrite.ScenariosPool.Add(actionItem);

                                actionItem.CurrentPyrite = this.Pyrite;

                                actionItem.ForAllActionAndChecker(x =>
                                {
                                    if (x is ICoreElement)
                                        ((ICoreElement)x).CurrentPyrite = this.Pyrite;
                                });
                            }
                        }
                        catch (Exception e)
                        {
                            result.AddWarning(new Warning(e.Message), true);
                        }
                    }

                    Pyrite.ScenariosPool.RefreshScenarios();
                    foreach (var scenario in Pyrite.ScenariosPool.Scenarios.OrderBy(x => x.Guid))
                        scenario.Refresh();
                }
            }
            catch (Exception e)
            {
                if (e is ParameterNotExistException)
                {
                    SetDefaults();
                    result.AddWarning(new Warning("Создана новая база сценариев."), true);
                }
                else
                {
                    result.AddWarning(new Warning(e.Message), true);
                }
            }
            finally
            {
                if (Savior != null)
                    Savior.ThrowsExceptionIfParameterNotExist = false;
            }
            return result;
        }
    }
}
