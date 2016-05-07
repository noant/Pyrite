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
            _setterImpl.ModeChanged += () =>
            {
                if (_setterImpl.Mode == AppendType.Decrement)
                    cbMode.SelectedIndex = 2;
                else if (_setterImpl.Mode == AppendType.Equalize)
                    cbMode.SelectedIndex = 0;
                else if (_setterImpl.Mode == AppendType.Increment)
                    cbMode.SelectedIndex = 1;
                else throw new Exception("unknown mode");
            };
            cbMode.SelectedIndexChanged += (o, e) =>
            {
                if (cbMode.SelectedIndex == 0)
                    _setterImpl.Mode = AppendType.Equalize;
                else if (cbMode.SelectedIndex == 1)
                    _setterImpl.Mode = AppendType.Increment;
                else if (cbMode.SelectedIndex == 2)
                    _setterImpl.Mode = AppendType.Decrement;
                else throw new Exception("unknown mode");
            };
            cbMode.SelectedIndex = 0;

            if (0 >= min && 0 <= max)
                nudValue.Value = 0;
            else nudValue.Value = min;
        }
    }
}
