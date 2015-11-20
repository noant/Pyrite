using ApplicationUserSettings;
using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace UniActionsCore
{
    public static class SAL
    {
        public static class Defaults
        {
            public static readonly string FileName = "coreSettings.xml";
        }

        private static Settings Settings { get; set; }

        public static VoidResult Save()
        {
            var result = new VoidResult();
            try
            {
                Settings.Clear();

                Settings.SetValue(VAC.AppSettingsNames.ServerPort, ServerThreading.Settings.ServerListenerPort.ToString());
                Settings.SetValue(VAC.AppSettingsNames.ResolveAll, ServerThreading.Settings.ResolveAll.ToString());
                Settings.SetValue(VAC.AppSettingsNames.ServerThreadCount, ServerThreading.Settings.ServerThreadCount.ToString());
                Settings.SetValue(VAC.AppSettingsNames.SecondsBetweenActions, Pool.Settings.SecondsBetweenActions.ToString());

                for (int i = 0; i < ServerThreading.Settings.ResolvedIp.Count(); i++)
                {
                    Settings.SetValue(VAC.AppSettingsNames.
                        ResolvedIp.Set(i),
                        ServerThreading.Settings.
                        ResolvedIp[i]);
                }

                var customCheckers = ModulesControl.CustomCheckers.Where(x => !ModulesControl.IsStandart(x));
                for (int i = 0; i < customCheckers.Count(); i++)
                {
                    Settings.SetValue(VAC.AppSettingsNames.
                        CheckerModule.Set(i),
                        customCheckers.ElementAt(i).Assembly.Location);
                }

                var customActions = ModulesControl.CustomActions.Where(x => !ModulesControl.IsStandart(x));
                for (int i = 0; i < customActions.Count(); i++)
                {
                    Settings.SetValue(VAC.AppSettingsNames.
                        ActionModule.Set(i),
                        customActions.ElementAt(i).Assembly.Location);
                }

                for (int i = 0; i < Pool.ActionItems.Count(); i++)
                {
                    try
                    {
                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedActionName.Set(i),
                            Pool.ActionItems.ElementAt(i).Name);

                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedActionCheckerCustomSettings.Set(i),
                            Pool.ActionItems.ElementAt(i).Checker.SetToString());

                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedActionCustomSettings.Set(i),
                            Pool.ActionItems.ElementAt(i).Action.SetToString());

                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedActionServerCommand.Set(i),
                            Pool.ActionItems.ElementAt(i).ServerCommand);

                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedCategory.Set(i),
                            Pool.ActionItems.ElementAt(i).Category);

                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedCustomAction.Set(i),
                            Pool.ActionItems.ElementAt(i).Action.GetType().FullName);

                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedCustomChecker.Set(i),
                            Pool.ActionItems.ElementAt(i).Checker.GetType().FullName);

                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedUseServerCommand.Set(i),
                            Pool.ActionItems.ElementAt(i).UseServerThreading.ToString());

                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedIsActive.Set(i),
                            Pool.ActionItems.ElementAt(i).IsActive.ToString());

                        Settings.SetValue(VAC.AppSettingsNames.
                            UsedIsOnlyOnce.Set(i),
                            Pool.ActionItems.ElementAt(i).IsOnlyOnce.ToString());
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

        public static void SetDefaults()
        {
            Pool.Settings.SecondsBetweenActions = Pool.Settings.Default.SecondsBetweenActions;
            ServerThreading.Settings.ResolveAll = ServerThreading.Settings.Defaults.ResolveAll;
            ServerThreading.Settings.ServerListenerPort = ServerThreading.Settings.Defaults.ServerListenerPort;
            ServerThreading.Settings.ServerThreadCount = ServerThreading.Settings.Defaults.ServerThreadCount;
        }

        public static VoidResult Load() {
            var result = new VoidResult();
            try
            {
                if (Settings == null)
                    Settings = new ApplicationUserSettings.Settings(Defaults.FileName);

                ServerThreading.Settings.ServerListenerPort = int.Parse(Settings.GetValue(VAC.AppSettingsNames.ServerPort));
                ServerThreading.Settings.ResolveAll = Convert.ToBoolean(Settings.GetValue(VAC.AppSettingsNames.ResolveAll));
                ServerThreading.Settings.ServerThreadCount = int.Parse(Settings.GetValue(VAC.AppSettingsNames.ServerThreadCount)); 
                Pool.Settings.SecondsBetweenActions = int.Parse(Settings.GetValue(VAC.AppSettingsNames.SecondsBetweenActions));

                int i = 0;
                ServerThreading.Settings.ResolvedIp.Clear();
                while (Settings.Contains(VAC.AppSettingsNames.ResolvedIp.Set(i)))
                {
                    ServerThreading.Settings.ResolvedIp.Add(
                        Settings.GetValue(VAC.AppSettingsNames.ResolvedIp.Set(i)));
                    i++;
                }

                i = 0;
                ModulesControl.Clear();
                while (Settings.Contains(VAC.AppSettingsNames.ActionModule.Set(i)))
                {
                    var name = Settings.GetValue(VAC.AppSettingsNames.ActionModule.Set(i));
                    ModulesControl.RegisterAction(name);
                    i++;
                }
                i = 0;
                while (Settings.Contains(VAC.AppSettingsNames.CheckerModule.Set(i)))
                {
                    var name = Settings.GetValue(VAC.AppSettingsNames.CheckerModule.Set(i));
                    ModulesControl.RegisterChecker(name);
                    i++;
                }

                i = 0;
                Pool.Clear();
                while (Settings.Contains(VAC.AppSettingsNames.UsedActionName.Set(i)))
                {
                    var actionItem = new ActionItem();

                    try
                    {
                        actionItem.Category = Settings.GetValue(VAC.AppSettingsNames.UsedCategory.Set(i));
                        actionItem.Name = Settings.GetValue(VAC.AppSettingsNames.UsedActionName.Set(i));
                        actionItem.ServerCommand = Settings.GetValue(VAC.AppSettingsNames.UsedActionServerCommand.Set(i));
                        actionItem.UseServerThreading = Convert.ToBoolean(Settings.GetValue(VAC.AppSettingsNames.UsedUseServerCommand.Set(i)));

                        var actionTypeName = Settings.GetValue(VAC.AppSettingsNames.UsedCustomAction.Set(i));
                        var actionType = ModulesControl.CustomActions.Single(x => x.FullName == actionTypeName);
                        actionItem.Action = (ICustomAction)actionType.GetConstructor(new Type[0]).Invoke(new object[0]);
                        actionItem.Action.SetFromString(Settings.GetValue(VAC.AppSettingsNames.UsedActionCustomSettings.Set(i)));

                        var checkerTypeName = Settings.GetValue(VAC.AppSettingsNames.UsedCustomChecker.Set(i));
                        var checkerType = ModulesControl.CustomCheckers.Single(x => x.FullName == checkerTypeName);
                        actionItem.Checker = (ICustomChecker)checkerType.GetConstructor(new Type[0]).Invoke(new object[0]);
                        actionItem.Checker.SetFromString(Settings.GetValue(VAC.AppSettingsNames.UsedActionCheckerCustomSettings.Set(i)));

                        actionItem.IsActive = bool.Parse(Settings.GetValue(VAC.AppSettingsNames.UsedIsActive.Set(i)));
                        actionItem.IsOnlyOnce = bool.Parse(Settings.GetValue(VAC.AppSettingsNames.UsedIsOnlyOnce.Set(i)));

                        Pool.AddItem(actionItem);
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

                Log.Write(e);

                result.AddException(e);
            }

            return result;
        }
    }
}
