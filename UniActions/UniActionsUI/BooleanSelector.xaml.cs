using System;
using System.Windows;
using System.Windows.Controls;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CBoolSelect.xaml
    /// </summary>
    public partial class BooleanSelector : UserControl
    {
        public static readonly DependencyProperty ValueProperty;

        static BooleanSelector()
        {
            ValueProperty = DependencyProperty.Register("Value", typeof(bool), typeof(BooleanSelector),
                new FrameworkPropertyMetadata()
                {
                    PropertyChangedCallback = (o, e) =>
                    {
                        var value = (bool)e.NewValue;
                        var boolSelector = (BooleanSelector)o;
                        boolSelector.rbNo.IsChecked = !value;
                        boolSelector.rbYes.IsChecked = value;
                    }
                }
            );
        }

        public BooleanSelector()
        {
            InitializeComponent();

            rbNo.GroupName =
                rbYes.GroupName = Guid.NewGuid().ToString();

            rbNo.Checked += (o, e) =>
            {
                if (BoolChanged != null)
                    BoolChanged(this);
                Value = false;
            };
            rbYes.Checked += (o, e) =>
            {
                if (BoolChanged != null)
                    BoolChanged(this);
                Value = true;
            };
        }
        public event BoolChanged BoolChanged;
        public bool Value
        {
            get
            {
                return (bool)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
    }

    public delegate void BoolChanged(BooleanSelector c);
}
