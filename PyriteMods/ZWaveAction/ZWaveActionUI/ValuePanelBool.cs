using System;
using OpenZWaveDotNet;

namespace OZWForm
{
    public class ValuePanelBool: ValuePanel
    {
        private System.Windows.Forms.CheckBox ValueCheckBox;
        
        public ValuePanelBool( ZWValueID valueID ): base( valueID )
        {
            InitializeComponent();

            if (Manager.IsValueReadOnly(valueID))
            {
                ValueCheckBox.Enabled = false;
            }

            ValueCheckBox.Text = Manager.GetValueLabel(valueID);
            
            bool state;
            if (Manager.GetValueAsBool(valueID, out state))
            {
                ValueCheckBox.Checked = state;
            }

            SendChanges = true;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.ValueCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ValueCheckBox
            // 
            this.ValueCheckBox.AutoSize = true;
            this.ValueCheckBox.Location = new System.Drawing.Point(4, 4);
            this.ValueCheckBox.Name = "ValueCheckBox";
            this.ValueCheckBox.Size = new System.Drawing.Size(52, 17);
            this.ValueCheckBox.TabIndex = 0;
            this.ValueCheckBox.Text = "Label";
            this.ValueCheckBox.UseVisualStyleBackColor = true;
            this.ValueCheckBox.CheckedChanged += new System.EventHandler(this.ValueCheckBox_CheckedChanged);
            // 
            // ValuePanelBool
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.ValueCheckBox);
            this.Name = "ValuePanelBool";
            this.Size = new System.Drawing.Size(59, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Handles the CheckedChanged event of the ValueCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ValueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SendChanges)
            {
                Manager.SetValue(ValueID, ValueCheckBox.Checked);
            }
        }
    }
}
