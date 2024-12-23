﻿using DocumentFormat.OpenXml.Spreadsheet;
using System;
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
    public partial class MesError : Form
    {
        public MesError(string message, int row = 1)
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            lblMessage.Text = message;
            int width = lblMessage.Width + 117;
            this.Width = width;
            if (this.Width < 246) this.Width = 246;
            if (row == 1)
                lblMessage.Location = new Point(lblMessage.Location.X, 41);
            else if(row == 2)
                lblMessage.Location = new Point(lblMessage.Location.X, 32);
            else
                lblMessage.Location = new Point(lblMessage.Location.X, 22);

            this.Load += MesError_Load;
        }

        private void MesError_Load(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Hand.Play();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
