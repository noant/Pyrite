namespace PyriteStandartActions.Actions
{
    partial class PowerOffActionView
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
            this.nudTimer = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCanCancel = new System.Windows.Forms.CheckBox();
            this.cbRestart = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(147, 114);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(66, 114);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 3;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // nudTimer
            // 
            this.nudTimer.Location = new System.Drawing.Point(105, 12);
            this.nudTimer.Name = "nudTimer";
            this.nudTimer.Size = new System.Drawing.Size(120, 20);
            this.nudTimer.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Таймер, секунд";
            // 
            // cbCanCancel
            // 
            this.cbCanCancel.AutoSize = true;
            this.cbCanCancel.Location = new System.Drawing.Point(15, 46);
            this.cbCanCancel.Name = "cbCanCancel";
            this.cbCanCancel.Size = new System.Drawing.Size(147, 17);
            this.cbCanCancel.TabIndex = 1;
            this.cbCanCancel.Text = "Возможность отменить";
            this.cbCanCancel.UseVisualStyleBackColor = true;
            // 
            // cbRestart
            // 
            this.cbRestart.AutoSize = true;
            this.cbRestart.Location = new System.Drawing.Point(15, 69);
            this.cbRestart.Name = "cbRestart";
            this.cbRestart.Size = new System.Drawing.Size(98, 17);
            this.cbRestart.TabIndex = 2;
            this.cbRestart.Text = "Перезагрузка";
            this.cbRestart.UseVisualStyleBackColor = true;
            // 
            // PowerOffActionView
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(234, 149);
            this.Controls.Add(this.cbRestart);
            this.Controls.Add(this.cbCanCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudTimer);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PowerOffActionView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выключение компьютера";
            ((System.ComponentModel.ISupportInitialize)(this.nudTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.NumericUpDown nudTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbCanCancel;
        private System.Windows.Forms.CheckBox cbRestart;
    }
}