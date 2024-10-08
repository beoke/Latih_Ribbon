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

        public SuratKeluarcs(Form mainForm,string NIS, string nama,string kelas)
        {
            InitializeComponent();
            db = new DbDal();
            keluarDal = new KeluarDal();
            this.NIS = NIS;
            this.nama = nama;
            this.kelas = kelas;
            isian();
            bahasa();

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.ControlBox = true;
            this.KeyPreview = true;
            this.mainForm = mainForm;

            
        }

        public void isian()
        {
            txtNIS.Text = NIS;
            txtNama.Text = nama;
            txtKelas.Text = kelas;
            tx_keluar.Text = globalCurrentTime.ToString("HH:mm");
            txtTanggal.Text = globalCurrentTime.ToString("dd MMM yyyy");
        }


        private void btn_kembali_Click(object sender, EventArgs e)
        {
            FormMilih fm = new FormMilih(mainForm,NIS,nama,kelas);
            fm.Show();
            this.Close();
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

            if (!mesBox.MesKonfirmasi("Apakah data sudah benar ?")) return;
            if (Print()) Insert();
            

            System.Threading.Thread.Sleep(1000);
            mainForm.Show();
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

                if (!PrinterIsAvailable())
                {
                    MessageBox.Show("Printer tidak tersedia atau offline.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //printPreviewDialogKeluar.Document = printDocumentKeluar;
                //printPreviewDialogKeluar.ShowDialog();

                printDocumentKeluar.Print();
                return true;
            }
            catch (Exception ex)
            {
                // Jika terjadi error saat proses print
                MessageBox.Show($"Gagal Mencetak Surat: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void printDocumentKeluar_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
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
                e.Graphics.DrawString("Mengetahui", new Font("Times New Roman", 8), Brushes.Black, new Point(312, 320));
                e.Graphics.DrawString("Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(320, 335));

                e.Graphics.DrawString("..................", new Font("Times New Roman", 8), Brushes.Black, new Point(310, 380));


                e.Graphics.DrawString("( Tinggal dipos satpam )", new Font("Times New Roman", 7), Brushes.Red, new Point(28, 390));
                e.Graphics.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  - - - - - - Potong Disini - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -", new Font("Times New Roman", 6), Brushes.Black, new Point(0, 406));


                e.Graphics.DrawString("SURAT IZIN MENINGGALKAN PELAJARAN", new Font("Times New Roman", 8), Brushes.Black, new Point(90, 415 + 15));
                e.Graphics.DrawString("Nama", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 440 + 15));
                e.Graphics.DrawString($": {txtNama.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 440 + 15));
                e.Graphics.DrawString("Kelas", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 460 + 15));
                e.Graphics.DrawString($": {txtKelas.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 460 + 15));
                e.Graphics.DrawString("Tanggal", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 480 + 15));
                e.Graphics.DrawString($": {txtTanggal.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 480 + 15));
                e.Graphics.DrawString("Keluar pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 500 + 15));
                e.Graphics.DrawString($": {tx_keluar.Text}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 500 + 15));
                e.Graphics.DrawString("Kembali pada jam", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 520 + 15));
                e.Graphics.DrawString($": {jamKembali.Value.ToString("HH:mm")}", new Font("Times New Roman", 8), Brushes.Black, new Point(125, 520 + 15));
                e.Graphics.DrawString("Mengetahui", new Font("Times New Roman", 7), Brushes.Black, new Point(315, 460 + 15 + 50));
                e.Graphics.DrawString("Guru BK", new Font("Times New Roman", 7), Brushes.Black, new Point(320, 475 + 15 + 50));
                e.Graphics.DrawString("..................", new Font("Times New Roman", 8), Brushes.Black, new Point(310, 520 + 15 + 50));
                e.Graphics.DrawString($"Keperluan", new Font("Times New Roman", 8), Brushes.Black, new Point(30, 540 + 15));


                e.Graphics.DrawString("( Dibawa siswa )", new Font("Times New Roman", 7), Brushes.Red, new Point(30, 580 + 10));


                string alasan = txtAlasan.Text;
                int batasPiksel = 150;

                if (GetTextWidth(alasan, new Font("Times New Roman", 9), e.Graphics) > batasPiksel)
                {
                    string[] kataarr = alasan.Split(' ');
                    string baris1 = "";
                    string baris2 = "";
                    string baris3 = "";

                    Font font = new Font("Times New Roman", 9);
                    int totalWidth = 0;

                    for (int i = 0; i < kataarr.Length; i++)
                    {
                        int kataWidth = GetTextWidth(kataarr[i] + " ", font, e.Graphics);
                        if (totalWidth + kataWidth <= batasPiksel)
                        {
                            baris1 += kataarr[i] + " ";
                            totalWidth += kataWidth;
                        }
                        else
                        {
                            totalWidth = 0;
                            for (int j = i; j < kataarr.Length; j++)
                            {
                                kataWidth = GetTextWidth(kataarr[j] + " ", font, e.Graphics);
                                if (totalWidth + kataWidth <= batasPiksel)
                                {
                                    baris2 += kataarr[j] + " ";
                                    totalWidth += kataWidth;
                                }
                                else
                                {
                                    totalWidth = 0;
                                    for (int k = j; k < kataarr.Length; k++)
                                    {
                                        kataWidth = GetTextWidth(kataarr[k] + " ", font, e.Graphics);
                                        if (totalWidth + kataWidth <= batasPiksel)
                                        {
                                            baris3 += kataarr[k] + " ";
                                            totalWidth += kataWidth;
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    e.Graphics.DrawString($": {baris1.Trim()}", font, Brushes.Black, new Point(125, 140));
                    e.Graphics.DrawString($"{baris2.Trim()}", font, Brushes.Black, new Point(125, 160));
                    e.Graphics.DrawString($"{baris3.Trim()}", font, Brushes.Black, new Point(125, 180));

                    e.Graphics.DrawString($": {baris1.Trim()}", font, Brushes.Black, new Point(125, 350));
                    e.Graphics.DrawString($"{baris2.Trim()}", font, Brushes.Black, new Point(125, 370));
                    e.Graphics.DrawString($"{baris3.Trim()}", font, Brushes.Black, new Point(125, 390));

                    e.Graphics.DrawString($": {baris1.Trim()}", font, Brushes.Black, new Point(125, 540 + 15));
                    e.Graphics.DrawString($"{baris2.Trim()}", font, Brushes.Black, new Point(125, 560 + 15));
                    e.Graphics.DrawString($"{baris3.Trim()}", font, Brushes.Black, new Point(125, 580 + 15));
                }
                else
                {
                    e.Graphics.DrawString($": {alasan}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 140));
                    e.Graphics.DrawString($": {alasan}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 336));
                    e.Graphics.DrawString($": {alasan}", new Font("Times New Roman", 9), Brushes.Black, new Point(125, 514 + 20));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat mencetak: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SuratKeluarcs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.K)
            {
                // Keluar dari aplikasi saat kombinasi tombol Ctrl + Alt + K ditekan
                mainForm.Show();
                this.Close();
            }
        }
    }
}