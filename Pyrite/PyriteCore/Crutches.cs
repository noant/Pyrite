using HierarchicalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PyriteCore.ScenarioCreation;
using PyriteCore.CoreStandartActions;

namespace PyriteCore
{
    public static class Crutches
    {
        public static void Execute()
        {
            HierarchicalObjectCrutch.Register(typeof(ComplexAction));
            HierarchicalObjectCrutch.Register(typeof(IfAction));
            HierarchicalObjectCrutch.Register(typeof(WhileAction));
            HierarchicalObjectCrutch.Register(typeof(ComplexChecker));
            HierarchicalObjectCrutch.Register(typeof(ActionBag));
            HierarchicalObjectCrutch.Register(typeof(DoNothingAction));
            HierarchicalObjectCrutch.Register(typeof(NeverChecker));
            HierarchicalObjectCrutch.Register(typeof(DoubleComplexAction));
            HierarchicalObjectCrutch.Register(typeof(RunExistingScenarioAction));
        }
    }
}
