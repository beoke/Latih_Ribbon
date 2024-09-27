using Dapper;
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
using System.Globalization;
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
            buf();
            rekapPersensiDal = new RekapPersensiDal();
            historyDal = new HistoryDal();
            InitComponen();
            Event();
            LoadHistory();
        }

        public void buf()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null,
            dataGridView1,
            new object[] { true });
        }

        public void InitComponen()
        {
            List<string> Keterangan = new List<string>() { "Semua", "A", "I", "S" };
            KeteranganCombo.DataSource = Keterangan;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.RowTemplate.Height = 30;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
           /* dataGridView1.Columns["Nis"].Width = 100;
            dataGridView1.Columns["Persensi"].Width = 100;
            dataGridView1.Columns["Nama"].Width = 250;
            dataGridView1.Columns["Kelas"].Width = 100;
            dataGridView1.Columns["Tanggal"].Width = 100;
            dataGridView1.Columns["Keterangan"].Width = 120;
            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.RowHeadersWidth = 51;*/
        }

        public void LoadHistory()
        {
            var gethistory = historyDal.GetData("RekapPersensi");
            txtKelas.Text = gethistory.History;
            LoadData();
        }


        bool tglchange = false;
        string sqlc2 = string.Empty;
        private string FilterSQL(bool data,string nis, string nama, string persensi,string keterangan)
        {
            string sqlc = string.Empty;
            
            List<string> fltr = new List<string>();
            List<string> fltr2 = new List<string>();
            
            if (data)
            {
                fltr.Add("sd.Kelas LIKE @Kelas+'%'");
                if (nis != "") fltr.Add("sd.NIS LIKE @NIS+'%'");
                if (nama != "") fltr.Add("sd.Nama LIKE '%'+@Nama+'%'");
                if (persensi != "") fltr.Add("sd.Persensi LIKE '%'+@Persensi+'%'");
                if (keterangan != "Semua") fltr.Add("COALESCE(a.Keterangan, '*') LIKE @Keterangan+'%'");
                if (tglchange) fltr.Add("sd.Tanggal BETWEEN @tgl1 AND @tgl2");
            }
            else
            {
                fltr.Add("Kelas LIKE @Kelas+'%'");
                if (nis != "") fltr.Add("NIS LIKE @NIS+'%'");
                if (nama != "") fltr.Add("Nama LIKE '%'+@Nama+'%'");
                if (persensi != "") fltr.Add("Persensi LIKE @Persensi+'%'");
                if (keterangan != "Semua") fltr2.Add("Keterangan LIKE @Keterangan+'%'");
                if (tglchange) fltr2.Add("Tanggal BETWEEN @tgl1 AND @tgl2");
            }

            if (fltr.Count > 0)
                sqlc += " WHERE "+string.Join(" AND ", fltr);
            if (fltr2.Count > 0)
                sqlc2 += " WHERE "+string.Join(" AND ", fltr2);
            return sqlc;
        }

        int Page = 1;
        int totalPage;
        private void LoadData()
        {
            string nis = txtNIS.Text;
            string nama = txtNama.Text;
            string persensi = txtPersensi.Text;
            string kelas = txtKelas.Text;
            string keterangan = KeteranganCombo.SelectedItem.ToString() ?? string.Empty;
            DateTime tgl1 = tglsatu.Value.Date;
            DateTime tgl2 = tgldua.Value.Date;

            var sqlc = FilterSQL(true,nis, nama,persensi, keterangan);
            var sqlcRow = FilterSQL(false,nis, nama,persensi, keterangan);

            var dp = new DynamicParameters();
            dp.Add("@Kelas", kelas);
            if (nis != "") dp.Add("@NIS", nis);
            if (nama != "") dp.Add("@Nama", nama);
            if (persensi != "") dp.Add("@Persensi", persensi);
            if (nis != "Semua") dp.Add("@Keterangan", keterangan);
            if (tglchange)
            {
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
            }

            string text = "Halaman ";
            int RowPerPage = 20;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = rekapPersensiDal.CekRows(sqlcRow,sqlc2,dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            dp.Add("Offset",inRowPage);
            dp.Add("Fetch",RowPerPage);
            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dataGridView1.DataSource = rekapPersensiDal.ListData2(sqlc,dp);

            sqlc2 = string.Empty;
        }

        #region     EVENT
        private void Event()
        {
            btnPrintRekap.Click += BtnPrintRekap_Click;

            txtNIS.TextChanged += txt_TextChanged;
            txtNama.TextChanged += txt_TextChanged;
            txtPersensi.TextChanged += txt_TextChanged;
            KeteranganCombo.SelectedIndexChanged += txt_TextChanged;
            tglsatu.ValueChanged += tgl_ValueChanged;
            tgldua.ValueChanged += tgl_ValueChanged;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void tgl_ValueChanged(object sender,EventArgs e)
        {
            Page = 1;
            tglchange = true;
            LoadData();
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Page < totalPage)
            {
                Page++;
                LoadData();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Page > 1)
            {
                Page--;
                LoadData();
            }

        }

        private void btnKelas_Click(object sender, EventArgs e)
        {
            PopUpKelas kelas = new PopUpKelas("RekapPersensi");
            kelas.ShowDialog();
            if (kelas.DialogResult == DialogResult.OK) 
            {
                LoadHistory();
                InitComponen();
            }
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtNIS.Clear();
            txtNama.Clear();
            KeteranganCombo.SelectedIndex = 0;
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            tglchange = false;
            LoadData();
        }
    }
}