using latihribbon.Dal;
using latihribbon.Model;
using latihribbon.UpdateInsert;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace latihribbon.ScreenAdmin
{
    public partial class FormRekapPersensi : Form
    {
        private readonly RekapPersensiDal rekapPersensiDal;
        private readonly HistoryDal historyDal;


        public FormRekapPersensi()
        {
            InitializeComponent();
            rekapPersensiDal = new RekapPersensiDal();
            historyDal = new HistoryDal();
            LoadHistory();
            InitComponen();
            Event();
        }

        public void InitComponen()
        {
            List<string> Keterangan = new List<string>() { "Semua", "A", "I", "S" };
            KeteranganCombo.DataSource = Keterangan;

            // DataGrid
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersHeight = 35;
            }
        }

        public void LoadHistory()
        {
            var gethistory = historyDal.GetData("RekapPersensi");
            txtKelas.Text = gethistory!=null ? gethistory.History : "X RPL 2";
            LoadData();
        }

        int Page = 1;
        int totalPage;
        private void LoadData()
        {
           /* var rekap = rekapPersensiDal.ListData().Select(x => new { NIS = x.Nis, Persensi = x.Persensi, Nama = x.Nama, Kelas = x.Kelas, Tanggal = x.Tanggal, Keterangan = x.Keterangan }).ToList();
            dataGridView1.DataSource = rekap;*/

            string text = "Halaman ";
            int RowPerPage = 15;
            int inRowPage = (Page-1)*RowPerPage;
            var jumlahRow = rekapPersensiDal.CekRows();
            if (jumlahRow == 0) return;
            totalPage = jumlahRow/RowPerPage;

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dataGridView1.DataSource = rekapPersensiDal.ListData2(inRowPage,RowPerPage);


        }

        #region     EVENT
        private void Event()
        {
            btnPrintRekap.Click += BtnPrintRekap_Click;
            txtKelas.Click += TxtKelas_Click;
        }

        private void TxtKelas_Click(object sender, EventArgs e)
        {
          /*  PopUpKelas popUp = new PopUpKelas();
            popUp.StartPosition = FormStartPosition.CenterScreen;
            popUp.ShowDialog();

            if (popUp.ShowDialog() == DialogResult.OK)
            {
                txtKelas.Text = popUp.KelasText;
            }*/
        }

        private void BtnPrintRekap_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Eksport Data ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Print();
        }


        public string Kelas
        {
            get { return txtKelas.Text; }
            set { txtKelas.Text = value;}
        }



        #endregion


        public void Print()
        {
            // Menyetujui lisensi EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Menampilkan dialog untuk memilih lokasi dan nama file
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Save an Excel File";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string excelFilePath = saveFileDialog.FileName;

                    string connectionString = "Server=(local);Database=RekapSiswa;Trusted_Connection=True;TrustServerCertificate=True";

                    // Query untuk mengambil data dari database
                    string query = @"
                SELECT s.NIS, s.Nama, s.Persensi, s.Kelas,
                       SUM(CASE WHEN p.Keterangan = 'A' THEN 1 ELSE 0 END) AS A,
                       SUM(CASE WHEN p.Keterangan = 'I' THEN 1 ELSE 0 END) AS I,
                       SUM(CASE WHEN p.Keterangan = 'S' THEN 1 ELSE 0 END) AS S
                FROM Siswa s
                LEFT JOIN Persensi p ON s.NIS = p.NIS
                GROUP BY s.NIS, s.Nama, s.Persensi, s.Kelas
                ORDER BY s.Kelas, s.NIS";

                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand(query, connection))
                        using (var reader = command.ExecuteReader())
                        {
                            // Membuat file Excel baru
                            using (var package = new ExcelPackage())
                            {
                                // Dictionary untuk menyimpan data per kelas
                                var classSheets = new Dictionary<string, ExcelWorksheet>();

                                int row;
                                while (reader.Read())
                                {
                                    string kelas = reader["Kelas"].ToString();
                                    ExcelWorksheet worksheet;

                                    // Jika sheet untuk kelas belum ada, buat sheet baru
                                    if (!classSheets.ContainsKey(kelas))
                                    {
                                        worksheet = package.Workbook.Worksheets.Add(kelas);
                                        classSheets[kelas] = worksheet;

                                        // Menambahkan header ke sheet
                                        worksheet.Cells[1, 1].Value = "Daftar Siswa Kelas " + kelas;
                                        worksheet.Cells[1, 1, 1, 6].Merge = true; // Menggabungkan kolom 1 sampai 6
                                        worksheet.Cells[1, 1].Style.Font.Bold = true;
                                        worksheet.Cells[1, 1].Style.Font.Size = 16;
                                        worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        worksheet.Cells[2, 1].Value = "Nis";
                                        worksheet.Cells[2, 2].Value = "Nama Siswa";
                                        worksheet.Cells[2, 3].Value = "Persensi";
                                        worksheet.Cells[2, 4].Value = "A";
                                        worksheet.Cells[2, 5].Value = "I";
                                        worksheet.Cells[2, 6].Value = "S";

                                        for (int col = 1; col <= 6; col++)
                                        {
                                            worksheet.Cells[2, col].Style.Font.Bold = true;
                                            worksheet.Cells[2, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                            worksheet.Cells[2, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[2, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                        }

                                        // Mengatur lebar kolom
                                        worksheet.Column(1).Width = 10; // NIS
                                        worksheet.Column(2).Width = 30; // Nama Siswa
                                        worksheet.Column(3).Width = 15; // Persensi
                                        worksheet.Column(4).Width = 8; // A
                                        worksheet.Column(5).Width = 8; // I
                                        worksheet.Column(6).Width = 8; // S
                                    }

                                    worksheet = classSheets[kelas];
                                    row = worksheet.Dimension?.End.Row + 1 ?? 3; // Menentukan baris untuk data baru

                                    // Menulis data ke sheet
                                    worksheet.Cells[row, 1].Value = reader["NIS"];
                                    worksheet.Cells[row, 2].Value = reader["Nama"];
                                    worksheet.Cells[row, 3].Value = reader["Persensi"];
                                    worksheet.Cells[row, 4].Value = reader["A"];
                                    worksheet.Cells[row, 5].Value = reader["I"];
                                    worksheet.Cells[row, 6].Value = reader["S"];

                                    for (int col = 1; col <= 6; col++)
                                    {
                                        worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                                    }
                                }

                                // Menyimpan file Excel
                                FileInfo excelFile = new FileInfo(excelFilePath);
                                package.SaveAs(excelFile);
                            }
                        }
                    }

                    MessageBox.Show("Data berhasil diekspor ke Excel dengan format khusus.");
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(Page < totalPage)
            {
                Page++;
                LoadData();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if(Page > 1)
            {
                Page--;
                LoadData();
            }

        }
        
        private void btnKelas_Click(object sender, EventArgs e)
        {
            PopUpKelas kelas = new PopUpKelas(txtKelas.Text);
            kelas.ShowDialog();
            if (kelas.DialogResult == DialogResult.OK) LoadHistory();
        }
    }
}
