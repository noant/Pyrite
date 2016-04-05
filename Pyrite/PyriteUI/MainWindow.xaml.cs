using System;
using System.Windows;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Background = App.WindowBackground;

            this.cSavior.Refresh();

            this.cSavior.NeedConfirm += () => this.cSettings.Confirm();

            this.cSavior.NeedCancel += () => this.cSettings.Refresh();

            this.cSavior.WhenAllBeginsToEnd += () =>
            {
                this.IsEnabled = false;
                this.Closing += MainWindow_Closing;
            };

            this.cSavior.BeforeRestart += () =>
            {
                this.IsEnabled = false;
                this.Closing += MainWindow_Closing;
            };

            this.cSavior.AfterRestart += () =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.IsEnabled = true;
                    this.Closing -= MainWindow_Closing;
                }));
            };

            bool _lockSelectionChangedEvent = false;
            this.tabControl.SelectionChanged += (o, e) =>
            {
                if (_lockSelectionChangedEvent)
                    return;

                if (e.RemovedItems.Count > 0 && e.RemovedItems[0].Equals(tabScenarios) && cScenariosView.WasChanged)
                {
                    if (cScenariosView.BeginConfirmationDialog() == MessageBoxResult.Cancel)
                    {
                        _lockSelectionChangedEvent = true;
                        this.tabControl.SelectedItem = this.tabScenarios;
                        _lockSelectionChangedEvent = false;
                    }
                }
            };
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
