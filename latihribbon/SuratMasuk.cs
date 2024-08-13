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

        private void btn_PrintMasuk_Click(object sender, EventArgs e)
        {
            printPreviewDialogMasuk.Document = printDocumentMasuk;
            printDocumentMasuk.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Default", 400, 590);
            printPreviewDialogMasuk.ShowDialog();
        }

        private void printDocumentMasuk_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var tanggal = DateTime.Now.ToString("dd MMM yyyy");

            e.Graphics.DrawString("Surat Ijin Mengikuti Pelajaran", new Font("Times New Roman", 10), Brushes.Black, new Point(130, 15));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 7), Brushes.Black, new Point(170, 30));
            e.Graphics.DrawString("Nama ", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 50));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 50));
            e.Graphics.DrawString("Kelas ", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 70));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 70));
            e.Graphics.DrawString($"Tanggal ", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 90));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 90));
            e.Graphics.DrawString($"Masuk pada jam ", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 110));
            e.Graphics.DrawString($": {tx_jam1.Text}", new Font("Times New Roman",9), Brushes.Black, new Point(110, 110));
            e.Graphics.DrawString("Alasan Terlambat ", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 130));
            e.Graphics.DrawString($": {txtAlasan.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 130));
            e.Graphics.DrawString($"*Ditinggal di pos satpam", new Font("Times New Roman", 8), Brushes.Red, new Point(260, 110));

            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(5, 170));

            e.Graphics.DrawString("Surat Ijin Mengikuti Pelajaran", new Font("Times New Roman", 9), Brushes.Black, new Point(130, 205));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 6), Brushes.Black, new Point(170, 220));
            e.Graphics.DrawString($"Nama                   : {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(25, 240));
            e.Graphics.DrawString($"Kelas                  : {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(25, 260));
            e.Graphics.DrawString($"Tanggal                : {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(25, 280));
            e.Graphics.DrawString($"Alasan terlambat : {txtAlasan.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(25, 300));
            e.Graphics.DrawString($"Tinggal di ruang BK", new Font("Times New Roman", 7), Brushes.Red, new Point(260, 280));

            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(5, 330));

            e.Graphics.DrawString("Surat Ijin Mengikuti Pelajaran", new Font("Times New Roman", 9), Brushes.Black, new Point(130, 365));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 6), Brushes.Black, new Point(170, 380));
            e.Graphics.DrawString($"Nama                   : {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(25, 400));
            e.Graphics.DrawString($"Kelas                  : {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(25, 420));
            e.Graphics.DrawString($"Tanggal                : {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(25, 440));
            e.Graphics.DrawString($"Keperluan : {txtAlasan.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(25, 480));
            e.Graphics.DrawString($"Mengetahui Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(280, 420));
            e.Graphics.DrawString($"........................", new Font("Times New Roman", 8), Brushes.Black, new Point(285, 460));

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}

