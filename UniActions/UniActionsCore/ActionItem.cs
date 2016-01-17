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
            var result = this.Dispatcher.Invoke(new Func<string>(() =>
            {
                lock (_locker)
                    return this.Action.Do(inputState);
            }), Defaults.DispatcherPriority, null);
            return result.ToString();
        }

        public void ExecuteAsync(string inputState, Action<string> callback)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                var state = "";
                lock (_locker)
                    state = this.Action.Do(inputState);
                callback(state);
            }), Defaults.DispatcherPriority ,null);
        }

        public void ExecuteAsync(Action<string> callback)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                var state = "";
                lock (_locker)
                    state = this.Action.Do(this.Action.State);
                callback(state);
            }), Defaults.DispatcherPriority, null);
        }

        public string Execute()
        {
            var result = this.Dispatcher.Invoke(new Func<string>(() =>
            {
                lock (_locker)
                    return this.Action.Do(this.Action.State);
            }), Defaults.DispatcherPriority, null);
            return result.ToString();
        }

        public void ExecuteWithoutRetval()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Action.Do(this.Action.State);
            }), Defaults.DispatcherPriority, null);
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
