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
    /// Interaction logic for CheckerView.xaml
    /// </summary>
    public partial class CheckerView : EditableUserControl
    {
        public CheckerView() : this(null) { }

        public CheckerView(OperatorCheckerPair checkerPair)
        {
            if (checkerPair == null)
                return;
            var context = new CheckerViewContext(checkerPair);
            this.DataContext = context;
            
            InitializeComponent();
            tbParams.MouseLeftButtonDown += (o, e) =>
            {
                context.BeginCheckerUserSettings();
            };

            context.Changed += (o, e) => RaiseChanged();

            btDelete.Click += (o, e) =>
            {
                if (Remove != null)
                {
                    Remove(this, new EventArgs());
                    RaiseChanged();
                }
            };
        }

        public bool IsFirst
        {
            get
            {
                return ((CheckerViewContext)DataContext).IsFirst;
            }
            set
            {
                ((CheckerViewContext)DataContext).IsFirst = value;
                cbOperator.SelectedValue = ((CheckerViewContext)DataContext).OperatorPairView;
            }
        }

        public void RaiseChanged()
        {
            if (Changed != null)
                Changed(this, new EventArgs());
        }

        public event Action<object, EventArgs> Changed;

        public event Action<object, EventArgs> Remove;
    }
}
