﻿using Dapper;
using latihribbon.Conn;
using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;
using LicensiContext = OfficeOpenXml.LicenseContext;

namespace latihribbon
{
    public partial class FormSIswa : Form
    {
        private readonly DbDal db;
        private readonly SiswaDal siswaDal;
        private readonly JurusanDal jurusanDal;
        private readonly MesBox mesBox;
        private bool SaveCondition = true;
        //private FormLoading formLoading;

        public FormSIswa()
        {
            InitializeComponent();
            //buf();
            db = new DbDal();
            siswaDal = new SiswaDal();
            jurusanDal = new JurusanDal();
            mesBox = new MesBox();
            InitCombo();
            InitComponent();
            LoadData();
        }

        public async Task LoadDataInBackgroundAsync()
        {
            await Task.Run(() => buf());
            //this.Show();
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

        public void InitCombo()
        {
            // Jurusan Combo
            var jurusan = jurusanDal.ListData();
            if (!jurusan.Any()) return;
            List<string> listJurusan = new List<string>();
            foreach (var item in jurusan)
                listJurusan.Add(item.NamaJurusan);
            jurusanCombo.DataSource = listJurusan;

            // Combo Filter
            var data = db.ListTahun();
            List<string> listTahun = new List<string>();
            listTahun.Add("Semua");
            foreach (var item in data)
            {
                listTahun.Add(item.Tahun);
            }
            comboTahunFilter.DataSource = listTahun;

            //textBox MaxLength
            txtNIS_FormSiswa.MaxLength = 9;
            txtNama_FormSiswa.MaxLength = 80;
            txtPersensi_FormSiswa.MaxLength = 3;
            txtRombel_FromSiswa.MaxLength = 5;
        }
        public void InitComponent()
        {
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

        public void ControlInsertUpdate()
        {
            if (SaveCondition)
            {
                lblInfo.Text = "INSERT";
                txtNIS_FormSiswa.ReadOnly = false;
            }
            else
            {
                lblInfo.Text = "UPDATE";
                txtNIS_FormSiswa.ReadOnly = true;
                lblNisSudahAda.Visible = false;
            }
        }

        public void GetData(int nis)
        {
            var getSiswa = siswaDal.GetData(nis);
            if (getSiswa is null) return;
            string[] kelas = getSiswa.Kelas.Split(' ');
            txtNIS_FormSiswa.Text = getSiswa.Nis.ToString();
            txtPersensi_FormSiswa.Text = getSiswa.Persensi.ToString();
            txtNama_FormSiswa.Text = getSiswa.Nama;
            txtRombel_FromSiswa.Text = (kelas.Length == 3) ? kelas[2] : string.Empty;
            txtTahun_FormSiswa.Text = getSiswa.Tahun;
            if (getSiswa.JenisKelamin == "L")
                lakiRadio.Checked = true;
            else
                perempuanRadio.Checked = true;
            if (kelas[0] == "X")
                XRadio.Checked = true;
            else if (kelas[0] == "XI")
                XIRadio.Checked = true;
            else
                XIIRadio.Checked = true;
            string jurusan = kelas[1];
            foreach (var item in jurusanCombo.Items)
                if ((string)item == jurusan)
                    jurusanCombo.SelectedItem = item;
            SaveCondition = false;
            ControlInsertUpdate();
        }

        public void SaveData()
        {
            string nis, persensi, nama, jenisKelamin = string.Empty, tingkat = string.Empty, jurusan, rombel, tahun;
            nis = txtNIS_FormSiswa.Text;
            nama = txtNama_FormSiswa.Text;
            persensi = txtPersensi_FormSiswa.Text;
            if (lakiRadio.Checked) jenisKelamin = "L";
            if (perempuanRadio.Checked) jenisKelamin = "P";

            if (XRadio.Checked) tingkat = "X";
            if (XIRadio.Checked) tingkat = "XI";
            if (XIIRadio.Checked) tingkat = "XII";
            jurusan = jurusanCombo.SelectedItem.ToString() ?? string.Empty;
            rombel = txtRombel_FromSiswa.Text;
            tahun = txtRombel_FromSiswa.Text;

            if (nis == "" || persensi == "" || nama == "" || jenisKelamin == "" || tingkat == "" || tahun == "")
            {
                mesBox.MesInfo("Seluruh Data Wajib Diisi!");
                return;
            }
            string namaKelas = $"{tingkat} {jurusan} {rombel}";
            var siswa = new SiswaModel
            {
                Nis = int.Parse(nis),
                Nama = nama,
                Persensi = int.Parse(persensi),
                JenisKelamin = jenisKelamin,
                Kelas = namaKelas,
                Tahun = tahun,
            };
            if (lblNisSudahAda.Visible == true)
            {
                mesBox.MesInfo("Nis Sudah Ada!!");
                return;
            };

            if (SaveCondition)
            {
                if(mesBox.MesKonfirmasi("Input Data?"))
                {
                    siswaDal.Insert(siswa);
                    LoadData();
                }
            }
            else
            {
                if (mesBox.MesKonfirmasi("Update Data?"))
                {
                    siswaDal.Update(siswa);
                    LoadData();
                }    
            }
        }

        private void Clear()
        {
            SaveCondition = true;
            ControlInsertUpdate();
            txtNIS_FormSiswa.Clear();
            txtNama_FormSiswa.Clear();
            txtPersensi_FormSiswa.Clear();
            lakiRadio.Checked = false;
            perempuanRadio.Checked = false;
            XRadio.Checked = false;
            XIIRadio.Checked = false;
            XIRadio.Checked = false;
            jurusanCombo.SelectedIndex = 0;
            txtRombel_FromSiswa.Clear();
            txtTahun_FormSiswa.Clear();
        }
        private void Delete()
        {
            string nis = txtNIS_FormSiswa.Text;
            if (SaveCondition)
            {
                mesBox.MesInfo("Pilih Data Terlebih Dahulu!");
                return;
            }
            if (mesBox.MesKonfirmasi("Hapus Data?"))
            {
                siswaDal.Delete(Convert.ToInt32(nis));
                LoadData();
                Clear();
            }
        }

        public void CekNis(int nis)
        {
            var cekNis = siswaDal.GetData(nis);
            if (cekNis != null)
            {
                lblNisSudahAda.Visible = true;
                return;
            }
            else
            {
                lblNisSudahAda.Visible = false;
            }
        }

        private void InputNumber(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }


        #region FILTER
        int Page = 1;
        int totalPage;
        public void LoadData()
        {
            string nis, nama, kelas, tahun;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tahun = comboTahunFilter.SelectedItem.ToString();

            var sqlc = FilterSQL(nis, nama, kelas, tahun);
            var dp = new DynamicParameters();
            dp.Add("@Nis", nis);
            dp.Add("@Nama", nama);
            dp.Add("@Kelas", kelas);
            dp.Add("@Tahun", tahun);

            string text = "Halaman ";
            int RowPerPage = 15;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = siswaDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = siswaDal.ListData(sqlc, dp);
        }

        private string FilterSQL(string nis, string nama, string kelas, string tahun)
        {
            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            if (nis != "") fltr.Add("Nis LIKE @NIS+'%'");
            if (nama != "") fltr.Add("Nama LIKE '%'+@Nama+'%'");
            if (kelas != "") fltr.Add("Kelas LIKE '%'+@Kelas+'%'");
            if (tahun != "Semua") fltr.Add("Tahun LIKE @Tahun+'%'");

            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ", fltr);
            return sqlc;
        }
        #endregion

        #region EVENT FILTER
        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void txtTahun_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }
        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void comboTahunFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }
        #endregion


        private void txtNIS_FormSiswa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void txtNIS_FormSiswa_TextChanged(object sender, EventArgs e)
        {
            string nis = txtNIS_FormSiswa.Text;
            if (nis.Length >= 5)
                CekNis(Convert.ToInt32(nis));
            else
                lblNisSudahAda.Visible = false;
        }

        private void txtNIS_FormSiswa_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            InputNumber(e);
        }

        private void txtPersensi_FormSiswa_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(e);
        }

        private void btnDelete_FormSiswa_Click(object sender, EventArgs e)
        {
            Delete();
            LoadData();
        }

        private void txtNIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(e);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string nis = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            GetData(Convert.ToInt32(nis));
        }

        private void txtTahun_FormSiswa_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(e);
        }

        private void txtRombel_FromSiswa_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(e);
        }

        private void ButtonInputSIswa_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            OpenFileDialog dialogOpen = new OpenFileDialog();
            dialogOpen.Filter = "File Excel |*.xls; *.xlsx";

            if (dialogOpen.ShowDialog() == DialogResult.OK)
            {
                FileInfo infoFile = new FileInfo(dialogOpen.FileName);

                using (ExcelPackage package = new ExcelPackage(infoFile))
                {
                    foreach (var sheet in package.Workbook.Worksheets)
                    {
                        int RowCount = sheet.Dimension.Rows;

                        using (IDbConnection Conn = new SqlConnection(conn.connstr()))
                        {
                            for (int baris = 2; baris <= RowCount; baris++)
                            {
                                var nis = long.TryParse(sheet.Cells[baris, 2].Value?.ToString(), out long parsedNis) ? parsedNis : (long?)null;
                                var nama = sheet.Cells[baris, 3].Value?.ToString();
                                var kelas = sheet.Cells[baris, 4].Value?.ToString();
                                var tahun = int.TryParse(sheet.Cells[baris, 6].Value?.ToString(), out int parsedTahun) ? parsedTahun : (int?)null;
                                var presensi = int.TryParse(sheet.Cells[baris, 1].Value?.ToString(), out int parsedPresensi) ? parsedPresensi : (int?)null;
                                var jenisKelamin = sheet.Cells[baris, 5].Value?.ToString();

                                bool isBarisKosong =
                                    nis == null &&
                                    string.IsNullOrWhiteSpace(nama) &&
                                    string.IsNullOrWhiteSpace(kelas) &&
                                    tahun == null &&
                                    presensi == null &&
                                    string.IsNullOrWhiteSpace(jenisKelamin);

                                if (isBarisKosong)
                                {
                                    continue;
                                }

                                if (nis == null || nama == null || kelas == null || tahun == null || presensi == null || jenisKelamin == null)
                                {
                                    continue;
                                }

                                var cekDb = Conn.QueryFirstOrDefault<long?>("SELECT Nis FROM siswa WHERE Nis = @Nis", new { Nis = nis });

                                if (cekDb != null)
                                {
                                    const string updateSql = @"
                            UPDATE siswa
                            SET Nama = @Nama, 
                                Kelas = @Kelas, 
                                Tahun = @Tahun, 
                                Persensi = @Persensi, 
                                JenisKelamin = @JenisKelamin
                            WHERE Nis = @Nis";

                                    var UpdateDp = new DynamicParameters();
                                    UpdateDp.Add("@Nis", nis, DbType.Int64);
                                    UpdateDp.Add("@Nama", nama, DbType.String);
                                    UpdateDp.Add("@Kelas", kelas, DbType.String);
                                    UpdateDp.Add("@Tahun", tahun, DbType.Int32);
                                    UpdateDp.Add("@Persensi", presensi, DbType.Int64);
                                    UpdateDp.Add("@JenisKelamin", jenisKelamin, DbType.String);

                                    Conn.Execute(updateSql, UpdateDp);
                                }
                                else
                                {
                                    const string insertSql = @"
                            INSERT INTO siswa 
                                (Nis, Nama, Kelas, Tahun, Persensi, JenisKelamin)
                            VALUES
                                (@Nis, @Nama, @Kelas, @Tahun, @Persensi, @JenisKelamin)";

                                    var InsertDp = new DynamicParameters();
                                    InsertDp.Add("@Nis", nis, DbType.Int64);
                                    InsertDp.Add("@Nama", nama, DbType.String);
                                    InsertDp.Add("@Kelas", kelas, DbType.String);
                                    InsertDp.Add("@Tahun", tahun, DbType.Int32);
                                    InsertDp.Add("@Persensi", presensi, DbType.Int64);
                                    InsertDp.Add("@JenisKelamin", jenisKelamin, DbType.String);

                                    Conn.Execute(insertSql, InsertDp);
                                }
                            }
                        }
                    }

                    LoadData(); 
                    mesBox.MesInfo("Data siswa berhasil ditambahkan atau diperbarui.");
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


        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtNIS.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            comboTahunFilter.SelectedIndex = 0;
            LoadData();
        }

        private void ButtonDownloadFormat_Click_1(object sender, EventArgs e)
        {

            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var sheet_X = package.Workbook.Worksheets.Add("X");
                    var sheet_XI = package.Workbook.Worksheets.Add("XI");
                    var sheet_XII = package.Workbook.Worksheets.Add("XII");

                    void CreateTables(ExcelWorksheet sheetExcel)
                    {
                        int barisAwal = 1;

                        for (int tabelIndek = 0; tabelIndek < 16; tabelIndek++)
                        {
                            sheetExcel.Cells[barisAwal, 1].Value = "Presensi";
                            sheetExcel.Cells[barisAwal, 2].Value = "NIS";
                            sheetExcel.Cells[barisAwal, 3].Value = "Nama";
                            sheetExcel.Cells[barisAwal, 4].Value = "Kelas";
                            sheetExcel.Cells[barisAwal, 5].Value = "Jenis Kelamin";
                            sheetExcel.Cells[barisAwal, 6].Value = "Tahun";

                            using (var range = sheetExcel.Cells[barisAwal, 1, barisAwal, 6])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            }

                            for (int barisIndek = 1; barisIndek <= 37; barisIndek++)
                            {
                                var range = sheetExcel.Cells[barisAwal + barisIndek, 1, barisAwal + barisIndek, 6];
                                range.Value = "";

                                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            }

                            barisAwal += 37 + 6;
                        }
                    }

                    CreateTables(sheet_X);
                    CreateTables(sheet_XI);
                    CreateTables(sheet_XII);

                    var saveDialog = new SaveFileDialog
                    {
                        Filter = "Excel Files|*.xlsx",
                        Title = "Save Excel File",
                        FileName = "FormatDataSiswa.xlsx"
                    };

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var filePath = saveDialog.FileName;
                        var directory = Path.GetDirectoryName(filePath);
                        var fileName = Path.GetFileNameWithoutExtension(filePath);
                        var extension = Path.GetExtension(filePath);
                        int counter = 1;

                        while (File.Exists(filePath))
                        {
                            filePath = Path.Combine(directory, $"{fileName}({counter}){extension}");
                            counter++;
                        }

                        FileInfo fi = new FileInfo(filePath);
                        package.SaveAs(fi);

                        MessageBox.Show("File Excel berhasil disimpan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


            }
        }
    }
}
