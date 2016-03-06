using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UniActionsClientIntefaces;
using UniActionsCore.ScenarioCreation;

namespace UniActionsUI.ScenarioCreation
{
    /// <summary>
    /// Interaction logic for ComplexCheckerView.xaml
    /// </summary>
    public partial class ComplexCheckerView : EditableUserControl
    {
        public static readonly DependencyProperty RootControlsVisibilityProperty;

        static ComplexCheckerView()
        {
            RootControlsVisibilityProperty = DependencyProperty.Register("RootControlsVisibility", typeof(Visibility), typeof(ComplexCheckerView), new FrameworkPropertyMetadata(Visibility.Visible));
        }

        public Visibility RootControlsVisibility
        {
            get
            {
                return (Visibility)GetValue(RootControlsVisibilityProperty);
            }
            set
            {
                SetValue(RootControlsVisibilityProperty, value);
            }
        }

        public ComplexCheckerView() : this(null)
        {
        }

        public ComplexCheckerView(OperatorCheckerPair checkerPair)
        {
            InitializeComponent();

            var context = new ComplexCheckerViewContext(checkerPair);
            this.DataContext = context;

            this.btAdd.Click += (o, e) =>
            {
                AddItem(context.AddChecker());
            };

            this.btAddGroup.Click += (o, e) =>
            {
                AddItem(context.AddGroupChecker());
            };

            this.btDelete.Click += (o1, e1) =>
            {
                if (Remove != null)
                {
                    Remove(this, new EventArgs());
                    RaiseChanged();
                }
                ProcessFirstItem();
            };

            foreach (var oPair in context.AllOperatorCheckerPairs)
            {
                AddItem(oPair);
            }
        }

        public void AddItem(OperatorCheckerPair oPair)
        {
            if (oPair.Checker is ComplexChecker)
            {
                var cView = new ComplexCheckerView(oPair) { IsFirst = false };
                cView.Remove += (o1, e1) =>
                {
                    this.stackCheckers.Children.Remove(cView);
                    ((ComplexCheckerViewContext)this.DataContext).RemoveChecker(oPair);
                    ProcessFirstItem();
                };
                cView.Margin = new Thickness(27, 0, 0, 0);
                cView.Changed += (o, e) => RaiseChanged();
                this.stackCheckers.Children.Add(cView);
            }
            else
            {
                var cView = new CheckerView(oPair) { IsFirst = false };
                cView.Remove += (o1, e1) =>
                {
                    this.stackCheckers.Children.Remove(cView);
                    ((ComplexCheckerViewContext)this.DataContext).RemoveChecker(oPair);
                    ProcessFirstItem();
                };
                cView.Changed += (o, e) => RaiseChanged();
                this.stackCheckers.Children.Add(cView);
            }

            RaiseChanged();

            ProcessFirstItem();
        }

        public void ProcessFirstItem()
        {
            if (this.stackCheckers.Children.Count > 0)
            {
                if (this.stackCheckers.Children[0] is CheckerView)
                    ((CheckerView)this.stackCheckers.Children[0]).IsFirst = true;
                if (this.stackCheckers.Children[0] is ComplexCheckerView)
                    ((ComplexCheckerView)this.stackCheckers.Children[0]).IsFirst = true;
            }
        }

        public bool IsFirst
        {
            get
            {
                return ((ComplexCheckerViewContext)DataContext).IsFirst;
            }
            set
            {
                ((ComplexCheckerViewContext)DataContext).IsFirst = value;
            }
        }

        public event Action<object, EventArgs> Remove;

        public void RaiseChanged()
        {
            if (Changed != null)
                Changed(this, new EventArgs());
        }

        public event Action<object, EventArgs> Changed;
    }
}
