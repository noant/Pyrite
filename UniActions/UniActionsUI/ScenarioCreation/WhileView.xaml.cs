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
using UniActionsCore.ScenarioCreation;

namespace UniActionsUI.ScenarioCreation
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
