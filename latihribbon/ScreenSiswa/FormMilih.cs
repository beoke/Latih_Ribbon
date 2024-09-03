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
        private Form _previousForm;

        public FormMilih(Form previousForm)
        {
            InitializeComponent();
            _previousForm = previousForm;

            // Menyelaraskan ukuran dan lokasi form ini dengan form sebelumnya
            this.Size = previousForm.Size; // Menyetel ukuran form
            this.Location = previousForm.Location; // Menyetel lokasi form
           // InitializeLabels();
        }

        private void btn_masuk_Click(object sender, EventArgs e)
        {
            // membuka form SuratMasuk dan menyembunyikan MainForm
            SuratMasuk suratMasuk = new SuratMasuk(this);
            suratMasuk.Show();
            this.Hide();
        }

        private void btn_Keluar_Click(object sender, EventArgs e)
        {
            // membuka form SuratKeluar dan menyembunyikan MainForm
            SuratKeluarcs Keluar = new SuratKeluarcs(this);
            Keluar.Show();
            this.Hide();
        }

        private void FormMilih_Load(object sender, EventArgs e)
        {
            txtNIS.Text += " "+Pemakai.NIS;
            txtNama.Text += " " + Pemakai.nama;
            txtKelas.Text += " " + Pemakai.kelas;
        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            Pemakai p = new Pemakai();
            p.Show();
            this.Close();
        }
    }
}
