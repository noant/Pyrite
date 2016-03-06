using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace UniStandartActions.Actions
{
    public partial class MessageShow : Form
    {
        private static int _startupHeight = 116;
        private static MessageShow _messageShow;

        private static Dispatcher _dispatcher;

        public static void SetMessage(string message)
        {
            if (_messageShow == null)
            {
                new Thread(() =>
                {
                    _messageShow = new MessageShow();
                    _messageShow.Show();
                    _messageShow.AddMessage(message);
                    _dispatcher = Dispatcher.CurrentDispatcher;
                    Dispatcher.Run();
                })
                {
                    IsBackground = true,
                    ApartmentState = ApartmentState.STA,
                    //Priority = ThreadPriority.AboveNormal
                }.Start();
            }
            else {
                _dispatcher.Invoke(new Action(() =>
                {
                    _messageShow.AddMessage(message);
                }), DispatcherPriority.Send);
            }
        }

        public MessageShow()
        {
            InitializeComponent();
        }

        private int _standartHeight;

        public void AddMessage(string str)
        {
            if (!this.Visible)
                this.Visible = true;

            if (string.IsNullOrEmpty(this.lblContent.Text))
            {
                this.lblContent.Text = str;
                _standartHeight = this.Height;
            }
            else
            {
                this.Height += 33;
                if (this.Height >= Screen.PrimaryScreen.Bounds.Height)
                {
                    this.Height = _startupHeight;
                    this.lblContent.Text = string.Empty;
                    AddMessage(str);
                }
                else
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
