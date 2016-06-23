using PyriteCore.ScenarioCreation;
using System;
using System.Windows.Input;

namespace PyriteUI.ScenarioCreation
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
                RaiseRemove();
            };

            this.KeyDown += (o, e) =>
            {
                if (e.Key == Key.Delete)
                {
                    RaiseRemove();
                }
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
