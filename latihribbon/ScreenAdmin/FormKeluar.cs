using Dapper;
using latihribbon.Dal;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace latihribbon
{
    public partial class FormKeluar : Form
    {
        private readonly SiswaDal siswaDal;
        private readonly KeluarDal keluarDal;
        private readonly KelasDal kelasDal;
        private ToolTip toolTip;
        int globalId = 0;
        private Dictionary<Control,Point> oriLocation = new Dictionary<Control,Point>();
        private Dictionary<Control,Size> oriSize = new Dictionary<Control,Size>();
        private System.Threading.Timer timer;
        public FormKeluar()
        {
            InitializeComponent();
            buf();
            this.Load += FormKeluar_Load;
            siswaDal = new SiswaDal();
            keluarDal = new KeluarDal();
            kelasDal = new KelasDal();
            toolTip = new ToolTip();
            InitCombo();
            LoadData();
            InitComponent();
            RegisterEvent();

            CultureInfo culture = new CultureInfo("id-ID");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void FormKeluar_Load(object sender, EventArgs e)
        {

            TextSearch.Focus();
            oriLocation[label13] = label13.Location;
            oriLocation[jamMasukDT] = jamMasukDT.Location;
            oriLocation[label11] = label11.Location;
            oriLocation[txtTujuan1] = txtTujuan1.Location;

            oriSize[jamKeluarDT] = jamKeluarDT.Size;
            oriSize[jamMasukDT] = jamMasukDT.Size;
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

        private void InitCombo()
        {
            //ComboBox
            comboPerPage.Items.Add(10);
            comboPerPage.Items.Add(20);
            comboPerPage.Items.Add(50);
            comboPerPage.Items.Add(100);
            comboPerPage.Items.Add(200);
            comboPerPage.SelectedIndex = 0;
            comboPerPage.DropDownStyle = ComboBoxStyle.DropDownList;
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
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.Columns[3].HeaderText = "Nama Kelas";
            dataGridView1.Columns[6].HeaderText = "Jam Keluar";
            dataGridView1.Columns[7].HeaderText = "Jam Masuk";

            dataGridView1.Columns["Id"].Visible = false;

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 350;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 110;
            dataGridView1.Columns[6].Width = 110;
            dataGridView1.Columns[7].Width = 110;
            dataGridView1.Columns[8].Width = 300;

            //TextBox
            txtNIS1.MaxLength = 9;
            txtTujuan1.MaxLength = 60;

            toolTip.SetToolTip(btnResetFilter, "Reset Filter");
            toolTip.SetToolTip(btnPrintKeluar,"Export To Excel");
        }

        bool tglchange = false;
        private string FilterSQL(string search)
        {
            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            if (search != "") fltr.Add("(k.NIS LIKE '%'+ @search+'%'  OR s.Nama LIKE '%' +@search+'%'  OR kls.NamaKelas LIKE'%' +@search+'%')");
            if (tglchange) fltr.Add("k.Tanggal BETWEEN @tgl1 AND @tgl2");
            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ", fltr);
            return sqlc;
        }

        int Page = 1;
        int totalPage;
        public void LoadData()
        {
            DateTime tgl1, tgl2;

            string search = TextSearch.Text;
            tgl1 = tglsatu.Value.Date;
            tgl2 = tgldua.Value.Date;

            var sqlc = FilterSQL(search);
            var dp = new DynamicParameters();
            if(search != "")dp.Add("@search", search);
            if (tglchange) 
            {
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
            }

            string text = "Halaman ";
            int RowPerPage = (int)comboPerPage.SelectedItem;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = keluarDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = keluarDal.ListData(sqlc, dp)
                .Select((x, index) => new 
                { 
                    Id = x.Id,
                    No = inRowPage + index + 1,
                    NIS = x.Nis,
                    Nama = x.Nama,
                    NamaKelas = x.NamaKelas,
                    Tanggal = x.Tanggal,
                    JamKeluar = x.JamKeluar.ToString(@"hh\:mm"),
                    JamMasuk = x.JamMasuk.ToString(@"hh\:mm"),
                    Tujuan = x.Tujuan,
                }).ToList();
        }

        private void ClearInput()
        {
            txtNIS1.Clear();
            txtNama1.Clear();
            txtKelas1.Clear();
            tglDT.Value = DateTime.Now;
            jamKeluarDT.Value = DateTime.Now;
            jamMasukDT.Value = DateTime.Now;
            txtTujuan1.Clear();
        }

        private void SaveData()
        {
            string nis, nama, tujuan;
            DateTime tgl;
            TimeSpan jamKeluar, jamMasuk;
            nis = txtNIS1.Text;
            nama = txtNama1.Text.Trim();
            tujuan = txtTujuan1.Text.Trim();
            tgl = tglDT.Value;
            jamKeluar = jamKeluarDT.Value.TimeOfDay;
            jamMasuk = jamMasukDT.Value.TimeOfDay;

            if (nis == "" || nama == "" || tujuan == "" || jamMasuk == TimeSpan.Zero || jamKeluar == TimeSpan.Zero) 
            {
                new MesWarningOK("Seluruh Data Wajib Diisi !").ShowDialog(this);
                return;
            }

            if (jamMasuk == jamKeluar)
            {
                new MesWarningOK("Jam Keluar & Jam Masuk Tidak Valid!").ShowDialog(this);
                return;
            }

            var keluar = new KeluarModel
            {
                Nis = Convert.ToInt32(nis),
                Tanggal= tgl,
                JamKeluar = jamKeluar,
                JamMasuk = jamMasuk,
                Tujuan = tujuan
            };
            if (new MesQuestionYN("Input Data?").ShowDialog(this) != DialogResult.Yes) return;
            keluarDal.Insert(keluar);
            LoadData();
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
            }
            else 
            {
                lblNisTidakDitemukan.Visible = false;
                txtNama1.Text = siswa.Nama;
                txtKelas1.Text = kelasDal.GetData(siswa.IdKelas)?.NamaKelas ?? string.Empty;
            }
        }

        #region EVENT
       private void RegisterEvent()
        {
            this.Resize += FormKeluar_Resize;
            btnSave_FormSiswa.Click += btnSave_FormSiswa_Click;
            TextSearch.TextChanged += filter_Changed;
            txtNIS1.TextChanged += txtNIS1_TextChanged;
            txtNIS1.KeyPress += txtNIS1_KeyPress;

            tglsatu.ValueChanged += filter_tglChanged;
            tgldua.ValueChanged += filter_tglChanged;
            comboPerPage.SelectedIndexChanged += filter_Changed;
            dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
            editToolStripMenuItem.Click += EditToolStripMenuItem_Click;
            deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;

            TextSearch.TextChanged += TextSearch_ChangeLeave;
            TextSearch.Leave += TextSearch_ChangeLeave;
            lblFilter.Click += LblFilter_Click;

            btnNext.Click += btnNext_Click;
            btnPrevious.Click += BtnPrevious_Click;
            btnPrintKeluar.Click += BtnPrintKeluar_Click;
        }

        private void BtnPrintKeluar_Click(object sender, EventArgs e)
        {
            new ExportSuratIzin(1).ShowDialog(this);
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (Page > 1)
            {
                Page--;
                LoadData();
            }
        }

        private void FormKeluar_Resize(object sender, EventArgs e)
        {
            if(panel2.Height < 505)
            {
                txtTujuan1.Multiline = false;
                jamKeluarDT.Width = 140;
                jamMasukDT.Width = 140;
                label13.Location = new Point(172, 243);
                label11.Location = new Point(22, 291);
                jamMasukDT.Location = new Point(169, 263);
                txtTujuan1.Location = new Point(19, 311);
            }
            else
            {
                txtTujuan1.Multiline = true;
                foreach (var control in oriLocation.Keys)
                    control.Location = oriLocation[control];
                foreach(var control in oriSize.Keys)
                    control.Size = oriSize[control];
            }
        }   

        private void LblFilter_Click(object sender, EventArgs e)
        {
            TextSearch.Focus();
        }

        private void TextSearch_ChangeLeave(object sender, EventArgs e)
        {
            if (TextSearch.Text.Length > 0)
                lblFilter.Visible = false;
            else
                lblFilter.Visible = true;
        }


        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalId == 0)
            {
                new MesWarningOK("Pilih data dahulu ").ShowDialog(this);
                return;
            }
            if (new MesQuestionYN("Hapus Data ?").ShowDialog(this) != DialogResult.Yes) return;
            keluarDal.Delete(globalId);
            LoadData();
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new EditKeluar(globalId).ShowDialog() != DialogResult.OK) return;
            LoadData();
        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                var Id = dataGridView1.CurrentRow.Cells["Id"].Value?.ToString() ?? string.Empty;
                globalId = Id == string.Empty ? 0 : int.Parse(Id);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void filter_Changed(object sender, EventArgs e)
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

        private void btnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void txtNIS1_TextChanged(object sender, EventArgs e)
        {
            if (txtNIS1.Text.Length >= 5)
            {
                CekNis();
            }
            else
            {
                txtNama1.Text = string.Empty;
                txtKelas1.Text = string.Empty;
                lblNisTidakDitemukan.Visible = false;
            }
        }

        private void txtNIS1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            TextSearch.Clear();
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            tglchange = false;
            LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Page < totalPage)
            {
                Page++;
                LoadData();
            }
        }


        #endregion

    }
}
