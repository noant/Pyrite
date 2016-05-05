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
using ZWaveAction;
using static ZWaveAction.ZWGlobal.Simplified;

namespace ZWaveActionUI.ActionPanels
{
    public partial class ValueSetter : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SetterImpl Setter { get; private set; }

        public ValueSetter()
        {
            InitializeComponent();
            Setter = new SetterImpl();
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

            var zwave = ZWGlobal.GetZWaveByValueID(ValueID);

            switch (_valueID.GetType())
            {
                case ZWValueID.ValueType.Bool:
                    {
                        appendSetter(new BoolSetterControl());
                        Setter.Mode = AppendType.Equalize;
                        break;
                    }
                case ZWValueID.ValueType.Button:
                    {
                        appendSetter(new ButtonSetterControl());
                        Setter.Mode = AppendType.Equalize;
                        break;
                    }
                case ZWValueID.ValueType.Byte:
                    {
                        appendSetter(new NumericSetterControl(0, 255, false));
                        break;
                    }
                case ZWValueID.ValueType.Decimal:
                    {
                        appendSetter(new NumericSetterControl(decimal.MinValue, decimal.MaxValue, true));
                        break;
                    }
                case ZWValueID.ValueType.Int:
                    {
                        appendSetter(new NumericSetterControl(int.MinValue, int.MaxValue, false));
                        break;
                    }
                case ZWValueID.ValueType.List:
                    {
                        string[] vals;
                        zwave.Manager.GetValueListItems(ValueID, out vals);
                        appendSetter(new ListSetterControl(vals));
                        Setter.Mode = AppendType.Equalize;
                        break;
                    }
                case ZWValueID.ValueType.Short:
                    {
                        appendSetter(new NumericSetterControl(short.MinValue, short.MaxValue, false));
                        break;
                    }
                case ZWValueID.ValueType.String:
                    {
                        appendSetter(new StringSetterControl());
                        Setter.Mode = AppendType.Equalize;
                        break;
                    }
                default:
                    throw new Exception("type not known");
            }
        }
    }
}
