using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class FormMilih : Form
    {
        private string NIS;
        private string nama;
        private string kelas;
        public FormMilih(string NIS,string nama,string kelas)
        {
            InitializeComponent();

            this.NIS = NIS;
            this.nama = nama;
            this.kelas = kelas;
            txtNIS.Text += " " + NIS;
            txtNama.Text += " " + nama;
            txtKelas.Text += " " + kelas;

/*            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;  // Menempatkan form di atas semua form lain
            this.ControlBox = true;  // Menyembunyikan tombol close, minimize, maximize*/
            this.KeyPreview = true;  // Agar form dapat menangani key press event
        }

        private void btn_masuk_Click(object sender, EventArgs e)
        {
            SuratMasuk suratMasuk = new SuratMasuk(NIS,nama,kelas);
            suratMasuk.Show();
            this.Close();
        }

        private void btn_Keluar_Click(object sender, EventArgs e)
        {
            SuratKeluarcs Keluar = new SuratKeluarcs(NIS,nama,kelas);
            Keluar.Show();
            this.Close();
        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            Pemakai p = new Pemakai();
            p.Show();
            this.Close();
        }

        private void FormMilih_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.K)
            {
                // Keluar dari aplikasi saat kombinasi tombol Ctrl + Alt + K ditekan
                Application.Exit();
            }
        }
    }
}
