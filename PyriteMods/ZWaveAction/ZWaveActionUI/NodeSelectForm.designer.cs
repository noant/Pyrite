namespace ZWaveActionUI
{
    partial class NodeSelectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btAddNewNode = new System.Windows.Forms.Button();
            this.btRemoveNode = new System.Windows.Forms.Button();
            this.btAddNewNodeSecure = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.NodeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.powerOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.requestNodeNeighborUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignReturnRouteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteReturnRouteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.hasNodeFailedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markNodeAsFailedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceFailedNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.NodeContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btAddNewNode
            // 
            this.btAddNewNode.Location = new System.Drawing.Point(2, 2);
            this.btAddNewNode.Name = "btAddNewNode";
            this.btAddNewNode.Size = new System.Drawing.Size(135, 23);
            this.btAddNewNode.TabIndex = 0;
            this.btAddNewNode.Text = "Добавить новый узел";
            this.btAddNewNode.UseVisualStyleBackColor = true;
            this.btAddNewNode.Click += new System.EventHandler(this.btAddNewNode_Click);
            // 
            // btRemoveNode
            // 
            this.btRemoveNode.Location = new System.Drawing.Point(326, 2);
            this.btRemoveNode.Name = "btRemoveNode";
            this.btRemoveNode.Size = new System.Drawing.Size(94, 23);
            this.btRemoveNode.TabIndex = 2;
            this.btRemoveNode.Text = "Удалить узел";
            this.btRemoveNode.UseVisualStyleBackColor = true;
            this.btRemoveNode.Click += new System.EventHandler(this.btRemoveNode_Click);
            // 
            // btAddNewNodeSecure
            // 
            this.btAddNewNodeSecure.Location = new System.Drawing.Point(138, 2);
            this.btAddNewNodeSecure.Name = "btAddNewNodeSecure";
            this.btAddNewNodeSecure.Size = new System.Drawing.Size(187, 23);
            this.btAddNewNodeSecure.TabIndex = 1;
            this.btAddNewNodeSecure.Text = "Добавить новый узел безопасно";
            this.btAddNewNodeSecure.UseVisualStyleBackColor = true;
            this.btAddNewNodeSecure.Click += new System.EventHandler(this.btAddNewNodeSecure_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.ContextMenuStrip = this.NodeContextMenuStrip;
            this.dataGrid.Location = new System.Drawing.Point(2, 27);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(690, 378);
            this.dataGrid.TabIndex = 3;
            // 
            // NodeContextMenuStrip
            // 
            this.NodeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.powerOnToolStripMenuItem,
            this.powerOffToolStripMenuItem,
            this.toolStripSeparator4,
            this.requestNodeNeighborUpdateToolStripMenuItem,
            this.assignReturnRouteToolStripMenuItem,
            this.deleteReturnRouteToolStripMenuItem,
            this.toolStripSeparator5,
            this.hasNodeFailedToolStripMenuItem,
            this.markNodeAsFailedToolStripMenuItem,
            this.replaceFailedNodeToolStripMenuItem,
            this.toolStripSeparator6,
            this.propertiesToolStripMenuItem});
            this.NodeContextMenuStrip.Name = "NodeContextMenuStrip";
            this.NodeContextMenuStrip.Size = new System.Drawing.Size(260, 220);
            // 
            // powerOnToolStripMenuItem
            // 
            this.powerOnToolStripMenuItem.Name = "powerOnToolStripMenuItem";
            this.powerOnToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.powerOnToolStripMenuItem.Text = "Включить";
            this.powerOnToolStripMenuItem.Click += new System.EventHandler(this.powerOnToolStripMenuItem_Click);
            // 
            // powerOffToolStripMenuItem
            // 
            this.powerOffToolStripMenuItem.Name = "powerOffToolStripMenuItem";
            this.powerOffToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.powerOffToolStripMenuItem.Text = "Выключить";
            this.powerOffToolStripMenuItem.Click += new System.EventHandler(this.powerOffToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(256, 6);
            // 
            // requestNodeNeighborUpdateToolStripMenuItem
            // 
            this.requestNodeNeighborUpdateToolStripMenuItem.Name = "requestNodeNeighborUpdateToolStripMenuItem";
            this.requestNodeNeighborUpdateToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.requestNodeNeighborUpdateToolStripMenuItem.Text = "Обновить соседние узлы";
            this.requestNodeNeighborUpdateToolStripMenuItem.Click += new System.EventHandler(this.requestNodeNeighborUpdateToolStripMenuItem_Click);
            // 
            // assignReturnRouteToolStripMenuItem
            // 
            this.assignReturnRouteToolStripMenuItem.Name = "assignReturnRouteToolStripMenuItem";
            this.assignReturnRouteToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.assignReturnRouteToolStripMenuItem.Text = "Назначить обратный маршрут";
            this.assignReturnRouteToolStripMenuItem.Click += new System.EventHandler(this.assignReturnRouteToolStripMenuItem_Click);
            // 
            // deleteReturnRouteToolStripMenuItem
            // 
            this.deleteReturnRouteToolStripMenuItem.Name = "deleteReturnRouteToolStripMenuItem";
            this.deleteReturnRouteToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.deleteReturnRouteToolStripMenuItem.Text = "Удалить все обратные маршруты";
            this.deleteReturnRouteToolStripMenuItem.Click += new System.EventHandler(this.deleteReturnRouteToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(256, 6);
            // 
            // hasNodeFailedToolStripMenuItem
            // 
            this.hasNodeFailedToolStripMenuItem.Name = "hasNodeFailedToolStripMenuItem";
            this.hasNodeFailedToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.hasNodeFailedToolStripMenuItem.Text = "Тестировать узел";
            this.hasNodeFailedToolStripMenuItem.Click += new System.EventHandler(this.hasNodeFailedToolStripMenuItem_Click);
            // 
            // markNodeAsFailedToolStripMenuItem
            // 
            this.markNodeAsFailedToolStripMenuItem.Name = "markNodeAsFailedToolStripMenuItem";
            this.markNodeAsFailedToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.markNodeAsFailedToolStripMenuItem.Text = "Удалить неисправный узел";
            this.markNodeAsFailedToolStripMenuItem.Click += new System.EventHandler(this.markNodeAsFailedToolStripMenuItem_Click);
            // 
            // replaceFailedNodeToolStripMenuItem
            // 
            this.replaceFailedNodeToolStripMenuItem.Name = "replaceFailedNodeToolStripMenuItem";
            this.replaceFailedNodeToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.replaceFailedNodeToolStripMenuItem.Text = "Заменить неисправный узел";
            this.replaceFailedNodeToolStripMenuItem.Click += new System.EventHandler(this.replaceFailedNodeToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(256, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.propertiesToolStripMenuItem.Text = "Изменить значения";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(599, 405);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(94, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Enabled = false;
            this.btOk.Location = new System.Drawing.Point(504, 405);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(94, 23);
            this.btOk.TabIndex = 4;
            this.btOk.Text = "Выбрать";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // NodeSelectForm
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(694, 429);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.btAddNewNodeSecure);
            this.Controls.Add(this.btRemoveNode);
            this.Controls.Add(this.btAddNewNode);
            this.MaximizeBox = false;
            this.Name = "NodeSelectForm";
            this.ShowIcon = false;
            this.Text = "Выбор узла";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.NodeContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btAddNewNode;
        private System.Windows.Forms.Button btRemoveNode;
        private System.Windows.Forms.Button btAddNewNodeSecure;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.ContextMenuStrip NodeContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem powerOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem requestNodeNeighborUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assignReturnRouteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteReturnRouteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem hasNodeFailedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markNodeAsFailedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceFailedNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
    }
}