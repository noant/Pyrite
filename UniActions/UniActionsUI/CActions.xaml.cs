using System.Windows;
using System.Windows.Controls;

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
            Refresh();

            this.btCreate.Click += (o, e) =>
            {
                WCreateAction w = new WCreateAction();
                if (w.ShowDialog().Value)
                    Refresh();
                ProcessButtonsEnable();
            };

            this.btEdit.Click += (o, e) =>
            {
                if (this.lvItems.SelectedItem != null)
                {
                    WCreateAction w = new WCreateAction();
                    w.IsEdit = true;
                    w.SetItem((Scenario)this.lvItems.SelectedItem);
                    if (w.ShowDialog().Value)
                        Refresh();
                }
                ProcessButtonsEnable();
            };

            this.lvItems.MouseDoubleClick += (o, e) =>
            {
                if (this.lvItems.SelectedItem != null)
                {
                    WCreateAction w = new WCreateAction();
                    w.IsEdit = true;
                    w.SetItem((Scenario)this.lvItems.SelectedItem);
                    if (w.ShowDialog().Value)
                        Refresh();
                }
                ProcessButtonsEnable();
            };

            this.btRemove.Click += (o, e) =>
            {
                if (this.lvItems.SelectedItem != null)
                {
                    if (MessageBox.Show("Удалить выбранный сценарий?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        App.Uni.TasksPool.RemoveItem((Scenario)this.lvItems.SelectedItem);
                        App.Uni.CommitChanges();
                        Refresh();
                    }
                }
                ProcessButtonsEnable();
            };

            this.lvItems.SelectionChanged += (o, e) =>
            {
                ProcessButtonsEnable();
            };
        }

        public void Refresh()
        {
            this.lvItems.ItemsSource = App.Uni.TasksPool.ActionItems;
            ProcessButtonsEnable();
        }

        private void ProcessButtonsEnable()
        {
            this.btEdit.IsEnabled = this.btRemove.IsEnabled
                = this.lvItems.SelectedItem != null;
        }
    }
}
