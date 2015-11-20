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
    /// Interaction logic for CBoolSelect.xaml
    /// </summary>
    public partial class CBoolSelect : UserControl
    {
        public CBoolSelect()
        {
            InitializeComponent();

            rbNo.GroupName =
                rbYes.GroupName = Guid.NewGuid().ToString();

            rbNo.Checked += (o, e) => {
                if (BoolChanged != null)
                    BoolChanged(this);
            };
            rbYes.Checked += (o, e) =>
            {
                if (BoolChanged != null)
                    BoolChanged(this);
            };            
        }
        public event BoolChanged BoolChanged;
        public bool Value
        {
            get
            {
                return rbYes.IsChecked.Value;
            }
            set
            {
                rbYes.IsChecked = value;
            }
        }
    }

    public delegate void BoolChanged(CBoolSelect c);
}
