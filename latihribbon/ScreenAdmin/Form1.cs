using latihribbon.ScreenAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }

        private void ribbon_terlambat_Click(object sender, EventArgs e) // untuk menampilkan form di dalam panel
        {
            FormTerlambat formTerlambat = new FormTerlambat();
            ShowFormInPanel(formTerlambat);
        }

        private void ribbon_keluar_Click(object sender, EventArgs e)
        {
            FormKeluar formkeluar  = new FormKeluar(); 
            ShowFormInPanel (formkeluar);
        }

        private void ribbon_Laporan_Click(object sender, EventArgs e)
        {
          FormJurusan jurusan  = new FormJurusan();
          ShowFormInPanel(jurusan);
        }

     

        private void ribbon_Siswa_Click(object sender, EventArgs e)
        {
            FormSIswa dataSiswa = new FormSIswa();
            ShowFormInPanel(dataSiswa);
        }


        private void ribbonAbsensi_Click(object sender, EventArgs e)
        {
            FormAbsensi dataSiswa = new FormAbsensi();
            ShowFormInPanel(dataSiswa);
        }

        private void ribbonRekapPersensi_Click(object sender, EventArgs e)
        {
            FormRekapPersensi p = new FormRekapPersensi();
            ShowFormInPanel(p);
        }

        private void ribbon_riwayatLogin_Click(object sender, EventArgs e)
        {
            FormUser_RiwayatLogin r = new FormUser_RiwayatLogin();
            ShowFormInPanel(r);
        }

        private void ribbon_InputSiswa_Click(object sender, EventArgs e)
        {
            FormKelas fk = new FormKelas();
            ShowFormInPanel(fk);
        }
    }
}

