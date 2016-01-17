using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    public class EveryIterationChecker : ICustomChecker
    {
        public bool IsCanDoNow
        {
            get{
                return true;
            }
        }

        public string Name
        {
            get { return "Всегда"; }
        }

        public bool BeginUserSettings()
        {
            return true;
        }

        public bool AllowUserSettings { get { return false; } }

        public void Refresh() { }
    }
}
