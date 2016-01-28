using System.Windows.Controls;
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
            ControlsHelper.AppendOnlyInteger(tbSecondsBetweenActions, 0, TasksPool.TasksPoolSettings.Defaults.MaxSecondsBetweenActions);
        }
    }
}
