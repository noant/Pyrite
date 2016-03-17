using System.Windows;
using System.Linq;
using System.Windows.Controls;
using UniActionsCore.ScenarioCreation;
using System;
using UniStandartActions.Actions;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Input;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CActions.xaml
    /// </summary>
    public partial class ScenariosView : UserControl, ControlsHelper.IRefreshable
    {
        public ScenariosView()
        {
            InitializeComponent();

            this.DataContext = new ScenariosViewContext();

            scenarioView.Applying += (o, e) =>
            {
                this.lvItems.Items.Refresh();
            };

            this.btCreate.Click += (o, e) =>
            {
                var scenario = new Scenario();

                if (cbMode.SelectedIndex == 0)
                {
                    scenario.ActionBag = new ActionBag() { Action = App.Uni.ModulesControl.CreateActionInstance(typeof(DoubleComplexAction)).Value };
                }
                else
                {
                    scenario.ActionBag = new ActionBag() { Action = App.Uni.ModulesControl.CreateActionInstance(typeof(DoNothingAction)).Value };
                }

                scenario.ServerCommand = Guid.NewGuid().ToString();
                scenario.Name = "Новый сценарий " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
                App.Uni.TasksPool.Add(scenario);
                RefreshListView();
                lvItems.SelectedItem = new ScenariosViewContext.ScenarioViewItem() { Scenario = scenario };
                App.Uni.CommitChanges();
            };

            this.btRemove.Click += (o, e) =>
            {
                RemoveCurrentScenario();
            };

            this.lvItems.SelectionChanged += (o, e) =>
            {
                if (this.lvItems.SelectedItem != null)
                    scenarioView.SetScenario(((ScenariosViewContext.ScenarioViewItem)this.lvItems.SelectedItem).Scenario);
                else if (this.lvItems.HasItems)
                    lvItems.SelectedIndex = 0;
            };

            this.KeyDown += (o, e) =>
            {
                var element = FocusManager
                    .GetFocusedElement(Window.GetWindow(this)); //get current focused element
                if (e.Key == Key.Delete
                    && element is ListBoxItem
                    && this.lvItems.Items.Contains(((ListViewItem)element).DataContext))
                    RemoveCurrentScenario();
            };

            Refresh();
        }

        private void RefreshListView()
        {
            this.lvItems.ItemsSource = ((ScenariosViewContext)DataContext).AllScenarioViews;
        }

        public void Refresh()
        {
            RefreshListView();
            if (App.Uni.TasksPool.Scenarios.Any())
                this.lvItems.SelectedIndex = 0;
            else this.lvItems.SelectedIndex = -1;
            scenarioView.DisableButtons();
        }

        private void RemoveCurrentScenario()
        {
            if (this.lvItems.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить выбранный сценарий?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    App.Uni.TasksPool.RemoveScenario(((ScenariosViewContext.ScenarioViewItem)this.lvItems.SelectedItem).Scenario);
                    App.Uni.CommitChanges();
                    Refresh();
                }
            }
            App.Uni.CommitChanges();
        }
    }

    [ValueConversion(typeof(object), typeof(Visibility))]
    public class ObjectToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
