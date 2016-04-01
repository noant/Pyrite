namespace PyriteStandartActions.Actions
{
    partial class ProcessActionView
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
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.tbArgs = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbStateOn = new System.Windows.Forms.TextBox();
            this.tbStateOff = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTracking = new System.Windows.Forms.CheckBox();
            this.cbCloseMainWindow = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tbPath
            // 
            this.tbPath.BackColor = System.Drawing.SystemColors.Window;
            this.tbPath.Location = new System.Drawing.Point(61, 12);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(572, 20);
            this.tbPath.TabIndex = 0;
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(639, 11);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 23);
            this.btBrowse.TabIndex = 1;
            this.btBrowse.Text = "Выбрать";
            this.btBrowse.UseVisualStyleBackColor = true;
            // 
            // tbArgs
            // 
            this.tbArgs.BackColor = System.Drawing.SystemColors.Window;
            this.tbArgs.Location = new System.Drawing.Point(81, 49);
            this.tbArgs.Name = "tbArgs";
            this.tbArgs.Size = new System.Drawing.Size(631, 20);
            this.tbArgs.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Файл";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Аргументы";
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(639, 129);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 8;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(558, 129);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 7;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Статус \"Включен\"";
            // 
            // tbStateOn
            // 
            this.tbStateOn.BackColor = System.Drawing.SystemColors.Window;
            this.tbStateOn.Location = new System.Drawing.Point(116, 84);
            this.tbStateOn.Name = "tbStateOn";
            this.tbStateOn.Size = new System.Drawing.Size(109, 20);
            this.tbStateOn.TabIndex = 3;
            // 
            // tbStateOff
            // 
            this.tbStateOff.BackColor = System.Drawing.SystemColors.Window;
            this.tbStateOff.Location = new System.Drawing.Point(369, 84);
            this.tbStateOff.Name = "tbStateOff";
            this.tbStateOff.Size = new System.Drawing.Size(109, 20);
            this.tbStateOff.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Статус \"Выключен\"";
            // 
            // cbTracking
            // 
            this.cbTracking.AutoSize = true;
            this.cbTracking.Location = new System.Drawing.Point(12, 129);
            this.cbTracking.Name = "cbTracking";
            this.cbTracking.Size = new System.Drawing.Size(157, 17);
            this.cbTracking.TabIndex = 5;
            this.cbTracking.Text = "Отслеживание состояния";
            this.cbTracking.UseVisualStyleBackColor = true;
            // 
            // cbCloseMainWindow
            // 
            this.cbCloseMainWindow.AutoSize = true;
            this.cbCloseMainWindow.Location = new System.Drawing.Point(175, 129);
            this.cbCloseMainWindow.Name = "cbCloseMainWindow";
            this.cbCloseMainWindow.Size = new System.Drawing.Size(269, 17);
            this.cbCloseMainWindow.TabIndex = 6;
            this.cbCloseMainWindow.Text = "Не убивать процесс, а закрывать главное окно";
            this.cbCloseMainWindow.UseVisualStyleBackColor = true;
            // 
            // ProcessActionView
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(724, 163);
            this.Controls.Add(this.cbCloseMainWindow);
            this.Controls.Add(this.cbTracking);
            this.Controls.Add(this.tbStateOff);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbStateOn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbArgs);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.tbPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProcessActionView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Запуск процесса";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.TextBox tbArgs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbStateOn;
        private System.Windows.Forms.TextBox tbStateOff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbTracking;
        private System.Windows.Forms.CheckBox cbCloseMainWindow;
    }
}