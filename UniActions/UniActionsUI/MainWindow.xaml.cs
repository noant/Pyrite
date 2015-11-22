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
