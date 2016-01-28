using System.Linq;
using System.Net;
using System.Windows.Controls;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CIpList.xaml
    /// </summary>
    public partial class CIpList : UserControl, ControlsHelper.IRefreshable
    {
        public CIpList()
        {
            InitializeComponent();
            Refresh();

            this.btAdd.Click += (o, e) => {
                var ip = tbIp.Ip;
                tbIp.RemoveIp();
                if (!App.Uni.ServerThreading.Settings.ResolvedIp.Contains(ip))
                {
                    App.Uni.ServerThreading.Settings.ResolvedIp.Add(ip);
                    listIp.Items.Add(ip);
                }
                ProcessButtonsEnabled();
            };

            this.btDelete.Click += (o, e) => {
                var ip = listIp.SelectedItem;
                if (ip != null)
                {
                    listIp.Items.Remove(ip);
                    App.Uni.ServerThreading.Settings.ResolvedIp.Remove((IPAddress)ip);
                }
                ProcessButtonsEnabled();
            };

            this.listIp.SelectionChanged += (o, e) => {
                ProcessButtonsEnabled();
            };

            this.tbIp.IpChanged += (tb) => {
                ProcessButtonsEnabled();
            };

            ProcessButtonsEnabled();
        }

        void ProcessButtonsEnabled()
        { 
            var ip = tbIp.Ip;
            this.btAdd.IsEnabled = !App.Uni.ServerThreading.Settings.ResolvedIp.Any(x => x.Equals(ip));
            btDelete.IsEnabled = listIp.Items.Count > 1 && listIp.SelectedIndex != -1;
        }

        public void Refresh()
        {
            listIp.Items.Clear();
            foreach (var ip in App.Uni.ServerThreading.Settings.ResolvedIp)
                listIp.Items.Add(ip);
            ProcessButtonsEnabled();
        }
    }
}
