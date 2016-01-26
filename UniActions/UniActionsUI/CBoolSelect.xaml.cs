using System;
using System.Windows.Controls;

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
                rbNo.IsChecked = !value;
            }
        }
    }

    public delegate void BoolChanged(CBoolSelect c);
}
