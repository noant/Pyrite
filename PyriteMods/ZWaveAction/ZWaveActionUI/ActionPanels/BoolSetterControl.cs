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
    public partial class BoolSetterControl : UserControl, ISetterControl
    {
        private SetterImpl _setterImpl;
        public SetterImpl Setter
        {
            get
            {
                return _setterImpl;
            }
        }
        public BoolSetterControl()
        {
            InitializeComponent();
            _setterImpl = new SetterImpl();
            _setterImpl.ValueChanged += () => cbValue.SelectedIndex = (bool)_setterImpl.Value ? 0 : 1;
            _setterImpl.Value = true;
        }
    }
}
