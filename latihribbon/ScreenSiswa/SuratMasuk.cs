﻿using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace latihribbon
{
    public partial class SuratMasuk : Form
    {
        private Form _previousForm; // untuk kembali ke form sebelumnya
        private MesBox mesbox = new MesBox();
        private readonly MasukDal masukDal = new MasukDal();
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
            txtAlasan.MaxLength = 60;
        }

        private void btn_Kembali_Click(object sender, EventArgs e)
        {
            // menampilkan form sebelumnya dan menutup form saat ini
            _previousForm.Show();
            this.Close();
        }

        private bool Validasi()
        {
            bool valid = true;
            if (txtAlasan.Text == string.Empty) valid = false;
            return valid;
        }

        private void btn_PrintMasuk_Click(object sender, EventArgs e)
        {
            if (!Validasi())
            {
                mesbox.MesInfo("Alasan Terlambat Wajib Diisi!");
                return;
            }
            PrintDocument();            
            Insert();

            System.Threading.Thread.Sleep(1000);
            this.Close();
        }

        #region PRINT

        private void PrintDocument()
        {
            printPreviewDialogMasuk.Document = printDocumentMasuk;
            printDocumentMasuk.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Default", 400, 590);
            //printDocumentMasuk.Print();
            printPreviewDialogMasuk.ShowDialog();
        }
        private void printDocumentMasuk_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            var tanggal = DateTime.Now.ToString("dd MMM yyyy");


            e.Graphics.DrawString("Surat Ijin Mengikuti Pelajaran", new Font("Times New Roman", 10), Brushes.Black, new Point(130, 15));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 7), Brushes.Black, new Point(170, 35));


            e.Graphics.DrawString("Nama", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 60));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 60));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 80));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 80));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 100));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 100));
            e.Graphics.DrawString("Masuk pada jam", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 120));
            e.Graphics.DrawString($": {tx_jam1.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 120));
            e.Graphics.DrawString("Alasan Terlambat", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 140));



            e.Graphics.DrawString("*Ditinggal di pos satpam", new Font("Times New Roman", 8), Brushes.Red, new Point(260, 160));


            e.Graphics.DrawString("- - - - - - - - - ✂ - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -",
                new Font("Times New Roman", 6), Brushes.Black, new Point(5, 180+16));

            // Bagian kedua
            e.Graphics.DrawString("Surat Ijin Mengikuti Pelajaran", new Font("Times New Roman", 10), Brushes.Black, new Point(130, 211));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 7), Brushes.Black, new Point(170, 231));

            e.Graphics.DrawString("Nama", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 256));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 256));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 276));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 276));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 296));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 296));
            e.Graphics.DrawString("Masuk pada jam", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 316));
            e.Graphics.DrawString($": {tx_jam1.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 316));
            e.Graphics.DrawString("Alasan terlambat", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 336));

            e.Graphics.DrawString("Tinggal di ruang BK", new Font("Times New Roman", 8), Brushes.Red, new Point(260, 356));


            e.Graphics.DrawString("- - - - - - - - - ✂ - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -",
               new Font("Times New Roman", 6), Brushes.Black, new Point(5, 394));


            e.Graphics.DrawString("Surat Ijin Mengikuti Pelajaran", new Font("Times New Roman", 10), Brushes.Black, new Point(130, 409));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 7), Brushes.Black, new Point(170, 429));

            e.Graphics.DrawString("Nama", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 454));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 454));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 474));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 474));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 494));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 494));
            e.Graphics.DrawString("Alasan Terlambat", new Font("Times New Roman", 9), Brushes.Black, new Point(20, 514));


            e.Graphics.DrawString("Mengetahui Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(280, 520));
            e.Graphics.DrawString("........................", new Font("Times New Roman", 8), Brushes.Black, new Point(285, 560));


            // memisahkan "Alasan Terlambat" jika terlalu panjang
            string alasan = txtAlasan.Text;
            int batasPanjang = 20;

            
            if (alasan.Length > batasPanjang)
            {
                char[] kata = alasan.ToCharArray();
                string baris1 = "";
                string baris2 = "";
                string baris3 = "";

                if (kata == null || kata.Length == 0)
                {
                    throw new Exception("String 'kata' tidak boleh null atau kosong.");
                }
                for (int i = 0; i < kata.Length; i++)
                {
                    char k = kata[i];

                    if ((baris1.Length + 1) < batasPanjang)
                    {
                        baris1 += k;
                    }
                    else if ((baris1.Length + 1) == batasPanjang)
                    {
                        baris1 += k;
                        baris1 += (char.IsLetterOrDigit(k) && char.IsLetterOrDigit(kata[i + 1])) ? '-' : ' ';
                    }
                    else if ((baris2.Length + 1) < batasPanjang)
                    {
                        baris2 += k;
                    }
                    else if ((baris2.Length + 1) == batasPanjang)
                    {
                        baris2 += k;
                        baris2 += (char.IsLetterOrDigit(k) && char.IsLetterOrDigit(kata[i + 1])) ? '-' : ' ';
                    }
                    else
                    {
                        baris3 += k;
                    }
                }
                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 140));
                e.Graphics.DrawString($"  {baris2.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 160));
                e.Graphics.DrawString($"  {baris3.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 180));

                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 336));
                e.Graphics.DrawString($"  {baris2.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 356));
                e.Graphics.DrawString($"  {baris3.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 376));

                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 475 + 36));
                e.Graphics.DrawString($"  {baris2.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 495 + 36));
                e.Graphics.DrawString($"  {baris3.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 515 + 36));
            }
            else
            {
                e.Graphics.DrawString($": {alasan}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 140));
            }


            //saya dan teman saya mau makan nasi padang seberat 10 kilogram di rumah pak camata
        }

        #endregion

        private void Insert()
        {
            string nis, alasan;
            DateTime tanggal;
            TimeSpan jamMasuk;


            nis = txtNIS.Text;
            tanggal = DateTime.Now.Date;
            jamMasuk = TimeSpan.Parse(tx_jam1.Text);
            alasan = txtAlasan.Text;

            var masuk = new MasukModel
            {
                NIS = int.Parse(nis),
                Tanggal = tanggal,
                JamMasuk = jamMasuk,
                Alasan = alasan
            };
            masukDal.Insert(masuk);
        }

        private void txtAlasan_TextChanged(object sender, EventArgs e)
        {
         
            lblLength.Text = $"{txtAlasan.Text.Length}/60";
        }
    }
    
}

