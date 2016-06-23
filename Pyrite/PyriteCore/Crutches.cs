using System;
using System.Linq;
using System.Collections.Generic;
using HierarchicalData;
using PyriteCore.CoreStandartActions;
using PyriteCore.ScenarioCreation;

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
