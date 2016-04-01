namespace PyriteStandartActions.Checkers.Utils
{
    partial class ValueEqualityView
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
            this.labelValueName = new System.Windows.Forms.Label();
            this.cbEquality = new System.Windows.Forms.ComboBox();
            this.nud1 = new System.Windows.Forms.NumericUpDown();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelValueName
            // 
            this.labelValueName.Location = new System.Drawing.Point(12, 13);
            this.labelValueName.Name = "labelValueName";
            this.labelValueName.Size = new System.Drawing.Size(65, 23);
            this.labelValueName.TabIndex = 0;
            this.labelValueName.Text = "label1";
            // 
            // cbEquality
            // 
            this.cbEquality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEquality.FormattingEnabled = true;
            this.cbEquality.Items.AddRange(new object[] {
            "=",
            ">",
            ">=",
            "<",
            "<="});
            this.cbEquality.Location = new System.Drawing.Point(83, 9);
            this.cbEquality.Name = "cbEquality";
            this.cbEquality.Size = new System.Drawing.Size(59, 21);
            this.cbEquality.TabIndex = 1;
            // 
            // nud1
            // 
            this.nud1.Location = new System.Drawing.Point(148, 9);
            this.nud1.Name = "nud1";
            this.nud1.Size = new System.Drawing.Size(74, 20);
            this.nud1.TabIndex = 2;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(147, 51);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(66, 51);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 4;
            this.btOk.Text = "Ок";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // ValueEqualityView
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(241, 90);
            this.ControlBox = false;
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.nud1);
            this.Controls.Add(this.cbEquality);
            this.Controls.Add(this.labelValueName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ValueEqualityView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ValueEqualityView";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelValueName;
        private System.Windows.Forms.ComboBox cbEquality;
        private System.Windows.Forms.NumericUpDown nud1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
    }
}