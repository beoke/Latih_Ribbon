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
    public partial class SuratKeluarcs : Form
    {
        private Form _previousForm; // untuk kembali ke form sebelumnya

        public SuratKeluarcs(Form previousForm)
        {
            InitializeComponent();
            _previousForm = previousForm; // menyimpan referensi ke form sebelumnya
           
            // Menyelaraskan ukuran dan lokasi form ini dengan form sebelumnya
            this.Size = previousForm.Size; // Menyetel ukuran form
            this.Location = previousForm.Location; // Menyetel lokasi form

            isian();
            jam();
        }

        public void isian()
        {
            txtNIS.Text = Pemakai.NIS;
            txtNama.Text = Pemakai.nama;
            txtKelas.Text = Pemakai.kelas;
        }

        public void jam()
        {
            List<string> jam = new List<string>() { "Jam Ke-1","Jam Ke-2","Jam Ke-3","Jam Ke-4","Jam Ke-5","Jam Ke-6","Jam Ke-7","Jam Ke-8","Jam Ke-9","Jam Ke-10" };
            combojam.DataSource = jam;

        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            // menampilkan form sebelumnya dan menutup form saat ini
            _previousForm.Show();
            this.Close();
        }
    }
}
