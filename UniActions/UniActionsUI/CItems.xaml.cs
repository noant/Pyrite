using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CItems.xaml
    /// </summary>
    public partial class CItems : Grid, ControlsHelper.IRefreshable
    {
        public CItems()
        {
            InitializeComponent();

            Refresh();
        }

        public void Run(int number)
        {
            foreach (var it in this.spItems.Children)
            {
                if (it is CItem)
                {
                    var item = (CItem)it;
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
            foreach (var item in this.spItems.Children)
                if (item is CItem)
                {
                    ((CItem)item).Activate();
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
            this.spItems.Children.Clear();

            int num = 1;

            foreach (var category in App.Uni.TasksPool.ActionItems.Where(x => 
                (x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand)) || !this.ShowOnlyServerActions)
                .Select(x=>x.Category).Distinct().OrderBy(x=>x))
            {
                if (!string.IsNullOrEmpty(category) && category.Length > 0)
                {
                    var lbl = new Label();
                    lbl.Foreground = Brushes.Gray;
                    lbl.Content = category;
                    this.spItems.Children.Add(lbl);
                }

                foreach (var item in App.Uni.TasksPool.ActionItems.Where(x => 
                    (x.UseServerThreading && !string.IsNullOrEmpty(x.ServerCommand)) || !this.ShowOnlyServerActions)
                    .Where(x => x.Category == category))
                {
                    var cItem = new CItem();
                    cItem.Number = num++;
                    cItem.ActionItem = item;
                    cItem.Clicked += () => {
                        if (this.Clicked != null)
                            this.Clicked();
                    };
                    this.spItems.Children.Add(cItem);
                }
            }
        }

        public bool IsMoreThenZero
        {
            get
            {
                return this.spItems.Children.Count>0;
            }
        }

        public event Clicked Clicked;
    }
}
