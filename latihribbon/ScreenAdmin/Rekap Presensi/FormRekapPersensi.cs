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
using ClosedXML.Excel;

namespace latihribbon.ScreenAdmin
{
    public partial class FormRekapPersensi : Form
    {
        private readonly RekapPersensiDal rekapPersensiDal;


        public FormRekapPersensi()
        {
            InitializeComponent();
            rekapPersensiDal = new RekapPersensiDal();
            LoadData();
            InitComponen();
            Event();

            txtNama.Text = "X RPL 1";
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
            PopUpKelas popUp = new PopUpKelas();
            popUp.StartPosition = FormStartPosition.CenterScreen;
            popUp.ShowDialog();

            if (popUp.ShowDialog() == DialogResult.OK)
            {
                txtKelas.Text = popUp.KelasText;
            }
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





        /* public void ImportExcelWithMultipleSheets(string filePath)
         {
             using (var workbook = new XLWorkbook(filePath))
             {
                 // Loop melalui setiap sheet di workbook
                 foreach (var worksheet in workbook.Worksheets)
                 {
                     int currentRow = 1;
                     string currentClass = string.Empty;

                     // Loop melalui setiap baris di sheet
                     while (currentRow <= worksheet.LastRowUsed().RowNumber())
                     {
                         var row = worksheet.Row(currentRow);
                         var firstCell = row.Cell(1).GetValue<string>();

                         // Jika menemukan nama kelas (misalnya dimulai dengan 'X ', 'XI ', 'XII ')
                         if (!string.IsNullOrEmpty(firstCell) && (firstCell.StartsWith("X ") || firstCell.StartsWith("XI ") || firstCell.StartsWith("XII ")))
                         {
                             currentClass = firstCell; // Menyimpan nama kelas saat ditemukan
                             currentRow++; // Lanjutkan ke baris berikutnya untuk membaca data siswa
                             currentRow = ReadStudentData(worksheet, currentRow, currentClass);
                         }
                         else
                         {
                             currentRow++; // Jika tidak menemukan nama kelas, lanjutkan ke baris berikutnya
                         }
                     }
                 }
             }
         }

         // Fungsi untuk membaca data siswa
         public int ReadStudentData(IXLWorksheet worksheet, int startRow, string currentClass)
         {
             int currentRow = startRow;
             var dataTable = new DataTable();
             dataTable.Columns.Add("Kelas", typeof(string));
             dataTable.Columns.Add("Persensi", typeof(string));
             dataTable.Columns.Add("NIS", typeof(string));
             dataTable.Columns.Add("Nama", typeof(string));
             dataTable.Columns.Add("JenisKelamin", typeof(string));

             while (currentRow <= worksheet.LastRowUsed().RowNumber())
             {
                 var row = worksheet.Row(currentRow);
                 var firstCell = row.Cell(1).GetValue<string>();

                 // Jika menemukan baris kosong atau nama kelas baru, berhenti
                 if (string.IsNullOrEmpty(firstCell))
                 {
                     currentRow++; // Lompat baris kosong
                     continue;
                 }

                 // Jika menemukan nama kelas berikutnya, hentikan pembacaan data siswa dan kembalikan row
                 if (firstCell.StartsWith("X ") || firstCell.StartsWith("XI ") || firstCell.StartsWith("XII "))
                 {
                     break;
                 }

                 // Ambil data siswa
                 var persensi = row.Cell(1).GetValue<string>(); // Kolom urt=Persensi
                 var nis = row.Cell(2).GetValue<string>();      // Kolom NIS
                 var nama = row.Cell(3).GetValue<string>();     // Kolom NAMA SISWA
                 var jenisKelamin = row.Cell(4).GetValue<string>(); // Kolom L/P

                 // Tambahkan ke DataTable
                 dataTable.Rows.Add(currentClass, persensi, nis, nama, jenisKelamin);

                 currentRow++; // Lanjutkan ke baris berikutnya
             }

             // Setelah selesai membaca siswa, insert data ke database
             InsertDataToDatabase(dataTable);

             return currentRow;
         }

         public void InsertDataToDatabase(DataTable dt)
         {
             // Implementasi untuk memasukkan data ke database
             // Misalnya menggunakan Dapper atau ADO.NET
         }*/




        public void ImportExcelFromSpecificSheet(string filePath)
        {
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet("X");
                    if (worksheet != null)
                    {
                        int currentRow = 1;
                        string currentClass = string.Empty;

                        while (currentRow <= worksheet.LastRowUsed().RowNumber())
                        {
                            var row = worksheet.Row(currentRow);
                            var firstCell = row.Cell(1).GetValue<string>();

                            // Debugging
                            MessageBox.Show($"Row {currentRow}: {firstCell}");

                            if (!string.IsNullOrEmpty(firstCell) && firstCell.Equals("X RPL 1"))
                            {
                                currentClass = firstCell;
                                currentRow++;
                                currentRow = ReadStudentData(worksheet, currentRow, currentClass);
                            }
                            else
                            {
                                currentRow++;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sheet dengan nama 'X' tidak ditemukan.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
            }
        }

        public int ReadStudentData(IXLWorksheet worksheet, int startRow, string currentClass)
        {
            int currentRow = startRow;
            var dataTable = new DataTable();
            dataTable.Columns.Add("Kelas", typeof(string));
            dataTable.Columns.Add("Persensi", typeof(string));
            dataTable.Columns.Add("NIS", typeof(string));
            dataTable.Columns.Add("Nama", typeof(string));
            dataTable.Columns.Add("JenisKelamin", typeof(string));

            while (currentRow <= worksheet.LastRowUsed().RowNumber())
            {
                var row = worksheet.Row(currentRow);
                var firstCell = row.Cell(1).GetValue<string>();

                if (string.IsNullOrEmpty(firstCell))
                {
                    currentRow++;
                    continue;
                }

                if (firstCell.StartsWith("X ") || firstCell.StartsWith("XI ") || firstCell.StartsWith("XII "))
                {
                    break;
                }

                var persensi = row.Cell(1).GetValue<string>();
                var nis = row.Cell(2).GetValue<string>();
                var nama = row.Cell(3).GetValue<string>();
                var jenisKelamin = row.Cell(4).GetValue<string>();

                dataTable.Rows.Add(currentClass, persensi, nis, nama, jenisKelamin);

                currentRow++;
            }

            // Debugging
            MessageBox.Show($"DataTable Rows: {dataTable.Rows.Count}");

            // Atur DataSource DataGridView dan refresh
            dataGridView1.DataSource = dataTable;
            dataGridView1.Refresh();

            return currentRow;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Membuka dialog untuk memilih file Excel
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
            openFileDialog.Title = "Pilih File Excel";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName; // Mendapatkan path file yang dipilih
                ImportExcelFromSpecificSheet(filePath); // Memanggil fungsi untuk mengimport data dari Excel
            }
        }

        private void btnKelas_Click(object sender, EventArgs e)
        {
            PopUpKelas kelas = new PopUpKelas();
            kelas.ShowDialog();
        }
    }
}
