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
        string CheckState();
        string Name { get; }

        bool InitializeNew();

        void SetFromString(string settings);

        string SetToString();
    }
}
