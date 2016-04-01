using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for CItems.xaml
    /// </summary>
    public partial class RunScenariosItemsView : Grid, ControlsHelper.IRefreshable
    {
        public RunScenariosItemsView()
        {
            InitializeComponent();

            Refresh();
        }

        public void Run(int number)
        {
            foreach (var control in this.spItems.Children)
            {
                if (control is RunScenarioView)
                {
                    var item = (RunScenarioView)control;
                    var num = item.Number;
                    if (num == number)
                    {
                        item.Run();
                        break;
                    }
                }
            }
        }

        public void SelectFirstControl()
        {
            foreach (var control in this.spItems.Children)
                if (control is RunScenarioView)
                {
                    ((RunScenarioView)control).Focus();
                    break;
                }
        }

        public bool ShowOnlyServerActions
        {
            get;
            set;
        }

        public void Refresh()
        {
            var actionUpdateAll = new ActionItemExecuted((x) =>
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (var control in this.spItems.Children)
                    {
                        if (control is RunScenarioView)
                            ((RunScenarioView)control).Refresh();
                    }
                }));
            });

            App.ItemExecuted += actionUpdateAll;

            this.Unloaded += (o, e) => App.ItemExecuted -= actionUpdateAll;

            this.spItems.Children.Clear();

            int num = 1;

            foreach (var category in App.Pyrite.ScenariosPool.Scenarios.Where(x =>
                (x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand)) || !this.ShowOnlyServerActions)
                .Select(x => x.Category).Distinct().OrderBy(x => x))
            {
                if (!string.IsNullOrEmpty(category))
                {
                    var lbl = new Label();
                    lbl.Foreground = Brushes.Gray;
                    lbl.Content = category;
                    this.spItems.Children.Add(lbl);
                }

                foreach (var item in App.Pyrite.ScenariosPool.Scenarios.Where(x =>
                    (x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand)) || !this.ShowOnlyServerActions)
                    .Where(x => x.Category == category).OrderBy(x=>x.Name).OrderBy(x => x.Index))
                {
                    var cRunScenario = new RunScenarioView();
                    cRunScenario.Number = num++;
                    cRunScenario.Scenario = item;
                    cRunScenario.NeedClose += (o, e) =>
                    {
                        if (this.Clicked != null)
                            this.Clicked(this, new EventArgs());
                    };

                    this.spItems.Children.Add(cRunScenario);
                }
            }
        }

        public bool IsMoreThenZero
        {
            get
            {
                return this.spItems.Children.Count > 0;
            }
        }

        public event Action<object, EventArgs> Clicked;
    }
}
