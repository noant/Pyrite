namespace ZWaveActionUI
{
    partial class TargetNodeValueSelectForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbControllerPath = new System.Windows.Forms.TextBox();
            this.btControllerSelect = new System.Windows.Forms.Button();
            this.btNodeSelect = new System.Windows.Forms.Button();
            this.tbNodeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btParameterSelect = new System.Windows.Forms.Button();
            this.tbParameterName = new System.Windows.Forms.TextBox();
            this.labelParameter = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Контроллер:";
            // 
            // tbControllerPath
            // 
            this.tbControllerPath.Location = new System.Drawing.Point(84, 12);
            this.tbControllerPath.Name = "tbControllerPath";
            this.tbControllerPath.ReadOnly = true;
            this.tbControllerPath.Size = new System.Drawing.Size(336, 20);
            this.tbControllerPath.TabIndex = 1;
            // 
            // btControllerSelect
            // 
            this.btControllerSelect.Location = new System.Drawing.Point(426, 10);
            this.btControllerSelect.Name = "btControllerSelect";
            this.btControllerSelect.Size = new System.Drawing.Size(51, 23);
            this.btControllerSelect.TabIndex = 2;
            this.btControllerSelect.Text = "...";
            this.btControllerSelect.UseVisualStyleBackColor = true;
            // 
            // btNodeSelect
            // 
            this.btNodeSelect.Location = new System.Drawing.Point(426, 39);
            this.btNodeSelect.Name = "btNodeSelect";
            this.btNodeSelect.Size = new System.Drawing.Size(51, 23);
            this.btNodeSelect.TabIndex = 4;
            this.btNodeSelect.Text = "...";
            this.btNodeSelect.UseVisualStyleBackColor = true;
            // 
            // tbNodeName
            // 
            this.tbNodeName.Location = new System.Drawing.Point(84, 41);
            this.tbNodeName.Name = "tbNodeName";
            this.tbNodeName.ReadOnly = true;
            this.tbNodeName.Size = new System.Drawing.Size(336, 20);
            this.tbNodeName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Узел:";
            // 
            // btParameterSelect
            // 
            this.btParameterSelect.Location = new System.Drawing.Point(426, 68);
            this.btParameterSelect.Name = "btParameterSelect";
            this.btParameterSelect.Size = new System.Drawing.Size(51, 23);
            this.btParameterSelect.TabIndex = 6;
            this.btParameterSelect.Text = "...";
            this.btParameterSelect.UseVisualStyleBackColor = true;
            // 
            // tbParameterName
            // 
            this.tbParameterName.Location = new System.Drawing.Point(84, 70);
            this.tbParameterName.Name = "tbParameterName";
            this.tbParameterName.ReadOnly = true;
            this.tbParameterName.Size = new System.Drawing.Size(336, 20);
            this.tbParameterName.TabIndex = 5;
            // 
            // labelParameter
            // 
            this.labelParameter.AutoSize = true;
            this.labelParameter.Location = new System.Drawing.Point(17, 73);
            this.labelParameter.Name = "labelParameter";
            this.labelParameter.Size = new System.Drawing.Size(61, 13);
            this.labelParameter.TabIndex = 6;
            this.labelParameter.Text = "Параметр:";
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(402, 99);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 8;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(321, 99);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 7;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // TargetNodeValueSelectForm
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(489, 132);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btParameterSelect);
            this.Controls.Add(this.tbParameterName);
            this.Controls.Add(this.labelParameter);
            this.Controls.Add(this.btNodeSelect);
            this.Controls.Add(this.tbNodeName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btControllerSelect);
            this.Controls.Add(this.tbControllerPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "TargetNodeValueSelectForm";
            this.ShowIcon = false;
            this.Text = "Контроллер/узел/параметр";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbControllerPath;
        private System.Windows.Forms.Button btControllerSelect;
        private System.Windows.Forms.Button btNodeSelect;
        private System.Windows.Forms.TextBox tbNodeName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btParameterSelect;
        private System.Windows.Forms.TextBox tbParameterName;
        private System.Windows.Forms.Label labelParameter;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
    }
}