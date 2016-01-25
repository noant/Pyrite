using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using UniActionsClientIntefaces;

namespace UniActionsCore
{
    public class ActionItem : IDisposable
    {
        private static class Defaults
        {
            public static readonly DispatcherPriority DispatcherPriority = System.Windows.Threading.DispatcherPriority.Background;
        }

        private Thread _thread;
        public ActionItem()
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
        public ICustomChecker Checker { get; set; }
        
        public bool UseServerThreading { get; set; }
        public string ServerCommand { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public bool IsOnlyOnce { get; set; }

        public event Action<ActionItem> AfterActionSlow;
        public event Action<ActionItem> BeforeActionSlow;

        private object _lockerAfterBefore = new object();

        private void RaiseAfterActionSlowAsync()
        {
            if (AfterActionSlow != null)
                Helper.AlterThread(() =>
                {
                    lock (_lockerAfterBefore)
                        AfterActionSlow(this);
                });
        }

        private void RaiseBeforeActionSlowAsync()
        {
            if (BeforeActionSlow != null)
                Helper.AlterThread(() => {
                    lock (_lockerAfterBefore)
                        BeforeActionSlow(this);
                });                
        }

        public event Action<ActionItem> AfterActionFast;
        public event Action<ActionItem> BeforeActionFast;

        private void RaiseAfterActionFast()
        {
            if (AfterActionFast != null)
                AfterActionFast(this);
        }

        private void RaiseBeforeActionFast()
        {
            if (BeforeActionFast != null)
                BeforeActionFast(this);
        }

        private void RaiseAfterEvent()
        {
            RaiseAfterActionSlowAsync();
            RaiseAfterActionFast();
        }

        private void RaiseBeforeEvent()
        {
            RaiseBeforeActionSlowAsync();
            RaiseBeforeActionFast();
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

        public string Execute(string inputState)
        {
            try
            {
                RaiseBeforeEvent();
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
                RaiseAfterEvent();
            }
        }
        
        public void ExecuteAsync(Action<string> callback)
        {
            RaiseBeforeEvent();
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
            RaiseBeforeEvent();
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lock (_locker)
                    state = this.Action.Do(state);

                if (callback != null)
                    callback(state);

                RaiseAfterEvent();
            }), Defaults.DispatcherPriority, null);
        }

        public string BusyState
        {
            get
            {
                return "Выполняется: " + Action.Name;
            }
        }

        public string Execute()
        {
            try
            {
                RaiseBeforeEvent();
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
        
        public ActionItem Clone()
        {
            var item = new ActionItem()
            {
                Action = ModulesControl.Clone(this.Action),
                Checker = ModulesControl.Clone(this.Checker),
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
