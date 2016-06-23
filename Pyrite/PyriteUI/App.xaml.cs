using System;
using PyriteCore;
using PyriteCore.ScenarioCreation;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Pyrite Pyrite { get; private set; }
        public App()
        {
            //#if !DEBUG
            //            var splash = new SplashScreen("Images/PyriteMedium.png");
            //            splash.Show(false,true);
            //#endif

            this.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;

            Resulting.NeedShutdown += () =>
                {
                    try
                    {
                        lock (App.Current)
                        {
                            MessageBox.Show("Критическая ошибка. Программа будет выключена");
                            App.Current.Shutdown();
                        }
                    }
                    catch { }
                };

            Resulting.WarningHandler += (warns) =>
            {
                foreach (var warn in warns)
                    MessageBox.Show(warn.Message, "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            Pyrite = Pyrite.Create().Value;
            foreach (var item in Pyrite.ScenariosPool.Scenarios)
                item.AfterAction += (x) =>
                {
                    RaiseItemExecutedEvent(item);
                };

            Starter.Initialize();
            //#if !DEBUG
            //            splash.Close(new TimeSpan(0,0,1));
            //#endif
        }

        public static readonly Brush WindowBackground = new SolidColorBrush(SystemColors.ControlColor);

        public static event ActionItemExecuted ItemExecuted;

        public static void RaiseItemExecutedEvent(Scenario scenario)
        {
            if (ItemExecuted != null)
                ItemExecuted(scenario);
        }
    }

    public delegate void ActionItemExecuted(Scenario item);
}
