using PyriteClientIntefaces;
using PyriteCore.ScenarioCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace PyriteCore.CoreStandartActions
{
    public class RunExistingScenarioAction : ICustomAction, ICoreElement, IHasCheckerAction
    {
        [XmlIgnore]
        public Pyrite CurrentPyrite { get; set; }

        public Guid ScenarioGuid { get; set; }

        [HumanFriendlyName("Сценарий")]
        [XmlIgnore]
        public string ScenarioName
        {
            get
            {
                if (_scenario == null)
                    UpdateScenarioClone();
                if (_scenario != null)
                {
                    return _scenario.Name;
                }
                return "[отсутствует]";
            }
        }

        private Scenario GetTargetScenario()
        {
            if (CurrentPyrite == null)
                return null;
            var scenario = CurrentPyrite
                .ScenariosPool
                .Scenarios
                .FirstOrDefault(x => x.Guid.Equals(ScenarioGuid));
            return scenario;
        }

        private Scenario _scenario;

        [XmlIgnore]
        public bool AllowUserSettings
        {
            get
            {
                return true;
            }
        }

        [XmlIgnore]
        public bool IsBusyNow
        {
            get; set;
        }

        [XmlIgnore]
        public string Name
        {
            get
            {
                return "Существующий сценарий";
            }
        }

        [XmlIgnore]
        public string State
        {
            get
            {
                if (_scenario == null)
                    UpdateScenarioClone();
                if (_scenario != null)
                {
                    return _scenario.CheckState();
                }
                return string.Empty;
            }
        }

        public bool BeginUserSettings()
        {
            var guid = ScenarioGuid;
            var allScenarios = new Dictionary<Guid, string>();
            foreach (var scenario in CurrentPyrite.ScenariosPool.Scenarios)
                allScenarios.Add(scenario.Guid, scenario.Name);

            var success = PyriteStandartActions
                .Implementations
                .BeginUserSettings_RunExistingScenario(allScenarios, ref guid);

            if (success)
            {
                ScenarioGuid = guid;
                return true;
            }
            return false;
        }

        public string Do(string inputState)
        {
            IsBusyNow = true;
            string state = "";
            if (_scenario == null)
                UpdateScenarioClone();
            if (_scenario != null)
            {
                state = _scenario.ExecuteFlat(_scenario.CheckStateFlat());
                Thread.Sleep(1);
            }
            IsBusyNow = false;
            return state;
        }

        public void UpdateScenarioClone()
        {
            _scenario = GetTargetScenario();
            if (_scenario != null)
                _scenario = _scenario.Clone().Value;
        }

        public void Refresh()
        {
            _scenario = null;
        }

        public bool RemoveChecker(Type checkerType)
        {
            if (_scenario != null && _scenario is IHasCheckerAction)
            {
                return ((IHasCheckerAction)_scenario).RemoveChecker(checkerType);
            }
            return false;
        }

        public bool RemoveAction(Type actionType)
        {
            if (_scenario != null && _scenario is IHasCheckerAction)
            {
                return ((IHasCheckerAction)_scenario).RemoveAction(actionType);
            }
            return false;
        }

        public void ForAllActionAndChecker(Action<object> action)
        {
            if (_scenario != null && _scenario is IHasCheckerAction)
            {
                ((IHasCheckerAction)_scenario).ForAllActionAndChecker(action);
            }
            action(this);
        }
    }
}
