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
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
            //this.TransparencyKey = Color.White;
            this.Opacity = 0.5;
            this.TopMost = true;
        }
    }
}
