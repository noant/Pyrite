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
    public partial class SingleActionScenarioView : EditableUserControl
    {
        public SingleActionScenarioView()
        {
            InitializeComponent();
        }

        private Scenario _scenario;
        public Scenario Scenario
        {
            get
            {
                return _scenario;
            }
            set
            {
                _scenario = value;

                var action = new ComplexActionView((ComplexAction)_scenario.Action)
                {
                    IgnoreChangedEvent = true, //ignore changes on initialize
                    RootControlsVisibility = Visibility.Collapsed,
                    IsInEditMode = this.IsInEditMode
                };

                action.Changed += (o, e) => RaiseChanged();

                borderScenarionHolder.Child = action;

                action.IgnoreChangedEvent = false;
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
