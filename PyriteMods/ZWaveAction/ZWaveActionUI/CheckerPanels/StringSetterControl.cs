using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZWaveActionUI.CheckerPanels
{
    public partial class StringSetterControl : UserControl, ISetterControl
    {
        private SetterImpl _setterImpl;
        public SetterImpl Setter
        {
            get
            {
                return _setterImpl;
            }
        }
        public StringSetterControl()
        {
            InitializeComponent();
            tbValue.TextChanged += (o, e) => _setterImpl.Value = tbValue.Text;
            _setterImpl = new SetterImpl();
            _setterImpl.ValueChanged += () => tbValue.Text = _setterImpl.Value != null ? _setterImpl.Value.ToString() : "";
            _setterImpl.Value = "";
        }
    }
}
