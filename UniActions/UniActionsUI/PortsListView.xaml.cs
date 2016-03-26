using System.Windows.Controls;
using UniActionsCore;

namespace UniActionsUI
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
                var portToDelete = ushort.Parse(listPort.SelectedItem.ToString());
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
            _tempPort.AddRange(App.Uni.ServerThreading.Settings.ActionsPorts);
            tbDistributionPort.Text = App.Uni.ServerThreading.Settings.DistributionPort.ToString();
            tbSharingPort.Text = App.Uni.ServerThreading.Settings.SharingPort.ToString();
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
            App.Uni.ServerThreading.Settings.DistributionPort = tbDistributionPort.GetUShort();
            App.Uni.ServerThreading.Settings.SharingPort = tbSharingPort.GetUShort();
            App.Uni.ServerThreading.Settings.ActionsPorts.Clear();
            App.Uni.ServerThreading.Settings.ActionsPorts.AddRange(_tempPort);
        }

        private SkeddedList<ushort> _tempPort = new SkeddedList<ushort>();
    }
}
