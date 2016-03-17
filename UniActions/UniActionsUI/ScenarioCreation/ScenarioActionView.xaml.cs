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
