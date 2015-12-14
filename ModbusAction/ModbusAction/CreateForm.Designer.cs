namespace ModbusAction
{
    partial class CreateForm
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
            this.btCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudBaudRate = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPortName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudWriteTimeout = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudReadTimeout = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudSingleCoil = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudSlaveId = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbOff = new System.Windows.Forms.RadioButton();
            this.rbOnOff = new System.Windows.Forms.RadioButton();
            this.rbOn = new System.Windows.Forms.RadioButton();
            this.tbStateOn = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbStateOff = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaudRate)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSingleCoil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlaveId)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(342, 775);
            this.btCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(112, 35);
            this.btCancel.TabIndex = 12;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbStopBits);
            this.groupBox1.Controls.Add(this.cbParity);
            this.groupBox1.Controls.Add(this.cbDataBits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nudBaudRate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbPortName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(435, 242);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки COM порта";
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(100, 192);
            this.cbStopBits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(324, 28);
            this.cbStopBits.TabIndex = 4;
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(100, 151);
            this.cbParity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(324, 28);
            this.cbParity.TabIndex = 3;
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Location = new System.Drawing.Point(100, 109);
            this.cbDataBits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(324, 28);
            this.cbDataBits.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 197);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Stop bits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 155);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Parity:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 114);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data bits:";
            // 
            // nudBaudRate
            // 
            this.nudBaudRate.Location = new System.Drawing.Point(100, 69);
            this.nudBaudRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudBaudRate.Maximum = new decimal(new int[] {
            57600,
            0,
            0,
            0});
            this.nudBaudRate.Name = "nudBaudRate";
            this.nudBaudRate.Size = new System.Drawing.Size(326, 26);
            this.nudBaudRate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baud rate:";
            // 
            // tbPortName
            // 
            this.tbPortName.Location = new System.Drawing.Point(100, 29);
            this.tbPortName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPortName.Name = "tbPortName";
            this.tbPortName.Size = new System.Drawing.Size(324, 26);
            this.tbPortName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudWriteTimeout);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.nudReadTimeout);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.nudSingleCoil);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.nudSlaveId);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(18, 269);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(435, 206);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки устройства Modbus";
            // 
            // nudWriteTimeout
            // 
            this.nudWriteTimeout.Location = new System.Drawing.Point(128, 152);
            this.nudWriteTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudWriteTimeout.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudWriteTimeout.Name = "nudWriteTimeout";
            this.nudWriteTimeout.Size = new System.Drawing.Size(298, 26);
            this.nudWriteTimeout.TabIndex = 8;
            this.nudWriteTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 155);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "Write timeout:";
            // 
            // nudReadTimeout
            // 
            this.nudReadTimeout.Location = new System.Drawing.Point(128, 112);
            this.nudReadTimeout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudReadTimeout.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudReadTimeout.Name = "nudReadTimeout";
            this.nudReadTimeout.Size = new System.Drawing.Size(298, 26);
            this.nudReadTimeout.TabIndex = 7;
            this.nudReadTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 115);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "Read timeout:";
            // 
            // nudSingleCoil
            // 
            this.nudSingleCoil.Location = new System.Drawing.Point(100, 72);
            this.nudSingleCoil.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudSingleCoil.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudSingleCoil.Name = "nudSingleCoil";
            this.nudSingleCoil.Size = new System.Drawing.Size(326, 26);
            this.nudSingleCoil.TabIndex = 6;
            this.nudSingleCoil.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 75);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Single coil:";
            // 
            // nudSlaveId
            // 
            this.nudSlaveId.Location = new System.Drawing.Point(100, 32);
            this.nudSlaveId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudSlaveId.Maximum = new decimal(new int[] {
            247,
            0,
            0,
            0});
            this.nudSlaveId.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSlaveId.Name = "nudSlaveId";
            this.nudSlaveId.Size = new System.Drawing.Size(326, 26);
            this.nudSlaveId.TabIndex = 5;
            this.nudSlaveId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 35);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Slave id:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbOff);
            this.groupBox3.Controls.Add(this.rbOnOff);
            this.groupBox3.Controls.Add(this.rbOn);
            this.groupBox3.Controls.Add(this.tbStateOn);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.tbStateOff);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(18, 485);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(435, 280);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Статусы";
            // 
            // rbOff
            // 
            this.rbOff.AutoSize = true;
            this.rbOff.Location = new System.Drawing.Point(13, 72);
            this.rbOff.Name = "rbOff";
            this.rbOff.Size = new System.Drawing.Size(171, 24);
            this.rbOff.TabIndex = 13;
            this.rbOff.Text = "Только выключать";
            this.rbOff.UseVisualStyleBackColor = true;
            // 
            // rbOnOff
            // 
            this.rbOnOff.AutoSize = true;
            this.rbOnOff.Checked = true;
            this.rbOnOff.Location = new System.Drawing.Point(13, 102);
            this.rbOnOff.Name = "rbOnOff";
            this.rbOnOff.Size = new System.Drawing.Size(207, 24);
            this.rbOnOff.TabIndex = 12;
            this.rbOnOff.TabStop = true;
            this.rbOnOff.Text = "Включать и выключать";
            this.rbOnOff.UseVisualStyleBackColor = true;
            // 
            // rbOn
            // 
            this.rbOn.AutoSize = true;
            this.rbOn.Location = new System.Drawing.Point(13, 42);
            this.rbOn.Name = "rbOn";
            this.rbOn.Size = new System.Drawing.Size(160, 24);
            this.rbOn.TabIndex = 11;
            this.rbOn.Text = "Только включать";
            this.rbOn.UseVisualStyleBackColor = true;
            // 
            // tbStateOn
            // 
            this.tbStateOn.Location = new System.Drawing.Point(9, 235);
            this.tbStateOn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbStateOn.Name = "tbStateOn";
            this.tbStateOn.Size = new System.Drawing.Size(415, 26);
            this.tbStateOn.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 210);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(197, 20);
            this.label11.TabIndex = 7;
            this.label11.Text = "Статус, когда включено:";
            // 
            // tbStateOff
            // 
            this.tbStateOff.Location = new System.Drawing.Point(9, 175);
            this.tbStateOff.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbStateOff.Name = "tbStateOff";
            this.tbStateOff.Size = new System.Drawing.Size(415, 26);
            this.tbStateOff.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 150);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(208, 20);
            this.label10.TabIndex = 5;
            this.label10.Text = "Статус, когда выключено:";
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(220, 775);
            this.btOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(112, 35);
            this.btOk.TabIndex = 11;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // CreateForm
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(470, 825);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "CreateForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Действие Modbus RTU";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaudRate)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSingleCoil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlaveId)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btOk;
        internal System.Windows.Forms.ComboBox cbStopBits;
        internal System.Windows.Forms.ComboBox cbParity;
        internal System.Windows.Forms.ComboBox cbDataBits;
        internal System.Windows.Forms.NumericUpDown nudBaudRate;
        internal System.Windows.Forms.TextBox tbPortName;
        internal System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.NumericUpDown nudSingleCoil;
        internal System.Windows.Forms.NumericUpDown nudSlaveId;
        internal System.Windows.Forms.NumericUpDown nudWriteTimeout;
        internal System.Windows.Forms.NumericUpDown nudReadTimeout;
        internal System.Windows.Forms.TextBox tbStateOn;
        internal System.Windows.Forms.TextBox tbStateOff;
        internal System.Windows.Forms.RadioButton rbOff;
        internal System.Windows.Forms.RadioButton rbOnOff;
        internal System.Windows.Forms.RadioButton rbOn;
    }
}