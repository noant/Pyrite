using PyriteUI;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

public partial class TabControlStyles
{
    public void SelectedTabChanged(object sender, RoutedEventArgs e)
    {
        var tabControl = (TabControl)sender;

        if (tabControl.SelectedIndex == -1) return;
        var contentControl = ((TabItem)tabControl.Items[tabControl.SelectedIndex]).Content;

        if (!_tempTabsSelected.ContainsKey(tabControl))
        {
            _tempTabsSelected.Add(tabControl, contentControl);
        }

        if (_tempTabsSelected[tabControl] != contentControl)
            _tempTabsSelected[tabControl] = contentControl;
        else return;

        if (contentControl is ControlsHelper.IRefreshable)
            ((ControlsHelper.IRefreshable)contentControl).Refresh();
        else if (contentControl is TabControl)
            SelectedTabChanged(contentControl, e);
        else if (contentControl is Grid && ((Grid)contentControl).Children.OfType<TabControl>().Count() > 0)
            foreach (var c in ((Grid)contentControl).Children.OfType<TabControl>())
            {
                SelectedTabChanged(c, e);
            }
    }

    private static Dictionary<TabControl, object> _tempTabsSelected = new Dictionary<TabControl, object>();
}
