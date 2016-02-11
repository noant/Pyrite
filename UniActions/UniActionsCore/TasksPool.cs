using System;
using System.Collections.Generic;
using System.Linq;

namespace UniActionsCore
{
    public class TasksPool
    {
        public Uni Uni { get; internal set; }

        private List<Scenario> _actionItems;
        public IEnumerable<Scenario> ActionItems
        {
            get
            {
                return _actionItems.ToArray();
            }
        }

        public void RemoveItem(Scenario item)
        {
            _actionItems.Remove(item);
        }

        public void RemoveAll(Func<Scenario, bool> func)
        {
            _actionItems.RemoveAll(x => func(x));
        }

        public Result<bool> CheckItem(Scenario item)
        {
            var result = new Result<bool>();

            if (item.Action == null)
                result.AddWarning(new Warning("Необходимо выбрать вид действия"));
            if (string.IsNullOrEmpty(item.Name))
                result.AddWarning(new Warning("Необходимо ввести имя сценария"));
            if (_actionItems.Count(x => x.Name == item.Name && item.Guid != x.Guid) > 0)
                result.AddWarning(new Warning("Действие с таким именем уже существует"));
            if (_actionItems.Count(x => x.ServerCommand == item.ServerCommand && !string.IsNullOrEmpty(x.ServerCommand) && item.Guid != x.Guid) > 0)
                result.AddWarning(new Warning("Действие с такой командой сервера уже существует"));

            result.Value = result.Warnings.Count() == 0;
            return result;
        }

        public Result<bool> AddItem(Scenario item)
        {
            UniActionsCore.Resulting.EnableExceptionHandling = false;
            var result = CheckItem(item);
            UniActionsCore.Resulting.EnableExceptionHandling = true;
            if (result.Value && !_actionItems.Contains(item))
                _actionItems.Add(item);

            item.AfterActionServerEvent += (x) =>
            {
                if (item.UseServerThreading && !string.IsNullOrEmpty(item.ServerCommand))
                    Uni.ServerThreading.ShareState(null, null);
            };
            return result;
        }

        public IEnumerable<string> GetCategories()
        {
            return ActionItems.Where(x => x.Category != "").Select(x => x.Category);
        }

        internal void Initialize()
        {
            _actionItems = new List<Scenario>();
        }

        internal void Clear()
        {
            _actionItems.ForEach(x => x.Dispose());
            _actionItems.Clear();
        }


    }
}
