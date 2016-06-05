using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultimediaKeysAction
{
    public partial class MultimediaKeysActionView : Form
    {
        public MultimediaKeysActionView(Keys selectedKey)
        {
            InitializeComponent();

            cbKey.SelectedIndexChanged += (o, e) => Key = MultimediaKeysAction.KeysNames.Single(x => x.Value.Equals(cbKey.SelectedItem.ToString())).Key;

            foreach (var pair in MultimediaKeysAction.KeysNames)
            {
                cbKey.Items.Add(pair.Value);
                if (pair.Key.Equals(selectedKey))
                    cbKey.SelectedItem = pair.Value;
            }
        }

        public Keys Key { get; private set; }
    }
}
