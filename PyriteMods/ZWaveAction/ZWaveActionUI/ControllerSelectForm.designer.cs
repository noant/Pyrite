namespace ZWaveActionUI
{
    partial class ControllerSelectForm
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
            this.cbControllerPath = new System.Windows.Forms.ComboBox();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btEraseAll = new System.Windows.Forms.Button();
            this.btSoftReset = new System.Windows.Forms.Button();
            this.btUpdateNetwork = new System.Windows.Forms.Button();
            this.btTransferConfiguration = new System.Windows.Forms.Button();
            this.btTransferPrimaryRole = new System.Windows.Forms.Button();
            this.btCreateNewPrimary = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btConnection = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbControllerPath
            // 
            this.cbControllerPath.FormattingEnabled = true;
            this.cbControllerPath.Location = new System.Drawing.Point(53, 12);
            this.cbControllerPath.Name = "cbControllerPath";
            this.cbControllerPath.Size = new System.Drawing.Size(337, 21);
            this.cbControllerPath.TabIndex = 0;
            this.cbControllerPath.Text = "COM4";
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Enabled = false;
            this.btOk.Location = new System.Drawing.Point(234, 298);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 1;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btCancel.Location = new System.Drawing.Point(315, 298);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Путь:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Тип:";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "SerialPort",
            "HID"});
            this.cbType.Location = new System.Drawing.Point(53, 39);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(337, 21);
            this.cbType.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btEraseAll);
            this.groupBox1.Controls.Add(this.btSoftReset);
            this.groupBox1.Controls.Add(this.btUpdateNetwork);
            this.groupBox1.Controls.Add(this.btTransferConfiguration);
            this.groupBox1.Controls.Add(this.btTransferPrimaryRole);
            this.groupBox1.Controls.Add(this.btCreateNewPrimary);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 199);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Другие функции контроллера";
            // 
            // btEraseAll
            // 
            this.btEraseAll.Enabled = false;
            this.btEraseAll.Location = new System.Drawing.Point(6, 168);
            this.btEraseAll.Name = "btEraseAll";
            this.btEraseAll.Size = new System.Drawing.Size(366, 23);
            this.btEraseAll.TabIndex = 6;
            this.btEraseAll.Text = "Сброс устройства";
            this.btEraseAll.UseVisualStyleBackColor = true;
            this.btEraseAll.Click += new System.EventHandler(this.btEraseAll_Click);
            // 
            // btSoftReset
            // 
            this.btSoftReset.Enabled = false;
            this.btSoftReset.Location = new System.Drawing.Point(6, 139);
            this.btSoftReset.Name = "btSoftReset";
            this.btSoftReset.Size = new System.Drawing.Size(366, 23);
            this.btSoftReset.TabIndex = 4;
            this.btSoftReset.Text = "Мягкий сброс устройства";
            this.btSoftReset.UseVisualStyleBackColor = true;
            this.btSoftReset.Click += new System.EventHandler(this.btSoftReset_Click);
            // 
            // btUpdateNetwork
            // 
            this.btUpdateNetwork.Enabled = false;
            this.btUpdateNetwork.Location = new System.Drawing.Point(6, 106);
            this.btUpdateNetwork.Name = "btUpdateNetwork";
            this.btUpdateNetwork.Size = new System.Drawing.Size(366, 23);
            this.btUpdateNetwork.TabIndex = 3;
            this.btUpdateNetwork.Text = "Обновить сеть устройств";
            this.btUpdateNetwork.UseVisualStyleBackColor = true;
            this.btUpdateNetwork.Click += new System.EventHandler(this.btUpdateNetwork_Click);
            // 
            // btTransferConfiguration
            // 
            this.btTransferConfiguration.Enabled = false;
            this.btTransferConfiguration.Location = new System.Drawing.Point(6, 77);
            this.btTransferConfiguration.Name = "btTransferConfiguration";
            this.btTransferConfiguration.Size = new System.Drawing.Size(366, 23);
            this.btTransferConfiguration.TabIndex = 2;
            this.btTransferConfiguration.Text = "Передать конфигурацию";
            this.btTransferConfiguration.UseVisualStyleBackColor = true;
            this.btTransferConfiguration.Click += new System.EventHandler(this.btTransferConfiguration_Click);
            // 
            // btTransferPrimaryRole
            // 
            this.btTransferPrimaryRole.Enabled = false;
            this.btTransferPrimaryRole.Location = new System.Drawing.Point(6, 48);
            this.btTransferPrimaryRole.Name = "btTransferPrimaryRole";
            this.btTransferPrimaryRole.Size = new System.Drawing.Size(366, 23);
            this.btTransferPrimaryRole.TabIndex = 1;
            this.btTransferPrimaryRole.Text = "Передать роль первичного контроллера";
            this.btTransferPrimaryRole.UseVisualStyleBackColor = true;
            this.btTransferPrimaryRole.Click += new System.EventHandler(this.btTransferPrimaryRole_Click);
            // 
            // btCreateNewPrimary
            // 
            this.btCreateNewPrimary.Enabled = false;
            this.btCreateNewPrimary.Location = new System.Drawing.Point(6, 19);
            this.btCreateNewPrimary.Name = "btCreateNewPrimary";
            this.btCreateNewPrimary.Size = new System.Drawing.Size(366, 23);
            this.btCreateNewPrimary.TabIndex = 0;
            this.btCreateNewPrimary.Text = "Создать новый первичный контроллер";
            this.btCreateNewPrimary.UseVisualStyleBackColor = true;
            this.btCreateNewPrimary.Click += new System.EventHandler(this.btCreateNewPrimary_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(375, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "___________________________________________________________________\r\n";
            // 
            // btConnection
            // 
            this.btConnection.Location = new System.Drawing.Point(12, 66);
            this.btConnection.Name = "btConnection";
            this.btConnection.Size = new System.Drawing.Size(378, 23);
            this.btConnection.TabIndex = 7;
            this.btConnection.Text = "Соединение";
            this.btConnection.UseVisualStyleBackColor = true;
            this.btConnection.Click += new System.EventHandler(this.btConnection_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 298);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(216, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 9;
            this.progressBar.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(17, 303);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 13);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Соединение...";
            this.lblStatus.Visible = false;
            // 
            // ControllerSelectForm
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(402, 332);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btConnection);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.cbControllerPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ControllerSelectForm";
            this.ShowIcon = false;
            this.Text = "Выбор контроллера";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbControllerPath;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btTransferPrimaryRole;
        private System.Windows.Forms.Button btCreateNewPrimary;
        private System.Windows.Forms.Button btTransferConfiguration;
        private System.Windows.Forms.Button btEraseAll;
        private System.Windows.Forms.Button btSoftReset;
        private System.Windows.Forms.Button btUpdateNetwork;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btConnection;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
    }
}