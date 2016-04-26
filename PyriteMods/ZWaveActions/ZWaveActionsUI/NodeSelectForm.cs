using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZWaveActions;

namespace ZWaveActionsUI
{
    public partial class NodeSelectForm : Form
    {
        public NodeSelectForm(string device, ControllerInterface @interface)
        {
            InitializeComponent();
            ColumnsInitialize();
            SetControllerToFindNodes(device, @interface);
        }

        private void ColumnsInitialize()
        {
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGrid.AutoGenerateColumns = false;
            dataGrid.AllowUserToResizeColumns = true;

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
            column.ToolTipText = "Наименование устройства";
            dataGrid.Columns.Add(column);

            // Manufacturer
            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Manufacturer";
            column.Name = "Производитель";
            column.Frozen = false;
            column.Resizable = DataGridViewTriState.True;
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
            column.ToolTipText = "Текущий уровень устройства";
            column.Frozen = false;
            dataGrid.Columns.Add(column);

            // Button
            var btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = "Питание";
            btnColumn.DataPropertyName = "ButtonText";
            btnColumn.Resizable = DataGridViewTriState.True;
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
                this.dataGrid.DataSource = _zwave.Nodes;
            }
            else
            {
                this.dataGrid.DataSource = null;
                MessageBox.Show("Время ожидания отклика от контроллера истекло.");
            }
        }

        private ZWave _zwave;
    }
}
