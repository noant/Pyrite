using System;
using System.Net;
using System.Windows.Controls;

namespace PyriteUI
{
    /// <summary>
    /// Interaction logic for IpBox.xaml
    /// </summary>
    public partial class IpBox : UserControl
    {
        public IpBox()
        {
            InitializeComponent();
            ControlsHelper.AppendOnlyInteger(tbNum1, 0, 255);
            ControlsHelper.AppendOnlyInteger(tbNum2, 0, 255);
            ControlsHelper.AppendOnlyInteger(tbNum3, 0, 255);
            ControlsHelper.AppendOnlyInteger(tbNum4, 0, 255);

            this.tbNum1.TextChanged += (o, e) => {
                if (IpChanged != null)
                    IpChanged(this);
            }; 
            this.tbNum2.TextChanged += (o, e) =>
            {
                if (IpChanged != null)
                    IpChanged(this);
            }; 
            this.tbNum3.TextChanged += (o, e) =>
            {
                if (IpChanged != null)
                    IpChanged(this);
            }; 
            this.tbNum4.TextChanged += (o, e) =>
            {
                if (IpChanged != null)
                    IpChanged(this);
            };
        }
        public IPAddress Ip
        {
            get
            {
                return new IPAddress(new byte[] {(byte)tbNum1.GetInt(), (byte)tbNum2.GetInt(), (byte)tbNum3.GetInt(), (byte)tbNum4.GetInt() });
            }
        }
        public void RemoveIp() {
            tbNum1.Text = tbNum2.Text = tbNum3.Text = tbNum4.Text = "0";
        }

        public event Action<IpBox> IpChanged;
    }
}
