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
    public partial class FormTerlambat : Form
    {
        public FormTerlambat()
        {
            InitializeComponent();
        }

        private void btn_terlambat_Click(object sender, EventArgs e)
        {
            PrintTerlambat telat = new PrintTerlambat();
            telat.Show(); 
        }
    }
}