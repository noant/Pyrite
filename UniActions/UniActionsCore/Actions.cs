using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniActionsCore
{
    public static class Actions
    {
        public static VoidResult Initialize()
        {
            var result = new VoidResult();

            Pool.Initialize();
            result.AddExceptions(ModulesControl.Initialize().Exceptions);
            ServerThreading.Initialize();

            result.AddExceptions(SAL.Load().Exceptions);

            result.AddExceptions(Pool.Start().Exceptions);
            result.AddExceptions(ServerThreading.Start().Exceptions);

            return result;
        }

        public static VoidResult ReIntialize()
        {
            var result = new VoidResult();
            Pool.Clear();
            ModulesControl.Clear();
            
            result.AddExceptions(SAL.Load().Exceptions);
            result.AddExceptions(Pool.Restart().Exceptions);
            result.AddExceptions(ServerThreading.Restart().Exceptions);

            return result;
        }
    }
}
