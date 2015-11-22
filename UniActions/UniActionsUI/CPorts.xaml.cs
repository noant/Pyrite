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
    /// Interaction logic for CPorts.xaml
    /// </summary>
    public partial class CPorts : UserControl, ControlsHelper.IRefreshable
    {
        public CPorts()
        {
            InitializeComponent();

            ControlsHelper.AppendOnlyInteger(tbPort, ushort.MaxValue, 0);

            btAdd.Click += (o, e) => {
                UniActionsCore.ServerThreading.Settings.ActionsPorts.Add(tbPort.GetUShort());
                ProcessButtonsEnabled();
                Refresh();
            };

            btDelete.Click += (o, e) => {
                UniActionsCore.ServerThreading.Settings.ActionsPorts.RemoveAll(x => x == tbPort.GetUShort());
                ProcessButtonsEnabled();
            };

            this.listPort.SelectionChanged += (o, e) =>
            {
                ProcessButtonsEnabled();
            };

            this.tbPort.TextChanged += (o, e) => {
                ProcessButtonsEnabled();
            };

            ProcessButtonsEnabled();
        }

        void ProcessButtonsEnabled()
        {
            btDelete.IsEnabled = listPort.SelectedIndex != -1 && listPort.Items.Count > 1;
            var port = tbPort.GetUShort();
            btAdd.IsEnabled = !UniActionsCore.ServerThreading.Settings.ActionsPorts.Any(x => x == port);
        }

        public void Refresh()
        {
            listPort.Items.Clear();
            foreach (var port in UniActionsCore.ServerThreading.Settings.ActionsPorts)
                listPort.Items.Add(port);
            ProcessButtonsEnabled();
        }
    }
}
