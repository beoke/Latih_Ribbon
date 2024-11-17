using DocumentFormat.OpenXml.ExtendedProperties;
using latihribbon.Dal;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace latihribbon
{
    public partial class SuratMasuk : Form
    {
        private readonly MasukDal masukDal = new MasukDal();
        private string NIS;
        private string nama;
        private string kelas;
        private DateTime jam;

        private Form mainForm;
        private Form indexForm;
        public SuratMasuk(Form mainForm, Form indexForm, string NIS, string nama, string kelas)
        {
            InitializeComponent(); 
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            //this.TopMost = true;
            this.ControlBox = true;
            this.mainForm = mainForm;
            this.indexForm = indexForm;
            this.NIS = NIS;
            this.nama = nama;
            this.kelas = kelas;

            btn_kembali.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn_kembali.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_kembali.Enter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.Leave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;
            btn_kembali.MouseEnter += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowAbu1;
            btn_kembali.MouseLeave += (s, e) => btn_kembali.BackgroundImage = Properties.Resources.LeftArrowHitam;

            CultureInfo culture = new CultureInfo("id-ID");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            jam = DateTime.Now;
            isian();
        }

        public void isian()
        {
            txtNIS.Text = NIS;
            txtNama.Text =nama;
            txtKelas.Text =kelas;
            tx_jam1.Text = jam.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            txtTanggal.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            txtAlasan.MaxLength = 60;
            txtAlasan.Focus();
        }

        private void btn_Kembali_Click(object sender, EventArgs e)
        {
            FormMilih fm = new FormMilih(mainForm,indexForm,NIS,nama,kelas);
            fm.Show();
            this.Close();
        }

        private bool Validasi()
        {
            bool valid = true;
            if (txtAlasan.Text == string.Empty) valid = false;
            return valid;
        }
        private void Insert()
        {
            string nis, alasan;
            DateTime tanggal;
            TimeSpan jamMasuk;

            nis = txtNIS.Text;
            tanggal = DateTime.Now.Date;
            jamMasuk = TimeSpan.Parse(jam.TimeOfDay.ToString());
            alasan = txtAlasan.Text;

            var masuk = new MasukModel
            {
                NIS = int.Parse(nis),
                Tanggal = tanggal,
                JamMasuk = jamMasuk,
                Alasan = alasan,
            };
            masukDal.Insert(masuk);
        }
        private void btn_PrintMasuk_Click(object sender, EventArgs e)
        {
            if (!Validasi())
            {
                new MesWarningOK("Alasan Terlambat Wajib Diisi!").ShowDialog(this);
                return;
            }

            if (new MesQuestionYN("Apakah data sudah benar ?").ShowDialog(this) != DialogResult.Yes) return;
            if (Print()) Insert();
            System.Threading.Thread.Sleep(1000);
            indexForm.Opacity = 1;
            this.Close();
        }


        #region PRINT

        public bool Print()
        {
            try
            {
                printDocumentMasuk.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Suit Detail", 400, 700);

                if (!PrinterIsAvailable())
                {
                    new MesError("Printer tidak tersedia atau offline.").ShowDialog(this);
                    return false;
                }
               /* printPreviewDialogMasuk.Document = printDocumentMasuk;
                printPreviewDialogMasuk.ShowDialog(this);*/
                printDocumentMasuk.Print();
                return true;
            }
            catch (Exception ex)
            {
                // Jika terjadi error saat proses print
                new MesError($"Gagal Mencetak Surat: {ex.Message}").ShowDialog(this);
                return false;
            }
        }

        private void printDocumentMasuk_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font Times8Regular = new Font("Times New Roman", 8, FontStyle.Regular);
            Font Times7Regular = new Font("Times New Roman", 7, FontStyle.Regular);

            try
            {
                string nama = txtNama.Text;
                nama = new string(nama.Where(x => x != '*').ToArray());

                if (nama.Length >= 25)
                {
                    string[] arrSiswa = nama.Trim().Split(' ');
                    nama = "";

                    string cekNama = "";
                    foreach (var x in arrSiswa)
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

                var tanggal = DateTime.Now.ToString("dd MMM yyyy");

                e.Graphics.DrawString("SURAT IZIN MENGIKUTI PELAJARAN", new Font("Times New Roman", 10), Brushes.Black, new Point(75, 15));

                e.Graphics.DrawString("Nama", Times8Regular, Brushes.Black, new Point(20, 60));
                e.Graphics.DrawString($": {nama}", Times8Regular, Brushes.Black, new Point(125, 60));
                e.Graphics.DrawString("Kelas", Times8Regular, Brushes.Black, new Point(20, 80));
                e.Graphics.DrawString($": {txtKelas.Text}", Times8Regular, Brushes.Black, new Point(125, 80));
                e.Graphics.DrawString("Tanggal", Times8Regular, Brushes.Black, new Point(20, 100));
                e.Graphics.DrawString($": {txtTanggal.Text}", Times8Regular, Brushes.Black, new Point(125, 100));
                e.Graphics.DrawString("Masuk pada jam", Times8Regular, Brushes.Black, new Point(20, 120));
                e.Graphics.DrawString($": {tx_jam1.Text}", Times8Regular, Brushes.Black, new Point(125, 120));
                e.Graphics.DrawString("Alasan Terlambat", Times8Regular, Brushes.Black, new Point(20, 140));



                e.Graphics.DrawString("*Ditinggal di pos satpam", Times7Regular, Brushes.Red, new Point(275, 180));


                e.Graphics.DrawString("- - - - - - - - - ✂ - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -",
                    new Font("Times New Roman", 6), Brushes.Black, new Point(5, 180 + 16));

                // Bagian kedua
                e.Graphics.DrawString("SURAT IZIN MENGIKUTI PELAJARAN", new Font("Times New Roman", 10), Brushes.Black, new Point(75, 211));

                e.Graphics.DrawString("Nama", Times8Regular, Brushes.Black, new Point(20, 256));
                e.Graphics.DrawString($": {nama}", Times8Regular, Brushes.Black, new Point(125, 256));
                e.Graphics.DrawString("Kelas", Times8Regular, Brushes.Black, new Point(20, 276));
                e.Graphics.DrawString($": {txtKelas.Text}", Times8Regular, Brushes.Black, new Point(125, 276));
                e.Graphics.DrawString("Tanggal", Times8Regular, Brushes.Black, new Point(20, 296));
                e.Graphics.DrawString($": {txtTanggal.Text}", Times8Regular, Brushes.Black, new Point(125, 296));
                e.Graphics.DrawString("Masuk pada jam", Times8Regular, Brushes.Black, new Point(20, 316));
                e.Graphics.DrawString($": {tx_jam1.Text}", Times8Regular, Brushes.Black, new Point(125, 316));
                e.Graphics.DrawString("Alasan terlambat", Times8Regular, Brushes.Black, new Point(20, 336));

                e.Graphics.DrawString("*Tinggal di ruang BK", Times7Regular, Brushes.Red, new Point(275, 376));


                e.Graphics.DrawString("- - - - - - - - - ✂ - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -",
                   new Font("Times New Roman", 6), Brushes.Black, new Point(5, 394));


                e.Graphics.DrawString("SURAT IZIN MENGIKUTI PELAJARAN", new Font("Times New Roman", 10), Brushes.Black, new Point(75, 409));

                e.Graphics.DrawString("Nama", Times8Regular, Brushes.Black, new Point(20, 454));
                e.Graphics.DrawString($": {nama}", Times8Regular, Brushes.Black, new Point(125, 454));
                e.Graphics.DrawString("Kelas", Times8Regular, Brushes.Black, new Point(20, 474));
                e.Graphics.DrawString($": {txtKelas.Text}", Times8Regular, Brushes.Black, new Point(125, 474));
                e.Graphics.DrawString("Tanggal", Times8Regular, Brushes.Black, new Point(20, 494));
                e.Graphics.DrawString($": {txtTanggal.Text}", Times8Regular, Brushes.Black, new Point(125, 494));
                e.Graphics.DrawString("Masuk pada jam", Times8Regular, Brushes.Black, new Point(20, 494 + 20));
                e.Graphics.DrawString($": {tx_jam1.Text}", Times8Regular, Brushes.Black, new Point(125, 494 + 20));
                e.Graphics.DrawString("Alasan Terlambat", Times8Regular, Brushes.Black, new Point(20, 514 + 20));


                e.Graphics.DrawString("Mengetahui Guru BK", Times7Regular, Brushes.Black, new Point(280, 520));
                e.Graphics.DrawString("........................", Times8Regular, Brushes.Black, new Point(285, 560));


                string alasan = txtAlasan.Text;
                int batasPiksel = 140;

                if (GetTextWidth(alasan, Times8Regular, e.Graphics) > batasPiksel)
                {
                    string[] kataarr = alasan.Split(' ');
                    string baris1 = "";
                    string baris2 = "";
                    string baris3 = "";

                    int totalWidth = 0;

                    for (int i = 0; i < kataarr.Length; i++)
                    {
                        int kataWidth = GetTextWidth(kataarr[i] + " ", Times8Regular, e.Graphics);
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
                                kataWidth = GetTextWidth(kataarr[j] + " ", Times8Regular, e.Graphics);
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
                                        kataWidth = GetTextWidth(kataarr[k] + " ", Times8Regular, e.Graphics);
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
                    e.Graphics.DrawString($": {baris1.Trim()}", Times8Regular, Brushes.Black, new Point(125, 140));
                    e.Graphics.DrawString($"  {baris2.Trim()}", Times8Regular, Brushes.Black, new Point(125, 160));
                    e.Graphics.DrawString($"  {baris3.Trim()}", Times8Regular, Brushes.Black, new Point(125, 180));

                    e.Graphics.DrawString($": {baris1.Trim()}", Times8Regular, Brushes.Black, new Point(125, 336));
                    e.Graphics.DrawString($"  {baris2.Trim()}", Times8Regular, Brushes.Black, new Point(125, 356));
                    e.Graphics.DrawString($"  {baris3.Trim()}", Times8Regular, Brushes.Black, new Point(125, 376));

                    e.Graphics.DrawString($": {baris1.Trim()}", Times8Regular, Brushes.Black, new Point(125, 514 + 20));
                    e.Graphics.DrawString($"  {baris2.Trim()}", Times8Regular, Brushes.Black, new Point(125, 514 + 40));
                    e.Graphics.DrawString($"  {baris3.Trim()}", Times8Regular, Brushes.Black, new Point(125, 514 + 60));
                }
                else
                {
                    e.Graphics.DrawString($": {alasan}", Times8Regular, Brushes.Black, new Point(125, 140));
                    e.Graphics.DrawString($": {alasan}", Times8Regular, Brushes.Black, new Point(125, 336));
                    e.Graphics.DrawString($": {alasan}", Times8Regular, Brushes.Black, new Point(125, 514 + 20));
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
         
            lblLength.Text = $"{txtAlasan.Text.Length}/60";
        }
        
        private void txtAlasan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }
    }
    
}

