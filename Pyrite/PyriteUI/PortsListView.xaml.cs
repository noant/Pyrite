using PyriteCore;
using System.Globalization;
using System.Windows.Controls;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for CPorts.xaml
    /// </summary>
    public partial class PortsListView : UserControl, ControlsHelper.IRefreshable, ControlsHelper.IConfirm
    {
        public PortsListView()
        {
            InitializeComponent();

            ControlsHelper.AppendOnlyInteger(tbPort, 0, ushort.MaxValue);
            ControlsHelper.AppendOnlyInteger(tbDistributionPort, 0, ushort.MaxValue);

            Refresh();

            btAdd.Click += (o, e) =>
            {
                _tempPort.Add(tbPort.GetUShort());
                ProcessButtonsEnabled();
                RefreshList();
            };

            btDelete.Click += (o, e) =>
            {
                var portToDelete = ushort.Parse(listPort.SelectedItem.ToString(), CultureInfo.InvariantCulture);
                _tempPort.RemoveAll(x =>
                    x == portToDelete
                    );
                listPort.Items.Remove(portToDelete);
                ProcessButtonsEnabled();
            };

            this.listPort.SelectionChanged += (o, e) =>
            {
                ProcessButtonsEnabled();
            };

            this.tbPort.TextChanged += (o, e) =>
            {
                ProcessButtonsEnabled();
            };

            ProcessButtonsEnabled();
        }

        void ProcessButtonsEnabled()
        {
            btDelete.IsEnabled = listPort.SelectedIndex != -1 && listPort.Items.Count > 1;
            var port = tbPort.GetUShort();
            btAdd.IsEnabled = !_tempPort.Contains(port) && port != 0;
        }

        public void Refresh()
        {
            _tempPort.Clear();
            _tempPort.AddRange(App.Pyrite.ServerThreading.Settings.ActionsPorts);
            tbDistributionPort.Text = App.Pyrite.ServerThreading.Settings.DistributionPort.ToString();
            RefreshList();
            ProcessButtonsEnabled();
        }

        private void RefreshList()
        {
            listPort.Items.Clear();
            foreach (var port in _tempPort)
                listPort.Items.Add(port);
        }

        public void Confirm()
        {
            App.Pyrite.ServerThreading.Settings.DistributionPort = tbDistributionPort.GetUShort();
            App.Pyrite.ServerThreading.Settings.ActionsPorts.Clear();
            App.Pyrite.ServerThreading.Settings.ActionsPorts.AddRange(_tempPort);
        }

        private SkeddedList<ushort> _tempPort = new SkeddedList<ushort>();
    }
}
