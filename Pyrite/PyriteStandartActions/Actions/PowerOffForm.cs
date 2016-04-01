using System.Diagnostics;
using System.Windows.Forms;

namespace PyriteStandartActions.Actions
{
    public partial class PowerOffForm : Form
    {
        public PowerOffForm()
        {
            InitializeComponent();
        }

        public bool CanCancel { get; set; }
        public bool Restart { get; set; }
        public int Timer { get; set; }

        public void Do()
        {
            var args = "/s /t 0";
            if (Restart) args = "/r /t 0";
            var psi = new ProcessStartInfo("shutdown", args);
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        public void Start()
        {
            if (Timer == 0)
            {
                Do();
                return;
            }
            this.lblTimer.Text = Timer--.ToString();
            if (Restart)
                lblTitle.Text = "Перезапуск компьютера через:";
            Timer t = new Timer();
            t.Interval = 1000;
            t.Tick += (o, e) => {
                if (Timer == 0)
                {
                    Do();
                    t.Stop();
                }
                else
                    this.lblTimer.Text = Timer--.ToString();
            };
            if (!CanCancel)
            {
                this.btCancel.Visible = false;
                this.Height = 70;
            }
            else 
                this.btCancel.Click += (o, e) => {
                    t.Stop();
                    this.Close();
                };

            t.Start();
        }
    }
}
