using OpenZWaveDotNet;
using OZWForm;
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
    public partial class NodeSelectForm : Form
    {
        public NodeSelectForm(string device, ControllerInterface @interface)
        {
            InitializeComponent();
            ColumnsInitialize();
            dataGrid.CellMouseDown += (o, e) =>
            {
                if (e.RowIndex >= 0) //dataGrid crutch
                    dataGrid.Rows[e.RowIndex].Selected = true;
            };
            dataGrid.SelectionChanged += (o, e) =>
            {
                btOk.Enabled = dataGrid.SelectedRows.Count > 0;
            };
            dataGrid.CellDoubleClick += (o, e) =>
            {
                this.DialogResult = DialogResult.OK;
            };
            SetControllerToFindNodes(device, @interface);
        }

        public Node SelectedNode
        {
            get
            {
                if (dataGrid.SelectedRows.Count > 0)
                    return (Node)dataGrid.SelectedRows[0].DataBoundItem;
                return null;
            }
            set
            {
                foreach (DataGridViewRow row in dataGrid.Rows)
                    if (row.DataBoundItem.Equals(value))
                    {
                        row.Selected = true;
                        break;
                    }
            }
        }

        private void ColumnsInitialize()
        {
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGrid.AutoGenerateColumns = false;
            dataGrid.AllowUserToResizeColumns = true;
            dataGrid.AllowUserToAddRows = false;
            dataGrid.MultiSelect = false;
            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DataGridViewTextBoxColumn column;

            // Id
            column = new DataGridViewTextBoxColumn();
            column.Name = "ID";
            column.DataPropertyName = "ID";
            column.Frozen = false;
            column.ReadOnly = true;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.Resizable = DataGridViewTriState.True;
            column.ToolTipText = "ID узла";
            dataGrid.Columns.Add(column);

            // Name
            column = new DataGridViewTextBoxColumn();
            column.Name = "Название";
            column.DataPropertyName = "Name";
            column.Resizable = DataGridViewTriState.True;
            column.Frozen = false;
            column.ReadOnly = true;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "Название узла Z-Wave";
            dataGrid.Columns.Add(column);

            // Product
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Product";
            column.Name = "Наименование устройства";
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.ReadOnly = true;
            column.ToolTipText = "Наименование устройства";
            dataGrid.Columns.Add(column);

            // Manufacturer
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Manufacturer";
            column.Name = "Производитель";
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
            column.ReadOnly = true;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ToolTipText = "Производитель Z-Wave устройства";
            dataGrid.Columns.Add(column);

            // Device Type
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Label";
            column.Name = "Тип";
            column.Frozen = false;
            column.ReadOnly = true;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.Resizable = DataGridViewTriState.True;
            column.ToolTipText = "Тип узла";
            dataGrid.Columns.Add(column);

            // Level
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Level";
            column.Name = "Уровень";
            column.Resizable = DataGridViewTriState.True;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.ReadOnly = true;
            column.ToolTipText = "Текущий уровень устройства";
            column.Frozen = false;
            dataGrid.Columns.Add(column);

            // Button
            var btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = "Питание";
            btnColumn.DataPropertyName = "ButtonText";
            btnColumn.Resizable = DataGridViewTriState.True;
            column.ReadOnly = true;
            btnColumn.Frozen = false;
            btnColumn.ToolTipText = "Включение/выключение устройства";
            btnColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGrid.Columns.Add(btnColumn);
        }

        private void SetControllerToFindNodes(string path, ControllerInterface @interface)
        {
            var zwave = ZWGlobal.PrepareZWave(path, @interface);
            if (zwave.WaitForControllerLoaded())
            {
                _zwave = zwave;
                RefreshData();
            }
            else
            {
                this.dataGrid.DataSource = null;
                MessageBox.Show("Время ожидания отклика от контроллера истекло.");
            }
        }

        private ZWave _zwave;

        private void btAddNewNode_Click(object sender, EventArgs e)
        {
            Execute(ZWControllerCommand.AddDevice, false);
        }

        private void btAddNewNodeSecure_Click(object sender, EventArgs e)
        {
            Execute(ZWControllerCommand.AddDevice, true);
        }

        private void btRemoveNode_Click(object sender, EventArgs e)
        {
            Execute(ZWControllerCommand.RemoveDevice, false);
        }

        byte _selectedNodeId = 0;

        private void RefreshData()
        {
            var node = SelectedNode;
            this.dataGrid.DataSource = null;
            this.dataGrid.DataSource = _zwave.Nodes;
            SelectedNode = node;
        }

        private void Execute(ZWControllerCommand command, bool secure)
        {
            if (dataGrid.SelectedRows.Count > 0)
                _selectedNodeId = Convert.ToByte(dataGrid.SelectedRows[0].Cells["ID"].Value);

            ControllerCommandDlg dlg = new ControllerCommandDlg(_zwave.Manager, _zwave.HomeId.Value, command, _selectedNodeId, secure);
            DialogResult d = dlg.ShowDialog(this);
            dlg.Dispose();
            RefreshData();
        }

        private void powerOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zwave.Manager.SetNodeOn(_zwave.HomeId.Value, _selectedNodeId);
            RefreshData();
        }

        private void powerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _zwave.Manager.SetNodeOff(_zwave.HomeId.Value, _selectedNodeId);
            RefreshData();
        }

        private void requestNodeNeighborUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(ZWControllerCommand.RequestNodeNeighborUpdate, false);
        }

        private void assignReturnRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(ZWControllerCommand.AssignReturnRoute, false);
        }

        private void deleteReturnRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(ZWControllerCommand.DeleteAllReturnRoutes, false);
        }

        private void hasNodeFailedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(ZWControllerCommand.HasNodeFailed, false);
        }

        private void markNodeAsFailedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(ZWControllerCommand.RemoveFailedNode, false);
        }

        private void replaceFailedNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Execute(ZWControllerCommand.ReplaceFailedNode, false);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangeNodeValuesForm((Node)dataGrid.SelectedRows[0].DataBoundItem).ShowDialog();
        }
    }
}
