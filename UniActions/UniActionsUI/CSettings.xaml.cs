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
    /// Interaction logic for CSettings.xaml
    /// </summary>
    public partial class CSettings : UserControl, ControlsHelper.IRefreshable
    {
        public CSettings()
        {
            InitializeComponent();
            Refresh();
            
            tbPort.TextChanged += (o, e) => {
                UniActionsCore.ServerThreading.Settings.ServerListenerPort = tbPort.GetInt();
            };

            tbSecondsBetweenActions.TextChanged += (o, e) => {
                UniActionsCore.Pool.Settings.SecondsBetweenActions = tbSecondsBetweenActions.GetInt();
            };

            tbThreadsCnt.TextChanged += (o, e) => {
                UniActionsCore.ServerThreading.Settings.ServerThreadCount = tbThreadsCnt.GetInt();
            };

            cbResolveAllIp.SelectionChanged += (o, e) => {
                UniActionsCore.ServerThreading.Settings.ResolveAll = cbResolveAllIp.SelectedIndex == 0;
            };            
        }

        public void Refresh()
        {             
            tbPort.Text = UniActionsCore.ServerThreading.Settings.ServerListenerPort.ToString();
            tbThreadsCnt.Text = UniActionsCore.ServerThreading.Settings.ServerThreadCount.ToString();
            tbSecondsBetweenActions.Text = UniActionsCore.Pool.Settings.SecondsBetweenActions.ToString();
            cbResolveAllIp.SelectedIndex = UniActionsCore.ServerThreading.Settings.ResolveAll ? 0 : 1;

            ControlsHelper.AppendOnlyInteger(tbPort, 0 , 255*255 -1);
            ControlsHelper.AppendOnlyInteger(tbSecondsBetweenActions, UniActionsCore.Pool.Settings.Default.MaxSecondsBetweenActions, 0);
            ControlsHelper.AppendOnlyInteger(tbThreadsCnt, 60, 1);
        }
    }
}
