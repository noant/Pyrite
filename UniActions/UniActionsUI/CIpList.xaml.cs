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
            };

            this.btDelete.Click += (o, e) => {
                var ip = listIp.SelectedItem;
                if (ip != null)
                {
                    listIp.Items.Remove(ip);
                    UniActionsCore.ServerThreading.Settings.ResolvedIp.Remove(ip.ToString());
                }
            };
        }

        public void Refresh()
        {
            listIp.Items.Clear();
            foreach (var ip in UniActionsCore.ServerThreading.Settings.ResolvedIp)
            {
                listIp.Items.Add(ip);
            }
        }
    }
}
