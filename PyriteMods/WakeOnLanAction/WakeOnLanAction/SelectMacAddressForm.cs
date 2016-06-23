using System.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

namespace WakeOnLanAction
{
    public partial class SelectMacAddressForm : Form
    {
        public SelectMacAddressForm()
        {
            InitializeComponent();
            this.btSelect.Enabled = false;

            this.listView.SelectedIndexChanged += (o, e) =>
            {
                btSelect.Enabled = listView.SelectedItems.Count > 0;
            };

            this.listView.MouseDoubleClick += (o, e) =>
            {
                if (listView.SelectedItems.Count > 0)
                    DialogResult = System.Windows.Forms.DialogResult.OK;
            };

            this.btSearch.Click += (o, e) => BeginAddressListing();

            listView.ColumnClick += (o, e) =>
            {
                if (e.Column == 0)
                {
                    if (listView.Sorting == SortOrder.Ascending)
                        listView.Sorting = SortOrder.Descending;
                    else listView.Sorting = SortOrder.Ascending;
                    listView.Sort();
                }
            };
        }

        private void BeginAddressListing()
        {
            listView.Items.Clear();
            LANHelper.ListAllHosts(
                new byte[] { byteBox1.Value, byteBox2.Value, byteBox3.Value },
                (address) =>
                {
                    this.BeginInvoke((Action)(() =>
                    {
                        var lvItem = new ListViewItem(address.IPAddress.ToString());
                        var bytes = address.MacAddress.GetAddressBytes();
                        var hexFormat = "x2";
                        lvItem.SubItems.Add(
                              bytes[0].ToString(hexFormat).ToUpper() + ":"
                            + bytes[1].ToString(hexFormat).ToUpper() + ":"
                            + bytes[2].ToString(hexFormat).ToUpper() + ":"
                            + bytes[3].ToString(hexFormat).ToUpper() + ":"
                            + bytes[4].ToString(hexFormat).ToUpper() + ":"
                            + bytes[5].ToString(hexFormat).ToUpper());
                        lvItem.Tag = address;
                        listView.Items.Add(lvItem);
                    }));
                });
        }

        public byte[] Address
        {
            get
            {
                if (listView.SelectedItems.Count > 0)
                    return ((LANHelper.AddressStruct)listView.SelectedItems[0].Tag).MacAddress.GetAddressBytes();
                else return null;
            }
        }
    }
}
