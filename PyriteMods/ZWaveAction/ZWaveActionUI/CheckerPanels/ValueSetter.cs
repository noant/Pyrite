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

namespace ZWaveActionUI.CheckerPanels
{
    public partial class ValueSetter : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SetterImpl Setter { get; private set; }

        public ValueSetter()
        {
            InitializeComponent();
            cbMode.Enabled = false;
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

        public bool LessOrMoreModeEnabled
        {
            get
            {
                return cbMode.Enabled;
            }
            set
            {
                cbMode.Enabled = value;
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            cbMode.SelectedIndex = 0;

            var appendSetter = new Action<ISetterControl>(delegate (ISetterControl setterControl)
            {
                Setter = setterControl.Setter;

                cbMode.SelectedIndexChanged += (o, e) =>
                {
                    if (cbMode.SelectedIndex == 0)
                        Setter.Mode = CheckerMode.Equals;
                    else if (cbMode.SelectedIndex == 1)
                        Setter.Mode = CheckerMode.More;
                    else if (cbMode.SelectedIndex == 2)
                        Setter.Mode = CheckerMode.MoreOrEquals;
                    else if (cbMode.SelectedIndex == 3)
                        Setter.Mode = CheckerMode.Less;
                    else if (cbMode.SelectedIndex == 4)
                        Setter.Mode = CheckerMode.LessOrEquals;
                    else throw new Exception("unknown mode");
                };

                Setter.ModeChanged += () =>
                {
                    if (Setter.Mode == CheckerMode.Equals)
                        cbMode.SelectedIndex = 0;
                    else if (Setter.Mode == CheckerMode.More)
                        cbMode.SelectedIndex = 1;
                    else if (Setter.Mode == CheckerMode.MoreOrEquals)
                        cbMode.SelectedIndex = 2;
                    else if (Setter.Mode == CheckerMode.Less)
                        cbMode.SelectedIndex = 3;
                    else if (Setter.Mode == CheckerMode.LessOrEquals)
                        cbMode.SelectedIndex = 4;
                    else throw new Exception("unknown mode");
                };

                panel.Controls.Clear();
                panel.Controls.Add((Control)setterControl);
            });

            var zwave = ZWGlobal.GetZWaveByValueID(ValueID);

            bool allowLessOrMoreMode = true;

            switch (_valueID.GetType())
            {
                case ZWValueID.ValueType.Bool:
                    {
                        appendSetter(new BoolSetterControl());
                        Setter.Mode = CheckerMode.Equals;
                        allowLessOrMoreMode = false;
                        break;
                    }
                case ZWValueID.ValueType.Button:
                    {
                        appendSetter(new ButtonSetterControl());
                        Setter.Mode = CheckerMode.Equals;
                        allowLessOrMoreMode = false;
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
                        Setter.Mode = CheckerMode.Equals;
                        allowLessOrMoreMode = false;
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
                        Setter.Mode = CheckerMode.Equals;
                        allowLessOrMoreMode = false;
                        break;
                    }
                default:
                    throw new Exception("type not known");
            }

            LessOrMoreModeEnabled = allowLessOrMoreMode;

            Setter.Mode = CheckerMode.Equals;
        }
    }
}
