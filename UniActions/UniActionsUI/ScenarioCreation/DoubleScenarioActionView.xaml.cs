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
    /// Interaction logic for DoubleScenarioActionView.xaml
    /// </summary>
    public partial class DoubleScenarioActionView : UserControl
    {
        public DoubleScenarioActionView()
        {
            InitializeComponent();

            svScenarioAlg.Changed += (o, e) => RaiseChanged();
            svScenarioEndingAlg.Changed += (o, e) => RaiseChanged();
        }

        public ActionBag _actionBag;
        public ActionBag ActionBag
        {
            get
            {
                return _actionBag;
            }
            set
            {
                _actionBag = value;
                svScenarioAlg.ActionBag = ((DoubleComplexAction)_actionBag.Action).ActionBagBegin;
                svScenarioEndingAlg.ActionBag = ((DoubleComplexAction)_actionBag.Action).ActionBagEnd;
            }
        }

        public void RaiseChanged()
        {
            if (Changed != null)
                Changed(this, new EventArgs());
        }

        public Action<object, EventArgs> Changed;
    }
}
