using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Dapper;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;


namespace latihribbon
{
    public partial class SuratKeluarcs : Form
    {
        private readonly DbDal db;
        private readonly KeluarDal keluarDal;
        private Form _previousForm;
        int print = 0;
        private readonly MesBox mesBox = new MesBox();
        DateTime globalCurrentTime = DateTime.Now;



        public SuratKeluarcs(Form previousForm)
        {
            InitializeComponent();
            db = new DbDal();
            keluarDal = new KeluarDal();

            this.Load += new System.EventHandler(this.SuratKeluarcs_Load);
            _previousForm = previousForm; // menyimpan referensi ke form sebelumnya

            // Menyelaraskan ukuran dan lokasi form ini dengan form sebelumnya
            this.Size = previousForm.Size; // Menyetel ukuran form
            this.Location = previousForm.Location; // Menyetel lokasi form

            isian();
            bahasa();
        }

        public void isian()
        {
            txtNIS.Text = Pemakai.NIS;
            txtNama.Text = Pemakai.nama;
            txtKelas.Text = Pemakai.kelas;
            tx_keluar.Text = globalCurrentTime.ToString("HH:mm");
            txtTanggal.Text = globalCurrentTime.ToString("dd MMM yyyy");
        }


        private void btn_kembali_Click(object sender, EventArgs e)
        {
            // menampilkan form sebelumnya dan menutup form saat ini
            _previousForm.Show();
            this.Close();
        }


        private void SuratKeluarcs_Load(object sender, EventArgs e) // ubah ke jam saja
        {
            // Format waktu yang diinginkan

            /*
                        // Opsional: Jika ingin membuat TextBox read-onsly agar tidak bisa diubah pengguna
                        tx_keluar.ReadOnly = true;*/
        }
        private bool Validasi()
        {

            bool validasi = true;
            string[] arr = jamKembali.Value.ToString("HH:mm").Split('.');
            string jamkembali = $"{arr[0]}:{arr[1]}";
            if (string.IsNullOrWhiteSpace(txtAlasan.Text) || jamkembali == tx_keluar.Text)
            {
                validasi = false;
            }
            return validasi;
        }

        public void btn_PrintKeluar_Click(object sender, EventArgs e)
        {

            if (!Validasi())
            {
                mesBox.MesInfo("Pastikan \"Jam Kembali\" dan \"Keperluan\" valid !");
                return;
            }

            if (mesBox.MesKonfirmasi("Apakah data sudah benar ?"))
            {
                Print();
                //Insert();
                System.Threading.Thread.Sleep(1000);
                Pemakai p = new Pemakai();
                p.Show();
                this.Close();
            }
        }

        public void Insert()
        {
            string nis, tujuan;
            DateTime tanggal;
            TimeSpan jamkeluar, jammasuk;

            nis = txtNIS.Text;
            tanggal = DateTime.Now.Date;
            jamkeluar = TimeSpan.Parse(tx_keluar.Text);
            jammasuk = TimeSpan.Parse(jamKembali.Text);
            tujuan = txtAlasan.Text;

            var keluar = new KeluarModel
            {
                Nis = Convert.ToInt32(nis),
                Tanggal = tanggal,
                JamKeluar = jamkeluar,
                JamMasuk = jammasuk,
                Tujuan = tujuan
            };
            keluarDal.Insert(keluar);
        }


        public void bahasa()
        {
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        #region   PRINT

        public void Print()
        {
            printPreviewDialogKeluar.Document = printDocumentKeluar;
            printDocumentKeluar.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Detail", 400, 610);
            //printDocumentKeluar.Print();
            printPreviewDialogKeluar.ShowDialog();
        }
        private void printDocumentKeluar_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("SURAT IZIN MENINGGALKAN PELAJARAN", new Font("Times New Roman", 8), Brushes.Black, new Point(90, 15));
            e.Graphics.DrawString("Nama", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 40));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 40));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 60));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 60));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 80));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 80));
            e.Graphics.DrawString("Keluar pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 100));
            e.Graphics.DrawString($": {tx_keluar.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 100));
            e.Graphics.DrawString("Kembali pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 120));
            e.Graphics.DrawString($": {jamKembali.Value.ToString("HH:mm")}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 120));
            e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 140));


            e.Graphics.DrawString("Mengetahui", new Font("Times New Roman", 8), Brushes.Black, new Point(310, 55));
            e.Graphics.DrawString("Guru Pengajar", new Font("Times New Roman", 7), Brushes.Black, new Point(310, 70));
            e.Graphics.DrawString("..................", new Font("Times New Roman", 8), Brushes.Black, new Point(310, 115));
            e.Graphics.DrawString("Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(320, 135));
            e.Graphics.DrawString("..................", new Font("Times New Roman", 8), Brushes.Black, new Point(310, 180));

            e.Graphics.DrawString($"( Ditinggal di kelas )", new Font("Times New Roman", 7), Brushes.Red, new Point(30, 185));
            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - -  - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(0, 203));



            e.Graphics.DrawString("SURAT IZIN MENINGGALKAN PELAJARAN", new Font("Times New Roman", 8), Brushes.Black, new Point(90, 225));
            e.Graphics.DrawString("Nama", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 250));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 250));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 270));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 270));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 290));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 290));
            e.Graphics.DrawString("Keluar pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 310));
            e.Graphics.DrawString($": {tx_keluar.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 310));
            e.Graphics.DrawString("Kembali pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 330));
            e.Graphics.DrawString($": {jamKembali.Value.ToString("HH:mm")}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 330));
            e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 350));
            e.Graphics.DrawString("Mengetahui", new Font("Times New Roman", 8), Brushes.Black, new Point(312, 270));
            e.Graphics.DrawString("Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(320, 285));

            e.Graphics.DrawString("..................", new Font("Times New Roman", 8), Brushes.Black, new Point(310, 330));


            e.Graphics.DrawString("( Tinggal dipos satpam )", new Font("Times New Roman", 7), Brushes.Red, new Point(28, 390));
            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(0, 406));


            e.Graphics.DrawString("SURAT IZIN MENINGGALKAN PELAJARAN", new Font("Times New Roman", 8), Brushes.Black, new Point(90, 415+15));
            e.Graphics.DrawString("Nama", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 440 + 15));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 440+15));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 460 + 15));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 460 + 15));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 480 + 15));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 480 + 15));
            e.Graphics.DrawString("Keluar pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 500 + 15));
            e.Graphics.DrawString($": {tx_keluar.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 500 + 15));
            e.Graphics.DrawString("Kembali pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 520 + 15));
            e.Graphics.DrawString($": {jamKembali.Value.ToString("HH:mm")}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 520 + 15));
            e.Graphics.DrawString("Mengetahui", new Font("Times New Roman", 7), Brushes.Black, new Point(315, 460 + 15));
            e.Graphics.DrawString("Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(320, 475 + 15));
            e.Graphics.DrawString("..................", new Font("Times New Roman", 8), Brushes.Black, new Point(310, 520 + 15));
            e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 540 + 15));


            e.Graphics.DrawString("( Dibawa siswa )", new Font("Times New Roman", 7), Brushes.Red, new Point(30, 580 + 10));



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

                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 350));
                e.Graphics.DrawString($"  {baris2.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 370));
                e.Graphics.DrawString($"  {baris3.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 390));

                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 540 + 15 ));
                e.Graphics.DrawString($"  {baris2.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 560 + 15));
                e.Graphics.DrawString($"  {baris3.Trim()}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 580 + 15));
            }
            else
            {
                e.Graphics.DrawString($": {alasan}", new Font("Times New Roman", 9), Brushes.Black, new Point(110, 140));
            }
        }


        #endregion

        private void txtAlasan_TextChanged(object sender, EventArgs e)
        {
            string keperluan = txtAlasan.Text;

            LabelLenghKeperluan.Text = $"{keperluan.Length}/60";
        }
    }
}