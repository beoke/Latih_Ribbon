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
    public partial class EditSiswa : Form
    {

        public EditSiswa()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            RegisterEvent();
        }

        private void RegisterEvent()
        {
            btnSave_FormSiswa.Click += BtnSave_FormSiswa_Click;
        }

        private void BtnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            new MesQuestionYN("Sya amaka aia aua aua aa aua aua a aa aua aua wuw wuw wuw wuwwwww uww",1).ShowDialog();
        }
    }
}
