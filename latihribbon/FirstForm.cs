﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class FirstForm : Form
    {
        private Timer opacityTimer;
        private Timer delayTimer;

        public FirstForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;

            this.Opacity = 0;
            this.opacityTimer = new Timer();
            this.delayTimer = new Timer();

            this.opacityTimer.Interval = 20;
            this.opacityTimer.Tick += OpacityTimer_Tick;
            this.delayTimer.Interval = 2000;
            this.delayTimer.Tick += DelayTimer_Tick;
            this.Load += FirstForm_Load;

        }

        private void FirstForm_Load(object sender, EventArgs e)
        {
            this.opacityTimer.Start(); 
        }

        private void OpacityTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.05; 
            }
            else
            {
                this.opacityTimer.Stop(); 
                this.delayTimer.Start(); 
            }
        }

        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            this.delayTimer.Stop();
            FormIndex formIndex = new FormIndex(this);
            formIndex.WindowState = FormWindowState.Maximized;
            formIndex.Show();
            this.Hide();
        }
    }
}
