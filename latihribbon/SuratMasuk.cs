using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
            _previousForm = previousForm; // menyimpan referensi ke form sebelumnya

            // Menyelaraskan ukuran dan lokasi form ini dengan form sebelumnya
            this.Size = previousForm.Size; // Menyetel ukuran form
            this.Location = previousForm.Location; // Menyetel lokasi form
            isian();
        }

        public void isian()
        {
            txtNIS.Text = Pemakai.NIS;
            txtNama.Text = Pemakai.nama;
            txtKelas.Text = Pemakai.kelas;
            string currentTime = DateTime.Now.ToString("HH:mm");
            tx_jam1.Text = currentTime;
            txtTanggal.Text = DateTime.Now.ToString("dddd ,dd MMMM yyyy");
        }

        private void btn_Kembali_Click(object sender, EventArgs e)
        {
            // menampilkan form sebelumnya dan menutup form saat ini
            _previousForm.Show();
            this.Close();
        }

        private void btn_PrintMasuk_Click(object sender, EventArgs e)
        {
        /*    printPreviewDialogMasuk.Document = printDocumentMasuk;
            printDocumentMasuk.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Default", 400, 590);
            printPreviewDialogMasuk.ShowDialog();*/


        }

        #region PRINT
        private void printDocumentMasuk_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            var tanggal = DateTime.Now.ToString("dd MMM yyyy");


            e.Graphics.DrawString("Surat Ijin Mengikuti Pelajaran", new Font("Times New Roman", 10), Brushes.Black, new Point(130, 15));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 7), Brushes.Black, new Point(170, 35));


            e.Graphics.DrawString("Nama", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 60));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 60));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 80));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 80));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 100));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 100));
            e.Graphics.DrawString("Masuk pada jam", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 120));
            e.Graphics.DrawString($": {tx_jam1.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 120));
            e.Graphics.DrawString("Alasan Terlambat", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 140));



            e.Graphics.DrawString("*Ditinggal di pos satpam", new Font("Times New Roman", 8), Brushes.Red, new Point(260, 160));


            e.Graphics.DrawString("- - - - - - - - - ✂ - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -",
                new Font("Times New Roman", 6), Brushes.Black, new Point(5, 180));

            // Bagian kedua
            e.Graphics.DrawString("Surat Ijin Mengikuti Pelajaran", new Font("Times New Roman", 10), Brushes.Black, new Point(130, 190));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 7), Brushes.Black, new Point(170, 210));

            e.Graphics.DrawString("Nama", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 230));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 230));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 250));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 250));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 270));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 270));
            e.Graphics.DrawString("Masuk pada jam", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 290));
            e.Graphics.DrawString($": {tx_jam1.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 290));
            e.Graphics.DrawString("Alasan terlambat", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 310));

            e.Graphics.DrawString("Tinggal di ruang BK", new Font("Times New Roman", 8), Brushes.Red, new Point(260, 330));


            e.Graphics.DrawString("- - - - - - - - - ✂ - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -",
               new Font("Times New Roman", 6), Brushes.Black, new Point(5, 350));


            e.Graphics.DrawString("Surat Ijin Mengikuti Pelajaran", new Font("Times New Roman", 9), Brushes.Black, new Point(130, 375));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 6), Brushes.Black, new Point(170, 395));

            e.Graphics.DrawString("Nama", new Font("Times New Roman", 8), Brushes.Black, new Point(20, 415));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 415));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 8), Brushes.Black, new Point(20, 435));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 435));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 8), Brushes.Black, new Point(20, 455));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 455));
            e.Graphics.DrawString("Alasan Terlambat", new Font("Times New Roman", 8), Brushes.Black, new Point(20, 475));


            e.Graphics.DrawString("Mengetahui Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(280, 415));
            e.Graphics.DrawString("........................", new Font("Times New Roman", 8), Brushes.Black, new Point(285, 455));




            // memisahkan "Alasan Terlambat" jika terlalu panjang
            string alasan = txtAlasan.Text;
            int batasPanjang = 300;

            if (e.Graphics.MeasureString(alasan, new Font("Times New Roman", 9)).Width > batasPanjang)
            {
                string[] kata = alasan.Split(' ');
                string baris1 = "";
                string baris2 = "";
                foreach (string k in kata)
                {
                    if (e.Graphics.MeasureString(baris1 + k + " ", new Font("Times New Roman", 9)).Width <= batasPanjang)
                    {
                        baris1 += k + " ";
                    }
                    else
                    {
                        baris2 += k + " ";
                    }
                }
                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 140));
                e.Graphics.DrawString($"{baris2.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 160));

                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 310));
                e.Graphics.DrawString($"{baris2.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 330));

                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 475));
                e.Graphics.DrawString($"{baris2.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 490));
            }
            else
            {
                e.Graphics.DrawString($": {alasan}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 140));
            }
        }
        #endregion

    }
    
}

