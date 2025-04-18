﻿using Dapper;
using latihribbon.Dal;
using latihribbon.Model;
using latihribbon.ScreenAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Globalization;


namespace latihribbon
{
    public partial class FormAbsensi : Form
    {
        private readonly AbsensiDal absensiDal;
        private readonly SiswaDal siswaDal;
        private readonly HistoryDal historyDal;
        private readonly KelasDal kelasDal;
        private bool InternalTextChange = true;
        private System.Threading.Timer timer;
        private ToolTip toolTip = new ToolTip();
        public FormAbsensi()
        {
            InitializeComponent();
            buf();
            absensiDal = new AbsensiDal();
            siswaDal = new SiswaDal();
            kelasDal = new KelasDal();
            historyDal = new HistoryDal();
            InitComponent();
            RegisterEvent();
            LoadData();
            InitGrid();
            this.Load += FormAbsensi_Load;
        }

        private void FormAbsensi_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
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
  
        public void InitComponent()
        {
            List<string> ketCombo = new List<string>() { "Semua","A","I","S"};
            KeteranganCombo.DataSource = ketCombo;

            List<string> listSorting = new List<string>() { "Terbaru", "Terlama"};
            comboSorting.DataSource = listSorting;

            comboPerPage.Items.Add(10);
            comboPerPage.Items.Add(20);
            comboPerPage.Items.Add(50);
            comboPerPage.Items.Add(100);
            comboPerPage.Items.Add(200);
            comboPerPage.SelectedIndex = 0;
            comboPerPage.DropDownStyle = ComboBoxStyle.DropDownList;

            autoClearCB.Checked = false;
        }

        private void InitGrid()
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

            dataGridView1.Columns[2].HeaderText = "NIS";
            dataGridView1.Columns[3].HeaderText = "Presensi";
            dataGridView1.Columns[5].HeaderText = "Nama Kelas";

            txtNIS1.MaxLength = 9;
            txtPersensi1.MaxLength = 3;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 350;
            dataGridView1.Columns[5].Width = 130;
            dataGridView1.Columns[6].Width = 110;
            dataGridView1.Columns[7].Width = 130;
            toolTip.SetToolTip(btnResetFilter,"Reset Filter");
        }


        bool tglchange = false;
        int Page = 1;
        int totalPage;
        public void LoadData()
        {
            string search = txtSearch.Text;
            string keterangan = KeteranganCombo?.SelectedItem.ToString() ?? string.Empty;
            DateTime tgl1 = tglsatu.Value.Date;
            DateTime tgl2 = tgldua.Value.Date;
            string sorting = comboSorting?.SelectedItem.ToString() ?? string.Empty;

            //Filter
            var sqlc = string.Empty;
            var sqlcSorting = string.Empty;
            var dp = new DynamicParameters();
            List<string> fltr = new List<string>();

            if (search != "")
            {
                dp.Add("@Search", search);
                fltr.Add("(p.NIS LIKE @Search||'%' OR s.Nama LIKE '%'||@Search||'%' OR s.Persensi LIKE @Search||'%' OR k.NamaKelas LIKE '%'||@Search||'%')");
            }
            if(keterangan != "Semua")
            {
                dp.Add("@Keterangan",keterangan);
                fltr.Add("p.Keterangan LIKE @Keterangan||'%'");
            }
            if (tglchange)
            {
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
                fltr.Add("p.Tanggal BETWEEN @tgl1 AND @tgl2");
            }
            if(sorting != string.Empty)
            {
                int index = comboSorting.SelectedIndex;
                if (index == 0) sqlcSorting = "p.ID DESC";
                if (index == 1) sqlcSorting = "p.ID ASC";
                //if (index == 2) sqlcSorting = "p.Tanggal DESC, s.Persensi ASC";
            }
            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ", fltr);

            //Halaman
            string text = "Halaman ";
            int RowPerPage = Convert.ToInt16(comboPerPage.SelectedItem);
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = absensiDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = absensiDal.ListData(sqlc, sqlcSorting, dp)
                .Select((x,index) => new
                {
                    x.Id,
                    No = inRowPage+index+1,
                    x.Nis,
                    x.Persensi,
                    x.Nama,
                    x.NamaKelas,
                    x.Tanggal,
                    x.Keterangan
                }).ToList();
        }

        private void ClearInput()
        {
            txtNIS1.Clear();
            txtNama1.Clear();
            txtKelas1.Clear();
            tglDT.Value = DateTime.Today;
            Izinradio.Checked = false;
            sakitRadio.Checked = false;
            alphaRadio.Checked = false;
        }

        private AbsensiModel ValidasiInput()
        {
            int Persensi = string.IsNullOrEmpty(txtPersensi1.Text) ? 0 : Convert.ToInt32(txtPersensi1.Text);
            string Kelas = txtKelas1.Text;
            var cekData = absensiDal.GetByAbsensiKelas(Kelas, Persensi);
            var absensi = new AbsensiModel 
            {
                Nis = cekData?.Nis ?? 0,
                Nama = cekData?.Nama ?? string.Empty,
            };
            return absensi;
        }
        private void SaveData()
        {
            string nis, nama,persensi, kelas, keterangan=string.Empty;
            DateTime tgl;

            nis = txtNIS1.Text;
            nama = txtNama1.Text.Trim();
            persensi = txtPersensi1.Text;
            kelas = txtKelas1.Text;
            tgl = tglDT.Value.Date;

            if (Izinradio.Checked) keterangan = "I";
            if (sakitRadio.Checked) keterangan = "S";
            if (alphaRadio.Checked) keterangan = "A";

            if (nis == "" || nama == "" || keterangan == "")
            {
                new MesWarningOK("Seluruh Data Wajib Diisi!").ShowDialog();
                return;
            }
            var masuk = new AbsensiModel
            {
                Nis = Convert.ToInt32(nis),
                Tanggal = tgl,
                Keterangan = keterangan
            };

            var dataCek = absensiDal.GetByCondition(" WHERE p.NIS=@NIS AND p.Tanggal = @Tanggal", new { NIS = masuk.Nis, Tanggal = masuk.Tanggal });
            if (dataCek != null)
            {
                new MesWarningOK($"{nama} Sudah Absensi Pada " + tgl.ToString("dd/MM/yyyy")).ShowDialog();
                return;
            }
            if (new MesQuestionYN("Input Data?").ShowDialog() != DialogResult.Yes) return;
            absensiDal.Insert(masuk);
            LoadData();
            if (autoClearCB.Checked)
                ClearInput();
        }

        private void CekNis()
        {
            var siswa = siswaDal.GetData(Convert.ToInt32(txtNIS1.Text));
            if (siswa == null)
            {
                lblNisTidakDitemukan.Visible = true;
                txtNama1.Text = string.Empty;
                txtKelas1.Text = string.Empty;
                txtPersensi1.Text = string.Empty;
            }
            else
            {
                lblNisTidakDitemukan.Visible = false;
                txtNama1.Text = siswa.Nama;
                txtKelas1.Text = siswa.NamaKelas;
                txtPersensi1.Text = siswa.Persensi.ToString() ?? string.Empty;
            }
        }
        #region EVENT

        private void RegisterEvent()
        {
            txtPersensi1.TextChanged += perkas_TextChanged;
            txtKelas1.TextChanged += perkas_TextChanged;

            txtSearch.TextChanged += filter_TextChanged;
            KeteranganCombo.SelectedIndexChanged += filter_TextChanged;

            tglsatu.ValueChanged += filter_tglChanged;
            tgldua.ValueChanged += filter_tglChanged;

            dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
            EditMenuStrip.Click += EditMenuStrip_Click;
            DeleteMenuStrip.Click += DeleteMenuStrip_Click;

            txtNIS1.KeyPress += Input_KeyPress;
            txtPersensi1.KeyPress += Input_KeyPress;
            
            txtSearch.TextChanged += TxtSearch_ChangeLeave;
            txtSearch.Leave += TxtSearch_ChangeLeave;
            lblFilter.Click += LblFilter_Click;
            btnKelas.Click += btnKelas_Click;
            btnSave_FormSiswa.Click += btnSave_FormSiswa_Click;
            txtNIS1.KeyPress += txtNIS1_KeyPress;
            txtNIS1.TextChanged += txtNIS1_TextChanged;

            comboPerPage.SelectedIndexChanged += filter_TextChanged;
            comboSorting.SelectedIndexChanged += (s,e) => LoadData();
        }

        private void LblFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void TxtSearch_ChangeLeave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
                lblFilter.Visible = false;
            else
                lblFilter.Visible = true;
        }

        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void DeleteMenuStrip_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            if (new MesWarningYN("Hapus Data?").ShowDialog() != DialogResult.Yes) return;
            var id = dataGridView1.CurrentRow.Cells[0].Value;

            absensiDal.Delete(Convert.ToInt32(id));
            LoadData();
        }
        private void EditMenuStrip_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            EditAbsensi edit = new EditAbsensi(id);

            if (edit.ShowDialog() == DialogResult.Yes)
                LoadData();
        }
        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
        private void filter_TextChanged(object sender,EventArgs e)
        {
            Page = 1;
            timer?.Dispose();
            timer = new System.Threading.Timer(x =>
            {
                this.Invoke(new Action(LoadData));
            },null,300,Timeout.Infinite);
        }
        private void filter_tglChanged(object sender, EventArgs e)
        {
            Page = 1;
            tglchange = true;
            LoadData();
        }
        private void perkas_TextChanged(object sender, EventArgs e)
        {
            if (!InternalTextChange) return;
            if(!string.IsNullOrEmpty(txtPersensi1.Text) && !string.IsNullOrEmpty(txtKelas1.Text))
            {
                InternalTextChange = false;

                var hasil = ValidasiInput();
                txtNIS1.Text = hasil.Nis != 0 ? hasil.Nis.ToString() : string.Empty; 
                txtNama1.Text = hasil.Nama;

                InternalTextChange = true;
            }
        }
        private void txtNIS1_TextChanged(object sender, EventArgs e)
        {
            if (!InternalTextChange) return;
            InternalTextChange= false;
            if (txtNIS1.Text.Length >= 5)
            {
                CekNis();
            }
            else
            {
                txtNama1.Text = string.Empty;
                txtPersensi1.Text = string.Empty;
                txtKelas1.Text = string.Empty;
                lblNisTidakDitemukan.Visible = false;
            }
            InternalTextChange = true;
        }
        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            KeteranganCombo.SelectedIndex = 0;
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            tglchange = false;
            LoadData();
        }


        private void txtNIS1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            SaveData();
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
            string namaKelas = txtKelas1.Text.Trim().ToUpper();
            PopUpKelas kelas = new PopUpKelas("Absensi",namaKelas);
            kelas.ShowDialog(this);
            if (kelas.DialogResult == DialogResult.OK)
                txtKelas1.Text = historyDal.GetData("Absensi")?.History.ToString() ?? string.Empty;
        }
        #endregion
    }
}
