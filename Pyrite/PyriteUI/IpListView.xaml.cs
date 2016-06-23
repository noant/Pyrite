using PyriteCore;
using System.Linq;
using System.Net;
using System.Windows.Controls;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for CIpList.xaml
    /// </summary>
    public partial class IpListView : UserControl, ControlsHelper.IRefreshable, ControlsHelper.IConfirm
    {
        public IpListView()
        {
            InitializeComponent();
            Refresh();

            this.btAdd.Click += (o, e) =>
            {
                var ip = tbIp.Ip;
                tbIp.RemoveIp();
                if (!_tempList.Contains(ip))
                {
                    _tempList.Add(ip);
                    listIp.Items.Add(ip);
                }
                ProcessButtonsEnabled();
            };

            this.btDelete.Click += (o, e) =>
            {
                var ip = listIp.SelectedItem;
                if (ip != null)
                {
                    listIp.Items.Remove(ip);
                    _tempList.Remove((IPAddress)ip);
                }
                ProcessButtonsEnabled();
            };

            this.listIp.SelectionChanged += (o, e) =>
            {
                ProcessButtonsEnabled();
            };

            this.tbIp.IpChanged += (tb) =>
            {
                ProcessButtonsEnabled();
            };

            ProcessButtonsEnabled();
        }

        void ProcessButtonsEnabled()
        {
            var ip = tbIp.Ip;
            this.btAdd.IsEnabled = !_tempList.Any(x => x.Equals(ip));
            btDelete.Visibility = listIp.SelectedIndex != -1
                ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public void Refresh()
        {
            bsResolveAllIp.Value = App.Pyrite.ServerThreading.Settings.ResolveAllIp;
            _tempList = new SkeddedList<IPAddress>();
            _tempList.AddRange(App.Pyrite.ServerThreading.Settings.ResolvedIp);
            listIp.Items.Clear();
            foreach (var ip in _tempList)
                listIp.Items.Add(ip);
            ProcessButtonsEnabled();
        }

        public void Confirm()
        {
            App.Pyrite.ServerThreading.Settings.ResolveAllIp = bsResolveAllIp.Value;
            App.Pyrite.ServerThreading.Settings.ResolvedIp.Clear();
            App.Pyrite.ServerThreading.Settings.ResolvedIp.AddRange(_tempList);
        }

        private SkeddedList<IPAddress> _tempList = new SkeddedList<IPAddress>();
    }
}
