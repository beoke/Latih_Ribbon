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
        private Form FormPemakai;

        public FormMilih(Form pemakai)
        {
            InitializeComponent();
            FormPemakai = pemakai;

            this.Size = pemakai.Size; 
            this.Location = pemakai.Location;

            txtNIS.Text += " " + Pemakai.NIS;
            txtNama.Text += " " + Pemakai.nama;
            txtKelas.Text += " " + Pemakai.kelas;
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
    }
}
