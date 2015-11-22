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
    public static class Pool
    {
        public static class Settings {
            public static class Default
            {
                public static readonly int SecondsBetweenActions = 30;
                public static readonly int MaxSecondsBetweenActions = 59;
            }
            private static int _secondsBetweenActions;
            public static int SecondsBetweenActions 
            {
                get {
                    return _secondsBetweenActions;
                }
                set {
                    if (value > Default.MaxSecondsBetweenActions)
                        throw new Exception("Max value is " + Default.MaxSecondsBetweenActions + " seconds");
                    else _secondsBetweenActions = value;
                }
            }
        }

        private static List<ActionItem> _actionItems;
        public static IEnumerable<ActionItem> ActionItems { 
            get {
                return _actionItems.ToArray();
            } 
        }

        public static void RemoveItem(ActionItem item)
        {
            _actionItems.Remove(item);
        }

        public static void RemoveAll(Func<ActionItem, bool> func)
        {
            _actionItems.RemoveAll(x => func(x));
        }

        public static Result<bool> CheckItem(ActionItem item)
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

        public static Result<bool> AddItem(ActionItem item)
        {
            var result = CheckItem(item);
            if (result.Value && !_actionItems.Contains(item))
                _actionItems.Add(item);
            return result;
        }

        public static IEnumerable<string> GetCategories()
        {
            return ActionItems.Where(x => x.Category != "").Select(x => x.Category);
        }

        internal static void Initialize()
        {
            _actionItems = new List<ActionItem>();
        }

        internal static void Clear() {
            _actionItems.Clear();
        }

        private static Thread _thread;
        public static VoidResult BeginStart()
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
                                if (action.Checker.IsCanDoNow())
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
                                                    Pool.RemoveItem(action);
                                                    SAL.Save();
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
                        Thread.Sleep(TimeSpan.FromSeconds(Settings.SecondsBetweenActions));
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
        
        public static event Action WhenStopped;

        private static volatile bool _isInActionNow;
        private static bool _prepareToStop;
        private static Action _whenStoppedCallback;
        public static void BeginStop(Action callback)
        {
            _prepareToStop = true;
            _whenStoppedCallback = callback;
            if (!_isInActionNow)
            {
                _thread.Abort();
                IsStopped = true;
            }
        }
        private static bool _isStopped;
        public static bool IsStopped
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
