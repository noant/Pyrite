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

            result.AddExceptions(Pool.BeginStart().Exceptions);
            result.AddExceptions(ServerThreading.BeginStart().Exceptions);

            return result;
        }

        public static VoidResult ReIntialize(Action<VoidResult> callback)
        {
            var result = new VoidResult();
            ModulesControl.Clear();
            Pool.Clear();
            
            result.AddExceptions(SAL.Load().Exceptions);

            object locker = new object();
            bool poolStopped = false;
            bool serverStopped = false;
            Pool.BeginStop(() =>
            {
                lock (locker)
                {
                    poolStopped = true;
                    if (serverStopped)
                    {
                        try
                        {
                            ServerThreading.BeginStart();
                            Pool.BeginStart();
                        }
                        catch (Exception e)
                        {
                            result.AddException(e);
                        }
                        callback(result);
                    }
                }
            });
            ServerThreading.BeginStop(() =>
            {
                lock (locker)
                {
                    serverStopped = true;
                    if (poolStopped)
                    {
                        try
                        {
                            ServerThreading.BeginStart();
                            Pool.BeginStart();
                        }
                        catch (Exception e)
                        {
                            result.AddException(e);
                        }
                        callback(result);                    
                    }
                }
            });

            return result;
        }

        public static void Stop(Action callback)
        {
            object locker = new object();
            bool poolStopped = false;
            bool serverStopped = false;
            Pool.BeginStop(() => {
                lock (locker)
                {
                    poolStopped = true;
                    if (serverStopped)
                        callback();
                }
            });
            ServerThreading.BeginStop(() => {
                lock (locker)
                {
                    serverStopped = true;
                    if (poolStopped)
                        callback();
                }
            });
        }
    }
}
