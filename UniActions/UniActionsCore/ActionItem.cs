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
            }), null);
            return result.ToString();            
        }

        public string BeginExecute(string inputState)
        {
            var result = this.Dispatcher.Invoke(new Func<string>(() =>
            {
                lock (_locker)
                    return this.Action.Do(inputState);
            }), null);
            return result.ToString();         
        }
        public string BeginExecute()
        {
            var result = this.Dispatcher.Invoke(new Func<string>(() =>
            {
                lock (_locker)
                    return this.Action.Do(this.Action.CheckState());
            }), null);
            return result.ToString();
        }

        public void ExecuteWithoutRetval()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Action.Do(this.Action.CheckState());
            }), null);
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
