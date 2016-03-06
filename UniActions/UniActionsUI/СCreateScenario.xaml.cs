using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UniActionsCore.ScenarioCreation;
using UniActionsUI.ScenarioCreation;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for WCreateAction.xaml
    /// </summary>
    public partial class CCreateScenario : UserControl
    {
        public CCreateScenario()
        {
            InitializeComponent();

            SetScenario(null);

            this.bUseInPool.Value = true;

            _item = new Scenario();

            ProcessOkEnable();

            this.tbName.TextChanged += (o, e) =>
            {
                _item.Name = tbName.Text;
                ProcessOkEnable();
            };

            this.cbCategory.SelectionChanged += (o, e) =>
            {
                _item.Category = this.cbCategory.SelectedItem.ToString();
                tbCategory.Text = this.cbCategory.SelectedItem.ToString();
                ProcessOkEnable();
            };

            this.tbCategory.TextChanged += (o, e) =>
            {
                _item.Category = this.tbCategory.Text;
                ProcessOkEnable();
            };

            this.bUseInPool.BoolChanged += (o) =>
            {
                _item.IsActive = o.Value;
                ProcessOkEnable();
            };

            this.bUseInServer.BoolChanged += (o) =>
            {
                _item.UseServerThreading = o.Value;
                ProcessOkEnable();
            };

            this.btCancel.Click += (o, e) =>
            {
                SetScenario(_tempItem);
            };

            this.btCreate.Click += (o, e) =>
            {
                var res = App.Uni.TasksPool.CheckScenario(_item);

                if (res.Value)
                {
                    _tempItem.ActionBag = _item.ActionBag;
                    _tempItem.Category = _item.Category;
                    _tempItem.IsActive = _item.IsActive;
                    _tempItem.Name = _item.Name;
                    _tempItem.ServerCommand = _item.ServerCommand;
                    _tempItem.UseServerThreading = _item.UseServerThreading;
                    DisableButtons();
                    App.Uni.CommitChanges();
                }
            };

            this.cbCategory.ItemsSource = App.Uni.TasksPool.Scenarios.
                Where(x => !string.IsNullOrEmpty(x.Category)).
                Select(x => x.Category).
                Distinct().
                OrderBy(x => x);
        }

        private Scenario _tempItem;
        public void SetScenario(Scenario scenario)
        {
            if (scenario == null)
                this.Visibility = Visibility.Collapsed;
            else
            {
                this.Visibility = Visibility.Visible;
                _tempItem = scenario;
                _item = scenario.Clone();

                this.tbCategory.Text = _item.Category;
                this.tbName.Text = _item.Name;
                this.bUseInServer.Value = _item.UseServerThreading;
                this.bUseInPool.Value = _item.IsActive;
                if (scenario.ActionBag.Action is ComplexAction)
                {
                    var scenarioView = new ScenarioView();
                    scenarioView.Changed += (o, e) => EnableButtons();
                    scenarioView.Scenario = scenario;
                    this.borderScenarioHolder.Child = scenarioView;
                }
                else
                {
                    var scenarioView = new SingleActionScenarioView();
                    scenarioView.Changed += (o, e) => EnableButtons();
                    scenarioView.Scenario = scenario;
                    this.borderScenarioHolder.Child = scenarioView;
                }
                DisableButtons();
            }
        }

        private Scenario _item;

        private void ProcessOkEnable()
        {
            var result = App.Uni.TasksPool.CheckScenario(_item);
            if (result.Value)
            {
                lblStatus.Content = "";
                EnableButtons();
            }
            else
            {
                var str = "";
                foreach (var warning in result.Warnings)
                    str += warning.Message + "\r\n";

                lblStatus.Content = str;

                DisableOkButton();
            }
        }

        private void EnableButtons()
        {
            this.btCreate.Visibility = btCancel.Visibility = Visibility.Visible;
        }

        private void DisableButtons()
        {
            this.btCreate.Visibility = btCancel.Visibility = Visibility.Collapsed;
        }

        private void DisableOkButton()
        {
            btCreate.Visibility = Visibility.Collapsed;
        }
    }

}
