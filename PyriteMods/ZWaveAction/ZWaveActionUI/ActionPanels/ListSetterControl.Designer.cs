﻿namespace ZWaveActionUI.ActionPanels
{
    partial class ListSetterControl
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
            this.SuspendLayout();
            // 
            // cbValue
            // 
            this.cbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbValue.FormattingEnabled = true;
            this.cbValue.Location = new System.Drawing.Point(3, 2);
            this.cbValue.Name = "cbValue";
            this.cbValue.Size = new System.Drawing.Size(224, 21);
            this.cbValue.TabIndex = 0;
            // 
            // ListSetterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbValue);
            this.Name = "ListSetterControl";
            this.Size = new System.Drawing.Size(230, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbValue;
    }
}