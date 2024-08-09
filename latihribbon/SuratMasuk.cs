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
    public partial class SuratMasuk : Form
    {
        private Form _previousForm; // untuk kembali ke form sebelumnya
        public SuratMasuk(Form previousForm)
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.SuratMasuk_Load);
            _previousForm = previousForm; // menyimpan referensi ke form sebelumnya

            // Menyelaraskan ukuran dan lokasi form ini dengan form sebelumnya
            this.Size = previousForm.Size; // Menyetel ukuran form
            this.Location = previousForm.Location; // Menyetel lokasi form
            isian();

            Tgl();
            
        }

        public void isian()
        {
            txtNIS.Text = Pemakai.NIS;
            txtNama.Text = Pemakai.nama;
            txtKelas.Text = Pemakai.kelas;
        }

        private void btn_Kembali_Click(object sender, EventArgs e)
        {
            // menampilkan form sebelumnya dan menutup form saat ini
            _previousForm.Show();
            this.Close();

        }

        private void SuratMasuk_Load(object sender, EventArgs e)
        {
            // Format waktu yang diinginkan
            string currentTime = DateTime.Now.ToString("HH:mm");

            // Atur waktu saat ini ke TextBox
            tx_jam1.Text = currentTime;
            /*
            // Opsional: Jika ingin membuat TextBox read-only agar tidak bisa diubah pengguna
              tx_keluar.ReadOnly = true;*/
        }

        public void Tgl()
        {
            txtTanggal.Text = DateTime.Now.ToString("dddd ,dd MMMM yyyy");
        }

        private void tx_jam1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTanggal_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}

