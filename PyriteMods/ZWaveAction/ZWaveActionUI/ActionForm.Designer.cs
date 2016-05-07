namespace ZWaveActionUI
{
    partial class ActionForm
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
            this.tbZWValue = new System.Windows.Forms.TextBox();
            this.btSelectValue = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.valueSetter = new ZWaveActionUI.ActionPanels.ValueSetter();
            this.label2 = new System.Windows.Forms.Label();
            this.tbText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbZWValue
            // 
            this.tbZWValue.Location = new System.Drawing.Point(83, 12);
            this.tbZWValue.Name = "tbZWValue";
            this.tbZWValue.ReadOnly = true;
            this.tbZWValue.Size = new System.Drawing.Size(311, 20);
            this.tbZWValue.TabIndex = 0;
            // 
            // btSelectValue
            // 
            this.btSelectValue.Location = new System.Drawing.Point(400, 10);
            this.btSelectValue.Name = "btSelectValue";
            this.btSelectValue.Size = new System.Drawing.Size(51, 23);
            this.btSelectValue.TabIndex = 1;
            this.btSelectValue.Text = "...";
            this.btSelectValue.UseVisualStyleBackColor = true;
            this.btSelectValue.Click += new System.EventHandler(this.btSelectValue_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(377, 110);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Enabled = false;
            this.btOk.Location = new System.Drawing.Point(296, 110);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 3;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Устройство:";
            // 
            // valueSetter
            // 
            this.valueSetter.Location = new System.Drawing.Point(5, 38);
            this.valueSetter.Name = "valueSetter";
            this.valueSetter.Size = new System.Drawing.Size(453, 24);
            this.valueSetter.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Текст:";
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(52, 69);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(399, 20);
            this.tbText.TabIndex = 2;
            // 
            // ActionForm
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(463, 142);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.valueSetter);
            this.Controls.Add(this.btSelectValue);
            this.Controls.Add(this.tbZWValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActionForm";
            this.ShowIcon = false;
            this.Text = "Действие ZWave";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbZWValue;
        private System.Windows.Forms.Button btSelectValue;
        private ActionPanels.ValueSetter valueSetter;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbText;
    }
}