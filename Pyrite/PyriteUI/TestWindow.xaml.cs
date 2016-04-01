using System.Windows;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.fff.IsInEditMode = !this.fff.IsInEditMode;
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
