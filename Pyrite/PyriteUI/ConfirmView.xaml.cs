using PyriteCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for CSavior.xaml
    /// </summary>
    public partial class ConfirmView : UserControl, ControlsHelper.IRefreshable
    {
        public ConfirmView()
        {
            InitializeComponent();
            this.btConfirm.Click += (o, e) =>
            {
                var btText = this.btConfirm.Content.ToString();
                this.btConfirm.Content = "Перезагрузка...";

                if (NeedConfirm != null)
                    NeedConfirm();

                if (BeforeRestart != null)
                    BeforeRestart();

                var res1 = App.Pyrite.CommitChanges();
                App.Pyrite.ReIntialize(new Action<VoidResult>((res2) =>
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.btConfirm.Content = btText;
                    }));

                    if (AfterRestart != null)
                        AfterRestart();

                    if (res1.Exceptions.Count() == 0 && res2.Exceptions.Count() == 0)
                        MessageBox.Show("Потоки сервера и потоки задач перезапущены.");
                }));
            };
            this.btClose.Click += (o, e) =>
            {
                this.btClose.IsEnabled = false;
                this.btClose.Content = "Завершение задач...";
                this.btConfirm.IsEnabled = false;

                if (WhenAllBeginsToEnd != null)
                    WhenAllBeginsToEnd();

                App.Pyrite.Stop(() =>
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        App.Current.Shutdown();
                    }));
                });
            };
            this.btReset.Click += (o, e) =>
            {
                if (NeedCancel != null)
                    NeedCancel();
            };
        }

        public event Action WhenAllBeginsToEnd;
        public event Action BeforeRestart;
        public event Action AfterRestart;
        public event Action NeedConfirm;
        public event Action NeedCancel;

        private void cbStartup_Checked(object sender, RoutedEventArgs e)
        {
            StartupHelper.IsAppInStartup = true;
        }

        private void cbStartup_Unchecked(object sender, RoutedEventArgs e)
        {
            StartupHelper.IsAppInStartup = false;
        }

        public void Refresh()
        {
            cbStartup.IsChecked = StartupHelper.IsAppInStartup;
        }

    }

}
