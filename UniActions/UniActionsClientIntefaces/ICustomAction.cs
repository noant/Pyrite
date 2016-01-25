using HierarchicalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniActionsClientIntefaces
{
    public interface ICustomAction
    {
        string Do(string inputState);
        string State { get; }
        string Name { get; }

        bool AllowUserSettings { get; }

        bool BeginUserSettings();

        bool IsBusyNow { get; }

        void Refresh();
    }
}
