using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniActionsClientIntefaces
{
    public interface ICustomChecker
    {
        bool IsCanDoNow();

        string Name { get; }

        bool InitializeNew();

        void SetFromString(string settings);

        string SetToString();
    }
}
