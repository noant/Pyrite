using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CActionModules.xaml
    /// </summary>
    public partial class CCheckerModules : System.Windows.Controls.UserControl, ControlsHelper.IRefreshable
    {
        public CCheckerModules()
        {
            InitializeComponent();
            Refresh();

            this.btAdd.Click += (o, e) => {
                Add();
            };

            this.btDelete.Click += (o, e) => {
                Remove();
            };
        }

        public void Refresh()
        {
            this.listModules.ItemsSource = App.Uni.ModulesControl.CustomCheckers.Where(x =>
                !App.Uni.ModulesControl.IsStandart(x)
                ).Select(x=>
                    App.Uni.ModulesControl.GetViewName(x).Value
                );
        }

        public void Remove()
        {
            if (this.listModules.SelectedItem == null)
                return;

            var name = this.listModules.SelectedItem.ToString();

            App.Uni.ModulesControl.RemoveChecker(
                App.Uni.ModulesControl.CustomCheckers
                .Single(x => App.Uni.ModulesControl.GetViewName(x).Value == name)
                );
            Refresh();
        }

        public void Add()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".dll";
            ofd.Filter = "*.dll|*.dll";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var types = App.Uni.ModulesControl.RegisterChecker(ofd.FileName).Value.ToList();
                if (types.Count() == 0)
                {
                    System.Windows.MessageBox.Show("Ни одного типа не зарегистрировано");
                }
                else
                {
                    var str = "Зарегистрированы следующие типы: ";
                    foreach (var typeName in types.Select(x=>x.Name))
                    {
                        str += "\r\n" + typeName;
                    }
                    System.Windows.MessageBox.Show(str);
                }
            }

            Refresh();
        }
    }
}
