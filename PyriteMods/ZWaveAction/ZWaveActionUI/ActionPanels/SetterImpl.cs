using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ZWaveAction.ZWGlobal.Simplified;

namespace ZWaveActionUI.ActionPanels
{
    public partial class SetterImpl
    {
        public SetterImpl()
        {
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

        public bool InvertOnRepeat { get; set; }

        private AppendType _mode;
        public AppendType Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                if (ModeChanged != null)
                    ModeChanged();
            }
        }

        public event Action ModeChanged;
        public event Action ValueChanged;
    }
}
