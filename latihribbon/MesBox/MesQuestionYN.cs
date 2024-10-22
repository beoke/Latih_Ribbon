﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class MesQuestionYN : Form
    {
        public MesQuestionYN(string message,int row = 1)
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            lblMessage.Text = message;
            int width = lblMessage.Width + 114;
            this.Width = width;
            if (this.Width < 246) this.Width = 246;
            if (row == 1)
                lblMessage.Location = new Point(lblMessage.Location.X, 41);
            else
                lblMessage.Location = new Point(lblMessage.Location.X , 32);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.No;
            this.Close ();
        }
    }
}