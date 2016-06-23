using PyriteCore.ScenarioCreation;
using System;
using System.Windows;

namespace PyriteUI.ScenarioCreation
{
    /// <summary>
    /// Interaction logic for ComplexActionView.xaml
    /// </summary>
    public partial class ComplexActionView : EditableUserControl
    {
        public static readonly DependencyProperty RootControlsVisibilityProperty;

        static ComplexActionView()
        {
            RootControlsVisibilityProperty = DependencyProperty.Register("RootControlsVisibility", typeof(Visibility), typeof(ComplexActionView), new FrameworkPropertyMetadata(Visibility.Visible));
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

        public ComplexActionView() : this(null) { }

        public ComplexActionView(ComplexAction action)
        {
            if (action == null)
                return;
            var context = new ComplexActionViewContext(action);
            this.DataContext = context;

            InitializeComponent();

            this.btAdd.Click += (o, e) =>
            {
                AddActionControl(context.AddAction());
            };

            this.btAddIf.Click += (o, e) =>
            {
                AddIfControl((IfAction)context.AddIfAction().Action);
            };

            this.btAddWhile.Click += (o, e) =>
            {
                AddWhileControl((WhileAction)context.AddWhileAction().Action);
            };

            RefreshAllItems();
        }

        public void RefreshAllItems()
        {
            this.spActions.Children.Clear();
            foreach (var actionBag in ((ComplexActionViewContext)this.DataContext).AllActions)
            {
                if (actionBag.Action is WhileAction)
                    AddWhileControl((WhileAction)actionBag.Action);
                else if (actionBag.Action is IfAction)
                    AddIfControl((IfAction)actionBag.Action);
                else
                    AddActionControl(actionBag);
            }
        }

        public ComplexAction Action
        {
            get
            {
                return ((ComplexActionViewContext)this.DataContext).Action;
            }
            set
            {
                ((ComplexActionViewContext)this.DataContext).Action = value;
                RefreshAllItems();
            }
        }

        private void AddIfControl(IfAction action)
        {
            var view = new IfView(action);
            view.Remove += (o, e) =>
            {
                ((ComplexActionViewContext)this.DataContext).RemoveAction(action);
                this.spActions.Children.Remove(view);
            };
            view.Changed += (o, e) => RaiseChanged();
            spActions.Children.Add(view);
            RaiseChanged();
        }

        private void AddWhileControl(WhileAction action)
        {
            var view = new WhileView(action);
            view.Remove += (o, e) =>
            {
                ((ComplexActionViewContext)this.DataContext).RemoveAction(action);
                this.spActions.Children.Remove(view);
            };
            view.Changed += (o, e) => RaiseChanged();
            spActions.Children.Add(view);
            RaiseChanged();
        }

        private void AddActionControl(ActionBag actionBag)
        {
            var view = new ActionView(actionBag);
            view.Remove += (o, e) =>
            {
                ((ComplexActionViewContext)this.DataContext).RemoveAction(actionBag.Action);
                this.spActions.Children.Remove(view);
            };
            view.Changed += (o, e) => RaiseChanged();
            spActions.Children.Add(view);
            RaiseChanged();
        }

        public bool IgnoreChangedEvent { get; set; }

        public void RaiseChanged()
        {
            if (Changed != null && !IgnoreChangedEvent)
                Changed(this, new EventArgs());
        }

        public event Action<object, EventArgs> Changed;
    }
}
