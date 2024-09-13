using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Dapper;
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
            txtTanggal.Text = globalCurrentTime.ToString("ddd, dd MMM yyyy");
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
                mesBox.MesInfo("Pastikan \"Jam Kembali\" dan \"Alasan\" valid !");
                return;
            }

            PopUp MesPopUp = new PopUp(this);
            MesPopUp.Show();

          /*
            printDocumentKeluar.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Detail", 400, 590);
            printDocumentKeluar.Print();
            print++;
            System.Threading.Thread.Sleep(5000);
            this.Close();*/


            Pemakai pakai = new Pemakai();
            pakai.Show();
        }

        public void Insert()
        {
            string nis, tujuan;
            DateTime tanggal;
            TimeSpan jamkeluar, jammasuk;

            MessageBox.Show(tx_keluar.Text);
            /*nis = txtNIS.Text;
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
            keluarDal.Insert(keluar);*/
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
            printDocumentKeluar.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Detail", 400, 590);
            printDocumentKeluar.Print();
            printPreviewDialogKeluar.ShowDialog();
        }
        private void printDocumentKeluar_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


            var tanggal = DateTime.Now.ToString("dd MMM yyyy");

            e.Graphics.DrawString("Surat Ijin Meninggalkan Pelajaran", new Font("Times New Roman", 9), Brushes.Black, new Point(100, 15));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 6), Brushes.Black, new Point(170, 30));
            e.Graphics.DrawString("Nama", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 50));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 50));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 70));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 70));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 90));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 90));
            e.Graphics.DrawString("Keluar pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 110));
            e.Graphics.DrawString($": {tx_keluar.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 110));
            e.Graphics.DrawString("Kembali pada jam ke", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 130));
            e.Graphics.DrawString($": {jamKembali.Value.ToString("HH:mm")}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 130));
            e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 170));



            e.Graphics.DrawString("Mengetahui Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(280, 50));
            e.Graphics.DrawString("...................", new Font("Times New Roman", 8), Brushes.Black, new Point(290, 90));
            e.Graphics.DrawString("Mengetahui Guru Pengajar", new Font("Times New Roman", 7), Brushes.Black, new Point(270, 110));
            e.Graphics.DrawString("........................", new Font("Times New Roman", 8), Brushes.Black, new Point(285, 150));

            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - -  - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(0, 210));

            e.Graphics.DrawString("Surat Ijin Meninggalkan Pelajaran", new Font("Times New Roman", 9), Brushes.Black, new Point(100, 225));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 6), Brushes.Black, new Point(170, 240));
            e.Graphics.DrawString("Nama", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 260));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 260));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 280));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 280));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 300));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 300));
            e.Graphics.DrawString("Keluar pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 320));
            e.Graphics.DrawString($": {tx_keluar.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 320));
            e.Graphics.DrawString("Kembali pada jam ke", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 340));
            e.Graphics.DrawString($": {jamKembali.Value.ToString("HH:mm")}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 340));
            e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 360));
            e.Graphics.DrawString("( Tinggal di pos satpam )", new Font("Times New Roman", 7), Brushes.Black, new Point(270, 260));
            e.Graphics.DrawString("Mengetahui Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(280, 280));
            e.Graphics.DrawString("........................", new Font("Times New Roman", 8), Brushes.Black, new Point(285, 310));

            e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(0, 400));

            e.Graphics.DrawString("Surat Ijin Meninggalkan Pelajaran", new Font("Times New Roman", 9), Brushes.Black, new Point(100, 415));
            e.Graphics.DrawString($"Bantul, {tanggal}", new Font("Times New Roman", 6), Brushes.Black, new Point(170, 430));
            e.Graphics.DrawString("Nama", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 450));
            e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 450));
            e.Graphics.DrawString("Kelas", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 470));
            e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 470));
            e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 490));
            e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 490));
            e.Graphics.DrawString("Keluar pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 510));
            e.Graphics.DrawString($": {tx_keluar.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 510));
            e.Graphics.DrawString("Kembali pada ", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 530));
            e.Graphics.DrawString($": {jamKembali.Value.ToString("HH:mm")}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 530));
            e.Graphics.DrawString("Mengetahui Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(280, 450));
            e.Graphics.DrawString("........................", new Font("Times New Roman", 8), Brushes.Black, new Point(285, 490));
            e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 550));


            string alasan = txtAlasan.Text;
            int batasPanjang = 200;

            string baris1 = "";
            string baris2 = "";

            if (e.Graphics.MeasureString(alasan, new Font("Times New Roman", 8)).Width > batasPanjang)
            {
                string[] kata = alasan.Split(' ');

                foreach (string k in kata)
                {
                    if (e.Graphics.MeasureString(baris1 + k + " ", new Font("Times New Roman", 8)).Width <= batasPanjang)
                    {
                        baris1 += k + " ";
                    }
                    else
                    {
                        baris2 += k + " ";
                    }
                }


                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 170));
                e.Graphics.DrawString($"{baris2.Trim()}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 190));
            }
            else
            {
                e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 170));
                e.Graphics.DrawString($": {alasan}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 170));
            }


            if (e.Graphics.MeasureString(alasan, new Font("Times New Roman", 8)).Width > batasPanjang)
            {
                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 360));
                e.Graphics.DrawString($"{baris2.Trim()}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 380));
            }
            else
            {
                e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 360));
                e.Graphics.DrawString($": {alasan}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 360));
            }


            if (e.Graphics.MeasureString(alasan, new Font("Times New Roman", 8)).Width > batasPanjang)
            {
                e.Graphics.DrawString($": {baris1.Trim()}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 550));
                e.Graphics.DrawString($"{baris2.Trim()}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 570));
            }
            else
            {
                e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 550));
                e.Graphics.DrawString($": {alasan}", new Font("Times New Roman", 8), Brushes.Black, new Point(110, 550));
            }
        }
        #endregion


        public string TextNama{ get { return txtNama.Text; } }
        public string TextKelas { get { return txtKelas.Text; } }
        public string TextTanggal { get { return txtTanggal.Text; } }
        public string TextKeluar { get { return txtTanggal.Text;} }
        public string PickerMasuk { get { return jamKembali.Value.ToString("HH:mm"); } }
        public string TextKeperluan { get { return txtAlasan.Text; } }

    }
}