using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
using System.Management;
using System.Globalization;
using System.Threading;


namespace latihribbon
{
    public partial class SuratKeluarcs : Form
    {
        private readonly Form FormPemakai;
        private Form FormMilih;
        private readonly DbDal db;
        private readonly KeluarDal keluarDal;
        int print = 0;
        private readonly MesBox mesBox = new MesBox();
        DateTime globalCurrentTime = DateTime.Now;
        private string NIS;
        private string nama;
        private string kelas;

        private Form mainForm;
        private Form indexForm;

        private ToolTip toolTip;

        public SuratKeluarcs(Form mainForm,Form indexForm,string NIS, string nama,string kelas)
        {
            InitializeComponent();
            toolTip = new ToolTip();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.ControlBox = true;
            this.mainForm = mainForm;
            this.indexForm = indexForm;
            
            db = new DbDal();
            keluarDal = new KeluarDal();
            this.NIS = NIS;
            this.nama = nama;
            this.kelas = kelas;

            bahasa();
            isian();

            toolTip.SetToolTip(ButtonCaraPengisianJam, "Petunjuk pengisian jam keluar"); 
        }

        public void isian()
        {
            txtNIS.Text = NIS;
            txtNama.Text = nama;
            txtKelas.Text = kelas;
            tx_keluar.Text = globalCurrentTime.ToString(@"HH\:mm");
            txtTanggal.Text = globalCurrentTime.ToString("dddd, dd MMMM yyyy");
            jamKembali.Focus();
        }


        private bool Validasi()
        {
            bool validasi = true;
            if (string.IsNullOrWhiteSpace(txtAlasan.Text) || jamKembali.Value.ToString(@"HH\:mm") == tx_keluar.Text)
            {
                validasi = false;
            }
            return validasi;
        }

        public void btn_PrintKeluar_Click(object sender, EventArgs e)
        {
            if (!Validasi())
            {
                new MesWarningOK("Pastikan \"Jam Kembali\" dan \"Keperluan\" valid !").ShowDialog(this);
                return;
            }
            if (new MesQuestionYN("Apakah data sudah benar ?").ShowDialog(this) != DialogResult.Yes) return;
            if (Print()) Insert();
            
            System.Threading.Thread.Sleep(1000);
            indexForm.Opacity = 1;
            this.Close();
        }

        public void Insert()
        {
            string nis, tujuan;
            DateTime tanggal;
            TimeSpan jamkeluar, jammasuk;

            nis = txtNIS.Text;
            tanggal = globalCurrentTime;
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

        public bool Print()
        {
            try
            {
                printDocumentKeluar.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Detail", 400, 700);

                /* if (!PrinterIsAvailable())
                 {
                     new MesError("Printer tidak tersedia atau offline.").ShowDialog(this);
                     return false;
                 }*/
                printPreviewDialogKeluar.Document = printDocumentKeluar;
                printPreviewDialogKeluar.ShowDialog(this);

                //printDocumentKeluar.Print();
                return true;
            }
            catch (Exception ex)
            {
                new MesError($"Gagal Mencetak Surat: {ex.Message}").ShowDialog(this);
                return false;
            }
        }

        private void printDocumentKeluar_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font Times8Regular = new Font("Times New Roman", 8, FontStyle.Regular);
            Font Times7Regular = new Font("Times New Roman", 7, FontStyle.Regular);
            try
            {

                string nama = txtNama.Text;
                if(nama.Length >= 25)
                {
                    string[] arrNama = nama.Split(' ');
                    nama = "";
                    string cekNama = "";
                    foreach(var x in arrNama)
                    {
                        cekNama += $"{x} ";
                        if (cekNama.Length > 21)
                        {
                            char[] huruf = x.ToCharArray();
                            nama += $"{huruf[0]}.";
                        }
                        else
                        {
                            nama += $"{x} ";
                        }
                    }
                }                

                e.Graphics.DrawString("SURAT IZIN MENINGGALKAN PELAJARAN", new Font("Times New Roman", 8), Brushes.Black, new Point(90, 15));
                e.Graphics.DrawString("Nama", Times8Regular, Brushes.Black, new Point(30, 40));
                e.Graphics.DrawString($": {nama}", Times8Regular, Brushes.Black, new Point(125, 40));
                e.Graphics.DrawString("Kelas", Times8Regular, Brushes.Black, new Point(30, 60));
                e.Graphics.DrawString($": {txtKelas.Text}", Times8Regular, Brushes.Black, new Point(125, 60));
                e.Graphics.DrawString("Tanggal", Times8Regular, Brushes.Black, new Point(30, 80));
                e.Graphics.DrawString($": {txtTanggal.Text}", Times8Regular, Brushes.Black, new Point(125, 80));
                e.Graphics.DrawString("Keluar pada jam", Times8Regular, Brushes.Black, new Point(30, 100));
                e.Graphics.DrawString($": {tx_keluar.Text}", Times8Regular, Brushes.Black, new Point(125, 100));
                e.Graphics.DrawString("Kembali pada jam", Times8Regular, Brushes.Black, new Point(30, 120));
                e.Graphics.DrawString($": {jamKembali.Value.ToString(@"HH\:mm")}", Times8Regular, Brushes.Black, new Point(125, 120));
                e.Graphics.DrawString($"Keperluan", Times8Regular, Brushes.Black, new Point(30, 140));


                e.Graphics.DrawString("Mengetahui", Times8Regular, Brushes.Black, new Point(310, 55));
                e.Graphics.DrawString("Guru Pengajar", Times7Regular, Brushes.Black, new Point(310, 70));
                e.Graphics.DrawString("..................", Times8Regular, Brushes.Black, new Point(310, 115));
                e.Graphics.DrawString("Guru BK", Times7Regular, Brushes.Black, new Point(320, 135));
                e.Graphics.DrawString("..................", Times8Regular, Brushes.Black, new Point(310, 180));

                e.Graphics.DrawString($"( Ditinggal di kelas )", Times7Regular, Brushes.Red, new Point(30, 185));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - -  - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(0, 203));



                e.Graphics.DrawString("SURAT IZIN MENINGGALKAN PELAJARAN", Times8Regular, Brushes.Black, new Point(90, 225));
                e.Graphics.DrawString("Nama", Times8Regular, Brushes.Black, new Point(30, 250));
                e.Graphics.DrawString($": {nama}", Times8Regular, Brushes.Black, new Point(125, 250));
                e.Graphics.DrawString("Kelas", Times8Regular, Brushes.Black, new Point(30, 270));
                e.Graphics.DrawString($": {txtKelas.Text}", Times8Regular, Brushes.Black, new Point(125, 270));
                e.Graphics.DrawString("Tanggal", Times8Regular, Brushes.Black, new Point(30, 290));
                e.Graphics.DrawString($": {txtTanggal.Text}", Times8Regular, Brushes.Black, new Point(125, 290));
                e.Graphics.DrawString("Keluar pada jam", Times8Regular, Brushes.Black, new Point(30, 310));
                e.Graphics.DrawString($": {tx_keluar.Text}", Times8Regular, Brushes.Black, new Point(125, 310));
                e.Graphics.DrawString("Kembali pada jam", Times8Regular, Brushes.Black, new Point(30, 330));
                e.Graphics.DrawString($": {jamKembali.Value.ToString(@"HH\:mm")}", Times8Regular, Brushes.Black, new Point(125, 330));
                e.Graphics.DrawString($"Keperluan", Times8Regular, Brushes.Black, new Point(30, 350));
                e.Graphics.DrawString("Mengetahui", Times8Regular, Brushes.Black, new Point(312, 320));
                e.Graphics.DrawString("Guru BK", Times7Regular, Brushes.Black, new Point(320, 335));

                e.Graphics.DrawString("..................", Times8Regular, Brushes.Black, new Point(310, 380));


                e.Graphics.DrawString("( Tinggal dipos satpam )", Times7Regular, Brushes.Red, new Point(28, 390));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(0, 406));


                e.Graphics.DrawString("SURAT IZIN MENINGGALKAN PELAJARAN", new Font("Times New Roman", 8), Brushes.Black, new Point(90, 415 + 15));
                e.Graphics.DrawString("Nama", Times8Regular, Brushes.Black, new Point(30, 440 + 15));
                e.Graphics.DrawString($": {nama}", Times8Regular, Brushes.Black, new Point(125, 440 + 15));
                e.Graphics.DrawString("Kelas", Times8Regular, Brushes.Black, new Point(30, 460 + 15));
                e.Graphics.DrawString($": {txtKelas.Text}", Times8Regular, Brushes.Black, new Point(125, 460 + 15));
                e.Graphics.DrawString("Tanggal", Times8Regular, Brushes.Black, new Point(30, 480 + 15));
                e.Graphics.DrawString($": {txtTanggal.Text}", Times8Regular, Brushes.Black, new Point(125, 480 + 15));
                e.Graphics.DrawString("Keluar pada jam", Times8Regular, Brushes.Black, new Point(30, 500 + 15));
                e.Graphics.DrawString($": {tx_keluar.Text}", Times8Regular, Brushes.Black, new Point(125, 500 + 15));
                e.Graphics.DrawString("Kembali pada jam", Times8Regular, Brushes.Black, new Point(30, 520 + 15));
                e.Graphics.DrawString($": {jamKembali.Value.ToString(@"hh\:mm")}", Times8Regular, Brushes.Black, new Point(125, 520 + 15));
                e.Graphics.DrawString("Mengetahui", Times7Regular, Brushes.Black, new Point(315, 460 + 15 + 50));
                e.Graphics.DrawString("Guru BK", Times7Regular, Brushes.Black, new Point(320, 475 + 15 + 50));
                e.Graphics.DrawString("..................", Times8Regular, Brushes.Black, new Point(310, 520 + 15 + 50));
                e.Graphics.DrawString($"Keperluan", Times8Regular, Brushes.Black, new Point(30, 540 + 15));


                e.Graphics.DrawString("( Dibawa siswa )", Times7Regular, Brushes.Red, new Point(30, 580 + 10));


                string alasan = txtAlasan.Text;
                int batasPiksel = 150;

                if (GetTextWidth(alasan, Times8Regular, e.Graphics) > batasPiksel)
                {
                    string[] kataArr = alasan.Split(' ');
                    string baris1 = "";
                    string baris2 = "";
                    string baris3 = "";

                    int totalWidth = 0;

                    for (int i = 0; i < kataArr.Length; i++)
                    {
                        int kataWidth = GetTextWidth(kataArr[i] + " ", Times8Regular, e.Graphics);
                        if (totalWidth + kataWidth <= batasPiksel)
                        {
                            baris1 += kataArr[i] + " ";
                            totalWidth += kataWidth;
                        }
                        else
                        {
                            totalWidth = 0;
                            for (int j = i; j < kataArr.Length; j++)
                            {
                                kataWidth = GetTextWidth(kataArr[j] + " ", Times8Regular, e.Graphics);
                                if (totalWidth + kataWidth <= batasPiksel)
                                {
                                    baris2 += kataArr[j] + " ";
                                    totalWidth += kataWidth;
                                }
                                else
                                {
                                    totalWidth = 0;
                                    for (int k = j; k < kataArr.Length; k++)
                                    {
                                        kataWidth = GetTextWidth(kataArr[k] + " ", Times8Regular, e.Graphics);
                                        if (totalWidth + kataWidth <= batasPiksel)
                                        {
                                            baris3 += kataArr[k] + " ";
                                            totalWidth += kataWidth;
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    e.Graphics.DrawString($": {baris1.Trim()}", Times8Regular, Brushes.Black, new Point(125, 140));
                    e.Graphics.DrawString($"  {baris2.Trim()}", Times8Regular, Brushes.Black, new Point(125, 160));
                    e.Graphics.DrawString($"  {baris3.Trim()}", Times8Regular, Brushes.Black, new Point(125, 180));

                    e.Graphics.DrawString($": {baris1.Trim()}", Times8Regular, Brushes.Black, new Point(125, 350));
                    e.Graphics.DrawString($"  {baris2.Trim()}", Times8Regular, Brushes.Black, new Point(125, 370));
                    e.Graphics.DrawString($"  {baris3.Trim()}", Times8Regular, Brushes.Black, new Point(125, 390));

                    e.Graphics.DrawString($": {baris1.Trim()}", Times8Regular, Brushes.Black, new Point(125, 540 + 15));
                    e.Graphics.DrawString($"  {baris2.Trim()}", Times8Regular, Brushes.Black, new Point(125, 560 + 15));
                    e.Graphics.DrawString($"  {baris3.Trim()}", Times8Regular, Brushes.Black, new Point(125, 580 + 15));
                }
                else
                {
                    e.Graphics.DrawString($": {alasan}", Times8Regular, Brushes.Black, new Point(125, 140));
                    e.Graphics.DrawString($": {alasan}", Times8Regular, Brushes.Black, new Point(125, 350));
                    e.Graphics.DrawString($": {alasan}", Times8Regular, Brushes.Black, new Point(125, 540 + 15));
                }
            }
            catch (Exception ex)
            {
                new MesError($"Terjadi kesalahan saat mencetak: {ex.Message}").ShowDialog(this);
            }
        }

        private int GetTextWidth(string text, Font font, Graphics g)
        {
            SizeF size = g.MeasureString(text, font);
            return (int)size.Width;
        }

        private bool PrinterIsAvailable()
        {
            string query = "SELECT * FROM Win32_Printer";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject printer in searcher.Get())
            {
                if (printer["Default"] != null && (bool)printer["Default"] == true)
                {
                    string printerStatus = printer["PrinterStatus"].ToString();
                    string printerState = printer["WorkOffline"].ToString();

                    if (printerStatus == "3" && printerState == "False")
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        #endregion

        private void txtAlasan_TextChanged(object sender, EventArgs e)
        {
            string keperluan = txtAlasan.Text;

            LabelLenghKeperluan.Text = $"{keperluan.Length}/60";
        }

        private void btn_Kembali_Click(object sender, EventArgs e)
        {
            FormMilih fm = new FormMilih(mainForm,indexForm, NIS, nama, kelas);
            fm.Show();
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ScreenSiswa.FormTutorJam f = new ScreenSiswa.FormTutorJam();
            f.ShowDialog(this);
        }
    }
}