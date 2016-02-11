using HierarchicalData;
using System;

namespace UniActionsCore
{
    public class Uni
    {
        public static Result<Uni> Create(SaveAndLoad sal)
        {
            var result = new Result<Uni>();
            var uni = new Uni();
            result.AddExceptions(uni.Initialize(sal).Exceptions);
            result.Value = uni;
            return result;
        }

        public static Result<Uni> Create()
        {
            return Create(new SaveAndLoad());
        }

        public VoidResult CommitChanges()
        {
            return SaveAndLoad.Save();
        }

        public ServerThreading ServerThreading { get; private set; }

        public TasksPool TasksPool { get; private set; }

        public ModulesControl ModulesControl { get; private set; }

        internal SaveAndLoad SaveAndLoad { get; private set; }

        internal HierarchicalObject Settings { get; private set; }

        public VoidResult Initialize(SaveAndLoad sal)
        {
            this.SaveAndLoad = sal;
            this.SaveAndLoad.Uni = this;

            var result = new VoidResult();

            TasksPool = new TasksPool();
            TasksPool.Uni = this;
            TasksPool.Initialize();
            ModulesControl = new ModulesControl();
            ModulesControl.Uni = this;
            result.AddExceptions(ModulesControl.Initialize().Exceptions);
            ServerThreading = new UniActionsCore.ServerThreading();
            ServerThreading.Uni = this;
            ServerThreading.Initialize();

            result.AddExceptions(SaveAndLoad.Load().Exceptions);
            
            result.AddExceptions(ServerThreading.BeginStart().Exceptions);

            return result;
        }

        public VoidResult ReIntialize(Action<VoidResult> callback)
        {
            var result = new VoidResult();
            ModulesControl.Clear();
            TasksPool.Clear();

            result.AddExceptions(SaveAndLoad.Load().Exceptions);
            
            ServerThreading.BeginStop(() =>
            {
                try
                {
                    ServerThreading.BeginStart();
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
            ServerThreading.BeginStop(() =>
            {
                callback();
            });
        }
    }
}
