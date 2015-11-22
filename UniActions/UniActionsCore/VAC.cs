﻿using System;
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
            public static readonly string UsedActionCustomSettings = "use_actioncustomsettings_#1";
            public static readonly string UsedActionCheckerCustomSettings = "use_actioncheckercustomsettings_#1";
            public static readonly string UsedActionServerCommand = "use_actionservercommand_#1";
            public static readonly string UsedCustomAction = "use_action_#1";
            public static readonly string UsedCustomChecker = "use_checker_#1";
            public static readonly string UsedUseServerCommand = "use_servercommand_#1";
            public static readonly string UsedActionName = "use_actionname_#1";
            public static readonly string UsedCategory = "use_category_#1";
            public static readonly string UsedIsActive = "use_isactive_#1";
            public static readonly string UsedIsOnlyOnce = "use_isonlyonce_#1";
            public static readonly string ActionModule = "actionmodule_#1";
            public static readonly string CheckerModule = "checkermodule_#1";
            public static readonly string ResolvedIp = "resolvedip_#1";
            public static readonly string ResolveAll = "resolveall";
            public static readonly string SecondsBetweenActions = "secondsbetweenactions";

            public static readonly string Splitter = "#";
        }

        public static class ServerCommands
        {
            public static readonly string Command_GetStartCommands = "getallcommands";
            public static readonly string Command_GetCategoryCommands = "getcategorycommands";
            public static readonly string Command_EndFastActions = "endfastactions";
        }
    }
}
