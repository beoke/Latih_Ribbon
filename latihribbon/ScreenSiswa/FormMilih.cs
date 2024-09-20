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
        private Pemakai FormPemakai;

        public FormMilih(Pemakai pemakai)
        {
            InitializeComponent();
            FormPemakai = pemakai;

            this.Size = pemakai.Size; 
            this.Location = pemakai.Location;

            txtNIS.Text += " " + Pemakai.NIS;
            txtNama.Text += " " + Pemakai.nama;
            txtKelas.Text += " " + Pemakai.kelas;

/*            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;  // Menempatkan form di atas semua form lain
            this.ControlBox = true;  // Menyembunyikan tombol close, minimize, maximize*/
            this.KeyPreview = true;  // Agar form dapat menangani key press event
        }

        private void btn_masuk_Click(object sender, EventArgs e)
        {
            SuratMasuk suratMasuk = new SuratMasuk(FormPemakai,this);
            this.Hide();
            suratMasuk.Show();
        }

        private void btn_Keluar_Click(object sender, EventArgs e)
        {
            SuratKeluarcs Keluar = new SuratKeluarcs(FormPemakai,this);
            this.Hide();
            Keluar.Show();
        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormPemakai.Show();
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
