using System;

namespace PyriteCore.ScenarioCreation
{
    public interface IHasCheckerAction
    {
        bool RemoveChecker(Type checkerType);
        bool RemoveAction(Type actionType);

        void ForAllActionAndChecker(Action<object> action);
    }
}
