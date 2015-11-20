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
using UniActionsCore;

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

            this.btCreate.Click += (o, e) => {
                WCreateAction w = new WCreateAction();
                if (w.ShowDialog().Value)
                    Refresh();
                ProcessButtonsEnable();
            };

            this.btEdit.Click += (o, e) => {
                if (this.lvItems.SelectedItem != null)
                {
                    WCreateAction w = new WCreateAction();
                    w.IsEdit = true;
                    w.SetItem((ActionItem)this.lvItems.SelectedItem);
                    if (w.ShowDialog().Value)
                        Refresh();
                }
                ProcessButtonsEnable();
            };

            this.lvItems.MouseDoubleClick += (o, e) => {
                if (this.lvItems.SelectedItem != null)
                {
                    WCreateAction w = new WCreateAction();
                    w.IsEdit = true;
                    w.SetItem((ActionItem)this.lvItems.SelectedItem);
                    if (w.ShowDialog().Value)
                        Refresh();
                }
                ProcessButtonsEnable();
            };

            this.btRemove.Click += (o, e) => {
                if (this.lvItems.SelectedItem != null)
                {
                    if (MessageBox.Show("Удалить выбранный сценарий?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        Pool.RemoveItem((ActionItem)this.lvItems.SelectedItem);
                        SAL.Save();
                        Refresh();
                    }
                }
                ProcessButtonsEnable();
            };

            this.lvItems.SelectionChanged += (o, e) => {
                ProcessButtonsEnable();
            };
        }

        public void Refresh()
        {
            this.lvItems.ItemsSource = UniActionsCore.Pool.ActionItems;
            ProcessButtonsEnable();
        }

        private void ProcessButtonsEnable()
        {
            this.btEdit.IsEnabled = this.btRemove.IsEnabled 
                = this.lvItems.SelectedItem != null;
        }
    }
}
