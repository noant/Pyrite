namespace ZWaveActionUI.ActionPanels
{
    partial class StringSetterControl
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
            this.tbValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // nudValue
            // 
            this.tbValue.Location = new System.Drawing.Point(3, 3);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(107, 20);
            this.tbValue.TabIndex = 0;
            // 
            // StringSetterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbValue);
            this.Name = "StringSetterControl";
            this.Size = new System.Drawing.Size(113, 26);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox tbValue;
    }
}
