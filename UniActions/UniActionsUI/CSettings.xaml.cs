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
                UniActionsCore.ServerThreading.Settings.DistributionPort = tbPort.GetUShort();
            };

            tbSecondsBetweenActions.TextChanged += (o, e) => {
                UniActionsCore.Pool.Settings.SecondsBetweenActions = tbSecondsBetweenActions.GetInt();
            };

            cbResolveAllIp.SelectionChanged += (o, e) => {
                UniActionsCore.ServerThreading.Settings.ResolveAllIp = cbResolveAllIp.SelectedIndex == 0;
            };            
        }

        public void Refresh()
        {             
            tbPort.Text = UniActionsCore.ServerThreading.Settings.DistributionPort.ToString();
            tbSecondsBetweenActions.Text = UniActionsCore.Pool.Settings.SecondsBetweenActions.ToString();
            cbResolveAllIp.SelectedIndex = UniActionsCore.ServerThreading.Settings.ResolveAllIp ? 0 : 1;

            ControlsHelper.AppendOnlyInteger(tbPort, 0 , 255*255 -1);
            ControlsHelper.AppendOnlyInteger(tbSecondsBetweenActions, UniActionsCore.Pool.Settings.Default.MaxSecondsBetweenActions, 0);
        }
    }
}
