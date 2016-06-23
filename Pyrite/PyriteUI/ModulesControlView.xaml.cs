using Microsoft.Win32;
using PyriteCore.ScenarioCreation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for ModulesControlView.xaml
    /// </summary>
    public partial class ModulesControlView : UserControl, ControlsHelper.IRefreshable
    {
        public ModulesControlView()
        {
            InitializeComponent();

            this.btAdd.Click += (o, e) => Add();
            this.btDelete.Click += (o, e) => Delete();

            this.lvDlls.SelectionChanged += (o, e) =>
            {
                var dllLocation = lvDlls.SelectedItem != null ? lvDlls.SelectedItem.ToString() : string.Empty;
                this.lvActionModules.ItemsSource =
                    App.Pyrite.ModulesControl.CustomActions.Where(x => x.Assembly.Location.Equals(dllLocation));
                this.lvCheckerModules.ItemsSource =
                    App.Pyrite.ModulesControl.CustomCheckers.Where(x => x.Assembly.Location.Equals(dllLocation));
            };
        }

        public void Refresh()
        {
            var allDlls =
                (from checker in App.Pyrite.ModulesControl.CustomCheckers
                 where !App.Pyrite.ModulesControl.IsStandart(checker)
                 select checker.Assembly.Location).Union
                (from action in App.Pyrite.ModulesControl.CustomActions
                 where !App.Pyrite.ModulesControl.IsStandart(action)
                 select action.Assembly.Location).Distinct();

            this.lvDlls.ItemsSource = allDlls;
        }

        private void Add()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".dll";
            ofd.Filter = "*.dll|*.dll";

            if (ofd.ShowDialog() == true)
            {
                var types =
                    App.Pyrite.ModulesControl.RegisterChecker(ofd.FileName).Value.ToList().Union(
                        App.Pyrite.ModulesControl.RegisterAction(ofd.FileName).Value.ToList()
                        );
                if (types.Count() == 0)
                {
                    System.Windows.MessageBox.Show("Ни одного типа не зарегистрировано");
                }
                else
                {
                    var str = "Зарегистрированы следующие типы: ";
                    foreach (var typeName in types.Select(x => x.Name))
                    {
                        str += "\r\n" + typeName;
                    }
                    System.Windows.MessageBox.Show(str);
                }
            }

            Refresh();

            lvDlls.SelectedItem = ofd.FileName;

            App.Pyrite.CommitChanges();
        }

        private void Delete()
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данный модуль? Все связанные действия будут удалены из всех сценариев.", "Удаление модуля", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                var dllName = lvDlls.SelectedItem.ToString();

                var checkers = App.Pyrite.ModulesControl.CustomCheckers.Where(x => x.Assembly.Location.Equals(dllName)).ToArray();
                var actions = App.Pyrite.ModulesControl.CustomActions.Where(x => x.Assembly.Location.Equals(dllName)).ToArray();

                var scenariosToRefresh = new List<Scenario>();

                foreach (var checker in checkers)
                    scenariosToRefresh.AddRange(App.Pyrite.ModulesControl.RemoveChecker(checker).Value);
                foreach (var action in actions)
                    scenariosToRefresh.AddRange(App.Pyrite.ModulesControl.RemoveAction(action).Value);

                foreach (var scenario in scenariosToRefresh.Distinct())
                    scenario.Refresh();

                App.Pyrite.CommitChanges();

                Refresh();
            }
        }
    }
}
