namespace WakeOnLanAction
{
    partial class MacAddressBox
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
            this.hexByteBox4 = new WakeOnLanAction.HexByteBox();
            this.hexByteBox3 = new WakeOnLanAction.HexByteBox();
            this.hexByteBox2 = new WakeOnLanAction.HexByteBox();
            this.hexByteBox1 = new WakeOnLanAction.HexByteBox();
            this.hexByteBox6 = new WakeOnLanAction.HexByteBox();
            this.hexByteBox5 = new WakeOnLanAction.HexByteBox();
            this.SuspendLayout();
            // 
            // hexByteBox4
            // 
            this.hexByteBox4.HexValue = "0";
            this.hexByteBox4.Location = new System.Drawing.Point(144, 3);
            this.hexByteBox4.Name = "hexByteBox4";
            this.hexByteBox4.ShowSplitter = true;
            this.hexByteBox4.Size = new System.Drawing.Size(51, 27);
            this.hexByteBox4.TabIndex = 3;
            // 
            // hexByteBox3
            // 
            this.hexByteBox3.HexValue = "0";
            this.hexByteBox3.Location = new System.Drawing.Point(97, 3);
            this.hexByteBox3.Name = "hexByteBox3";
            this.hexByteBox3.ShowSplitter = true;
            this.hexByteBox3.Size = new System.Drawing.Size(51, 27);
            this.hexByteBox3.TabIndex = 2;
            // 
            // hexByteBox2
            // 
            this.hexByteBox2.HexValue = "0";
            this.hexByteBox2.Location = new System.Drawing.Point(50, 3);
            this.hexByteBox2.Name = "hexByteBox2";
            this.hexByteBox2.ShowSplitter = true;
            this.hexByteBox2.Size = new System.Drawing.Size(51, 27);
            this.hexByteBox2.TabIndex = 1;
            // 
            // hexByteBox1
            // 
            this.hexByteBox1.HexValue = "0";
            this.hexByteBox1.Location = new System.Drawing.Point(3, 3);
            this.hexByteBox1.Name = "hexByteBox1";
            this.hexByteBox1.ShowSplitter = true;
            this.hexByteBox1.Size = new System.Drawing.Size(51, 27);
            this.hexByteBox1.TabIndex = 0;
            // 
            // hexByteBox6
            // 
            this.hexByteBox6.HexValue = "0";
            this.hexByteBox6.Location = new System.Drawing.Point(238, 3);
            this.hexByteBox6.Name = "hexByteBox6";
            this.hexByteBox6.ShowSplitter = false;
            this.hexByteBox6.Size = new System.Drawing.Size(51, 27);
            this.hexByteBox6.TabIndex = 5;
            this.hexByteBox6.Value = ((byte)(0));
            // 
            // hexByteBox5
            // 
            this.hexByteBox5.HexValue = "0";
            this.hexByteBox5.Location = new System.Drawing.Point(191, 3);
            this.hexByteBox5.Name = "hexByteBox5";
            this.hexByteBox5.ShowSplitter = true;
            this.hexByteBox5.Size = new System.Drawing.Size(51, 27);
            this.hexByteBox5.TabIndex = 4;
            this.hexByteBox5.Value = ((byte)(0));
            // 
            // MacAddressBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hexByteBox6);
            this.Controls.Add(this.hexByteBox5);
            this.Controls.Add(this.hexByteBox4);
            this.Controls.Add(this.hexByteBox3);
            this.Controls.Add(this.hexByteBox2);
            this.Controls.Add(this.hexByteBox1);
            this.Name = "MacAddressBox";
            this.Size = new System.Drawing.Size(287, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private HexByteBox hexByteBox1;
        private HexByteBox hexByteBox2;
        private HexByteBox hexByteBox3;
        private HexByteBox hexByteBox4;
        private HexByteBox hexByteBox6;
        private HexByteBox hexByteBox5;
    }
}
