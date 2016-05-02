namespace ZWaveActionUI
{
    partial class SelectNodeValueForm
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
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbUser = new System.Windows.Forms.RadioButton();
            this.rbBasic = new System.Windows.Forms.RadioButton();
            this.rbConfig = new System.Windows.Forms.RadioButton();
            this.rbSystem = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(1, 27);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(906, 325);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Тип";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Наименование";
            this.columnHeader2.Width = 700;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(833, 353);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Enabled = false;
            this.btOk.Location = new System.Drawing.Point(759, 353);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 2;
            this.btOk.Text = "Выбрать";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(6, 5);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(44, 17);
            this.rbAll.TabIndex = 3;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "Все";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbUser
            // 
            this.rbUser.AutoSize = true;
            this.rbUser.Location = new System.Drawing.Point(139, 5);
            this.rbUser.Name = "rbUser";
            this.rbUser.Size = new System.Drawing.Size(122, 17);
            this.rbUser.TabIndex = 4;
            this.rbUser.Text = "Пользовательские";
            this.rbUser.UseVisualStyleBackColor = true;
            // 
            // rbBasic
            // 
            this.rbBasic.AutoSize = true;
            this.rbBasic.Location = new System.Drawing.Point(56, 5);
            this.rbBasic.Name = "rbBasic";
            this.rbBasic.Size = new System.Drawing.Size(77, 17);
            this.rbBasic.TabIndex = 5;
            this.rbBasic.Text = "Основные";
            this.rbBasic.UseVisualStyleBackColor = true;
            // 
            // rbConfig
            // 
            this.rbConfig.AutoSize = true;
            this.rbConfig.Location = new System.Drawing.Point(267, 5);
            this.rbConfig.Name = "rbConfig";
            this.rbConfig.Size = new System.Drawing.Size(124, 17);
            this.rbConfig.TabIndex = 6;
            this.rbConfig.TabStop = true;
            this.rbConfig.Text = "Конфигурационные";
            this.rbConfig.UseVisualStyleBackColor = true;
            // 
            // rbSystem
            // 
            this.rbSystem.AutoSize = true;
            this.rbSystem.Location = new System.Drawing.Point(397, 5);
            this.rbSystem.Name = "rbSystem";
            this.rbSystem.Size = new System.Drawing.Size(83, 17);
            this.rbSystem.TabIndex = 7;
            this.rbSystem.Text = "Системные";
            this.rbSystem.UseVisualStyleBackColor = true;
            // 
            // SelectNodeValueForm
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(909, 377);
            this.Controls.Add(this.rbSystem);
            this.Controls.Add(this.rbConfig);
            this.Controls.Add(this.rbBasic);
            this.Controls.Add(this.rbUser);
            this.Controls.Add(this.rbAll);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.listView);
            this.Name = "SelectNodeValueForm";
            this.ShowIcon = false;
            this.Text = "Выбрать параметр";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbUser;
        private System.Windows.Forms.RadioButton rbBasic;
        private System.Windows.Forms.RadioButton rbConfig;
        private System.Windows.Forms.RadioButton rbSystem;
    }
}