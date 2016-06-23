using PyriteCore.ScenarioCreation;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PyriteUI
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
                scenario.CurrentPyrite = App.Pyrite;
                scenario.AfterAction += (scen) => App.RaiseItemExecutedEvent(scen);

                if (cbMode.SelectedIndex == 0)
                {
                    scenario.ActionBag = new ActionBag() { Action = App.Pyrite.ModulesControl.CreateActionInstance(typeof(DoubleComplexAction), true).Value };
                }
                else
                {
                    scenario.ActionBag = new ActionBag() { Action = App.Pyrite.ModulesControl.CreateActionInstance(typeof(DoNothingAction), true).Value };
                }

                scenario.ActionBag.Action.Refresh();

                scenario.ServerCommand = Guid.NewGuid().ToString();
                scenario.Name = "Новый сценарий " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                App.Pyrite.ScenariosPool.Add(scenario);
                RefreshListView();
                lvItems.SelectedItem = new ScenariosViewContext.ScenarioViewItem() { Scenario = scenario };
                App.Pyrite.CommitChanges();
            };

            this.btRemove.Click += (o, e) =>
            {
                RemoveCurrentScenario();
            };

            this.lvItems.SelectionChanged += (o, e) =>
            {
                if (_lockSelectionChangedEvent)
                    return;

                if (lvItems.SelectedValue != null && scenarioView.WasChanged)
                {
                    if (BeginConfirmationDialog() == MessageBoxResult.Cancel)
                    {
                        _lockSelectionChangedEvent = true;
                        lvItems.SelectedItem = e.RemovedItems[0];
                        _lockSelectionChangedEvent = false;
                    }
                }
                else
                    BeginEditSelectedScenario();
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

        private bool _lockSelectionChangedEvent;

        private void RefreshListView()
        {
            this.lvItems.ItemsSource = ((ScenariosViewContext)DataContext).AllScenarioViews;
        }

        public void Refresh()
        {
            RefreshListView();
            if (App.Pyrite.ScenariosPool.Scenarios.Any())
            {
                this.lvItems.SelectedIndex = 0;
            }
            else this.lvItems.SelectedIndex = -1;
            scenarioView.DisableButtons();
            this.scenarioView.Refresh();
        }

        private void RemoveCurrentScenario()
        {
            if (this.lvItems.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить выбранный сценарий?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    App.Pyrite.ScenariosPool.RemoveScenario(((ScenariosViewContext.ScenarioViewItem)this.lvItems.SelectedItem).Scenario);
                    App.Pyrite.CommitChanges();
                    Refresh();
                }
            }
            App.Pyrite.CommitChanges();
        }

        private void BeginEditSelectedScenario()
        {
            if (this.lvItems.SelectedItem != null)
                scenarioView.SetScenario(((ScenariosViewContext.ScenarioViewItem)this.lvItems.SelectedItem).Scenario);
            else if (this.lvItems.HasItems)
                lvItems.SelectedIndex = 0;
        }

        public MessageBoxResult BeginConfirmationDialog()
        {
            switch (MessageBox.Show("Выбранный сценарий был изменен. Применить изменения?", "Применить изменения", MessageBoxButton.YesNoCancel, MessageBoxImage.Question))
            {
                case (MessageBoxResult.Yes):
                    {
                        scenarioView.Confirm();
                        BeginEditSelectedScenario();
                        return MessageBoxResult.Yes;
                    }
                case (MessageBoxResult.No):
                    {
                        BeginEditSelectedScenario();
                        return MessageBoxResult.No;
                    }
                case (MessageBoxResult.Cancel):
                    {
                        return MessageBoxResult.Cancel;
                    }
            }
            throw new Exception();
        }

        public bool WasChanged
        {
            get
            {
                return scenarioView.WasChanged;
            }
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
