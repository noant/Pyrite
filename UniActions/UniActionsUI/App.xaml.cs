using System.Linq;
using System.Windows;
using System.Windows.Media;
using UniActionsCore;
using UniActionsCore.ScenarioCreation;

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
                        {
                            MessageBox.Show("Критическая ошибка. Программа будет выключена");
                            App.Current.Shutdown();
                        }
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
            foreach (var item in Uni.ScenariosPool.Scenarios)
                item.AfterAction += (x) =>
                {
                    if (ItemExecuted != null)
                        ItemExecuted(item);
                };

            Starter.Initialize();
        }

        public static readonly Brush WindowBackground = new SolidColorBrush(SystemColors.ControlColor);

        public static event ActionItemExecuted ItemExecuted;

    }

    public delegate void ActionItemExecuted(Scenario item);
}
