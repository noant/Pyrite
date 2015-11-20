using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    public class NeverChecker : ICustomChecker
    {
        public bool IsCanDoNow()
        {
            return false;
        }

        public string Name
        {
            get { return "Никогда"; }
        }

        public bool InitializeNew()
        {
            return true;
        }

        public void SetFromString(string settings)
        {
            return;
        }

        public string SetToString()
        {
            return "";
        }
    }
}
