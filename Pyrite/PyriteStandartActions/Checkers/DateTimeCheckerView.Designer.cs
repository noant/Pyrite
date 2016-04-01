namespace PyriteStandartActions.Checkers
{
    partial class DateTimeCheckerView
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
            this.cbMonday = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSunday = new System.Windows.Forms.CheckBox();
            this.cbSaturday = new System.Windows.Forms.CheckBox();
            this.cbFriday = new System.Windows.Forms.CheckBox();
            this.cbThursday = new System.Windows.Forms.CheckBox();
            this.cbWednesday = new System.Windows.Forms.CheckBox();
            this.cbTuesday = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbEveryMinute = new System.Windows.Forms.CheckBox();
            this.cbEveryHour = new System.Windows.Forms.CheckBox();
            this.nudMinute = new System.Windows.Forms.NumericUpDown();
            this.nudHour = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbEveryDay = new System.Windows.Forms.CheckBox();
            this.cbEveryMonth = new System.Windows.Forms.CheckBox();
            this.cbEveryYear = new System.Windows.Forms.CheckBox();
            this.tbDateView = new System.Windows.Forms.TextBox();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHour)).BeginInit();
            this.SuspendLayout();
            // 
            // cbMonday
            // 
            this.cbMonday.AutoSize = true;
            this.cbMonday.Checked = true;
            this.cbMonday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMonday.Location = new System.Drawing.Point(6, 19);
            this.cbMonday.Name = "cbMonday";
            this.cbMonday.Size = new System.Drawing.Size(94, 17);
            this.cbMonday.TabIndex = 0;
            this.cbMonday.Text = "Понедельник";
            this.cbMonday.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSunday);
            this.groupBox1.Controls.Add(this.cbSaturday);
            this.groupBox1.Controls.Add(this.cbFriday);
            this.groupBox1.Controls.Add(this.cbThursday);
            this.groupBox1.Controls.Add(this.cbWednesday);
            this.groupBox1.Controls.Add(this.cbTuesday);
            this.groupBox1.Controls.Add(this.cbMonday);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(110, 185);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дни недели";
            // 
            // cbSunday
            // 
            this.cbSunday.AutoSize = true;
            this.cbSunday.Checked = true;
            this.cbSunday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSunday.Location = new System.Drawing.Point(6, 157);
            this.cbSunday.Name = "cbSunday";
            this.cbSunday.Size = new System.Drawing.Size(93, 17);
            this.cbSunday.TabIndex = 6;
            this.cbSunday.Text = "Воскресенье";
            this.cbSunday.UseVisualStyleBackColor = true;
            // 
            // cbSaturday
            // 
            this.cbSaturday.AutoSize = true;
            this.cbSaturday.Checked = true;
            this.cbSaturday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSaturday.Location = new System.Drawing.Point(6, 134);
            this.cbSaturday.Name = "cbSaturday";
            this.cbSaturday.Size = new System.Drawing.Size(67, 17);
            this.cbSaturday.TabIndex = 5;
            this.cbSaturday.Text = "Суббота\r\n";
            this.cbSaturday.UseVisualStyleBackColor = true;
            // 
            // cbFriday
            // 
            this.cbFriday.AutoSize = true;
            this.cbFriday.Checked = true;
            this.cbFriday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFriday.Location = new System.Drawing.Point(6, 111);
            this.cbFriday.Name = "cbFriday";
            this.cbFriday.Size = new System.Drawing.Size(69, 17);
            this.cbFriday.TabIndex = 4;
            this.cbFriday.Text = "Пятница";
            this.cbFriday.UseVisualStyleBackColor = true;
            // 
            // cbThursday
            // 
            this.cbThursday.AutoSize = true;
            this.cbThursday.Checked = true;
            this.cbThursday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbThursday.Location = new System.Drawing.Point(6, 88);
            this.cbThursday.Name = "cbThursday";
            this.cbThursday.Size = new System.Drawing.Size(68, 17);
            this.cbThursday.TabIndex = 3;
            this.cbThursday.Text = "Четверг";
            this.cbThursday.UseVisualStyleBackColor = true;
            // 
            // cbWednesday
            // 
            this.cbWednesday.AutoSize = true;
            this.cbWednesday.Checked = true;
            this.cbWednesday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWednesday.Location = new System.Drawing.Point(6, 65);
            this.cbWednesday.Name = "cbWednesday";
            this.cbWednesday.Size = new System.Drawing.Size(57, 17);
            this.cbWednesday.TabIndex = 2;
            this.cbWednesday.Text = "Среда";
            this.cbWednesday.UseVisualStyleBackColor = true;
            // 
            // cbTuesday
            // 
            this.cbTuesday.AutoSize = true;
            this.cbTuesday.Checked = true;
            this.cbTuesday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTuesday.Location = new System.Drawing.Point(6, 42);
            this.cbTuesday.Name = "cbTuesday";
            this.cbTuesday.Size = new System.Drawing.Size(68, 17);
            this.cbTuesday.TabIndex = 1;
            this.cbTuesday.Text = "Вторник";
            this.cbTuesday.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbEveryMinute);
            this.groupBox2.Controls.Add(this.cbEveryHour);
            this.groupBox2.Controls.Add(this.nudMinute);
            this.groupBox2.Controls.Add(this.nudHour);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbEveryDay);
            this.groupBox2.Controls.Add(this.cbEveryMonth);
            this.groupBox2.Controls.Add(this.cbEveryYear);
            this.groupBox2.Controls.Add(this.tbDateView);
            this.groupBox2.Controls.Add(this.dtPicker);
            this.groupBox2.Location = new System.Drawing.Point(128, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 185);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Дата и время";
            // 
            // cbEveryMinute
            // 
            this.cbEveryMinute.AutoSize = true;
            this.cbEveryMinute.Location = new System.Drawing.Point(118, 144);
            this.cbEveryMinute.Name = "cbEveryMinute";
            this.cbEveryMinute.Size = new System.Drawing.Size(104, 17);
            this.cbEveryMinute.TabIndex = 7;
            this.cbEveryMinute.Text = "Каждую минуту";
            this.cbEveryMinute.UseVisualStyleBackColor = true;
            // 
            // cbEveryHour
            // 
            this.cbEveryHour.AutoSize = true;
            this.cbEveryHour.Location = new System.Drawing.Point(118, 121);
            this.cbEveryHour.Name = "cbEveryHour";
            this.cbEveryHour.Size = new System.Drawing.Size(87, 17);
            this.cbEveryHour.TabIndex = 6;
            this.cbEveryHour.Text = "Каждый час";
            this.cbEveryHour.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cbEveryHour.UseVisualStyleBackColor = true;
            // 
            // nudMinute
            // 
            this.nudMinute.Location = new System.Drawing.Point(56, 74);
            this.nudMinute.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudMinute.Name = "nudMinute";
            this.nudMinute.Size = new System.Drawing.Size(63, 20);
            this.nudMinute.TabIndex = 2;
            // 
            // nudHour
            // 
            this.nudHour.Location = new System.Drawing.Point(56, 47);
            this.nudHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudHour.Name = "nudHour";
            this.nudHour.Size = new System.Drawing.Size(63, 20);
            this.nudHour.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Минута";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Час";
            // 
            // cbEveryDay
            // 
            this.cbEveryDay.AutoSize = true;
            this.cbEveryDay.Location = new System.Drawing.Point(10, 154);
            this.cbEveryDay.Name = "cbEveryDay";
            this.cbEveryDay.Size = new System.Drawing.Size(94, 17);
            this.cbEveryDay.TabIndex = 5;
            this.cbEveryDay.Text = "Каждый день";
            this.cbEveryDay.UseVisualStyleBackColor = true;
            // 
            // cbEveryMonth
            // 
            this.cbEveryMonth.AutoSize = true;
            this.cbEveryMonth.Location = new System.Drawing.Point(10, 131);
            this.cbEveryMonth.Name = "cbEveryMonth";
            this.cbEveryMonth.Size = new System.Drawing.Size(102, 17);
            this.cbEveryMonth.TabIndex = 4;
            this.cbEveryMonth.Text = "Каждый месяц";
            this.cbEveryMonth.UseVisualStyleBackColor = true;
            // 
            // cbEveryYear
            // 
            this.cbEveryYear.AutoSize = true;
            this.cbEveryYear.Location = new System.Drawing.Point(10, 108);
            this.cbEveryYear.Name = "cbEveryYear";
            this.cbEveryYear.Size = new System.Drawing.Size(87, 17);
            this.cbEveryYear.TabIndex = 3;
            this.cbEveryYear.Text = "Каждый год";
            this.cbEveryYear.UseVisualStyleBackColor = true;
            // 
            // tbDateView
            // 
            this.tbDateView.Location = new System.Drawing.Point(6, 19);
            this.tbDateView.Name = "tbDateView";
            this.tbDateView.ReadOnly = true;
            this.tbDateView.Size = new System.Drawing.Size(195, 20);
            this.tbDateView.TabIndex = 0;
            // 
            // dtPicker
            // 
            this.dtPicker.Location = new System.Drawing.Point(201, 19);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(19, 20);
            this.dtPicker.TabIndex = 0;
            this.dtPicker.Value = new System.DateTime(2015, 11, 11, 21, 26, 55, 0);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(283, 206);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(196, 206);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 3;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // DateTimeCheckerView
            // 
            this.AcceptButton = this.btOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(371, 239);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "DateTimeCheckerView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проверка по дате";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHour)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbDateView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        internal System.Windows.Forms.CheckBox cbMonday;
        internal System.Windows.Forms.CheckBox cbSunday;
        internal System.Windows.Forms.CheckBox cbSaturday;
        internal System.Windows.Forms.CheckBox cbFriday;
        internal System.Windows.Forms.CheckBox cbThursday;
        internal System.Windows.Forms.CheckBox cbWednesday;
        internal System.Windows.Forms.CheckBox cbTuesday;
        internal System.Windows.Forms.DateTimePicker dtPicker;
        internal System.Windows.Forms.CheckBox cbEveryDay;
        internal System.Windows.Forms.CheckBox cbEveryMonth;
        internal System.Windows.Forms.CheckBox cbEveryYear;
        internal System.Windows.Forms.CheckBox cbEveryMinute;
        internal System.Windows.Forms.CheckBox cbEveryHour;
        internal System.Windows.Forms.NumericUpDown nudMinute;
        internal System.Windows.Forms.NumericUpDown nudHour;
    }
}