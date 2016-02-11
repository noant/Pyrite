using System;

namespace UniActionsCore.ScenarioCreating
{
    public interface IHasChecker
    {
        bool HasChecker(Type checkerType);
    }
}
