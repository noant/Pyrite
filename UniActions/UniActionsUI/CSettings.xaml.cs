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
    /// Interaction logic for CSettings.xaml
    /// </summary>
    public partial class CSettings : UserControl, ControlsHelper.IRefreshable
    {
        public CSettings()
        {
            InitializeComponent();
            Refresh();
            
            tbPort.TextChanged += (o, e) => {
                App.Uni.ServerThreading.Settings.DistributionPort = tbPort.GetUShort();
            };

            tbSecondsBetweenActions.TextChanged += (o, e) => {
                App.Uni.TasksPool.Settings.SecondsBetweenActions = tbSecondsBetweenActions.GetInt();
            };

            cbResolveAllIp.SelectionChanged += (o, e) => {
                App.Uni.ServerThreading.Settings.ResolveAllIp = cbResolveAllIp.SelectedIndex == 0;
            };            
        }

        public void Refresh()
        {
            tbPort.Text = App.Uni.ServerThreading.Settings.DistributionPort.ToString();
            tbSecondsBetweenActions.Text = App.Uni.TasksPool.Settings.SecondsBetweenActions.ToString();
            cbResolveAllIp.SelectedIndex = App.Uni.ServerThreading.Settings.ResolveAllIp ? 0 : 1;

            ControlsHelper.AppendOnlyInteger(tbPort, 0 , 255*255 -1);
            ControlsHelper.AppendOnlyInteger(tbSecondsBetweenActions, TasksPool.TasksPoolSettings.Defaults.MaxSecondsBetweenActions, 0);
        }
    }
}
