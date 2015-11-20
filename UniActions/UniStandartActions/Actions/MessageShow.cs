using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniStandartActions.Actions
{
    public partial class MessageShow : Form
    {
        private static MessageShow _messageShow;

        public static void SetMessage(string message)
        {
            if (_messageShow == null)
            {
                _messageShow = new MessageShow();
                _messageShow.Show();
            }

            _messageShow.BeginInvoke(new Action(() => {
                _messageShow.AddMessage(message);                
            }));
        }

        public MessageShow()
        {
            InitializeComponent();
        }

        private int _standartHeight;

        public void AddMessage(string str)
        {
            this.Visible = true;
            if (string.IsNullOrEmpty(this.lblContent.Text))
            {
                this.lblContent.Text = str;
                _standartHeight = this.Height;
            }
            else
            {
                this.Height += 33;
                this.lblContent.Text += "\r\n\r\n" + str;
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.lblContent.Text = "";
            this.Height = _standartHeight;
        }
    }
}
