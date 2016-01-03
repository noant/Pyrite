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
using UniActionsClientIntefaces;
using UniActionsCore;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for WCreateAction.xaml
    /// </summary>
    public partial class WCreateAction : Window
    {
        public WCreateAction()
        {
            InitializeComponent();
            this.Background = App.WindowBackground;
            this.bUseInPool.Value = true;

            _item = new ActionItem();

            ProcessOkEnable();

            this.cbAction.SelectionChanged += (o, e) => {
                if (_blockSelectActions) return;
                if (this.cbAction.SelectedIndex != 0)
                {
                    var type = App.Uni.ModulesControl.CustomActions.Where(x =>
                        App.Uni.ModulesControl.GetViewName(x).Value == this.cbAction.SelectedItem.ToString()).Single();

                    _item.Action = App.Uni.ModulesControl.CreateActionInstance(type).Value;
                    if (_item.Action == null)
                        this.cbAction.SelectedIndex = 0;
                }
                else
                {
                    _item.Action = null;
                }
                ProcessOkEnable();
            };

            this.cbChecker.SelectionChanged += (o, e) =>
            {
                if (_blockSelectActions) return;
                if (this.cbChecker.SelectedIndex != 0)
                {
                    var type = App.Uni.ModulesControl.CustomCheckers.Where(x =>
                        App.Uni.ModulesControl.GetViewName(x).Value == this.cbChecker.SelectedItem.ToString()).Single();

                    _item.Checker = App.Uni.ModulesControl.CreateCheckerInstance(type).Value;
                    if (_item.Checker == null)
                        this.cbChecker.SelectedIndex = 0;
                }
                else _item.Checker = null;
                ProcessOkEnable();
            };

            this.tbName.TextChanged += (o, e) => {
                _item.Name = tbName.Text;
                ProcessOkEnable();
            };

            this.tbServerCommand.TextChanged += (o, e) => {
                _item.ServerCommand = tbServerCommand.Text;
                ProcessOkEnable();
            };

            this.cbCategory.SelectionChanged += (o, e) => {
                _item.Category = this.cbCategory.SelectedItem.ToString();
                tbCategory.Text = this.cbCategory.SelectedItem.ToString();
                ProcessOkEnable();
            };

            this.tbCategory.TextChanged += (o, e) =>
            {
                _item.Category = this.tbCategory.Text;
                ProcessOkEnable();                
            };

            this.bUseInPool.BoolChanged += (o) => {
                _item.IsActive = o.Value;
                ProcessOkEnable();
            };

            this.bUseInServer.BoolChanged += (o) =>
            {
                _item.UseServerThreading = o.Value;
                ProcessOkEnable();
            };

            this.btCancel.Click += (o, e) => {
                this.DialogResult = false;
            };

            this.btCreate.Click += (o, e) => {
                if (!this.IsEdit)
                {
                    var res = App.Uni.TasksPool.AddItem(_item);
                    if (res.Value)
                    {
                        App.Uni.CommitChanges();
                        this.DialogResult = true;
                    }
                }
                else
                {
                    var res = App.Uni.TasksPool.CheckItem(_item);

                    if (res.Value)
                    {
                        _tempItem.Action = _item.Action;
                        _tempItem.Category = _item.Category;
                        _tempItem.Checker = _item.Checker;
                        _tempItem.IsActive = _item.IsActive;
                        _tempItem.Name = _item.Name;
                        _tempItem.ServerCommand = _item.ServerCommand;
                        _tempItem.UseServerThreading = _item.UseServerThreading;
                        _tempItem.IsOnlyOnce = _item.IsOnlyOnce;
                        App.Uni.CommitChanges();
                        this.DialogResult = true;
                    }
                }
            };

            this.cbCategory.ItemsSource = App.Uni.TasksPool.ActionItems.
                Where(x => !string.IsNullOrEmpty(x.Category)).
                Select(x => x.Category).
                Distinct().
                OrderBy(x => x);

            this.cbAction.ItemsSource = new string[] { "-" }.Union(App.Uni.ModulesControl.CustomActions
                .Select(x => App.Uni.ModulesControl.GetViewName(x).Value));

            this.cbChecker.ItemsSource = new string[] { "-" }.Union(App.Uni.ModulesControl.CustomCheckers
                .Select(x => App.Uni.ModulesControl.GetViewName(x).Value));

            this.bOnlyOnce.BoolChanged += (o) => {
                _item.IsOnlyOnce = o.Value;
                ProcessOkEnable();
            };
        }

        private bool _isEdit;
        public bool IsEdit {
            get {
                return _isEdit;
            }
            set {
                _isEdit = value;
                if (value)
                {
                    btCreate.Content = "Применить";
                    this.Title = "Редактирование сценария";
                }
                else
                {
                    btCreate.Content = "Создать";
                    this.Title = "Создание сценария";
                }
            }
        }

        private ActionItem _tempItem;
        public void SetItem(ActionItem item)
        {
            if (!this.IsEdit)
                throw new Exception("IsEdit is false");

            _blockSelectActions = true;

            _tempItem = item;
            _item = item.Clone();

            this.cbAction.SelectedItem = _item.Action.Name;
            this.cbCategory.Text = _item.Category;
            this.cbChecker.SelectedItem = _item.Checker.Name;
            this.tbName.Text = _item.Name;
            this.tbServerCommand.Text = _item.ServerCommand;
            this.bUseInServer.Value = _item.UseServerThreading;
            this.bUseInPool.Value = _item.IsActive;
            this.bOnlyOnce.Value = _item.IsOnlyOnce;

            _blockSelectActions = false;
        }

        private ActionItem _item;
        bool _blockSelectActions = false;

        private void ProcessOkEnable()
        {
            var result = App.Uni.TasksPool.CheckItem(_item);
            if (result.Value)
            {
                lblStatus.Content = "";
                EnableOk();
            }
            else
            {
                var str = "";
                foreach (var exception in result.Exceptions)
                    str += exception.Message + "\r\n";

                lblStatus.Content = str;

                DisableOk();
            }
        }

        private void EnableOk()
        {
            this.btCreate.IsEnabled = true;
        }

        private void DisableOk()
        {
            this.btCreate.IsEnabled = false;
        }
    }

}
