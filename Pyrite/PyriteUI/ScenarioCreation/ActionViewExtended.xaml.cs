using PyriteCore.ScenarioCreation;
using System;

namespace PyriteUI.ScenarioCreation
{
    /// <summary>
    /// Interaction logic for ActionView.xaml
    /// </summary>
    public partial class ActionViewExtended : EditableUserControl
    {
        public ActionViewExtended(ActionBag actionBag)
        {
            if (actionBag == null)
                return;
            var context = new ActionViewContext(actionBag);
            this.DataContext = context;
            InitializeComponent();

            context.ActionStringSplitter = "\r\n";

            context.Changed += (o, e) => RaiseChanged();

            this.tbParams.MouseLeftButtonDown += (o, e) =>
            {
                context.BeginActionUserSettings();
            };

            this.btCheck.Click += (o, e) =>
            {
                context.ExecuteCurrentAction();
            };
        }

        public void RaiseChanged()
        {
            if (Changed != null && !IgnoreChangedEvent)
                Changed(this, new EventArgs());
        }

        public bool IgnoreChangedEvent
        {
            get; set;
        }

        public event Action<object, EventArgs> Changed;
    }
}
