namespace PyriteStandartActions.Checkers.Utils
{
    partial class BetweenValueView
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
            this.btOk = new System.Windows.Forms.Button();
            this.nud1 = new System.Windows.Forms.NumericUpDown();
            this.nud2 = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbMore = new System.Windows.Forms.RadioButton();
            this.rbMoreOrEqual = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbLess = new System.Windows.Forms.RadioButton();
            this.rbLessOrEqual = new System.Windows.Forms.RadioButton();
            this.lblParName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(149, 85);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(68, 85);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 4;
            this.btOk.Text = "Ок";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // nud1
            // 
            this.nud1.Location = new System.Drawing.Point(211, 10);
            this.nud1.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nud1.Name = "nud1";
            this.nud1.Size = new System.Drawing.Size(62, 20);
            this.nud1.TabIndex = 6;
            // 
            // nud2
            // 
            this.nud2.Location = new System.Drawing.Point(211, 39);
            this.nud2.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nud2.Name = "nud2";
            this.nud2.Size = new System.Drawing.Size(62, 20);
            this.nud2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbMore);
            this.panel1.Controls.Add(this.rbMoreOrEqual);
            this.panel1.Location = new System.Drawing.Point(123, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(82, 20);
            this.panel1.TabIndex = 8;
            // 
            // rbMore
            // 
            this.rbMore.AutoSize = true;
            this.rbMore.Location = new System.Drawing.Point(8, 0);
            this.rbMore.Name = "rbMore";
            this.rbMore.Size = new System.Drawing.Size(31, 17);
            this.rbMore.TabIndex = 1;
            this.rbMore.TabStop = true;
            this.rbMore.Text = ">";
            this.rbMore.UseVisualStyleBackColor = true;
            // 
            // rbMoreOrEqual
            // 
            this.rbMoreOrEqual.AutoSize = true;
            this.rbMoreOrEqual.Location = new System.Drawing.Point(44, 0);
            this.rbMoreOrEqual.Name = "rbMoreOrEqual";
            this.rbMoreOrEqual.Size = new System.Drawing.Size(37, 17);
            this.rbMoreOrEqual.TabIndex = 0;
            this.rbMoreOrEqual.TabStop = true;
            this.rbMoreOrEqual.Text = ">=";
            this.rbMoreOrEqual.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbLess);
            this.panel2.Controls.Add(this.rbLessOrEqual);
            this.panel2.Location = new System.Drawing.Point(123, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(82, 20);
            this.panel2.TabIndex = 9;
            // 
            // rbLess
            // 
            this.rbLess.AutoSize = true;
            this.rbLess.Location = new System.Drawing.Point(8, 0);
            this.rbLess.Name = "rbLess";
            this.rbLess.Size = new System.Drawing.Size(31, 17);
            this.rbLess.TabIndex = 1;
            this.rbLess.TabStop = true;
            this.rbLess.Text = "<";
            this.rbLess.UseVisualStyleBackColor = true;
            // 
            // rbLessOrEqual
            // 
            this.rbLessOrEqual.AutoSize = true;
            this.rbLessOrEqual.Location = new System.Drawing.Point(44, 0);
            this.rbLessOrEqual.Name = "rbLessOrEqual";
            this.rbLessOrEqual.Size = new System.Drawing.Size(37, 17);
            this.rbLessOrEqual.TabIndex = 0;
            this.rbLessOrEqual.TabStop = true;
            this.rbLessOrEqual.Text = "<=";
            this.rbLessOrEqual.UseVisualStyleBackColor = true;
            // 
            // lblParName
            // 
            this.lblParName.Location = new System.Drawing.Point(12, 9);
            this.lblParName.Name = "lblParName";
            this.lblParName.Size = new System.Drawing.Size(100, 50);
            this.lblParName.TabIndex = 10;
            this.lblParName.Text = "label1";
            this.lblParName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BetweenValueView
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(285, 120);
            this.ControlBox = false;
            this.Controls.Add(this.lblParName);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.nud2);
            this.Controls.Add(this.nud1);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BetweenValueView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Дата между";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.NumericUpDown nud1;
        private System.Windows.Forms.NumericUpDown nud2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbMore;
        private System.Windows.Forms.RadioButton rbMoreOrEqual;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbLess;
        private System.Windows.Forms.RadioButton rbLessOrEqual;
        private System.Windows.Forms.Label lblParName;
    }
}