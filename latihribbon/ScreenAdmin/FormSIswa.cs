 using Dapper;
using latihribbon.Conn;
using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
using latihribbon.ScreenAdmin;
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

namespace latihribbon
{
    public partial class FormSIswa : Form
    {
        private readonly DbDal db;
        private readonly SiswaDal siswaDal;
        private readonly JurusanDal jurusanDal;
        private readonly KelasDal kelasDal;
        private readonly MesBox mesBox;
        private bool SaveCondition = true;
        //private FormLoading formLoading;
        
        public FormSIswa()
        {
            InitializeComponent();
            buf();
            db = new DbDal();
            siswaDal = new SiswaDal();
            jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();
            mesBox = new MesBox();
            InitCombo();
            LoadData();
            InitComponent();
            RegisterEvent();
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
        private void Form1_Shown(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.Focus();
            }
        }

        public void InitCombo()
        {
            //textBox MaxLength
            txtNIS_FormSiswa.MaxLength = 9;
            txtNama_FormSiswa.MaxLength = 80;
            txtPersensi_FormSiswa.MaxLength = 3;

            // Jurusan Combo
            var jurusan = jurusanDal.ListData();
            if (!jurusan.Any()) return;
            jurusanCombo.DataSource = jurusan;
            jurusanCombo.DisplayMember = "NamaJurusan";
            jurusanCombo.ValueMember = "Id";

            // Combo Filter
       
            var data = db.ListTahun();
            List<string> listTahun = new List<string>();
            listTahun.Add("Semua");
            foreach (var item in data)
            {
                listTahun.Add(item.Tahun);
            }
            comboTahunFilter.DataSource = listTahun;
        }
        public void InitComponent()
        {
            // DataGrid
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.RowTemplate.Height = 30;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.Columns[3].HeaderText = "Jenis Kelamin";

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 350;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 100;
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
        public void GetDataGrid(DataGridViewCellEventArgs e)
        {
            var data = dataGridView1.Rows[e.RowIndex];
            int nis = Convert.ToInt32(data.Cells[0].Value);
            int persensi = Convert.ToInt32(data.Cells[1].Value);
            string nama = data.Cells[2].Value.ToString();
            string jenisKelamin = data.Cells[3].Value.ToString();
            string[] namaKelas = (data.Cells[4].Value.ToString()).Split(' ');
            string tahun = data.Cells[5].Value.ToString();



            txtNIS_FormSiswa.Text = nis.ToString();
            txtNama_FormSiswa.Text = nama;
            txtPersensi_FormSiswa.Text = persensi.ToString();
            txtTahun_FormSiswa.Text = tahun;
            lakiRadio.Checked = jenisKelamin == "L";
            perempuanRadio.Checked = jenisKelamin != "L";
            XRadio.Checked = namaKelas[0] == "X";
            XIRadio.Checked = namaKelas[0] == "XI";
            XIIRadio.Checked = namaKelas[0] == "XII";
            foreach (var item in jurusanCombo.Items)
                if (item is JurusanModel j)
                    if (j.NamaJurusan == namaKelas[1])
                        jurusanCombo.SelectedItem = j;
            rombelCombo.DataSource = kelasDal.GetDataRombel((int)jurusanCombo.SelectedValue, namaKelas[0])
                .Select(x => x.Rombel).ToList();
            SaveCondition = false;
            ControlInsertUpdate();
            if (namaKelas.Length < 3) return;
            foreach (var x in rombelCombo.Items)
                if((string)x == namaKelas[2])
                    rombelCombo.SelectedItem = x;
        }
        public void GetData(int nis)
        {
            var getSiswa = siswaDal.GetData(nis);
            if (getSiswa is null) return;
            var dataKelas = kelasDal.GetData(getSiswa.IdKelas);
            if (dataKelas is null) return;

            txtNIS_FormSiswa.Text = getSiswa.Nis.ToString();
            txtPersensi_FormSiswa.Text = getSiswa.Persensi.ToString();
            txtNama_FormSiswa.Text = getSiswa.Nama;

            txtTahun_FormSiswa.Text = getSiswa.Tahun;
            if (getSiswa.JenisKelamin == "L")
                lakiRadio.Checked = true;
            else
                perempuanRadio.Checked = true;
            if (dataKelas.Rombel == "X")
                XRadio.Checked = true;
            else if (dataKelas.Rombel == "XI")
                XIRadio.Checked = true;
            else
                XIIRadio.Checked = true;
            foreach (var item in jurusanCombo.Items)
                if (item is JurusanModel j)
                    if (j.NamaJurusan == dataKelas.NamaJurusan)
                        jurusanCombo.SelectedItem = j;
            rombelCombo.DataSource = kelasDal.GetDataRombel(dataKelas.IdJurusan,dataKelas.Tingkat)
                                        .Select(item => item.Rombel).ToList();
            foreach (var item in rombelCombo.Items)
                if ((string)item == dataKelas.Rombel)
                    rombelCombo.SelectedItem = item;
            SaveCondition = false;
            ControlInsertUpdate();
        }

        public void SaveData()
        {
            string nis, persensi, nama, jenisKelamin = string.Empty, tingkat = string.Empty, rombel, tahun;
            nis = txtNIS_FormSiswa.Text;
            nama = txtNama_FormSiswa.Text.Trim(); 
            persensi = txtPersensi_FormSiswa.Text;
            if (lakiRadio.Checked) jenisKelamin = "L";
            if (perempuanRadio.Checked) jenisKelamin = "P";

            var idJurusan = jurusanCombo.Items.Count < 1 ? 0 : (int)jurusanCombo.SelectedValue;
            if (idJurusan == 0) return;
            if (XRadio.Checked) tingkat = "X";
            if (XIRadio.Checked) tingkat = "XI";
            if (XIIRadio.Checked) tingkat = "XII";
            rombel = rombelCombo.SelectedItem?.ToString() ?? string.Empty;
            tahun = txtTahun_FormSiswa.Text;

            if (nis == "" || persensi == "" || nama == "" || jenisKelamin == "" || tingkat == "" || tahun == "")
            {
                mesBox.MesInfo("Seluruh Data Wajib Diisi!");
                return;
            }

            if (lblNisSudahAda.Visible == true)
            {
                mesBox.MesInfo("Nis Sudah Ada!!");
                return;
            };
            bool cekRombel = rombel != string.Empty ? true : false;
            int idKelas = kelasDal.GetDataRombel(idJurusan, tingkat).FirstOrDefault(x => cekRombel ? x.Rombel == rombel : true)?.Id ?? 0;
            if (idKelas == 0) return;
            var siswa = new SiswaModel
            {
                Nis = int.Parse(nis),
                Nama = nama,
                Persensi = int.Parse(persensi),
                JenisKelamin = jenisKelamin,
                IdKelas = idKelas,
                Tahun = tahun,
            };


            if (SaveCondition)
            {
                if (!mesBox.MesKonfirmasi("Input Data?")) return;
                siswaDal.Insert(siswa);
                LoadData();
            }
            else
            {
                if (!mesBox.MesKonfirmasi("Update Data?")) return;
                siswaDal.Update(siswa);
                LoadData();
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
            txtTahun_FormSiswa.Clear();
            if(rombelCombo.Items.Count < 1) return;
            rombelCombo.SelectedIndex = 0;

            if (jurusanCombo.Items.Count == 0) return;  
            jurusanCombo.SelectedIndex = 0;
        }
        private void Delete()
        {
            string nis = txtNIS_FormSiswa.Text;
            if (SaveCondition)
            {
                mesBox.MesInfo("Pilih Data Terlebih Dahulu!");
                return;
            }
            if (MessageBox.Show($"Anda yakin ingin menghapus data dengan NIS\" {nis} \" ? \n Jika Dihapus, maka data yang terhubung akan ikut Terhapus", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            siswaDal.Delete(Convert.ToInt32(nis));
            LoadData();
            Clear();
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

        #region FILTER
        int Page = 1;
        int totalPage;
        private string FilterSQL(string nis, string nama, string persensi, string kelas, string tahun)
        {
            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            if (nis != "") fltr.Add("s.Nis LIKE @Nis+'%'");
            if (nama != "") fltr.Add("s.Nama LIKE '%'+@Nama+'%'");
            if (persensi != "") fltr.Add("s.Persensi LIKE @Persensi+'%'");
            if (kelas != "") fltr.Add("k.NamaKelas LIKE '%'+@Kelas+'%'");
            if (tahun != "Semua") fltr.Add("s.Tahun LIKE @Tahun+'%'");

            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ", fltr);
            return sqlc;
        }
        public void LoadData()
        {
            string nis = txtNIS.Text;
            string nama = txtNama.Text;
            string persensi = txtPersensi.Text;
            string kelas = txtKelas.Text;
            string tahun = comboTahunFilter.SelectedItem?.ToString() ?? string.Empty;
            var sqlc = FilterSQL(nis, nama,persensi, kelas, tahun);
            var dp = new DynamicParameters();
            if (nis != "") dp.Add("@Nis", nis);
            if (nama != "") dp.Add("@Nama", nama);
            if (persensi != "") dp.Add("@Persensi", persensi);
            if (kelas != "") dp.Add("@Kelas", kelas);
            if (tahun != "Semua") dp.Add("@Tahun", tahun);

            string text = "Halaman ";
            int RowPerPage = 15;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = siswaDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = siswaDal.ListData(sqlc, dp)
                .Select(x => new
                {
                    NIS = x.Nis,
                    Persensi = x.Persensi,
                    Nama = x.Nama,
                    JenisKelamin = x.JenisKelamin,
                    Kelas = x.NamaKelas == null ? "Sudah Lulus" : x.NamaKelas,
                    Tahun = x.Tahun
                }).ToList();
        }

        #endregion

        #region EVENT
        private void RegisterEvent()
        {
            txtNIS.TextChanged += txtFilter_TextChanged;
            txtNama.TextChanged += txtFilter_TextChanged;
            txtPersensi.TextChanged += txtFilter_TextChanged;
            txtKelas.TextChanged += txtFilter_TextChanged;
            comboTahunFilter.SelectedIndexChanged += txtFilter_TextChanged;

            txtNIS_FormSiswa.KeyPress += input_KeyPress;
            txtPersensi_FormSiswa.KeyPress += input_KeyPress;
            txtTahun_FormSiswa.KeyPress += input_KeyPress;

            btnResetFilter.Click += BtnResetFilter_Click;

            XRadio.CheckedChanged += radio_CheckedChange;
            XIRadio.CheckedChanged += radio_CheckedChange;
            XIIRadio.CheckedChanged += radio_CheckedChange;
            jurusanCombo.SelectedIndexChanged += radio_CheckedChange;

            //this.Shown += Form1_Shown;
        }

        private void txtFilter_TextChanged(object sender,EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void input_KeyPress(object sender,KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
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
        private void btnDelete_FormSiswa_Click(object sender, EventArgs e)
        {
            Delete();
            LoadData();
        }
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*string nis = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            GetData(Convert.ToInt32(nis));*/
             GetDataGrid(e);
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
        private void BtnResetFilter_Click(object sender, EventArgs e)
        {
            txtNIS.Clear();
            txtNama.Clear();
            txtKelas.Clear();

            if (comboTahunFilter.Items.Count == 0) return;
            comboTahunFilter.SelectedIndex = 0;
            
            LoadData();
        }

        private void radio_CheckedChange(object sender, EventArgs e)
        {
            string tingkat = XRadio.Checked ? "X" : XIRadio.Checked ? "XI" : XIIRadio.Checked ? "XII" : string.Empty;
            string jurusan = ((JurusanModel)jurusanCombo.SelectedItem)?.Id.ToString() ?? string.Empty;
            if (tingkat == string.Empty || jurusan == "")
            {
                rombelCombo.DataSource = null;
                return;
            }
            var cari = kelasDal.GetDataRombel(Convert.ToInt32(jurusan),tingkat);
            rombelCombo.DataSource = cari.Select(item => item.Rombel).ToList();
        }

        #endregion
        private void ButtonInputSIswa_Click(object sender, EventArgs e)
        {
            FormKetentuanImport ketentuan = new FormKetentuanImport();
            if (ketentuan.ShowDialog() != DialogResult.OK) return;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            OpenFileDialog dialogOpen = new OpenFileDialog();
            dialogOpen.Filter = "File Excel |*.xls; *.xlsx";

            if (dialogOpen.ShowDialog() == DialogResult.OK)
            {
                FileInfo infoFile = new FileInfo(dialogOpen.FileName);
                List<string> daftarKelasError = new List<string>();

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
                                string[] kelasName = kelas.Trim().Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                                string namaKelas = string.Join(" ",kelasName);
                                int idKelas = kelasDal.GetIdKelas(namaKelas);

                                string gender = jenisKelamin.Trim();
                                /*MessageBox.Show(idKelas.ToString()); return;*/
                                if (idKelas != 0)
                                {
                                    var cekDb = Conn.QueryFirstOrDefault<long?>("SELECT Nis FROM siswa WHERE Nis = @Nis", new { Nis = nis });

                                    var dp = new DynamicParameters();
                                    dp.Add("@Nis", nis, DbType.Int64);
                                    dp.Add("@Nama", nama, DbType.String);
                                    dp.Add("@IdKelas", idKelas, DbType.Int32);
                                    dp.Add("@Tahun", tahun, DbType.Int32);
                                    dp.Add("@Persensi", presensi, DbType.Int64);
                                    dp.Add("@JenisKelamin", gender, DbType.String);

                                    if (cekDb != null)
                                    {
                                        const string updateSql = @"
                                                            UPDATE siswa
                                                            SET Nama = @Nama,
                                                                IdKelas = @IdKelas, 
                                                                Tahun = @Tahun, 
                                                                Persensi = @Persensi, 
                                                                JenisKelamin = @JenisKelamin
                                                            WHERE Nis = @Nis";
                                        Conn.Execute(updateSql, dp);
                                    }
                                    else
                                    {
                                        const string insertSql = @"
                                                                INSERT INTO siswa 
                                                                    (Nis, Nama, IdKelas, Tahun, Persensi, JenisKelamin)
                                                                VALUES
                                                                    (@Nis, @Nama, @IdKelas, @Tahun, @Persensi, @JenisKelamin)";
                                        Conn.Execute(insertSql, dp);
                                    }
                                }
                                else
                                {
                                    if(!daftarKelasError.Contains(namaKelas)) daftarKelasError.Add(namaKelas);
                                }
                            }
                        }
                    }

                    LoadData();
                    mesBox.MesInfo("Data siswa berhasil ditambahkan atau diperbarui.");
                    string daftarKelas = daftarKelasError.Count < 1 ? "Tidak Ada" : string.Join(",", daftarKelasError);
                    mesBox.MesInfo($"Daftar Kelas Error: {daftarKelas}");
                }
            }
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

        private void ButtonNaikKelas_Click(object sender, EventArgs e)
        {
            FormPopUpNaikKelas pop = new FormPopUpNaikKelas();
            pop.ShowDialog();   


            if (pop.DialogResult == DialogResult.OK)
            {
                FormNaikKelas naik = new FormNaikKelas();
                naik.ShowDialog();

                if (naik.DialogResult == DialogResult.OK)
                {
                    UpdateNaikKelas();
                    LoadData();
                    mesBox.MesInfo("Proses kenaikan kelas berhasil ");
                }
            }
        }

        private void UpdateNaikKelas()
        {
            using (var Conn = new SqlConnection(conn.connstr()))
            {
                const string sqlKenaikanKelas = @"
    -- Langkah 1: Duplicating kelas XII with ' (Lulus)' for graduates
    INSERT INTO Kelas (Namakelas, Rombel, idJurusan, Tingkat)
    SELECT CONCAT(K.Namakelas, ' (Lulus)'), K.Rombel, K.idJurusan, K.Tingkat
    FROM Kelas K
    WHERE K.Tingkat = 'XII'
    AND K.Namakelas NOT LIKE '%(Lulus)%'
    AND NOT EXISTS (
        SELECT 1
        FROM Kelas K2
        WHERE K2.Namakelas = CONCAT(K.Namakelas, ' (Lulus)')
        AND K2.Rombel = K.Rombel
        AND K2.idJurusan = K.idJurusan
    );

    -- Langkah 2: Updating siswa to the next class
    UPDATE S
    SET S.idKelas = K2.Id
    FROM Siswa S
    JOIN Kelas K1 ON S.idKelas = K1.Id
    JOIN Kelas K2 ON K1.idJurusan = K2.idJurusan 
                  AND K1.Rombel = K2.Rombel 
                  AND (
                      (K1.Tingkat = 'X' AND K2.Tingkat = 'XI') OR 
                      (K1.Tingkat = 'XI' AND K2.Tingkat = 'XII')
                  )
    WHERE K1.Tingkat IN ('X', 'XI');

    -- Langkah 3: Adding ' (Lulus)' to existing kelas XII
    -- Pastikan bahwa ini sudah dilakukan pada langkah 1
";





                Conn.Execute(sqlKenaikanKelas);
             
            }
        }

    }
}
