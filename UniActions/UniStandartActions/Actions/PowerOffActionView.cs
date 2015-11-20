﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniStandartActions.Actions
{
    public partial class PowerOffActionView : Form
    {
        public PowerOffActionView()
        {
            InitializeComponent();
        }

        public int Timer
        {
            get
            {
                return (int)nudTimer.Value;
            }
        }

        public bool CanCancel
        {
            get
            {
                return cbCanCancel.Checked;
            }
        }

        public bool Restart
        {
            get
            {
                return cbRestart.Checked;
            }
        }
    }
}
