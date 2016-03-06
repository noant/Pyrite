using System;

namespace UniActionsCore.ScenarioCreation
{
    public interface IHasCheckerAction
    {
        void RemoveChecker(Type checkerType);
        void RemoveAction(Type actionType);
    }
}
