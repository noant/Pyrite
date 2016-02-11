using System.Windows.Controls;

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

            ControlsHelper.AppendOnlyInteger(tbPort, 0, ushort.MaxValue);

            btAdd.Click += (o, e) =>
            {
                App.Uni.ServerThreading.Settings.ActionsPorts.Add(tbPort.GetUShort());
                ProcessButtonsEnabled();
                Refresh();
            };

            btDelete.Click += (o, e) =>
            {
                var portToDelete = ushort.Parse(listPort.SelectedItem.ToString());
                App.Uni.ServerThreading.Settings.ActionsPorts.RemoveAll(x =>
                    x == portToDelete
                    );
                Refresh();
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
            btAdd.IsEnabled = App.Uni.ServerThreading.Settings.ActionsPorts.CanAdd(port);
        }

        public void Refresh()
        {
            listPort.Items.Clear();
            foreach (var port in App.Uni.ServerThreading.Settings.ActionsPorts)
                listPort.Items.Add(port);
            ProcessButtonsEnabled();
        }
    }
}
