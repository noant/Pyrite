namespace PyriteStandartActions.Checkers
{
    partial class DateBetweenCheckerView
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
            this.dtPicker1 = new System.Windows.Forms.DateTimePicker();
            this.dtPicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cbOrEqual1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbOrEqual2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtPicker1
            // 
            this.dtPicker1.CustomFormat = "dd.MM.yyyy hh:mm:ss";
            this.dtPicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPicker1.Location = new System.Drawing.Point(12, 32);
            this.dtPicker1.Name = "dtPicker1";
            this.dtPicker1.Size = new System.Drawing.Size(200, 20);
            this.dtPicker1.TabIndex = 1;
            // 
            // dtPicker2
            // 
            this.dtPicker2.CustomFormat = "dd.MM.yyyy hh:mm:ss";
            this.dtPicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPicker2.Location = new System.Drawing.Point(265, 32);
            this.dtPicker2.Name = "dtPicker2";
            this.dtPicker2.Size = new System.Drawing.Size(200, 20);
            this.dtPicker2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дата сейчас больше";
            // 
            // cbOrEqual1
            // 
            this.cbOrEqual1.AutoSize = true;
            this.cbOrEqual1.Location = new System.Drawing.Point(130, 8);
            this.cbOrEqual1.Name = "cbOrEqual1";
            this.cbOrEqual1.Size = new System.Drawing.Size(77, 17);
            this.cbOrEqual1.TabIndex = 0;
            this.cbOrEqual1.Text = "или равна";
            this.cbOrEqual1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "и";
            // 
            // cbOrEqual2
            // 
            this.cbOrEqual2.AutoSize = true;
            this.cbOrEqual2.Location = new System.Drawing.Point(380, 8);
            this.cbOrEqual2.Name = "cbOrEqual2";
            this.cbOrEqual2.Size = new System.Drawing.Size(77, 17);
            this.cbOrEqual2.TabIndex = 2;
            this.cbOrEqual2.Text = "или равна";
            this.cbOrEqual2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Дата сейчас меньше";
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(390, 72);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(309, 72);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 4;
            this.btOk.Text = "Ок";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // DateBetweenCheckerView
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(482, 107);
            this.ControlBox = false;
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.cbOrEqual2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbOrEqual1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtPicker2);
            this.Controls.Add(this.dtPicker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DateBetweenCheckerView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дата между";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtPicker1;
        private System.Windows.Forms.DateTimePicker dtPicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbOrEqual1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbOrEqual2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
    }
}