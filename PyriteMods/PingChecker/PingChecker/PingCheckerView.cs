using System;
using System.Windows.Forms;

namespace PingChecker
{
    public partial class PingCheckerView : Form
    {
        public PingCheckerView()
        {
            InitializeComponent();
        }

        public string Host
        {
            get { return tbHost.Text.Trim(); }
            set { tbHost.Text = value; }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            var dialogResult = DialogResult.Cancel;
            if (string.IsNullOrEmpty(tbHost.Text))
            {
                if (MessageBox.Show("Хост не введен. Продолжить?", "Внимание!", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) == DialogResult.OK)
                    dialogResult = DialogResult.OK;
            }
            else if (tbHost.Text.Trim().Contains(" "))
            {
                if (MessageBox.Show("В адресе присутствуют пробелы. Продолжить?", "Внимание!", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) == DialogResult.OK)
                    dialogResult = DialogResult.OK;
            }
            else 
                dialogResult = DialogResult.OK;

            this.DialogResult = dialogResult;
        }
    }
}
