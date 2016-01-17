using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniActionsCore
{
    internal static class VAC
    {
        public static class AppSettingsNames
        {
            public static readonly string ActionsPorts = "action_ports";
            public static readonly string DistributionPort = "serverPort";
            public static readonly string UsedActionCustomSettings = "use_actioncustomsettings";
            public static readonly string UsedActionCheckerCustomSettings = "use_actioncheckercustomsettings";
            public static readonly string UsedActionServerCommand = "use_actionservercommand";
            public static readonly string UsedCustomAction = "use_action";
            public static readonly string UsedCustomChecker = "use_checker";
            public static readonly string UsedUseServerCommand = "use_servercommand";
            public static readonly string UsedActionName = "use_actionname";
            public static readonly string UsedCategory = "use_category";
            public static readonly string UsedIsActive = "use_isactive";
            public static readonly string UsedIsOnlyOnce = "use_isonlyonce";
            public static readonly string ActionModule = "actionmodule";
            public static readonly string CheckerModule = "checkermodule";
            public static readonly string ResolvedIp = "resolvedip";
            public static readonly string ResolveAll = "resolveall";
            public static readonly string SecondsBetweenActions = "secondsbetweenactions";
            public static readonly string Action = "action";
        }

        public static class ServerCommands
        {
            public static readonly string Command_GetStartCommands = "getallcommands";
            public static readonly string Command_GetCategoryCommands = "getcategorycommands";
            public static readonly string Command_EndFastActions = "endfastactions";
        }
    }
}
