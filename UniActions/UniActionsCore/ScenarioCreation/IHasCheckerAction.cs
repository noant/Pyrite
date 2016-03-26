using System;

namespace UniActionsCore.ScenarioCreation
{
    public interface IHasCheckerAction
    {
        bool RemoveChecker(Type checkerType);
        bool RemoveAction(Type actionType);
    }
}
