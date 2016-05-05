namespace ZWaveActionUI.ActionPanels
{
    partial class BoolSetterControl
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
            this.cbValue = new System.Windows.Forms.ComboBox();
            this.cbInvert = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cbValue
            // 
            this.cbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValue.FormattingEnabled = true;
            this.cbValue.Items.AddRange(new object[] {
            "Да",
            "Нет"});
            this.cbValue.Location = new System.Drawing.Point(3, 2);
            this.cbValue.Name = "cbValue";
            this.cbValue.Size = new System.Drawing.Size(108, 21);
            this.cbValue.TabIndex = 0;
            // 
            // cbInvert
            // 
            this.cbInvert.AutoSize = true;
            this.cbInvert.Location = new System.Drawing.Point(117, 5);
            this.cbInvert.Name = "cbInvert";
            this.cbInvert.Size = new System.Drawing.Size(253, 17);
            this.cbInvert.TabIndex = 1;
            this.cbInvert.Text = "Переключать на противоположное значение";
            this.cbInvert.UseVisualStyleBackColor = true;
            this.cbInvert.CheckedChanged += new System.EventHandler(this.cbInvert_CheckedChanged);
            // 
            // BoolSetterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbInvert);
            this.Controls.Add(this.cbValue);
            this.Name = "BoolSetterControl";
            this.Size = new System.Drawing.Size(370, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbValue;
        private System.Windows.Forms.CheckBox cbInvert;
    }
}
