using PyriteCore.ScenarioCreation;
using System;
using System.Windows.Input;

namespace PyriteUI.ScenarioCreation
{
    /// <summary>
    /// Interaction logic for ActionView.xaml
    /// </summary>
    public partial class ActionView : EditableUserControl
    {
        public ActionView(ActionBag actionBag)
        {
            if (actionBag == null)
                return;

            var context = new ActionViewContext(actionBag);
            this.DataContext = context;
            InitializeComponent();

            context.Changed += (o, e) => RaiseChanged();

            this.tbParams.MouseLeftButtonDown += (o, e) =>
            {
                context.BeginActionUserSettings();
            };

            this.btCheck.Click += (o, e) =>
            {
                context.ExecuteCurrentAction();
            };

            this.btDelete.Click += (o, e) =>
            {
                RaiseRemove();
            };

            this.KeyDown += (o, e) =>
            {
                if (e.Key == Key.Delete)
                    RaiseRemove();
            };
        }

        public void RaiseRemove()
        {
            if (Utils.IsUserSureToDeleteCurrentOperator())
                if (Remove != null)
                {
                    Remove(this, new EventArgs());
                    RaiseChanged();
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
