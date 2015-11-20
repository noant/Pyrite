using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CSavior.xaml
    /// </summary>
    public partial class CSavior : UserControl
    {
        public CSavior()
        {
            InitializeComponent();
            this.btRestart.Click += (o, e) => {
                var res1 = V.Process(UniActionsCore.SAL.Save());
                var res2 = V.Process(UniActionsCore.Actions.ReIntialize());

                if (res1.Exceptions.Count()==0 && res2.Exceptions.Count()==0)
                    MessageBox.Show("Потоки сервера и поток задач перезапущены.");
            };
            this.btClose.Click += (o, e) => {
                App.Current.Shutdown();
            };
        }
    }
}
