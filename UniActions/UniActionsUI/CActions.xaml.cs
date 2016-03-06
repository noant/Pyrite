using System.Windows;
using System.Linq;
using System.Windows.Controls;
using UniActionsCore.ScenarioCreation;
using System;
using UniStandartActions.Actions;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CActions.xaml
    /// </summary>
    public partial class CActions : UserControl, ControlsHelper.IRefreshable
    {
        public CActions()
        {
            InitializeComponent();

            this.btCreate.Click += (o, e) =>
            {
                var scenario = new Scenario();
                scenario.ServerCommand = Guid.NewGuid().ToString();
                scenario.ActionBag = new ActionBag() { Action = App.Uni.ModulesControl.CreateActionInstance(typeof(ComplexAction)).Value };
                scenario.Name = "Новый сценарий " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
                App.Uni.TasksPool.Add(scenario);
                Refresh();
                lvItems.SelectedItem = scenario;
                ProcessButtonsEnable();
                App.Uni.CommitChanges();
            };

            this.btCreateSingle.Click += (o, e) =>
            {
                var scenario = new Scenario();
                scenario.ServerCommand = Guid.NewGuid().ToString();
                scenario.ActionBag = new ActionBag() { Action = App.Uni.ModulesControl.CreateActionInstance(typeof(DoNothingAction)).Value };
                scenario.Name = "Новый сценарий " + DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
                App.Uni.TasksPool.Add(scenario);
                Refresh();
                lvItems.SelectedItem = scenario;
                ProcessButtonsEnable();
                App.Uni.CommitChanges();
            };

            this.lvItems.MouseLeftButtonUp += (o, e) =>
            {
                scenarioView.SetScenario(this.lvItems.SelectedItem as Scenario);
                ProcessButtonsEnable();
            };

            this.btRemove.Click += (o, e) =>
            {
                if (this.lvItems.SelectedItem != null)
                {
                    if (MessageBox.Show("Удалить выбранный сценарий?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        App.Uni.TasksPool.RemoveScenario((Scenario)this.lvItems.SelectedItem);
                        App.Uni.CommitChanges();
                        Refresh();
                    }
                }
                App.Uni.CommitChanges();
                ProcessButtonsEnable();
            };

            this.lvItems.SelectionChanged += (o, e) =>
            {
                scenarioView.SetScenario((Scenario)this.lvItems.SelectedItem);
                ProcessButtonsEnable();
            };
            Refresh();
        }

        public void Refresh()
        {
            this.lvItems.ItemsSource = App.Uni.TasksPool.Scenarios;
            if (App.Uni.TasksPool.Scenarios.Any())
                this.lvItems.SelectedIndex = 0;
            else this.lvItems.SelectedIndex = -1;
            ProcessButtonsEnable();
        }

        private void ProcessButtonsEnable()
        {
            this.btRemove.Visibility = this.lvItems.SelectedItem != null ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
