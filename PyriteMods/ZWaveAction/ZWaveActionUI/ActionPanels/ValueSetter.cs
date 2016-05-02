using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenZWaveDotNet;

namespace ZWaveActionUI.ActionPanels
{
    public partial class ValueSetter : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SetterImpl Setter { get; private set; }

        public ValueSetter()
        {
            InitializeComponent();
        }

        private ZWValueID _valueID;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ZWValueID ValueID
        {
            get
            {
                return _valueID;
            }
            set
            {
                _valueID = value;
                Refresh();
            }
        }
        public override void Refresh()
        {
            base.Refresh();

            var appendSetter = new Action<ISetterControl>(delegate (ISetterControl setterControl)
            {
                Setter = setterControl.Setter;
                panel.Controls.Clear();
                panel.Controls.Add((Control)setterControl);
            });

            switch (_valueID.GetType())
            {
                case ZWValueID.ValueType.Bool:
                    {
                        appendSetter(new BoolSetterControl());
                        break;
                    }

                default:
                    throw new Exception("type not known");
            }
        }
    }
}
