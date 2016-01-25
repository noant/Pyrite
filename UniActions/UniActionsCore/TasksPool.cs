using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UniStandartActions;

namespace UniActionsCore
{
    public class TasksPool
    {
        public class TasksPoolSettings {
            public static class Defaults
            {
                public static readonly int SecondsBetweenActions = 30;
                public static readonly int MaxSecondsBetweenActions = 59;
            }
            private int _secondsBetweenActions;
            public int SecondsBetweenActions 
            {
                get {
                    return _secondsBetweenActions;
                }
                set {
                    if (value > Defaults.MaxSecondsBetweenActions)
                        throw new Exception("Max value is " + Defaults.MaxSecondsBetweenActions + " seconds");
                    else _secondsBetweenActions = value;
                }
            }
        }

        public TasksPoolSettings Settings { get; private set; }

        public Uni Uni { get; internal set; }

        private List<ActionItem> _actionItems;
        public IEnumerable<ActionItem> ActionItems { 
            get {
                return _actionItems.ToArray();
            } 
        }

        public void RemoveItem(ActionItem item)
        {
            _actionItems.Remove(item);
        }

        public void RemoveAll(Func<ActionItem, bool> func)
        {
            _actionItems.RemoveAll(x => func(x));
        }

        public Result<bool> CheckItem(ActionItem item)
        {
            var result = new Result<bool>();

            if (item.Action == null)
                result.AddException(new Exception("Необходимо выбрать вид действия"));
            if (item.Checker == null)
                result.AddException(new Exception("Необходимо выбрать вид проверки"));
            if (string.IsNullOrEmpty(item.Name))
                result.AddException(new Exception("Необходимо ввести имя сценария"));
            if (_actionItems.Count(x => x.Name == item.Name && item.Guid != x.Guid) > 0)
                result.AddException(new Exception("Действие с таким именем уже существует"));
            if (_actionItems.Count(x => x.ServerCommand == item.ServerCommand && !string.IsNullOrEmpty(x.ServerCommand) && item.Guid != x.Guid) > 0)
                result.AddException(new Exception("Действие с такой командой сервера уже существует"));

            result.Value = result.Exceptions.Count() == 0;
            return result;
        }

        public Result<bool> AddItem(ActionItem item)
        {
            UniActionsCore.Resulting.EnableExceptionHandling = false;
            var result = CheckItem(item);
            UniActionsCore.Resulting.EnableExceptionHandling = true;
            if (result.Value && !_actionItems.Contains(item))
                _actionItems.Add(item);

            item.AfterActionSlow += (x) => 
                this.Uni.ServerThreading.ShareState(x, false);
            item.BeforeActionSlow += (x) => 
                this.Uni.ServerThreading.ShareState(x, true);
            return result;
        }

        public IEnumerable<string> GetCategories()
        {
            return ActionItems.Where(x => x.Category != "").Select(x => x.Category);
        }

        internal void Initialize()
        {
            Settings = new TasksPoolSettings();
            _actionItems = new List<ActionItem>();
        }

        internal void Clear() {
            _actionItems.ForEach(x => x.Dispose());
            _actionItems.Clear();
        }

        private Thread _thread;
        public VoidResult BeginStart()
        {
            _prepareToStop = false;
            IsStopped = false;
            var executiongNowCount = 0;
            var result = new VoidResult();
            try
            {
                _thread = Helper.AlterThread(() =>
                {
                    while (!_prepareToStop)
                    {
                        lock (ActionItems)
                        {
                            foreach (var action in ActionItems.Where(x => x.IsActive))
                            {
                                if (_prepareToStop)
                                    break;
                                if (action.Checker.IsCanDoNow)
                                {
                                    Helper.AlterHardThread(() =>
                                    {
                                        _isInActionNow = true;
                                        executiongNowCount++;
                                        try
                                        {
                                            action.Execute();
                                            if (action.IsOnlyOnce)
                                            {
                                                lock (ActionItems)
                                                {
                                                    this.RemoveItem(action);
                                                    Uni.SaveAndLoad.Save();
                                                }
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            Log.Write(e);
                                        }
                                        executiongNowCount--;
                                        if (executiongNowCount==0 )
                                        {
                                            _isInActionNow = false;
                                            if (_prepareToStop)
                                                IsStopped = true;
                                        }
                                    });
                                }
                            }
                        }
                        Thread.Sleep(TimeSpan.FromSeconds(this.Settings.SecondsBetweenActions));
                    }
                    //end
                    IsStopped = true;
                }, 
                true, 
                ApartmentState.MTA);
            }
            catch (Exception e)
            {
                result.AddException(e);
                Log.Write(e);
            }
            return result;
        }
        
        public event Action WhenStopped;

        private volatile bool _isInActionNow;
        private bool _prepareToStop;
        private Action _whenStoppedCallback;
        public void BeginStop(Action callback)
        {
            _prepareToStop = true;
            _whenStoppedCallback = callback;
            if (!_isInActionNow)
            {
                _thread.Abort();
                IsStopped = true;
            }
        }
        private bool _isStopped;
        public bool IsStopped
        {
            get
            {
                return _isStopped;
            }
            set
            {
                _isStopped = value;
                if (value && _whenStoppedCallback != null)
                {
                    _whenStoppedCallback();
                    _whenStoppedCallback = null;
                }
                if (value && WhenStopped != null)
                    WhenStopped();
            }
        }
    }
}
