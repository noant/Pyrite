using HierarchicalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsCore.ScenarioCreation;

namespace UniActionsCore
{
    public static class Сrutches
    {
        public static void Execute()
        {
            HierarchicalObjectCrutch.Register(typeof(ComplexAction));
            HierarchicalObjectCrutch.Register(typeof(IfAction));
            HierarchicalObjectCrutch.Register(typeof(WhileAction));
            HierarchicalObjectCrutch.Register(typeof(ComplexChecker));
        }
    }
}
