using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for WFast.xaml
    /// </summary>
    public partial class WFast : Window
    {
        public WFast()
        {
            InitializeComponent();
            this.KeyDown += (o, e) =>
            {
                if (e.Key == Key.Escape)
                    this.Close();
                else if (e.Key == Key.Up || e.Key == Key.Down)
                {
                    this.cItems.SelectFirstControl();
                }
                else
                    Run(e.Key);
            };
            this.cItems.Clicked += (o, e) => this.Close();
        }

        private void Run(Key key)
        {
            var keyStr = key.ToString();
            int num;
            if (keyStr[0] == 'F' && int.TryParse(keyStr.Replace("F", ""), NumberStyles.Number, CultureInfo.InvariantCulture, out num))
            {
                cItems.Run(num);
                Close();
            }
        }

        public bool IsClosed { get; private set; }
        public new void Close()
        {
            if (IsClosed) return;
            IsClosed = true;
            try
            {
                base.Close();
            }
            catch { }
        }

        public bool Refresh()
        {
            cItems.ShowOnlyServerActions = true;
            cItems.Refresh();
            return cItems.IsMoreThenZero;
        }

        private void gridBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
