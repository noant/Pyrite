﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniActionsClientIntefaces;

namespace UniStandartActions.Checkers
{
    public class EveryIterationChecker : ICustomChecker
    {
        public bool IsCanDoNow()
        {
            return true;
        }

        public string Name
        {
            get { return "Всегда"; }
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
            return string.Empty;
        }
    }
}
