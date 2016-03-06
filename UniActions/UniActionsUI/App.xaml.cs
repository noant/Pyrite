using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UniActionsClientIntefaces;
using UniActionsCore;
using UniActionsCore.ScenarioCreation;
using UniStandartActions.Actions;
using UniStandartActions.Checkers;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Uni Uni { get; private set; }
        public App()
        {
            this.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;

            Resulting.NeedShutdown += () =>
                {
                    try
                    {
                        lock (App.Current)
                            App.Current.Shutdown();
                    }
                    catch { }
                };
            Resulting.CriticalHandler += (exceptions) =>
            {
                if (exceptions.Count() != 0)
                {
                    var message = "Выброшены следующие ошибки:\r\n";
                    foreach (var e in exceptions)
                        message += e.Message + "\r\n";

                    MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            Uni = Uni.Create().Value;
            foreach (var item in Uni.TasksPool.Scenarios)
                item.AfterAction += (x) =>
                {
                    if (ItemExecuted != null)
                        ItemExecuted(item);
                };

            Starter.Initialize();
        }

        public static Brush WindowBackground = new SolidColorBrush(SystemColors.ControlColor);

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

        public static event ActionItemExecuted ItemExecuted;

        private static Dictionary<TabControl, object> _tempTabsSelected = new Dictionary<TabControl, object>();
    }

    public delegate void ActionItemExecuted(Scenario item);
}
