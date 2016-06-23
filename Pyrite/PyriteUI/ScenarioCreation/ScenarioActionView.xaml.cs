using PyriteCore.ScenarioCreation;
using System;
using System.Windows;

namespace PyriteUI.ScenarioCreation
{
    /// <summary>
    /// Interaction logic for ScenarioView.xaml
    /// </summary>
    public partial class ScenarioActionView : EditableUserControl
    {
        public ScenarioActionView()
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

                var action = new ComplexActionView((ComplexAction)_actionBag.Action)
                {
                    IgnoreChangedEvent = true, //ignore changes on initialize
                    RootControlsVisibility = Visibility.Collapsed,
                    IsInEditMode = this.IsInEditMode
                };

                action.Changed += (o, e) => RaiseChanged();

                contentScenarionHolder.Content = action;

                action.IgnoreChangedEvent = false;
            }
        }

        public string Caption
        {
            get
            {
                return lblCaption.Content.ToString();
            }
            set
            {
                lblCaption.Content = value;
            }
        }

        public void RaiseChanged()
        {
            if (Changed != null)
                Changed(this, new EventArgs());
        }

        public event Action<object, EventArgs> Changed;

        private void btChangeMode_Click(object sender, RoutedEventArgs e)
        {
            this.IsInEditMode = !this.IsInEditMode;
            btChangeMode.Content = this.IsInEditMode ? "Режим просмотра" : "Режим редактирования";
        }
    }
}
