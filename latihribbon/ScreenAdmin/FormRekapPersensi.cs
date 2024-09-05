using latihribbon.Dal;
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
        public FormRekapPersensi()
        {
            InitializeComponent();
            InitKeterangan();
            rekapPersensiDal = new RekapPersensiDal();
            dataGridView1.DataSource = rekapPersensiDal.ListData();

            Event();
        }

        public void InitKeterangan()
        {
            List<string> Keterangan = new List<string>() { "Semua", "A", "I", "S" };
            KeteranganCombo.DataSource = Keterangan;
        }


        #region     EVENT
        private void Event()
        {
            btnPrintRekap.Click += BtnPrintRekap_Click;
        }


        private void BtnPrintRekap_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Eksport Data ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Print();
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

    }
}
