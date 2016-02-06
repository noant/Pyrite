using HierarchicalData;
using System;
using System.IO;
using System.Linq;
using System.Net;
using UniActionsClientIntefaces;

namespace UniActionsCore
{
    public class SaveAndLoad
    {
        public static class Defaults
        {
            public static readonly string FileName = "coreSettings.xml";
        }

        private string _fileName = Defaults.FileName;

        public SaveAndLoad(string fileName)
        {
            _fileName = fileName;
        }

        public SaveAndLoad() : this(Defaults.FileName) { }
        
        public HierarchicalObject Savior { get; private set; }

        internal Uni Uni { get; set; }
        
        public VoidResult Save()
        {
            var result = new VoidResult();
            try
            {
                Savior.Clear();

                Savior[VAC.AppSettingsNames.DistributionPort] = Uni.ServerThreading.Settings.DistributionPort;
                Savior[VAC.AppSettingsNames.ResolveAll] = Uni.ServerThreading.Settings.ResolveAllIp;
                Savior[VAC.AppSettingsNames.SecondsBetweenActions] = Uni.TasksPool.Settings.SecondsBetweenActions;
                Savior[VAC.AppSettingsNames.ActionsPorts] = Uni.ServerThreading.Settings.ActionsPorts.ToHierarchicalObject();
                Savior[VAC.AppSettingsNames.ResolvedIp] = Uni.ServerThreading.Settings.ResolvedIp.ToHierarchicalObject();

                Savior[VAC.AppSettingsNames.CheckerModule] = Uni.ModulesControl.CustomCheckers.Where(x => !Uni.ModulesControl.IsStandart(x)).Select(x => x.Assembly.Location).ToHierarchicalObject();
                Savior[VAC.AppSettingsNames.ActionModule] = Uni.ModulesControl.CustomActions.Where(x => !Uni.ModulesControl.IsStandart(x)).Select(x => x.Assembly.Location).ToHierarchicalObject();

                for (int i = 0; i < Uni.TasksPool.ActionItems.Count(); i++)
                {
                    try
                    {
                        var item = Uni.TasksPool.ActionItems.ElementAt(i);

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionName] = item.Name;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionCustomSettings] = HierarchicalData.Helper.CreateBySettingsAttribute(item.Action);

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionCheckerCustomSettings] = HierarchicalData.Helper.CreateBySettingsAttribute(item.Checker);
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedActionServerCommand] = item.ServerCommand;

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedCategory] = item.Category;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedCustomAction] = item.Action.GetType().FullName;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedCustomChecker] = item.Checker.GetType().FullName;

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedUseServerCommand] = item.UseServerThreading;
                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedIsActive] = item.IsActive;

                        Savior[VAC.AppSettingsNames.Action][i][VAC.AppSettingsNames.UsedIsOnlyOnce] = item.IsOnlyOnce;
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
            Uni.TasksPool.Settings.SecondsBetweenActions = TasksPool.TasksPoolSettings.Defaults.SecondsBetweenActions;
            Uni.ServerThreading.Settings.ResolveAllIp = ServerThreading.ServerThreadingSettings.Defaults.ResolveAll;
            Uni.ServerThreading.Settings.DistributionPort = ServerThreading.ServerThreadingSettings.Defaults.DistributionPort;
            Uni.ServerThreading.Settings.ActionsPorts = SkeddedList<ushort>.Create(ServerThreading.ServerThreadingSettings.Defaults.ActionPorts);
        }

        public VoidResult Load()
        {
            var result = new VoidResult();
            try
            {
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

                Uni.TasksPool.Settings.SecondsBetweenActions = Savior[VAC.AppSettingsNames.SecondsBetweenActions];

                if (Savior.ContainsKey(VAC.AppSettingsNames.ResolvedIp))
                    Uni.ServerThreading.Settings.ResolvedIp = SkeddedList<IPAddress>.Create(Savior[VAC.AppSettingsNames.ResolvedIp].ToList<IPAddress>());

                Uni.ModulesControl.Clear();

                if (Savior.ContainsKey(VAC.AppSettingsNames.ActionModule))
                    foreach (var item in Savior[VAC.AppSettingsNames.ActionModule].Values)
                    {
                        Uni.ModulesControl.RegisterAction(item);
                    }
                if (Savior.ContainsKey(VAC.AppSettingsNames.CheckerModule))
                    foreach (var item in Savior[VAC.AppSettingsNames.CheckerModule].Values)
                    {
                        Uni.ModulesControl.RegisterChecker(item);
                    }

                Uni.TasksPool.Clear();
                if (Savior.ContainsKey(VAC.AppSettingsNames.Action))
                    foreach (var hobject in Savior[VAC.AppSettingsNames.Action].Values)
                    {
                        var actionItem = new ActionItem();

                        try
                        {
                            actionItem.Category = hobject[VAC.AppSettingsNames.UsedCategory];
                            actionItem.Name = hobject[VAC.AppSettingsNames.UsedActionName];
                            actionItem.ServerCommand = hobject[VAC.AppSettingsNames.UsedActionServerCommand];
                            actionItem.UseServerThreading = hobject[VAC.AppSettingsNames.UsedUseServerCommand];

                            var actionTypeName = hobject[VAC.AppSettingsNames.UsedCustomAction];
                            var actionType = Uni.ModulesControl.CustomActions.Single(x => x.FullName == actionTypeName);
                            actionItem.Action = (ICustomAction)actionType.GetConstructor(new Type[0]).Invoke(new object[0]);
                            if (hobject.ContainsKey(VAC.AppSettingsNames.UsedActionCustomSettings))
                                HierarchicalData.Helper.SetToObject(actionItem.Action, hobject[VAC.AppSettingsNames.UsedActionCustomSettings]);

                            var checkerTypeName = hobject[VAC.AppSettingsNames.UsedCustomChecker];
                            var checkerType = Uni.ModulesControl.CustomCheckers.Single(x => x.FullName == checkerTypeName);
                            actionItem.Checker = (ICustomChecker)checkerType.GetConstructor(new Type[0]).Invoke(new object[0]);
                            if (hobject.ContainsKey(VAC.AppSettingsNames.UsedActionCheckerCustomSettings))
                                HierarchicalData.Helper.SetToObject(actionItem.Checker, hobject[VAC.AppSettingsNames.UsedActionCheckerCustomSettings]);

                            actionItem.IsActive = hobject[VAC.AppSettingsNames.UsedIsActive];
                            actionItem.IsOnlyOnce = hobject[VAC.AppSettingsNames.UsedIsOnlyOnce];

                            actionItem.Action.Refresh();
                            actionItem.Checker.Refresh();

                            Uni.TasksPool.AddItem(actionItem);
                        }
                        catch (Exception e)
                        {
                            result.AddException(e);
                        }
                    }
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
                Savior.ThrowsExceptionIfParameterNotExist = false;
            }
            return result;
        }
    }
}
