using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UniActionsCore;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CSavior.xaml
    /// </summary>
    public partial class CSavior : UserControl
    {
        public CSavior()
        {
            InitializeComponent();
            this.btRestart.Click += (o, e) => {
                var btText = this.btRestart.Content.ToString();
                this.btRestart.Content = "Перезагрузка...";

                if (BeforeRestart != null)
                    BeforeRestart();

                var res1 = App.Uni.CommitChanges();
                App.Uni.ReIntialize(new Action<VoidResult>((res2) =>
                {
                    this.Dispatcher.BeginInvoke(new Action(() => { 
                        this.btRestart.Content = btText;                        
                    }));

                    if (AfterRestart != null)
                        AfterRestart();

                    if (res1.Exceptions.Count()==0 && res2.Exceptions.Count()==0)
                        MessageBox.Show("Потоки сервера и поток задач перезапущены.");                    
                }));
            };
            this.btClose.Click += (o, e) => {
                this.btClose.IsEnabled = false;
                this.btClose.Content = "Завершение задач...";
                this.btRestart.IsEnabled = false;

                if (WhenAllBeginsToEnd != null)
                    WhenAllBeginsToEnd();

                App.Uni.Stop(() =>
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() => { 
                        App.Current.Shutdown();                    
                    }));
                });
            };
        }

        public event Action WhenAllBeginsToEnd;
        public event Action BeforeRestart;
        public event Action AfterRestart;
    }

}
