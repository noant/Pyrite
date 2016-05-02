using OpenZWaveDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveAction;

namespace ZWaveActionUI
{
    public partial class SelectNodeValueForm : Form
    {
        public SelectNodeValueForm(Node node)
        {
            InitializeComponent();

            _node = node;

            listView.SelectedIndexChanged += (o, e) =>
            {
                btOk.Enabled = listView.SelectedItems.Count > 0;
            };

            listView.DoubleClick += (o, e) =>
            {
                if (listView.SelectedItems.Count > 0)
                    DialogResult = DialogResult.OK;
            };

            var rbCheckedChanged = new Action(() =>
            {
                Refresh();
            });

            rbAll.CheckedChanged += (o, e) => rbCheckedChanged();
            rbBasic.CheckedChanged += (o, e) => rbCheckedChanged();
            rbUser.CheckedChanged += (o, e) => rbCheckedChanged();
            rbConfig.CheckedChanged += (o, e) => rbCheckedChanged();
            rbSystem.CheckedChanged += (o, e) => rbCheckedChanged();

            Refresh();
        }

        public override void Refresh()
        {
            base.Refresh();

            listView.Items.Clear();

            var zwave = ZWGlobal.GetZWaveByValueID(_node.Values.First());

            var values = _node.Values.ToArray();

            if (rbBasic.Checked)
                values = values.Where(x => x.GetGenre() == ZWValueID.ValueGenre.Basic).ToArray();
            if (rbUser.Checked)
                values = values.Where(x => x.GetGenre() == ZWValueID.ValueGenre.User).ToArray();
            if (rbConfig.Checked)
                values = values.Where(x => x.GetGenre() == ZWValueID.ValueGenre.Config).ToArray();
            if (rbSystem.Checked)
                values = values.Where(x => x.GetGenre() == ZWValueID.ValueGenre.System).ToArray();

            foreach (var value in values)
            {
                var item = new ListViewItem(value.GetType().ToString());
                item.SubItems.Add(zwave.Manager.GetValueLabel(value) + "/" + zwave.Manager.GetValueUnits(value) + "/" + zwave.Manager.GetValueHelp(value));
                item.Tag = value;
                listView.Items.Add(item);
            }
        }

        private Node _node;

        public ZWValueID SelectedValue
        {
            get
            {
                if (listView.SelectedItems.Count > 0)
                    return (ZWValueID)listView.SelectedItems[0].Tag;
                return null;
            }
            set
            {
                listView.Items.Cast<ListViewItem>().Where(x => x.Tag.Equals(value)).Select(x => x.Selected = true).ToList();
            }
        }
    }
}
