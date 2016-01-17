using HierarchicalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniActionsClientIntefaces
{
    public interface ICustomChecker
    {
        bool IsCanDoNow {get;}

        string Name { get; }

        bool BeginUserSettings();
        
        bool AllowUserSettings { get; }

        void Refresh();
        //HierarchicalObject Settings { get; set; }
    }
}
