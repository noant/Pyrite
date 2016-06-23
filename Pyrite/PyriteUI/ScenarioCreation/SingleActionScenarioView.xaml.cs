using PyriteCore.ScenarioCreation;
using System;

namespace PyriteUI.ScenarioCreation
{
    /// <summary>
    /// Interaction logic for ScenarioView.xaml
    /// </summary>
    public partial class SingleActionScenarioView : EditableUserControl
    {
        public SingleActionScenarioView()
        {
            InitializeComponent();
        }

        private ActionBag _actionBag;
        public ActionBag ActionBag
        {
            get
            {
                return _actionBag;
            }
            set
            {
                _actionBag = value;

                var action = new ActionViewExtended(_actionBag)
                {
                    IgnoreChangedEvent = true, //ignore changes on initialize
                    IsInEditMode = this.IsInEditMode
                };

                action.Changed += (o, e) => RaiseChanged();

                contentScenarioHolder.Content = action;

                action.IgnoreChangedEvent = false;
            }
        }

        public void RaiseChanged()
        {
            if (Changed != null)
                Changed(this, new EventArgs());
        }

        public event Action<object, EventArgs> Changed;
    }
}
