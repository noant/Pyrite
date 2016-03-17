using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CItems.xaml
    /// </summary>
    public partial class CRunScenariosItems : Grid, ControlsHelper.IRefreshable
    {
        public CRunScenariosItems()
        {
            InitializeComponent();

            Refresh();
        }

        public void Run(int number)
        {
            foreach (var control in this.spItems.Children)
            {
                if (control is CRunScenario)
                {
                    var item = (CRunScenario)control;
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
                if (control is CRunScenario)
                {
                    ((CRunScenario)control).Focus();
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
                        if (control is CRunScenario)
                            ((CRunScenario)control).Refresh();
                    }
                }));
            });

            App.ItemExecuted += actionUpdateAll;

            this.Unloaded += (o, e) => App.ItemExecuted -= actionUpdateAll;

            this.spItems.Children.Clear();

            int num = 1;

            foreach (var category in App.Uni.TasksPool.Scenarios.Where(x =>
                (x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand)) || !this.ShowOnlyServerActions)
                .Select(x => x.Category).Distinct().OrderBy(x => x))
            {
                if (!string.IsNullOrEmpty(category) && category.Length > 0)
                {
                    var lbl = new Label();
                    lbl.Foreground = Brushes.Gray;
                    lbl.Content = category;
                    this.spItems.Children.Add(lbl);
                }

                foreach (var item in App.Uni.TasksPool.Scenarios.Where(x =>
                    (x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand)) || !this.ShowOnlyServerActions)
                    .Where(x => x.Category == category).OrderBy(x => x.Index))
                {
                    var cRunScenario = new CRunScenario();
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
