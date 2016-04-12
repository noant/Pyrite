using HierarchicalData;
using Logging;
using System;

namespace PyriteCore
{
    public class Pyrite
    {
        static Pyrite()
        {
            Crutches.Execute();
        }

        public static Result<Pyrite> Create(SaveAndLoad sal)
        {
            var result = new Result<Pyrite>();
            var pyrite = new Pyrite();
            result.AddExceptions(pyrite.Initialize(sal).Exceptions);
            result.Value = pyrite;
            return result;
        }

        public static Result<Pyrite> Create()
        {
            return Create(new SaveAndLoad());
        }

        public VoidResult CommitChanges()
        {
            ScenariosPool.RefreshScenarios();
            return SaveAndLoad.Save();
        }

        public ServerThreading ServerThreading { get; private set; }

        public ScenariosPool ScenariosPool { get; private set; }

        public ModulesControl ModulesControl { get; private set; }

        internal SaveAndLoad SaveAndLoad { get; private set; }

        internal HierarchicalObject Settings { get; private set; }

        public VoidResult Initialize(SaveAndLoad sal)
        {
            this.SaveAndLoad = sal;
            this.SaveAndLoad.Pyrite = this;

            var result = new VoidResult();

            ScenariosPool = new ScenariosPool();
            ScenariosPool.Pyrite = this;
            ScenariosPool.Initialize();
            Log.Write("ScenariosPool initialized");

            ModulesControl = new ModulesControl();
            ModulesControl.Pyrite = this;
            result.AddExceptions(ModulesControl.Initialize().Exceptions);
            Log.Write("ModulesControl initialized");
            ServerThreading = new PyriteCore.ServerThreading();
            ServerThreading.Pyrite = this;
            ServerThreading.ServerStarted += () => ScenariosPool.StartActiveScenarios();
            Log.Write("ScenariosPool items started");
            ServerThreading.Initialize();
            Log.Write("ServerThreading started");

            result.AddExceptions(SaveAndLoad.Load().Exceptions);
            result.AddExceptions(ServerThreading.BeginStart().Exceptions);

            return result;
        }

        public VoidResult ReIntialize(Action<VoidResult> callback)
        {
            var result = new VoidResult();
            ModulesControl.Clear();
            ScenariosPool.Clear();

            result.AddExceptions(SaveAndLoad.Load().Exceptions);

            Stop(() =>
            {
                try
                {
                    ServerThreading.BeginStart();
                    Log.Write("ServerThreading started");
                    ServerThreading.ServerStarted += () => ScenariosPool.StartActiveScenarios();
                    Log.Write("ScenariosPool items started");
                    callback(result);
                }
                catch (Exception e)
                {
                    result.AddException(e);
                }
            });

            return result;
        }

        public void Stop(Action callback)
        {
            foreach (var scenario in ScenariosPool.Scenarios)
                scenario.KillDispatcher();

            ServerThreading.BeginStop(() =>
            {
                callback();
            });
        }
    }
}
