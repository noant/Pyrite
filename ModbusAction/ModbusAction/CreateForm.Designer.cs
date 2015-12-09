﻿namespace ModbusAction
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbPortName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudBaudRate = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nudSlaveId = new System.Windows.Forms.NumericUpDown();
            this.nudSingleCoil = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudReadTimeout = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudWriteTimeout = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbStateOff = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbStateOn = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaudRate)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlaveId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSingleCoil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteTimeout)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(228, 436);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 157);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки COM порта";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Port:";
            // 
            // tbPortName
            // 
            this.tbPortName.Location = new System.Drawing.Point(67, 19);
            this.tbPortName.Name = "tbPortName";
            this.tbPortName.Size = new System.Drawing.Size(217, 20);
            this.tbPortName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Baud rate:";
            // 
            // nudBaudRate
            // 
            this.nudBaudRate.Location = new System.Drawing.Point(67, 45);
            this.nudBaudRate.Maximum = new decimal(new int[] {
            57600,
            0,
            0,
            0});
            this.nudBaudRate.Name = "nudBaudRate";
            this.nudBaudRate.Size = new System.Drawing.Size(217, 20);
            this.nudBaudRate.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data bits:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Parity:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Stop bits";
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Location = new System.Drawing.Point(67, 71);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(217, 21);
            this.cbDataBits.TabIndex = 2;
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(67, 98);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(217, 21);
            this.cbParity.TabIndex = 3;
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(67, 125);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(217, 21);
            this.cbStopBits.TabIndex = 4;
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
            this.groupBox2.Location = new System.Drawing.Point(12, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 134);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки устройства Modbus";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Slave id:";
            // 
            // nudSlaveId
            // 
            this.nudSlaveId.Location = new System.Drawing.Point(67, 21);
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
            this.nudSlaveId.Size = new System.Drawing.Size(217, 20);
            this.nudSlaveId.TabIndex = 5;
            this.nudSlaveId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudSingleCoil
            // 
            this.nudSingleCoil.Location = new System.Drawing.Point(67, 47);
            this.nudSingleCoil.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudSingleCoil.Name = "nudSingleCoil";
            this.nudSingleCoil.Size = new System.Drawing.Size(217, 20);
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
            this.label7.Location = new System.Drawing.Point(6, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Single coil:";
            // 
            // nudReadTimeout
            // 
            this.nudReadTimeout.Location = new System.Drawing.Point(85, 73);
            this.nudReadTimeout.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudReadTimeout.Name = "nudReadTimeout";
            this.nudReadTimeout.Size = new System.Drawing.Size(199, 20);
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
            this.label8.Location = new System.Drawing.Point(6, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Read timeout:";
            // 
            // nudWriteTimeout
            // 
            this.nudWriteTimeout.Location = new System.Drawing.Point(85, 99);
            this.nudWriteTimeout.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudWriteTimeout.Name = "nudWriteTimeout";
            this.nudWriteTimeout.Size = new System.Drawing.Size(199, 20);
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
            this.label9.Location = new System.Drawing.Point(6, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Write timeout:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbStateOn);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.tbStateOff);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(12, 315);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 115);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Статусы";
            // 
            // tbStateOff
            // 
            this.tbStateOff.Location = new System.Drawing.Point(6, 39);
            this.tbStateOff.Name = "tbStateOff";
            this.tbStateOff.Size = new System.Drawing.Size(278, 20);
            this.tbStateOff.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(139, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Статус, когда выключено:";
            // 
            // tbStateOn
            // 
            this.tbStateOn.Location = new System.Drawing.Point(6, 78);
            this.tbStateOn.Name = "tbStateOn";
            this.tbStateOn.Size = new System.Drawing.Size(278, 20);
            this.tbStateOn.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Статус, когда включено:";
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(147, 436);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 11;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // CreateForm
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(313, 471);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
            ((System.ComponentModel.ISupportInitialize)(this.nudSlaveId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSingleCoil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteTimeout)).EndInit();
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
    }
}