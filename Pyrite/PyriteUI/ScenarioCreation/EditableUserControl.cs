using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PyriteUI.ScenarioCreation
{
    public class EditableUserControl : UserControl
    {
        public EditableUserControl()
        {
            this.SetBinding(IsInEditModeProperty,
                new Binding("EditModeVisibility")
                {
                    Converter = new VisibilityToBoolConverter(),
                    Mode = BindingMode.TwoWay,
                    Source = this
                }
            );

            this.MouseEnter += (o, e) =>
            {
                this.Background = new SolidColorBrush(
                    Color.FromArgb(18, Colors.Blue.R, Colors.Blue.G, Colors.Blue.B)
                    );
            };
            this.MouseLeave += (o, e) =>
            {
                this.Background = Brushes.Transparent;
            };

            this.Background = Brushes.Transparent;
        }

        public static readonly DependencyProperty IsInEditModeProperty;
        public static readonly DependencyProperty EditModeVisibilityProperty;

        public bool IsInEditMode
        {
            get
            {
                return (bool)GetValue(IsInEditModeProperty);
            }
            set
            {
                SetValue(IsInEditModeProperty, value);
            }
        }

        public Visibility EditModeVisibility
        {
            get
            {
                return (Visibility)GetValue(EditModeVisibilityProperty);
            }
            set
            {
                SetValue(EditModeVisibilityProperty, value);
            }
        }
        static EditableUserControl()
        {
            var IsInEditModeFrameworkPropertyMetadata = new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault);
            IsInEditModeFrameworkPropertyMetadata.PropertyChangedCallback = (obj, args) =>
            {
                if (obj is Panel)
                    foreach (var ctrl in ((Panel)obj).Children)
                        ProcessChangeEditMode((Panel)ctrl, (bool)args.NewValue);
                else if (obj is ContentControl)
                    ProcessChangeEditMode(((ContentControl)obj).Content, (bool)args.NewValue);
            };
            IsInEditModeProperty = DependencyProperty.Register(
               "IsInEditMode",
               typeof(bool),
               typeof(EditableUserControl),
               IsInEditModeFrameworkPropertyMetadata
               );

            EditModeVisibilityProperty = DependencyProperty.Register(
                "EditModeVisibility",
                typeof(Visibility),
                typeof(EditableUserControl),
                new FrameworkPropertyMetadata(Visibility.Collapsed, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        }

        static void ProcessChangeEditMode(object control, bool isInEditMode)
        {
            if (control is EditableUserControl)
                ((EditableUserControl)control).IsInEditMode = isInEditMode;
            else if (control is Panel)
                foreach (var ctrl in ((Panel)control).Children)
                {
                    ProcessChangeEditMode(ctrl, isInEditMode);
                }
            else if (control is ContentControl)
                ProcessChangeEditMode(((ContentControl)control).Content, isInEditMode);
        }
    }

    public class VisibilityToBoolConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Visibility)value) == Visibility.Visible;
        }
    }
}
