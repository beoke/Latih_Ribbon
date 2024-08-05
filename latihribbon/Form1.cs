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
    public partial class Form1 : RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ribbon1_Click(object sender, EventArgs e)
        {

        }


        private void ribbon_Input_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
          FormLaporan laporan  = new FormLaporan();
          ShowFormInPanel(laporan);
        }

        private void ribbon_surat_Click(object sender, EventArgs e)
        {
            FormSurat surat = new FormSurat();
            ShowFormInPanel(surat);
        }

        private void ribbon_Siswa_Click(object sender, EventArgs e)
        {
            FormSIswa dataSiswa = new FormSIswa();
            ShowFormInPanel(dataSiswa);
        }
    }
}

