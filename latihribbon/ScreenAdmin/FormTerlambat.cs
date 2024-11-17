using Dapper;
using latihribbon.Conn;
using latihribbon.Dal;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon
{
    
    public partial class FormTerlambat : Form
    {
        private readonly MasukDal masukDal;
        private readonly SiswaDal siswaDal;
        private readonly KelasDal kelasDal;
        
        private ToolTip toolTip;
        private System.Threading.Timer timer;

        public FormTerlambat()
        {
            InitializeComponent();
            buf();
            masukDal = new MasukDal();
            siswaDal = new SiswaDal();
            kelasDal = new KelasDal();
            toolTip = new ToolTip();
            InitCombo();
            RegisterEvent();
            LoadData();
            InitComponen();

            CultureInfo culture = new CultureInfo("id-ID");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
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
            //textBox MaxLength
            txtNIS1.MaxLength = 10;
            txtAlasan1.MaxLength = 60;
            
            // DataGrid
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.Columns["NamaKelas"].HeaderText = "Nama Kelas";
            dataGridView1.Columns["JamMasuk"].HeaderText = "Jam Masuk";

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 350;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 110;
            dataGridView1.Columns[6].Width = 110;
            dataGridView1.Columns[7].Width = 300;

            toolTip.SetToolTip(btnResetFilter, "Reset Filter");
        }

        private void InitCombo()
        {
            comboPerPage.Items.Add(10);
            comboPerPage.Items.Add(20);
            comboPerPage.Items.Add(50);
            comboPerPage.Items.Add(100);
            comboPerPage.Items.Add(200);
            comboPerPage.SelectedIndex = 0;
            comboPerPage.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        bool tglchange = false;
        int Page = 1;
        int totalPage;
        public void LoadData()
        {
            string search = txtFilter.Text;
            DateTime tgl1 = tglsatu.Value.Date;
            DateTime tgl2 = tgldua.Value.Date;

            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            var dp = new DynamicParameters();

            if (search != "") 
            {
                dp.Add("@Search", search);
                if (search != "") fltr.Add("(m.NIS LIKE @Search+'%' OR s.Nama LIKE '%'+@Search+'%' OR kls.NamaKelas LIKE '%'+@Search+'%')");
            }
            if (tglchange)
            {
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
                fltr.Add("m.Tanggal BETWEEN @tgl1 AND @tgl2");
            }
            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ",fltr);
            string text = "Halaman ";
            int RowPerPage = (int)comboPerPage.SelectedItem;
            int inRowPage = (Page - 1) * RowPerPage;

            var jumlahRow = masukDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = masukDal.ListData(sqlc, dp)
                .Select((x,index) => new
                {
                    Id = x.Id,
                    No = inRowPage + index + 1,
                    NIS = x.NIS,
                    Nama = x.Nama,
                    NamaKelas = x.NamaKelas,
                    Tanggal = x.Tanggal,
                    JamMasuk = x.JamMasuk.ToString(@"hh\:mm"),
                    AlasanTerlambat = x.Alasan 
                }).ToList();
        }

        private void ClearInput()
        {
            txtNIS1.ReadOnly = false;
            txtNIS1.Clear();
            txtNama1.Clear();
            txtKelas1.Clear();
            tglDT.Value = DateTime.Now;
            jamMasukDT.Value = DateTime.Now;
            txtAlasan1.Clear();
        }
        private void SaveData()
        {
            string nis = txtNIS1.Text;
            string nama = txtNama1.Text.Trim();
            string alasan = txtAlasan1.Text.Trim();
            DateTime tgl = tglDT.Value;
            TimeSpan jamMasuk = jamMasukDT.Value.TimeOfDay;

            if (nis == "" || nama == "" || alasan == "")
            {
                new MesWarningOK("Seluruh Data Wajib Diisi!").ShowDialog(this);
                return;
            }

            var masuk = new MasukModel
            {
                NIS = Convert.ToInt32(nis),
                Tanggal = tgl,
                JamMasuk = jamMasuk,
                Alasan = alasan
            };
            if (new MesQuestionYN("Input Data?").ShowDialog(this) != DialogResult.Yes) return;
            masukDal.Insert(masuk);
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
                txtKelas1.Text = siswa.NamaKelas;
            }
        }

        #region EVENT
        private void RegisterEvent()
        {
            btnSave_FormSiswa.Click += btnSave_FormSiswa_Click;
            txtNIS1.TextChanged += txtNIS1_TextChanged;
            txtNIS1.KeyPress += txtNIS1_KeyPress;
            txtFilter.TextChanged += filter_TextChanged;
            tglsatu.ValueChanged += filter_tglChanged;
            tgldua.ValueChanged += filter_tglChanged;
            comboPerPage.SelectedIndexChanged += filter_TextChanged;
            txtFilter.Leave += TxtFilter_ChangeLeave;
            txtFilter.TextChanged += TxtFilter_ChangeLeave;
            lblFilter.Click += LblFilter_Click;
            this.Resize += FormTerlambat_Resize;

            dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
            EditMenuStrip.Click += EditMenuStrip_Click;
            DeleteMenuStrip.Click += DeleteMenuStrip_Click;

            btnNext.Click += btnNext_Click;
            btnPrevious.Click += btnPrevious_Click;
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

        private void DeleteMenuStrip_Click(object sender, EventArgs e)
        {
            if (new MesWarningYN("Hapus Data?").ShowDialog(this) != DialogResult.Yes) return;
            var id = dataGridView1.CurrentRow.Cells[0].Value;

            masukDal.Delete(Convert.ToInt32(id));
            LoadData();
        }
        private void EditMenuStrip_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            EditTerlambat edit = new EditTerlambat(Id);

            if (edit.ShowDialog(this) == DialogResult.Yes)
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
        private void LblFilter_Click(object sender, EventArgs e)
        {
            txtFilter.Focus();
        }

        private void TxtFilter_ChangeLeave(object sender, EventArgs e)
        {
            if(txtFilter.Text.Length > 0)
                lblFilter.Visible = false;
            else
                lblFilter.Visible = true;
        }

        private void FormTerlambat_Resize(object sender, EventArgs e)
        {
            if (panel4.Height < 458)
            {
                txtAlasan1.Multiline = false;
                tglDT.Width = 140;
                jamMasukDT.Width = 140;

                lblJamMasuk.Location = new Point(175, 194);
                jamMasukDT.Location = new Point(171, 215);
                lblAlasan.Location = new Point(23, 242);
                txtAlasan1.Location = new Point(21, 264);
                
            }
            else
            {
                txtAlasan1.Multiline = true;
                tglDT.Width = 290;
                jamMasukDT.Width = 290;
                txtAlasan1.Height = 60;

                lblJamMasuk.Location = new Point(23, 242);
                jamMasukDT.Location = new Point(21, 263);
                lblAlasan.Location = new Point(23, 290);
                txtAlasan1.Location = new Point(21, 312);
            }
                
        }

        private void filter_TextChanged(object sender, EventArgs e)
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
            txtFilter.Clear();
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            tglchange = false;
            LoadData();
        }
        #endregion

        private void FormTerlambat_Load(object sender, EventArgs e)
        {
            txtFilter.Focus();
        }

    }
}
