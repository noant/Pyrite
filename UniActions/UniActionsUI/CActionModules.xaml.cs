﻿using System.Linq;
using System.Windows.Forms;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CActionModules.xaml
    /// </summary>
    public partial class CActionModules : System.Windows.Controls.UserControl, ControlsHelper.IRefreshable
    {
        public CActionModules()
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
            this.listModules.ItemsSource = App.Uni.ModulesControl.CustomActions.Where(x =>
                !App.Uni.ModulesControl.IsStandart(x)
                ).Select(x=> 
                    {
                        return App.Uni.ModulesControl.GetViewName(x).Value;
                    }
                );
        }

        public void Remove()
        {
            if (this.listModules.SelectedItem == null)
                return;

            var name = this.listModules.SelectedItem.ToString();

            App.Uni.ModulesControl.RemoveAction(
                App.Uni.ModulesControl.CustomActions
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
                var types = App.Uni.ModulesControl.RegisterAction(ofd.FileName).Value.ToList();
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
