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
    public partial class ListSetterControl : UserControl, ISetterControl
    {
        private SetterImpl _setterImpl;
        public SetterImpl Setter
        {
            get
            {
                return _setterImpl;
            }
        }
        public ListSetterControl(string[] values)
        {
            InitializeComponent();
            _setterImpl = new SetterImpl();
            cbValue.SelectedIndexChanged += (o, e) =>
            {
                _setterImpl.Value = cbValue.SelectedItem.ToString();
            };
            foreach (var value in values)
            {
                cbValue.Items.Add(value);
            }
            if (cbValue.Items.Count > 0)
                cbValue.SelectedIndex = 0;
            _setterImpl = new SetterImpl();
            _setterImpl.ValueChanged += () =>
            {
                if (_setterImpl.Value != null)
                    cbValue.SelectedItem = _setterImpl.Value.ToString();
            };
            _setterImpl.Value = values.FirstOrDefault();
        }
    }
}
