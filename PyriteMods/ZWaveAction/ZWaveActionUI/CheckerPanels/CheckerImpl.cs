using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ZWaveAction.ZWGlobal.Simplified;

namespace ZWaveActionUI.CheckerPanels
{
    public class CheckerImpl
    {
        private CheckerMode _checkerMode;
        public CheckerMode CheckerMode
        {
            get
            {
                return _checkerMode;
            }
            set
            {
                _checkerMode = value;
                if (ModeChanged != null)
                    ModeChanged();
            }
        }

        private object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (ValueChanged != null)
                    ValueChanged();
            }
        }

        public event Action ModeChanged;
        public event Action ValueChanged;
    }
}
