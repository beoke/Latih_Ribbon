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
    public partial class SuratKeluarcs : Form
    {
        private Form _previousForm; // untuk kembali ke form sebelumnya

        public SuratKeluarcs(Form previousForm)
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.SuratKeluarcs_Load);
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
            txtTanggal.Text = DateTime.Now.ToString("ddd dd MMM yyyy");
            List<string> jam = new List<string>() { "Jam Ke-1","Jam Ke-2","Jam Ke-3","Jam Ke-4","Jam Ke-5","Jam Ke-6","Jam Ke-7","Jam Ke-8","Jam Ke-9","Jam Ke-10" };
            combojam.DataSource = jam;

        }

        private void btn_kembali_Click(object sender, EventArgs e)
        {
            // menampilkan form sebelumnya dan menutup form saat ini
            _previousForm.Show();
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void SuratKeluarcs_Load(object sender, EventArgs e) // ubah ke jam saja
        {
            // Format waktu yang diinginkan
            string currentTime = DateTime.Now.ToString("HH:mm");

            // Atur waktu saat ini ke TextBox
            tx_keluar.Text = currentTime;
/*
            // Opsional: Jika ingin membuat TextBox read-onsly agar tidak bisa diubah pengguna
            tx_keluar.ReadOnly = true;*/
        }

        private void btn_PrintKeluar_Click(object sender, EventArgs e)
        {
            printPreviewDialogKeluar.Document = printDocumentKeluar;
            // print preview untuk menampilkan lembar yang akan di print , jika ingin langsug print bisa pakai ==> "printDocumentKeluar.Print";
            printDocumentKeluar.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Detail", 400, 590);
            printDocumentKeluar.Print();
        }

        private void printDocumentKeluar_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Surat Ijin Meninggalkan Pelajaran", new Font("Times New Roman", 9),Brushes.Black, new Point(100,15) );
            e.Graphics.DrawString("Bantul," + DateTime.Now.ToString("dd MMM yyyy"), new Font("Times New Roman", 6), Brushes.Black, new Point(170, 30));
            e.Graphics.DrawString($"Nama                   : {txtNama.Text}", new Font("Times New Roman", 8),Brushes.Black, new Point(30,50) );
            e.Graphics.DrawString($"Kelas                  : {txtKelas.Text}", new Font("Times New Roman", 8),Brushes.Black, new Point(30,70) );
            e.Graphics.DrawString($"Tanggal                : {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 90));
            e.Graphics.DrawString($"Keluar pada jam      : {tx_keluar.Text}", new Font("Times New Roman", 8),Brushes.Black, new Point(30,110) );
            e.Graphics.DrawString($"Kembali pada jam ke : {combojam.SelectedItem}", new Font("Times New Roman", 8),Brushes.Black, new Point(30,130) );
            e.Graphics.DrawString($"Keperluan  : {txtAlasan.Text}", new Font("Times New Roman", 8),Brushes.Black, new Point(30,170) );
            e.Graphics.DrawString($"Mengetahui Guru BK", new Font("Times New Roman", 7),Brushes.Black, new Point(280,50) );
            e.Graphics.DrawString($"...................", new Font("Times New Roman", 8),Brushes.Black, new Point(290,90) );
            e.Graphics.DrawString($"Mengetahui Guru Pengajar", new Font("Times New Roman", 7),Brushes.Black, new Point(270,110) );
            e.Graphics.DrawString($"........................", new Font("Times New Roman", 8),Brushes.Black, new Point(285,150) );

            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - -  - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(0, 190));
                                                                                                              
            e.Graphics.DrawString("Surat Ijin Meninggalkan Pelajaran", new Font("Times New Roman", 9),Brushes.Black, new Point(100,205) );
            e.Graphics.DrawString("Bantul," + DateTime.Now.ToString("dd MMM yyyy"), new Font("Times New Roman", 6), Brushes.Black, new Point(170, 220));
            e.Graphics.DrawString($"Nama                   : {txtNama.Text}", new Font("Times New Roman", 8),Brushes.Black, new Point(30,240) );
            e.Graphics.DrawString($"Kelas                  : {txtKelas.Text}", new Font("Times New Roman", 8),Brushes.Black, new Point(30,260) );
            e.Graphics.DrawString($"Tanggal                : {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 280));
            e.Graphics.DrawString($"Keluar pada jam        : {tx_keluar.Text}", new Font("Times New Roman", 8),Brushes.Black, new Point(30,300) );
            e.Graphics.DrawString($"Kembali pada jam ke    : {combojam.SelectedItem}", new Font("Times New Roman", 8),Brushes.Black, new Point(30, 320));
            e.Graphics.DrawString($"Keperluan : {txtAlasan.Text}", new Font("Times New Roman", 8),Brushes.Black, new Point(30,360) );
            e.Graphics.DrawString($"( Tinggal di pos satpam )", new Font("Times New Roman", 7),Brushes.Black, new Point(270,280) );
            e.Graphics.DrawString($"Mengetahui Guru BK", new Font("Times New Roman", 7),Brushes.Black, new Point(280,300) );
            e.Graphics.DrawString($"........................", new Font("Times New Roman", 8),Brushes.Black, new Point(285, 340));

            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(0, 380));

            e.Graphics.DrawString("Surat Ijin Meninggalkan Pelajaran", new Font("Times New Roman", 9), Brushes.Black, new Point(100, 395));
            e.Graphics.DrawString("Bantul," + DateTime.Now.ToString("dd MMM yyyy"), new Font("Times New Roman", 6), Brushes.Black, new Point(170, 410));
            e.Graphics.DrawString($"Nama                   : {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 430));
            e.Graphics.DrawString($"Kelas                  : {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 450));
            e.Graphics.DrawString($"Tanggal                : {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 470));
            e.Graphics.DrawString($"Keluar pada jam        : {tx_keluar.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 490));
            e.Graphics.DrawString($"Kembali pada jam ke    : {combojam.SelectedItem}", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 510));
            e.Graphics.DrawString($"Keperluan : {txtAlasan.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 550));
            e.Graphics.DrawString($"Mengetahui Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(280, 490));
            e.Graphics.DrawString($"........................", new Font("Times New Roman", 8), Brushes.Black, new Point(285, 530));
        }
    }
}
