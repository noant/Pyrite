namespace ZWaveActionUI.ActionPanels
{
    partial class NumericSetterControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nudValue = new System.Windows.Forms.NumericUpDown();
            this.cbMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).BeginInit();
            this.SuspendLayout();
            // 
            // nudValue
            // 
            this.nudValue.Location = new System.Drawing.Point(3, 3);
            this.nudValue.Name = "nudValue";
            this.nudValue.Size = new System.Drawing.Size(107, 20);
            this.nudValue.TabIndex = 0;
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "Выставить",
            "Инкремент",
            "Декремент"});
            this.cbMode.Location = new System.Drawing.Point(114, 3);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(121, 21);
            this.cbMode.TabIndex = 1;
            // 
            // NumericSetterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbMode);
            this.Controls.Add(this.nudValue);
            this.Name = "NumericSetterControl";
            this.Size = new System.Drawing.Size(239, 26);
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudValue;
        private System.Windows.Forms.ComboBox cbMode;
    }
}
