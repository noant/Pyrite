using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZWaveActionUI.ActionPanels
{
    public partial class SetterImpl
    {
        public SetterImpl()
        {
        }

        public event Action ValueChanged;

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
    }
}
