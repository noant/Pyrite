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
using UniActionsCore.ScenarioCreation;

namespace UniActionsUI.ScenarioCreation
{
    /// <summary>
    /// Interaction logic for ActionView.xaml
    /// </summary>
    public partial class ActionView : EditableUserControl
    {
        public ActionView(ActionBag actionBag)
        {
            if (actionBag == null)
                return;

            var context = new ActionViewContext(actionBag);
            this.DataContext = context;
            InitializeComponent();

            context.Changed += (o, e) => RaiseChanged();

            this.tbParams.MouseLeftButtonDown += (o, e) =>
            {
                context.BeginActionUserSettings();
            };

            this.btCheck.Click += (o, e) =>
            {
                context.ExecuteCurrentAction();
            };

            this.btDelete.Click += (o, e) =>
            {
                RaiseRemove();
            };

            this.KeyDown += (o, e) =>
            {
                if (e.Key == Key.Delete)
                    RaiseRemove();
            };
        }

        public void RaiseRemove()
        {
            if (Utils.IsUserSureToDeleteCurrentOperator())
                if (Remove != null)
                {
                    Remove(this, new EventArgs());
                    RaiseChanged();
                }
        }

        public event Action<object, EventArgs> Remove;

        public void RaiseChanged()
        {
            if (Changed != null)
                Changed(this, new EventArgs());
        }

        public event Action<object, EventArgs> Changed;
    }
}
