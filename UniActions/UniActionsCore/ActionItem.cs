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
    public class ActionItem
    {
        private static class Defaults
        {
            public static readonly DispatcherPriority DispatcherPriority = System.Windows.Threading.DispatcherPriority.Background;
        }

        public ActionItem()
        {
            this.IsActive = true;
            Guid = Guid.NewGuid();
            var thread = new Thread(() =>
            {
                this.Dispatcher = Dispatcher.CurrentDispatcher;
                Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
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
                    return this.Action.CheckState();
            }), Defaults.DispatcherPriority, null);
            return result.ToString();
        }

        public void CheckStateAsync(Action<string> callback)
        {
            var result = this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lock (_locker)
                    callback(this.Action.CheckState());
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
                    state = this.Action.Do(this.Action.CheckState());
                callback(state);
            }), Defaults.DispatcherPriority, null);
        }

        public string Execute()
        {
            var result = this.Dispatcher.Invoke(new Func<string>(() =>
            {
                lock (_locker)
                    return this.Action.Do(this.Action.CheckState());
            }), Defaults.DispatcherPriority, null);
            return result.ToString();
        }

        public void ExecuteWithoutRetval()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Action.Do(this.Action.CheckState());
            }), Defaults.DispatcherPriority, null);
        }

        public ActionItem Clone()
        {
            var item = new ActionItem()
            {
                Action = this.Action,
                Category = this.Category,
                Checker = this.Checker,
                Guid = this.Guid,
                IsActive = this.IsActive,
                Name = this.Name,
                ServerCommand = this.ServerCommand,
                UseServerThreading = this.UseServerThreading,
                IsOnlyOnce = this.IsOnlyOnce
            };

            return item;
        }

        private Dispatcher Dispatcher;
    }
}
