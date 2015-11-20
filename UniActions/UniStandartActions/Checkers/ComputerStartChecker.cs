using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    public class ComputerStartChecker : ICustomChecker
    {
        private bool _started;
        public bool IsCanDoNow()
        {
            if (!_started)
            {
                _started = true;
                return true;
            }
            else return false;
        }

        public string Name
        {
            get { return "При включении компьютера";  }
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
