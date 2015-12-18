using System.Windows;
using System.Windows.Input;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for WFast.xaml
    /// </summary>
    public partial class WFast : Window
    {
        public WFast()
        {
            InitializeComponent();
            this.KeyDown += (o, e) => {
                if (e.Key == Key.Escape)
                    this.Close();
                else if (e.Key == Key.Up || e.Key == Key.Down)
                {
                    this.cItems.SelectFirstControl();
                }
                else
                    Run(e.Key);
            };
            this.ShowActivated = true;
            this.cItems.Clicked += () => this.Close();
            this.Deactivated += (o, e) => this.Close();
            this.gridBg.MouseDown += (o, e) => this.Close();
        }

        private void Run(Key key)
        {
            var keyStr = key.ToString();
            int num;
            if (keyStr[0] == 'F' && int.TryParse(keyStr.Replace("F", ""), out num))
            {
                cItems.Run(num);
            }
        }

        public bool IsClosed { get; private set; }
        public new void Close() {
            if (IsClosed) return;
            IsClosed = true;
            try
            {
                base.Close();
            }
            catch {}
        }

        public bool Refresh()
        {
            cItems.ShowOnlyServerActions = true;
            cItems.Refresh();
            return cItems.IsMoreThenZero;
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Label_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
                this.Close();
        }
    }
}
