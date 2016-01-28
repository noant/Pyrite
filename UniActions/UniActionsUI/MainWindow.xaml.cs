using System;
using System.Windows;

namespace UniActionsUI
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

            this.cSavior.WhenAllBeginsToEnd += () => {
                this.IsEnabled = false;
                this.Closing += MainWindow_Closing;
            };

            this.cSavior.BeforeRestart += () => {
                this.IsEnabled = false;
                this.Closing += MainWindow_Closing;
            };

            this.cSavior.AfterRestart += () => {
                this.Dispatcher.BeginInvoke(new Action(() => { 
                    this.IsEnabled = true;
                    this.Closing -= MainWindow_Closing;
                }));
            };
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
