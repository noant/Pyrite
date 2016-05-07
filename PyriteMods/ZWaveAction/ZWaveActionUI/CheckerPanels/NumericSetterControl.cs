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

namespace ZWaveActionUI.CheckerPanels
{
    public partial class NumericSetterControl : UserControl, ISetterControl
    {
        private SetterImpl _setterImpl;
        public SetterImpl Setter
        {
            get
            {
                return _setterImpl;
            }
        }
        public NumericSetterControl(decimal min, decimal max, bool @float)
        {
            InitializeComponent();

            this.Load += (o, e) =>
            {
                this.ParentForm.FormClosing += (o1, e1) =>
                {
                    _setterImpl.Value = nudValue.Value;
                };
            };

            nudValue.Maximum = max;
            nudValue.Minimum = min;
            nudValue.ThousandsSeparator = true;
            if (!@float)
                nudValue.DecimalPlaces = 0;

            nudValue.KeyUp += (o, e) =>
                _setterImpl.Value = nudValue.Value;

            _setterImpl = new SetterImpl();
            _setterImpl.ValueChanged += () =>
            {
                if (_setterImpl.Value != null)
                    nudValue.Value = Convert.ToDecimal(_setterImpl.Value);
            };

            if (0 >= min && 0 <= max)
                nudValue.Value = 0;
            else nudValue.Value = min;
        }
    }
}
