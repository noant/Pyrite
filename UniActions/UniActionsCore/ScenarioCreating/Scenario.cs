using System;
using System.Threading;
using System.Windows.Threading;
using UniActionsClientIntefaces;

namespace UniActionsCore.ScenarioCreating
{
    public class Scenario : IDisposable
    {
        private static class Defaults
        {
            public static readonly DispatcherPriority DispatcherPriority = System.Windows.Threading.DispatcherPriority.Background;
        }

        private Thread _thread;
        public Scenario()
        {
            this.IsActive = true;
            Guid = Guid.NewGuid();
            _thread = new Thread(() =>
            {
                this.Dispatcher = Dispatcher.CurrentDispatcher;
                Dispatcher.Run();
            });
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.IsBackground = true;
            _thread.Start();
        }

        public ICustomAction Action { get; set; }
        
        public bool UseServerThreading { get; set; }
        public string ServerCommand { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public bool IsOnlyOnce { get; set; }

        public event Action<Scenario> AfterActionServerEvent;

        private object _lockerAfterBefore = new object();

        private void RaiseAfterActionServerAsync()
        {
            if (AfterActionServerEvent != null)
                ThreadHelper.AlterThread(() =>
                {
                    lock (_lockerAfterBefore)
                        AfterActionServerEvent(this);
                });
        }

        public event Action<Scenario> AfterAction;

        private void RaiseAfterAction()
        {
            if (AfterAction != null)
                AfterAction(this);
        }

        private void RaiseAfterEvent()
        {
            RaiseAfterActionServerAsync();
            RaiseAfterAction();
        }

        internal Guid Guid { get; set; }

        private object _locker = new object();

        public string CheckState()
        {
            var result = this.Dispatcher.Invoke(new Func<string>(() =>
            {
                lock (_locker)
                    return this.Action.State;
            }), Defaults.DispatcherPriority, null);
            return result.ToString();
        }

        public void CheckStateAsync(Action<string> callback)
        {
            var result = this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lock (_locker)
                    callback(this.Action.State);
            }), Defaults.DispatcherPriority, null);
        }

        public string Execute(string inputState, bool withoutServerEvent)
        {
            try
            {
                var result = this.Dispatcher.Invoke(new Func<string>(() =>
                {
                    lock (_locker)
                        return this.Action.Do(inputState);
                }), Defaults.DispatcherPriority, null);
                return result.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (withoutServerEvent)
                    RaiseAfterAction();
                else
                    RaiseAfterEvent();
            }
        }

        public string Execute(string inputState)
        {
            return Execute(inputState, false);
        }
        
        public void ExecuteAsync(Action<string> callback)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                var state = "";
                lock (_locker)
                    state = this.Action.Do(this.Action.State);
                callback(state);
                RaiseAfterEvent();
            }), Defaults.DispatcherPriority, null);
        }

        public void ExecuteAsync(string state, Action<string> callback)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lock (_locker)
                    state = this.Action.Do(state);

                if (callback != null)
                    callback(state);

                RaiseAfterEvent();
            }), Defaults.DispatcherPriority, null);
        }
        
        public string Execute()
        {
            try
            {
                var result = this.Dispatcher.Invoke(new Func<string>(() =>
                {
                    lock (_locker)
                        return this.Action.Do(this.Action.State);
                }), Defaults.DispatcherPriority, null);
                return result.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RaiseAfterEvent();
            }
        }
        
        public Scenario Clone()
        {
            var item = new Scenario()
            {
                Action = ModulesControl.Clone(this.Action),
                Category = this.Category,
                Guid = this.Guid,
                IsActive = this.IsActive,
                Name = this.Name,
                ServerCommand = this.ServerCommand,
                UseServerThreading = this.UseServerThreading,
                IsOnlyOnce = this.IsOnlyOnce
            };

            return item;
        }

        public void Dispose()
        {
            this._thread.Abort();
        }

        private Dispatcher Dispatcher;
    }
}
