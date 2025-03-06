using Dapper;
using DocumentFormat.OpenXml.ExtendedProperties;
using latihribbon.Conn;
using latihribbon.Dal;
using latihribbon.Model;
using latihribbon.ScreenAdmin;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    public partial class FormSIswa : Form
    {
        private readonly DbDal db;
        private readonly SiswaDal siswaDal;
        private readonly JurusanDal jurusanDal;
        private readonly KelasDal kelasDal;
        private ToolTip toolTip;
        private Dictionary<Control, Point> originalLocations = new Dictionary<Control, Point>();
        private Dictionary<Control, Size> originalSizes = new Dictionary<Control, Size>();
        private System.Threading.Timer debounceTimer;
        private const int debounceDelay = 300;

        private bool isTrue = true;

        public FormSIswa()
        {
            InitializeComponent();
            buf();
            this.Load += FormSIswa_Load;
            db = new DbDal();
            siswaDal = new SiswaDal();
            jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();

            InitCombo();
            InitComboTahun();
            LoadData();
            InitComponent();
            RegisterEvent();
        }
        private void FormSIswa_Load(object sender, EventArgs e)
        {
            originalLocations[txtNIS_FormSiswa] = txtNIS_FormSiswa.Location;
            originalLocations[lblNisSudahAda] = lblNisSudahAda.Location;
            originalLocations[label11] = label11.Location;
            originalLocations[txtPersensi_FormSiswa] = txtPersensi_FormSiswa.Location;
            originalLocations[label6] = label6.Location;
            originalLocations[txtNama_FormSiswa] = txtNama_FormSiswa.Location;
            originalLocations[label7] = label7.Location;
            originalLocations[groupBox1] = groupBox1.Location;
            originalLocations[label8] = label8.Location;
            originalLocations[XRadio] = XRadio.Location;
            originalLocations[XIRadio] = XIRadio.Location;
            originalLocations[XIIRadio] = XIIRadio.Location;
            originalLocations[label10] = label10.Location;
            originalLocations[label9] = label9.Location;
            originalLocations[jurusanCombo] = jurusanCombo.Location;
            originalLocations[rombelCombo] = rombelCombo.Location;
            originalLocations[label12] = label12.Location;
            originalLocations[txtTahun_FormSiswa] = txtTahun_FormSiswa.Location;

            originalSizes[txtNIS_FormSiswa] = txtNIS_FormSiswa.Size;
            originalSizes[txtPersensi_FormSiswa] = txtPersensi_FormSiswa.Size;
            originalSizes[jurusanCombo] = jurusanCombo.Size;
            originalSizes[rombelCombo] = rombelCombo.Size;
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
            //textBox MaxLength
            txtNIS_FormSiswa.MaxLength = 9;
            txtNama_FormSiswa.MaxLength = 80;
            txtPersensi_FormSiswa.MaxLength = 3;

            
            List<int> list = new List<int>() {10,20,50,100,200 };
            comboPerPage.DataSource = list;
            comboPerPage.DropDownStyle = ComboBoxStyle.DropDownList;
                
            // Jurusan Combo
            var jurusan = jurusanDal.ListData();
            if (!jurusan.Any()) return;
            jurusanCombo.DataSource = jurusan
                .Select(x => new JurusanModel
                {
                    Id = x.Id,
                    Kode = x.Kode,
                    NamaJurusan = $"{x.Kode} - {x.NamaJurusan}"
                }).ToList(); ;
            jurusanCombo.DisplayMember = "NamaJurusan";
            jurusanCombo.ValueMember = "Id";
            
            toolTip = new ToolTip();

            toolTip.SetToolTip(ButtonDownloadFormat, "Template Import Data");
            toolTip.SetToolTip(ButtonInputSIswa, "Import Data");
            toolTip.SetToolTip(btnResetFilter, "Reset Filter");
        }

        private void InitComboTahun()
        {

            // Combo Filter
            var data = db.ListTahun();
            List<string> listTahun = new List<string>();
            listTahun.Add("Semua");
            foreach (var item in data)
            {
                listTahun.Add(item.Tahun);
            }
            comboTahunFilter.DataSource = listTahun;
            comboTahunFilter.KeyPress += (s, e) => e.Handled = true;
            comboTahunFilter.MouseDown += (s, e) => comboTahunFilter.DroppedDown = true;
        }

        public void InitComponent()
        {
            if(dataGridView1.Rows.Count > 0)
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
                dataGridView1.Columns[1].HeaderText = "NIS";
                dataGridView1.Columns[2].HeaderText = "Presensi";
                dataGridView1.Columns[4].HeaderText = "Jenis Kelamin";
                dataGridView1.Columns[5].HeaderText = "Nama Kelas";


                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Width = 80;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 350;
                dataGridView1.Columns[4].Width = 120;
                dataGridView1.Columns[5].Width = 150;
                dataGridView1.Columns[6].Width = 100;
            }
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
                new MesWarningOK("Seluruh Data Wajib Diisi!").ShowDialog(this);
                return;
            }

            if (lblNisSudahAda.Visible == true)
            {
                new MesError("Nis sudah ada !").ShowDialog(this);
                return;
            };
            bool cekRombel = rombel != string.Empty ? true : false;
            int idKelas = kelasDal.GetDataRombel(idJurusan, tingkat).FirstOrDefault(x => cekRombel ? x.Rombel == rombel : true)?.Id ?? 0;
            if (idKelas == 0)
            {
                new MesError($"Kelas {tingkat} {((JurusanModel)jurusanCombo.SelectedItem).NamaJurusan} Tidak Ada Di Table Kelas!").ShowDialog(this);
                return;
            }
            var siswa = new SiswaModel
            {
                Nis = int.Parse(nis),
                Nama = nama,
                Persensi = int.Parse(persensi),
                JenisKelamin = jenisKelamin,
                IdKelas = idKelas,
                Tahun = tahun,
            };
            if (new MesQuestionYN("Input Data?", 1).ShowDialog(this) != DialogResult.Yes) return;
            siswaDal.Insert(siswa);
            LoadData();
            Clear();
            InitComboTahun();
        }

        private void Clear()
        {
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
        public void LoadData()
        {
            string search = txtFilter.Text;
            string tahun = comboTahunFilter.SelectedItem?.ToString() ?? string.Empty;
            string sqlc = string.Empty;
            var dp = new DynamicParameters();
            List<string> fltr = new List<string>();
            if (search != "") 
            { 
                fltr.Add("(s.Nis LIKE @Search||'%' OR s.Nama LIKE '%'||@Search||'%' OR k.NamaKelas LIKE '%'||@Search||'%')");
                dp.Add("@Search", search); 
            }
            
            if (tahun != "Semua") 
            { 
                fltr.Add("s.Tahun LIKE @Tahun||'%'");
                dp.Add("@Tahun", tahun); 
            }
            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ", fltr);

            string text = "Halaman ";
            int RowPerPage = (int)comboPerPage.SelectedItem;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = siswaDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);

            dataGridView1.DataSource = siswaDal.ListData(sqlc, dp)
                .Select((x,index) => new
                {
                    No = inRowPage +index + 1, 
                    x.Nis,
                    x.Persensi,
                    x.Nama,
                    x.JenisKelamin,
                    x.NamaKelas,
                    x.Tahun
                }).ToList();
        }

        #endregion

        #region EVENT
        private void RegisterEvent()
        {
            this.Resize += FormSIswa_Resize;
            txtFilter.TextChanged += txtFilter_TextChanged;
            comboTahunFilter.SelectedIndexChanged += txtFilter_TextChanged;
            comboPerPage.SelectedIndexChanged += txtFilter_TextChanged;

            txtNIS_FormSiswa.KeyPress += input_KeyPress;
            txtPersensi_FormSiswa.KeyPress += input_KeyPress;
            txtTahun_FormSiswa.KeyPress += input_KeyPress;

            btnSave_FormSiswa.Click += BtnSave_FormSiswa_Click;
            btnResetFilter.Click += BtnResetFilter_Click;
            btnNext.Click += btnNext_Click;
            btnPrevious.Click += btnPrevious_Click;
            XRadio.CheckedChanged += radio_CheckedChange;
            XIRadio.CheckedChanged += radio_CheckedChange;
            XIIRadio.CheckedChanged += radio_CheckedChange;
            jurusanCombo.SelectedIndexChanged += radio_CheckedChange;
            txtFilter.TextChanged += TxtFilter_ChangeLeave;
            txtFilter.Leave += TxtFilter_ChangeLeave;
            lblFilter.Click += LblFilter_Click;
            dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
            EditMenuStrip.Click += EditMenuStrip_Click;
            DeleteMenuStrip.Click += DeleteMenuStrip_Click;

            ButtonNaikKelas.Click += ButtonNaikKelas_Click;
            NaikKelasContext.Click += NaikKelasContext_Click;
            HapusSiswaLulus.Click += HapusSiswaLulus_Click;
        }

        private void FormSIswa_Resize(object sender, EventArgs e)
        {
            if (panel5.Height < 505)
            {
                txtNIS_FormSiswa.Width = 140;
                txtNIS_FormSiswa.Location = new Point(18,67);
                lblNisSudahAda.Location = new Point(66, 50);
                label11.Location = new Point(171,48);
                txtPersensi_FormSiswa.Width = 140;
                txtPersensi_FormSiswa.Location = new Point(168,67);
                label6.Location = new Point(21,95);
                txtNama_FormSiswa.Location = new Point(18,114);
                label7.Location = new Point(22,144);
                groupBox1.Location = new Point(18,162);
                label8.Location = new Point(22,193);
                XRadio.Location = new Point(54,212);
                XIRadio.Location = new Point(122,212);
                XIIRadio.Location = new Point(190,212);
                label10.Location = new Point(22,247);
                label9.Location = new Point(171, 247);
                jurusanCombo.Width = 140;
                rombelCombo.Width = 140;
                jurusanCombo.Location = new Point(19,266);
                rombelCombo.Location = new Point(168,266);
                label12.Location = new Point(22,296);
                txtTahun_FormSiswa.Location = new Point(19,315);
            }
            else
            {
                foreach (var control in originalLocations.Keys)
                {
                    control.Location = originalLocations[control];
                }

                foreach (var control in originalSizes.Keys)
                {
                    control.Size = originalSizes[control];
                }
            }
        }

        private void ButtonNaikKelas_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(Cursor.Position);
            InitComboTahun();
        }
        private void HapusSiswaLulus_Click(object sender, EventArgs e)
        {
            if (new MesWarningYN("Hapus Data Siswa Yang Telah Lulus?").ShowDialog(this) != DialogResult.Yes) return;
               
            if (kelasDal.DeleteSiswaLulus() < 1 ) 
            {
                new MesWarningOK("Data Lulus Tidak Tersedia").ShowDialog(this);
                return;
            }

            LoadData();
            InitComboTahun();
            new MesInformasi("Data Berhasil Dihapus").ShowDialog(this);
        }

        private void NaikKelasContext_Click(object sender, EventArgs e)
        {
            if(siswaDal.CekDataSiswa() == 0)
            {
                new MesError("Data Siswa Kosong!").ShowDialog(this);
                return;
            }
            if (kelasDal.cekLulus())
            {
                new MesError("Data siswa LULUS masih ada\nAnda harus menghapusnya jika ingin menaikkan kelas untuk seluruh siswa!",2).ShowDialog(this);
                return;
            }

            if (new MesWarningYN("Naik kelas untuk seluruh siswa?\nTindakan ini tidak dapat urungkan!!", 2).ShowDialog(this) != DialogResult.Yes) return;

            var allKelas = kelasDal.listKelas(string.Empty, new { });
            if (!allKelas.Any()) return;
            kelasDal.DuplikatKelas("X");
            foreach (var x in allKelas)
            {
                string tingkat = x.Tingkat == "X" ? "XI"
                    : x.Tingkat == "XI" ? "XII"
                    : x.Tingkat == "XII" ? "LULUS"
                    : string.Empty;
                var kelas = new KelasModel()
                {
                    Id = x.Id,
                    NamaKelas = $"{tingkat} {x.Kode} {x.Rombel}".Trim(),
                    Tingkat = tingkat,
                    IdJurusan = x.IdJurusan,
                    Rombel = x.Rombel,
                    status = x.Tingkat.Trim() == "XII" ? 0 : 1
                };
                kelasDal.Update(kelas);
            }
            new MesInformasi("Seluruh Siswa Berhasil Naik Kelas!").ShowDialog(this);
            LoadData();
            InitComboTahun();
        }

        private void BtnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void DeleteMenuStrip_Click(object sender, EventArgs e)
        {
            if (new MesWarningYN("Hapus Data?\nJika Dihapus Maka Data Yang Terhubung Akan Ikut Terhapus!",2).ShowDialog(this) != DialogResult.Yes) return;

            var id = dataGridView1.CurrentRow.Cells[1].Value;

            siswaDal.Delete(Convert.ToInt32(id));
            LoadData();
            InitComboTahun();
        }

        private void EditMenuStrip_Click(object sender, EventArgs e)
        {
/*            string kelas = dataGridView1.CurrentRow.Cells[5].Value?.ToString() ?? string.Empty;
            string[] kelasArr = kelas.Split(' ');
            if (kelasArr[0] == "LULUS")
            {
                new MesError("Data siswa LULUS tidak dapat di edit!").ShowDialog(this);
                return;
            };*/

            int Nis = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);
            if (new EditSiswa(Nis).ShowDialog() == DialogResult.Yes)
                LoadData();
            InitComboTahun();
        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1. CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void LblFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Focus();
        }

        private void TxtFilter_ChangeLeave(object sender, EventArgs e)
        {
            if (txtFilter.Text.Length > 0)
                lblFilter.Visible = false;
            else
                lblFilter.Visible = true;
        }
        
        private void txtFilter_TextChanged(object sender,EventArgs e)
        {
            Page = 1;
            debounceTimer?.Dispose();

            debounceTimer = new System.Threading.Timer(x =>
            {
                this.Invoke(new Action(LoadData));
            }, null, debounceDelay, Timeout.Infinite);
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
            txtFilter.Clear();
            if (comboTahunFilter.Items.Count == 0) return;
            comboTahunFilter.SelectedIndex = 0;
            
            LoadData();
        }
        private void input_KeyPress(object sender,KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        private void txtNIS_FormSiswa_TextChanged(object sender, EventArgs e)
        {
            string nis = txtNIS_FormSiswa.Text;
            if (nis.Length >= 5)
                CekNis(Convert.ToInt32(nis));
            else
                lblNisSudahAda.Visible = false;
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

        #region IMPORT DATA
        private async void ButtonInputSIswa_Click(object sender, EventArgs e)
        {
            Loading loading = new Loading();
            loading.WindowState = FormWindowState.Maximized;
            await Task.Delay(500);
            
            FormKetentuanImport ketentuan = new FormKetentuanImport();
            if (ketentuan.ShowDialog(this) != DialogResult.OK) return;

            OpenFileDialog dialogOpen = new OpenFileDialog();
            dialogOpen.Filter = "File Excel |*.xls; *.xlsx";

            if (dialogOpen.ShowDialog() == DialogResult.OK)
            {
                loading.Show();
                await Task.Delay(500);
                FileInfo infoFile = new FileInfo(dialogOpen.FileName);
                List<string> daftarKelasError = new List<string>();

                using (ExcelPackage package = new ExcelPackage(infoFile))
                {
                    foreach (var sheet in package.Workbook.Worksheets)
                    {
                        int RowCount = sheet.Dimension.Rows;
                        using (IDbConnection Conn = new SQLiteConnection(conn.connstr()))
                        {
                            for (int baris = 2; baris <= RowCount; baris++)
                            {
                                var nis = long.TryParse(sheet.Cells[baris, 2].Value?.ToString().Trim(), out long parsedNis) ? parsedNis : (long?)null;
                                var nama = sheet.Cells[baris, 3].Value?.ToString().Trim();
                                var kelas = sheet.Cells[baris, 4].Value?.ToString().Trim();
                                        
                                var tahun = int.TryParse(sheet.Cells[baris, 6].Value?.ToString().Trim(), out int parsedTahun) ? parsedTahun : (int?)null;
                                var presensi = int.TryParse(sheet.Cells[baris, 1].Value?.ToString().Trim(), out int parsedPresensi) ? parsedPresensi : (int?)null;
                                var jenisKelamin = sheet.Cells[baris, 5].Value?.ToString().Trim();

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
                    loading.Close();
                    LoadData();
                    new MesInformasi("Data siswa berhasil ditambahkan atau diperbarui").ShowDialog(this);
                    int row = daftarKelasError.Count <= 4 ? 1 : daftarKelasError.Count <= 10 ? 2 : 3;
                    string dftrKelas = string.Empty;

                    if (daftarKelasError.Count < 1)
                        dftrKelas = "Tidak Ada";
                    else if (row == 1)
                        dftrKelas += string.Join(", ", daftarKelasError);
                    else
                        for (int i = 0; i <= daftarKelasError.Count - 1; i++)
                            dftrKelas += i == 3 || i == 9 ? $"{daftarKelasError[i]}, \n" : $"{daftarKelasError[i]}, ";

                    new MesError($"Daftar Kelas Error: {dftrKelas.TrimEnd(',',' ')}",row).ShowDialog(this);
                }
            }
        }

        private void ButtonDownloadFormat_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var sheet_X = package.Workbook.Worksheets.Add("X");
                    var sheet_XI = package.Workbook.Worksheets.Add("XI");
                    var sheet_XII = package.Workbook.Worksheets.Add("XII");

                    void CreateTables(ExcelWorksheet sheetExcel)
                    {
                        sheetExcel.Column(3).Width = 50;
                        sheetExcel.Column(4).Width = 12;
                        sheetExcel.Column(5).Width = 20;
                        sheetExcel.Column(6).Width = 10;
                        sheetExcel.Cells.Style.Font.Size = 12;

                        int barisAwal = 1;

                        for (int tabelIndek = 0; tabelIndek < 16; tabelIndek++)
                        {
                            sheetExcel.Cells[barisAwal, 1].Value = "Presensi";
                            sheetExcel.Cells[barisAwal, 2].Value = "NIS";
                            sheetExcel.Cells[barisAwal, 3].Value = "Nama";
                            sheetExcel.Cells[barisAwal, 4].Value = "Kelas";
                            sheetExcel.Cells[barisAwal, 5].Value = "Jenis Kelamin (L/P)";
                            sheetExcel.Cells[barisAwal, 6].Value = "Tahun";

                            using (var range = sheetExcel.Cells[barisAwal, 1, barisAwal, 6])
                            {
                                range.Style.Font.Bold = true;
                                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                sheetExcel.Row(barisAwal).Height = 23;
                            }

                            for (int barisIndek = 1; barisIndek <= 37; barisIndek++)
                            {
                                var range = sheetExcel.Cells[barisAwal + barisIndek, 1, barisAwal + barisIndek, 6];
                                range.Value = "";

                                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                sheetExcel.Row(barisAwal + barisIndek).Height = 21;
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

                        new MesInformasi("File Excel berhasil disimpan!").ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                new MesError($"Terjadi kesalahan saat mendownload data: {ex.Message}").ShowDialog();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion
    }
}
