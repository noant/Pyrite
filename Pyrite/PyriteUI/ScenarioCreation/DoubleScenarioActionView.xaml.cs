using PyriteCore.ScenarioCreation;
using System;
using System.Windows.Controls;

namespace PyriteUI.ScenarioCreation
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
