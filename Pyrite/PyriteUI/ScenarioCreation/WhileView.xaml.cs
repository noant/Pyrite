using PyriteCore.ScenarioCreation;
using System;
using System.Windows;

namespace PyriteUI.ScenarioCreation
{
    /// <summary>
    /// Interaction logic for IfView.xaml
    /// </summary>
    public partial class WhileView : EditableUserControl
    {
        public WhileView(WhileAction whileAction)
        {
            if (whileAction == null)
                return;
            var context = new WhileViewContext(whileAction);
            this.DataContext = context;
            InitializeComponent();
            var ifControl = new ComplexActionView(context.Action) { RootControlsVisibility = Visibility.Collapsed };
            ifControl.Changed += (o, e) => RaiseChanged();
            this.gridIfHolder.Children.Add(ifControl);
            var checkerControl = new ComplexCheckerView(new OperatorCheckerPair() { Checker = context.Checker }) { RootControlsVisibility = Visibility.Collapsed };
            checkerControl.Changed += (o, e) => RaiseChanged();
            this.gridCheckerHolder.Children.Add(checkerControl);
            this.btRemove.Click += (o, e) =>
            {
                if (Utils.IsUserSureToDeleteCurrentOperator())
                    if (Remove != null)
                    {
                        Remove(this, new EventArgs());
                        RaiseChanged();
                    }
            };
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
