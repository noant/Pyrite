using PyriteCore.ScenarioCreation;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for CRunScenario.xaml
    /// </summary>
    public partial class RunScenarioView : UserControl
    {
        public RunScenarioView()
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
                Refresh();
            }
        }

        private void btScenarioRun_Click(object sender, RoutedEventArgs e)
        {
            Run();
            if (NeedClose != null)
                NeedClose(this, new EventArgs());
        }

        private void btScenarioRun_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Run();
        }

        public void Run()
        {
            if (_scenario != null)
            {
                btScenarioRun.IsEnabled = false;
                btScenarioRun.Content = "Выполняется...";
                _scenario.ExecuteAsync((state) =>
                {
                    //btScenarioRun.Dispatcher.BeginInvoke(new Action(() =>
                    //{
                    //    //btScenarioRun.Content = state.CheckState();
                    //    btScenarioRun.IsEnabled = true;
                    //    btScenarioRun.Focus();
                    //}));
                });
            }
        }

        public int Number
        {
            get
            {
                return int.Parse(lblNumber.Content.ToString(), CultureInfo.InvariantCulture);
            }
            set
            {
                lblNumber.Content = value;
            }
        }

        public void Refresh()
        {
            _scenario.CheckStateAsync((state) => btScenarioRun.Dispatcher.BeginInvoke(
                new Action(() =>
                {
                    btScenarioRun.IsEnabled = true;
                    btScenarioRun.Content = state;
                }
            )));
        }

        public event Action<object, EventArgs> NeedClose;
    }
}
