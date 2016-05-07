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
    public partial class ButtonSetterControl : UserControl, ISetterControl
    {
        public SetterImpl Setter
        {
            get
            {
                return new SetterImpl();
            }
        }
        public ButtonSetterControl()
        {
            InitializeComponent();
        }
    }
}
