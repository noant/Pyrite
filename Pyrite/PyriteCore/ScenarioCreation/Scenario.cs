using Logging;
using PyriteClientIntefaces;
using PyriteCore.CoreStandartActions;
using System;
using System.Threading;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace PyriteCore.ScenarioCreation
{
    [Serializable]
    public class Scenario : IDisposable, IHasCheckerAction, ICoreElement
    {
        private static class Defaults
        {
            public static readonly DispatcherPriority DispatcherPriority = System.Windows.Threading.DispatcherPriority.Background;
        }

        public Pyrite CurrentPyrite { get; set; }

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
                    return "Выключить: " + this.Name.ToLower();
                else return this.Name;
            }
        }

        private string OffState_DCAction
        {
            get
            {
                if (UseOnOffState)
                    return this.Name.Length > 0
                        ? this.Name.Substring(0, 1).ToUpper() + this.Name.Substring(1).ToLower()
                        : string.Empty;
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

        public Guid Guid { get; set; }

        private object _locker = new object();

        public string CheckStateFlat()
        {
            if (this.Action is DoubleComplexAction)
            {
                return Action.State == DoubleComplexAction.BeginState ?
                        OffState_DCAction :
                        OnState_DCAction;
            }
            else
            {
                lock (_locker)
                {
                    return Action.State;
                }
            }
        }

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

        public string ExecuteFlat(string inputState)
        {
            try
            {
                if (this.Action is DoubleComplexAction)
                {
                    var state = DoubleComplexAction.BeginState;
                    if (((DoubleComplexAction)Action).CurrentState == DoubleComplexAction.CurrentDCActionState.Ended)
                        state = DoubleComplexAction.EndState;

                    lock (_locker)
                    {
                        this.IsBusyNow = true;
                        state = this.Action.Do(state);
                        this.IsBusyNow = false;
                    }

                    return state;
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
                if (e is ThreadAbortException)
                {
                    //do nothing
                }
                else
                    Log.Write(e);
            }
            return string.Empty;
        }

        private string ExecuteBase(string inputState, bool withoutServerEvent)
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
                        object _lockerToRaiseEvent = new object(); //crutch
                        lock (_locker)
                        {
                            this.IsBusyNow = true;
                            ThreadHelper.AlterThread(
                                    () =>
                                    {
                                        Thread.Sleep(1); //crutch
                                        lock (_lockerToRaiseEvent)
                                            RaiseAfterEvent(withoutServerEvent);
                                    },
                                    true,
                                    ApartmentState.Unknown
                                );
                            this.Action.Do(state);
                            this.IsBusyNow = false;
                            lock (_lockerToRaiseEvent)
                                RaiseAfterEvent(withoutServerEvent);
                        }
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
                        RaiseAfterEvent(withoutServerEvent);
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
                    return ExecuteBase(inputState, withoutServerEvent);
                }), Defaults.DispatcherPriority, null);
                return result.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void ExecuteAsync(Action<Scenario> callback)
        {
            if (this.Action is DoubleComplexAction)
                ClearDispatcher();
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                var res = ExecuteBase(CheckState(), false);
                if (callback != null)
                    callback(this);
            }), Defaults.DispatcherPriority);
        }

        public bool IsBusyNow
        {
            get; private set;
        }

        public Result<Scenario> Clone()
        {
            var res = new Result<Scenario>();
            var clone = ModulesControl.CloneAction(this.Action).Value;

            var item = new Scenario()
            {
                ActionBag = new ActionBag() { Action = clone },
                Category = this.Category,
                Guid = this.Guid,
                IsActive = this.IsActive,
                UseOnOffState = this.UseOnOffState,
                Index = this.Index,
                Name = this.Name,
                ServerCommand = this.ServerCommand,
                UseServerThreading = this.UseServerThreading,
                CurrentPyrite = this.CurrentPyrite
            };

            item.ForAllActionAndChecker(x =>
            {
                if (x is ICoreElement)
                    ((ICoreElement)x).CurrentPyrite = this.CurrentPyrite;
            });

            if (item.Action == null)
            {
                if (this.Action is DoubleComplexAction)
                    item.ActionBag.Action = new DoubleComplexAction();
                else item.ActionBag.Action = new DoNothingAction();
            }

            res.Value = item;
            return res;
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
            {
                if (((IHasCheckerAction)this.Action).RemoveAction(actionType))
                {
                    return true;
                }
            }
            else
                this.ActionBag.Action = new DoNothingAction();
            return false;
        }

        public void ForAllActionAndChecker(Action<object> action)
        {
            if (this.Action is IHasCheckerAction)
                ((IHasCheckerAction)this.Action).ForAllActionAndChecker(action);
            else
            {
                if (this.Action != null)
                    action(this.Action);
            }
        }

        private Dispatcher Dispatcher;
    }
}
