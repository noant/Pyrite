using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                if (!UniActionsCore.ServerThreading.Settings.ResolvedIp.Contains(ip))
                {
                    UniActionsCore.ServerThreading.Settings.ResolvedIp.Add(ip);
                    listIp.Items.Add(ip);
                }
                ProcessButtonsEnabled();
            };

            this.btDelete.Click += (o, e) => {
                var ip = listIp.SelectedItem;
                if (ip != null)
                {
                    listIp.Items.Remove(ip);
                    UniActionsCore.ServerThreading.Settings.ResolvedIp.Remove((IPAddress)ip);
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
            this.btAdd.IsEnabled = !UniActionsCore.ServerThreading.Settings.ResolvedIp.Any(x => x.Equals(ip));
            btDelete.IsEnabled = listIp.Items.Count > 1 && listIp.SelectedIndex != -1;
        }

        public void Refresh()
        {
            listIp.Items.Clear();
            foreach (var ip in UniActionsCore.ServerThreading.Settings.ResolvedIp)
                listIp.Items.Add(ip);
            ProcessButtonsEnabled();
        }
    }
}
