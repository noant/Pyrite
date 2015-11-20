using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using UniActionsCore;

namespace UniActionsUI
{
    /// <summary>
    /// Interaction logic for CItem.xaml
    /// </summary>
    public partial class CItem : Grid
    {
        private ActionItem _actionItem;
        public ActionItem ActionItem { 
            get {
                return _actionItem;
            }
            set {
                _actionItem = value;
                Refresh();
            }
        }

        public int Number
        {
            get
            {
                return int.Parse(lblNum.Content.ToString());
            }
            set
            {
                lblNum.Content = value.ToString();
            }
        }

        public void Activate()
        {
            this.grid.Focus();
        }

        private Brush _bg;
        public void Refresh()
        {
            this.Background = this._bg = GetFromString(_actionItem.Category);
            this.lblContent.Content = _actionItem.CheckState();
        }

        private Brush CurrBrush;

        private Brush SelectBrush = Brushes.Tomato;

        private Brush GetFromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return Brushes.SteelBlue;

            var alpha = (byte)120;
            var byteR = new byte();
            var byteG = new byte();
            var byteB = new byte();

            for (int i=0; i < str.Length; i++)
            {
                var val = (byte) ((((byte)str[i]) / str.Length) * 3);
                if (i < str.Length / 3)
                    byteR += val;
                if (i >= str.Length / 3 && i < (str.Length/3)*2)
                    byteG += val; 
                if (i >= (str.Length / 3)*2)
                    byteB += val;
            }

            var color = Color.FromArgb(alpha, byteR, byteG, byteB);

            if (IsDark(color))
                color = Light(color);

            if (Near(byteR, byteG))
                byteG = (byte)(200 - byteR);

            return new SolidColorBrush(color);
        }

        private bool Near(byte a, byte b)
        {
            return (Math.Abs(a - b) < 30);
        }

        private bool IsDark(Color color)
        {
            return color.R + color.G + color.B < 255*2;
        }

        private Color Light(Color color)
        {
            var r = color.R + 120;
            if (r > 255) r = 255;
            var g = color.G + 120;
            if (g > 255) g = 255;
            var b = color.B + 120;
            if (b > 255) b = 255;
            return Color.FromArgb(color.A, (byte)r, (byte)g, (byte)b);
        }

        public void Run()
        {
            this.Background = Brushes.AliceBlue;
            this.lblContent.Content = _actionItem.BeginExecute(this.lblContent.Content.ToString());
            if (Clicked != null)
                Clicked();
        }

        public CItem()
        {
            InitializeComponent();
            
            var actionUp = new Action(() => { 
                this.Background = this._bg;
            });            

            this.MouseDown += (o, e) => Run() ;
            this.MouseUp += (o, e) => actionUp();

            this.KeyDown += (o, e) => {
                if (e.Key == Key.Enter || e.Key == Key.Space)
                {
                    Run();
                    var t = new Thread(() => {
                        Thread.Sleep(180);
                        this.Dispatcher.BeginInvoke(new Action(() => {
                            actionUp();
                        }));
                    });
                    t.Start();
                }
            };
        }

        public event Clicked Clicked;
    }
    
    public delegate void Clicked();
}
