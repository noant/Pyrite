using ApplicationUserSettings;
using Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace UniActionsCore
{
    public class SaveAndLoad
    {
        public static class Defaults
        {
            public static readonly string FileName = "coreSettings.xml";
        }

        public SaveAndLoad(Func<Stream> needStreamToRead, Func<Stream> needStreamToWrite, Action<Stream> afterCommit) {
            Savior = new Settings(
                (settings) => needStreamToRead(),
                (settings) => needStreamToWrite(),
                (settings, stream) => afterCommit(stream)
                );
        }

        public SaveAndLoad(string fileName)
        {
            Savior = new Settings(fileName);
        }

        public SaveAndLoad() : this(Defaults.FileName) { }
        
        public Settings Savior { get; private set; }

        internal Uni Uni { get; set; }
        
        public VoidResult Save()
        {
            var result = new VoidResult();
            try
            {
                Savior.Clear();

                Savior.SetValue(VAC.AppSettingsNames.DistributionPort, Uni.ServerThreading.Settings.DistributionPort.ToString());
                Savior.SetValue(VAC.AppSettingsNames.ResolveAll, Uni.ServerThreading.Settings.ResolveAllIp.ToString());
                Savior.SetValue(VAC.AppSettingsNames.SecondsBetweenActions, Uni.TasksPool.Settings.SecondsBetweenActions.ToString());
                Savior.SetValue(VAC.AppSettingsNames.ActionsPorts, Helper.MassToString(Uni.ServerThreading.Settings.ActionsPorts.Cast<object>(), VAC.AppSettingsNames.Splitter));

                for (int i = 0; i < Uni.ServerThreading.Settings.ResolvedIp.Count(); i++)
                {
                    Savior.SetValue(VAC.AppSettingsNames.
                        ResolvedIp.Set(i),
                        Uni.ServerThreading.Settings.
                        ResolvedIp[i].ToString());
                }

                var customCheckers = Uni.ModulesControl.CustomCheckers.Where(x => !Uni.ModulesControl.IsStandart(x));
                for (int i = 0; i < customCheckers.Count(); i++)
                {
                    Savior.SetValue(VAC.AppSettingsNames.
                        CheckerModule.Set(i),
                        customCheckers.ElementAt(i).Assembly.Location);
                }

                var customActions = Uni.ModulesControl.CustomActions.Where(x => !Uni.ModulesControl.IsStandart(x));
                for (int i = 0; i < customActions.Count(); i++)
                {
                    Savior.SetValue(VAC.AppSettingsNames.
                        ActionModule.Set(i),
                        customActions.ElementAt(i).Assembly.Location);
                }

                for (int i = 0; i < Uni.TasksPool.ActionItems.Count(); i++)
                {
                    try
                    {
                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedActionName.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).Name);

                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedActionCheckerCustomSettings.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).Checker.SetToString());

                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedActionCustomSettings.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).Action.SetToString());

                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedActionServerCommand.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).ServerCommand);

                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedCategory.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).Category);

                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedCustomAction.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).Action.GetType().FullName);

                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedCustomChecker.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).Checker.GetType().FullName);

                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedUseServerCommand.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).UseServerThreading.ToString());

                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedIsActive.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).IsActive.ToString());

                        Savior.SetValue(VAC.AppSettingsNames.
                            UsedIsOnlyOnce.Set(i),
                            Uni.TasksPool.ActionItems.ElementAt(i).IsOnlyOnce.ToString());
                    }
                    catch (Exception e)
                    {
                        Log.Write(e);
                        result.AddException(e);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                result.AddException(e);
            }
            return result;
        }

        public void SetDefaults()
        {
            Uni.TasksPool.Settings.SecondsBetweenActions = TasksPool.TasksPoolSettings.Defaults.SecondsBetweenActions;
            Uni.ServerThreading.Settings.ResolveAllIp = ServerThreading.ServerThreadingSettings.Defaults.ResolveAll;
            Uni.ServerThreading.Settings.DistributionPort = ServerThreading.ServerThreadingSettings.Defaults.DistributionPort;
            Uni.ServerThreading.Settings.ActionsPorts = ServerThreading.ServerThreadingSettings.Defaults.ActionPorts.ToList();
        }

        public VoidResult Load() {
            var result = new VoidResult();
            try
            {
                if (Savior == null)
                    Savior = new ApplicationUserSettings.Settings(Defaults.FileName);

                Uni.ServerThreading.Settings.DistributionPort = ushort.Parse(Savior.GetValue(VAC.AppSettingsNames.DistributionPort));
                Uni.ServerThreading.Settings.ResolveAllIp = Convert.ToBoolean(Savior.GetValue(VAC.AppSettingsNames.ResolveAll));
                Uni.ServerThreading.Settings.ActionsPorts = Savior.GetValue(VAC.AppSettingsNames.ActionsPorts)
                    .Split(VAC.AppSettingsNames.Splitter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => ushort.Parse(x))
                    .ToList();

                Uni.TasksPool.Settings.SecondsBetweenActions = int.Parse(Savior.GetValue(VAC.AppSettingsNames.SecondsBetweenActions));

                int i = 0;
                Uni.ServerThreading.Settings.ResolvedIp.Clear();
                while (Savior.Contains(VAC.AppSettingsNames.ResolvedIp.Set(i)))
                {
                    Uni.ServerThreading.Settings.ResolvedIp.Add(
                        IPAddress.Parse(Savior.GetValue(VAC.AppSettingsNames.ResolvedIp.Set(i))));
                    i++;
                }

                i = 0;
                Uni.ModulesControl.Clear();
                while (Savior.Contains(VAC.AppSettingsNames.ActionModule.Set(i)))
                {
                    var name = Savior.GetValue(VAC.AppSettingsNames.ActionModule.Set(i));
                    Uni.ModulesControl.RegisterAction(name);
                    i++;
                }
                i = 0;
                while (Savior.Contains(VAC.AppSettingsNames.CheckerModule.Set(i)))
                {
                    var name = Savior.GetValue(VAC.AppSettingsNames.CheckerModule.Set(i));
                    Uni.ModulesControl.RegisterChecker(name);
                    i++;
                }

                i = 0;
                Uni.TasksPool.Clear();
                while (Savior.Contains(VAC.AppSettingsNames.UsedActionName.Set(i)))
                {
                    var actionItem = new ActionItem();

                    try
                    {
                        actionItem.Category = Savior.GetValue(VAC.AppSettingsNames.UsedCategory.Set(i));
                        actionItem.Name = Savior.GetValue(VAC.AppSettingsNames.UsedActionName.Set(i));
                        actionItem.ServerCommand = Savior.GetValue(VAC.AppSettingsNames.UsedActionServerCommand.Set(i));
                        actionItem.UseServerThreading = Convert.ToBoolean(Savior.GetValue(VAC.AppSettingsNames.UsedUseServerCommand.Set(i)));

                        var actionTypeName = Savior.GetValue(VAC.AppSettingsNames.UsedCustomAction.Set(i));
                        var actionType = Uni.ModulesControl.CustomActions.Single(x => x.FullName == actionTypeName);
                        actionItem.Action = (ICustomAction)actionType.GetConstructor(new Type[0]).Invoke(new object[0]);
                        actionItem.Action.SetFromString(Savior.GetValue(VAC.AppSettingsNames.UsedActionCustomSettings.Set(i)));

                        var checkerTypeName = Savior.GetValue(VAC.AppSettingsNames.UsedCustomChecker.Set(i));
                        var checkerType = Uni.ModulesControl.CustomCheckers.Single(x => x.FullName == checkerTypeName);
                        actionItem.Checker = (ICustomChecker)checkerType.GetConstructor(new Type[0]).Invoke(new object[0]);
                        actionItem.Checker.SetFromString(Savior.GetValue(VAC.AppSettingsNames.UsedActionCheckerCustomSettings.Set(i)));

                        actionItem.IsActive = bool.Parse(Savior.GetValue(VAC.AppSettingsNames.UsedIsActive.Set(i)));
                        actionItem.IsOnlyOnce = bool.Parse(Savior.GetValue(VAC.AppSettingsNames.UsedIsOnlyOnce.Set(i)));

                        Uni.TasksPool.AddItem(actionItem);
                    }
                    catch (Exception e)
                    {
                        Log.Write(e);
                        result.AddException(e);
                    }

                    i++;
                }
            }
            catch (Exception e)
            {
                if (e is ApplicationUserSettings.Settings.ParameterNotExistException)
                    SetDefaults();
                else result.AddException(e);
                
                Log.Write(e);
            }

            return result;
        }
    }
}
