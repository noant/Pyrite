using System.Windows.Controls;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for CSettings.xaml
    /// </summary>
    public partial class SettingsView : UserControl, ControlsHelper.IRefreshable, ControlsHelper.IConfirm
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        public void Confirm()
        {
            ipList.Confirm();
            portList.Confirm();
        }

        public void Refresh()
        {
            ipList.Refresh();
            portList.Refresh();
        }
    }
}
