using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
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
        private Form mainForm;
        public FormMilih(Form mainForm,string NIS,string nama,string kelas)
        {
            InitializeComponent();

            this.NIS = NIS;
            this.nama = nama;
            this.kelas = kelas;
            txtNIS.Text += " " + NIS;
            txtNama.Text += " " + nama;
            txtKelas.Text += " " + kelas;

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;  
            this.ControlBox = true;  
            this.KeyPreview = true;  
            this.mainForm = mainForm;
        }

        private void btn_masuk_Click(object sender, EventArgs e)
        {
            SuratMasuk suratMasuk = new SuratMasuk(mainForm, NIS,nama,kelas);
            suratMasuk.Show();
            this.Close();
        }

        private void btn_Keluar_Click(object sender, EventArgs e)
        {
            SuratKeluarcs Keluar = new SuratKeluarcs(mainForm, NIS,nama,kelas);
            Keluar.Show();
            this.Close();
        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            Pemakai p = new Pemakai(this);
            p.Show();
            this.Close();
        }

        private void FormMilih_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.K)
            {
                mainForm.Show();

                this.Close();

                // Keluar dari aplikasi saat kombinasi tombol Ctrl + Alt + K ditekan
                //Application.Exit();
            }
        }
    }
}
