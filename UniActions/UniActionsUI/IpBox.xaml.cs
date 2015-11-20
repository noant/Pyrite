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
    /// Interaction logic for IpBox.xaml
    /// </summary>
    public partial class IpBox : UserControl
    {
        public IpBox()
        {
            InitializeComponent();
            ControlsHelper.AppendOnlyInteger(tbNum1, 255, 0);
            ControlsHelper.AppendOnlyInteger(tbNum2, 255, 0);
            ControlsHelper.AppendOnlyInteger(tbNum3, 255, 0);
            ControlsHelper.AppendOnlyInteger(tbNum4, 255, 0);
        }
        public string Ip
        {
            get
            {
                return tbNum1.GetInt() + "." + tbNum2.GetInt() + "." + tbNum3.GetInt() + "." + tbNum4.GetInt();
            }
        }
        public void RemoveIp() {
            tbNum1.Text = tbNum2.Text = tbNum3.Text = tbNum4.Text = "0";
        }
    }
}
