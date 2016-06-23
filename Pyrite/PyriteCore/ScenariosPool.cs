using System;
using PyriteCore.ScenarioCreation;
using System.Collections.Generic;
using System.Linq;

namespace PyriteCore
{
    public class ScenariosPool
    {
        public Pyrite Pyrite { get; internal set; }

        private List<Scenario> _scenarios;
        public IEnumerable<Scenario> Scenarios
        {
            get
            {
                return _scenarios.ToArray();
            }
        }

        public void RemoveScenario(Scenario item)
        {
            item.PrepareToRemove();
            _scenarios.Remove(item);
        }

        public Result<bool> CheckScenario(Scenario item)
        {
            var result = new Result<bool>();

            if (item.ActionBag == null)
                result.AddWarning(new Warning("Необходимо выбрать вид действия"));
            if (string.IsNullOrEmpty(item.Name))
                result.AddWarning(new Warning("Необходимо ввести имя сценария"));
            if (_scenarios.Count(x => x.Name == item.Name && item.Guid != x.Guid) > 0)
                result.AddWarning(new Warning("Действие с таким именем уже существует"));
            if (_scenarios.Count(x => x.ServerCommand == item.ServerCommand && !string.IsNullOrEmpty(x.ServerCommand) && item.Guid != x.Guid) > 0)
                result.AddWarning(new Warning("Действие с такой командой сервера уже существует"));

            result.Value = result.Warnings.Count() == 0;
            return result;
        }

        public void RefreshScenarios()
        {
            foreach (var scenario in this.Scenarios.OrderBy(x => x.Guid))
            {
                scenario.ForAllActionAndChecker(x => ((dynamic)x).Refresh());
            }
        }

        public Result<bool> Add(Scenario item)
        {
            Resulting.EnableExceptionHandling = false;
            var result = CheckScenario(item);
            Resulting.EnableExceptionHandling = true;
            if (result.Value && !_scenarios.Contains(item))
                _scenarios.Add(item);

            item.AfterActionServerEvent += (x) =>
            {
                Pyrite.ServerThreading.UpdateClients();
            };

            return result;
        }

        public void StartActiveScenarios()
        {
            foreach (var scenario in _scenarios.Where(x => x.IsActive))
                scenario.ExecuteAsync(null);
        }

        public IEnumerable<string> GetCategories()
        {
            return Scenarios.Where(x => x.Category != "").Select(x => x.Category);
        }

        internal void Initialize()
        {
            _scenarios = new List<Scenario>();
        }

        internal void Clear()
        {
            _scenarios.ForEach(x => x.Dispose());
            _scenarios.Clear();
        }
    }
}
