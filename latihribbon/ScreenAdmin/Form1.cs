﻿using latihribbon.ScreenAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class Form1 : RibbonForm
    {
        // halo rek
        public Form1()
        {
            InitializeComponent();

            this.MinimumSize = new Size(1200, 900);

        }
        private void ShowFormInPanel(Form form) // untuk menambahkan form di dalam panel
        {
           
            form.TopLevel = false; // Memberitahu bahwa form ini bagian dari form lain
            form.FormBorderStyle = FormBorderStyle.None; // Menghilangkan border form
            form.Dock = DockStyle.Fill; // Menyesuaikan ukuran form dengan panel
            panel1.Controls.Clear(); // Membersihkan panel dari form atau kontrol lainnya
            panel1.Controls.Add(form);
            form.Show();
        }
        private  void ribbonSiswaAbsensi_Click(object sender, EventArgs e)
        {
            FormSIswa fs = new FormSIswa();
            ShowFormInPanel(fs);

            ClearCheckRibbon();
            ribbonSiswaAbsensi.CheckOnClick = true;
            ribbonSiswaAbsensi.Checked = true;
        }

        private void ribbon_terlambat_Click(object sender, EventArgs e) // untuk menampilkan form di dalam panel
        {
            FormTerlambat formTerlambat = new FormTerlambat();
            ShowFormInPanel(formTerlambat);

            ClearCheckRibbon();
            ribbon_terlambat.CheckOnClick = true;
            ribbon_terlambat.Checked = true;
        }

        private void ribbon_keluar_Click(object sender, EventArgs e)
        {
            FormKeluar formkeluar  = new FormKeluar(); 
            ShowFormInPanel (formkeluar);

            ClearCheckRibbon();
            ribbon_keluar.CheckOnClick = true;
            ribbon_keluar.Checked = true;
        }
     

        private void ribbon_Siswa_Click(object sender, EventArgs e)
        {
            FormSIswa dataSiswa = new FormSIswa();
            ShowFormInPanel(dataSiswa);

            ClearCheckRibbon();
            ribbon_Siswa.CheckOnClick = true;
            ribbon_Siswa.Checked = true;
        }


        private void ribbonAbsensi_Click(object sender, EventArgs e)
        {
            FormAbsensi dataSiswa = new FormAbsensi();
            ShowFormInPanel(dataSiswa);

            ClearCheckRibbon();
            ribbonAbsensi.CheckOnClick = true;
            ribbonAbsensi.Checked = true;
        }

        private void ribbonRekapPersensi_Click(object sender, EventArgs e)
        {
            FormRekapPersensi p = new FormRekapPersensi();
            ShowFormInPanel(p);

            ClearCheckRibbon();
            ribbonRekapPersensi.CheckOnClick = true;
            ribbonRekapPersensi.Checked = true;
        }

        private void ribbonUserLogin_Click(object sender, EventArgs e)
        {
            FormUser_RiwayatLogin formUser_RiwayatLogin = new FormUser_RiwayatLogin();
            ShowFormInPanel(formUser_RiwayatLogin);

            ClearCheckRibbon();
            ribbonUserLogin.CheckOnClick = true;
            ribbonUserLogin.Checked = true;
        }

        private void ribbonKelas_Click(object sender, EventArgs e)
        {
            FormKelas formKelas = new FormKelas();
            ShowFormInPanel(formKelas);

            ClearCheckRibbon();
            ribbonKelas.CheckOnClick = true;
            ribbonKelas.Checked = true;
        }

        private void ribbonJurusan_Click(object sender, EventArgs e)
        {
            FormJurusan formJurusan = new FormJurusan();
            ShowFormInPanel(formJurusan);

            ClearCheckRibbon();
            ribbonJurusan.CheckOnClick = true;
            ribbonJurusan.Checked = true;
        }

        private void ClearCheckRibbon()
        {
            ribbonJurusan.CheckOnClick = false;
            ribbonKelas.CheckOnClick = false;
            ribbonUserLogin.CheckOnClick = false;
            ribbonRekapPersensi.CheckOnClick = false;
            ribbonAbsensi.CheckOnClick = false;
            ribbon_Siswa.CheckOnClick = false;
            ribbon_keluar.CheckOnClick = false;
            ribbon_terlambat.CheckOnClick = false;
            ribbonSiswaAbsensi.CheckOnClick = false;

            ribbonJurusan.Checked = false;
            ribbonKelas.Checked = false;
            ribbonUserLogin.Checked = false;
            ribbonRekapPersensi.Checked = false;
            ribbonAbsensi.Checked = false;
            ribbon_Siswa.Checked = false;
            ribbon_keluar.Checked = false;
            ribbon_terlambat.Checked = false;
            ribbonSiswaAbsensi.Checked = false;
        }
    }
}

