using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MultimediaKeysAction.MultimediaKeysAction action = new MultimediaKeysAction.MultimediaKeysAction();

        private void button2_Click(object sender, EventArgs e)
        {
            action.BeginUserSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            action.Do("");
        }
    }
}
