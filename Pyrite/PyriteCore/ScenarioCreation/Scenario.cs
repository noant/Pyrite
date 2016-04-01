using Logging;
using System;
using System.Threading;
using System.Windows.Threading;
using System.Xml.Serialization;
using PyriteClientIntefaces;

namespace PyriteCore.ScenarioCreation
{
    [Serializable]
    public class Scenario : IDisposable, IHasCheckerAction
    {
        private static class Defaults
        {
            public static readonly DispatcherPriority DispatcherPriority = System.Windows.Threading.DispatcherPriority.Background;
        }

        public Scenario()
        {
            this.IsActive = false;
            this.UseServerThreading = true;
            this.UseOnOffState = true;
            Guid = Guid.NewGuid();
            StartDispatcher();
        }

        public void StartDispatcher()
        {
            this.Dispatcher = null;
            _thread = new Thread(() =>
            {
                this.Dispatcher = Dispatcher.CurrentDispatcher;
                Dispatcher.Run();
            });
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.IsBackground = true;
            _thread.Start();
            while (this.Dispatcher == null) //waitinig while dispatcher starts
                Thread.Sleep(1);
        }

        internal void PrepareToRemove()
        {
            ClearDispatcher();
        }

        public void ClearDispatcher()
        {
            KillDispatcher();
            StartDispatcher();
        }

        public void KillDispatcher()
        {
            this.IsBusyNow = false;
            if (_thread != null)
            {
                _thread.Abort();
                _thread = null;
            }
        }

        public void Refresh()
        {
            bool wasBusy = this.IsBusyNow;
            ClearDispatcher();
            if (wasBusy)
                ExecuteAsync(null);
        }

        private Thread _thread;

        private ActionBag _actionBag;
        public ActionBag ActionBag
        {
            get
            {
                return _actionBag;
            }
            set
            {
                _actionBag = value;
            }
        }

        [XmlIgnore]
        public ICustomAction Action
        {
            get
            {
                return ActionBag.Action;
            }
        }

        public bool UseServerThreading { get; set; }
        public string ServerCommand { get; set; }
        public string Name { get; set; }
        public bool UseOnOffState { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public int Index { get; set; }

        private string OnState_DCAction
        {
            get
            {
                if (UseOnOffState)
                    return "Завершить: " + this.Name;
                else return this.Name;
            }
        }

        private string OffState_DCAction
        {
            get
            {
                if (UseOnOffState)
                    return "Начать: " + this.Name;
                else return this.Name;
            }
        }

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

        private void RaiseAfterEvent(bool withoutServerEvent)
        {
            if (!withoutServerEvent)
                RaiseAfterActionServerAsync();
            RaiseAfterAction();
        }

        internal Guid Guid { get; set; }

        private object _locker = new object();

        public string CheckState()
        {
            if (this.Action is DoubleComplexAction)
            {
                return Action.State == DoubleComplexAction.BeginState ?
                        OffState_DCAction :
                        OnState_DCAction;
            }
            else
            {
                var result = this.Dispatcher.Invoke(new Func<string>(() =>
                {
                    lock (_locker)
                    {
                        return Action.State;
                    }
                }), Defaults.DispatcherPriority, null);
                return result.ToString();
            }
        }

        public void CheckStateAsync(Action<string> callback)
        {
            if (this.Action is DoubleComplexAction)
            {
                callback(Action.State == DoubleComplexAction.BeginState ?
                        OffState_DCAction :
                        OnState_DCAction);
            }
            else
            {
                var result = this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    lock (_locker)
                    {
                        callback(Action.State);
                    }
                }), Defaults.DispatcherPriority);
            }
        }

        private string ExecuteWithoutDispatcherInvoking(string inputState, bool withoutServerEvent)
        {
            try
            {
                if (this.Action is DoubleComplexAction)
                {
                    var state = DoubleComplexAction.BeginState;
                    if (((DoubleComplexAction)Action).CurrentState == DoubleComplexAction.CurrentDCActionState.Ended)
                        state = DoubleComplexAction.EndState;

                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        lock (_locker)
                        {
                            this.IsBusyNow = true;
                            this.Action.Do(state);
                            this.IsBusyNow = false;
                        }
                        RaiseAfterEvent(withoutServerEvent);
                    }));

                    return ((DoubleComplexAction)Action).CurrentState == DoubleComplexAction.CurrentDCActionState.Began ?
                        OffState_DCAction :
                        OnState_DCAction;
                }
                else
                    lock (_locker)
                    {
                        this.IsBusyNow = true;
                        var result = this.Action.Do(inputState);
                        this.IsBusyNow = false;
                        return result;
                    }
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
            return string.Empty;
        }

        public string Execute(string inputState, bool withoutServerEvent)
        {
            try
            {
                if (this.Action is DoubleComplexAction)
                    ClearDispatcher();
                var result = this.Dispatcher.Invoke(new Func<string>(() =>
                {
                    return ExecuteWithoutDispatcherInvoking(inputState, withoutServerEvent);
                }), Defaults.DispatcherPriority, null);
                return result.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                RaiseAfterEvent(withoutServerEvent);
            }
        }

        public void ExecuteAsync(Action<string> callback)
        {
            if (this.Action is DoubleComplexAction)
                ClearDispatcher();
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                var res = ExecuteWithoutDispatcherInvoking(CheckState(), false);
                if (callback != null)
                    callback(res);
                RaiseAfterEvent(false);
            }), Defaults.DispatcherPriority);
            if (this.Action is DoubleComplexAction)
                RaiseAfterEvent(false);
        }

        public bool IsBusyNow
        {
            get; private set;
        }

        public Scenario Clone()
        {
            var item = new Scenario()
            {
                ActionBag = new ActionBag() { Action = ModulesControl.Clone(this.Action) },
                Category = this.Category,
                Guid = this.Guid,
                IsActive = this.IsActive,
                UseOnOffState = this.UseOnOffState,
                Index = this.Index,
                Name = this.Name,
                ServerCommand = this.ServerCommand,
                UseServerThreading = this.UseServerThreading
            };

            return item;
        }

        public void Dispose()
        {
            KillDispatcher();
        }

        public bool RemoveChecker(Type checkerType)
        {
            if (this.Action is IHasCheckerAction)
                if (((IHasCheckerAction)this.Action).RemoveChecker(checkerType))
                {
                    return true;
                }
            return false;
        }

        public bool RemoveAction(Type actionType)
        {
            if (this.Action is IHasCheckerAction)
                if (((IHasCheckerAction)this.Action).RemoveAction(actionType))
                {
                    return true;
                }
            return false;
        }

        private Dispatcher Dispatcher;
    }
}
