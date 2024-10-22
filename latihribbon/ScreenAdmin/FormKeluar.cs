using Dapper;
using DocumentFormat.OpenXml.Wordprocessing;
using latihribbon.Dal;
using latihribbon.Helper;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace latihribbon
{
    public partial class FormKeluar : Form
    {
        private readonly SiswaDal siswaDal;
        private readonly KeluarDal keluarDal;
        private readonly KelasDal kelasDal;
        private readonly MesBox mesBox = new MesBox();
        int globalId = 0;
        public FormKeluar()
        {
            InitializeComponent();
            buf();
            siswaDal = new SiswaDal();
            keluarDal = new KeluarDal();
            kelasDal = new KelasDal();
            
        

            RegisterEvent();
            InitCombo();
            LoadData();
            InitComponent();


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
            dataGridView1.Columns[5].HeaderText = "Jam Keluar";
            dataGridView1.Columns[6].HeaderText = "Jam Masuk";

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 350;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 110;
            dataGridView1.Columns[5].Width = 110;
            dataGridView1.Columns[6].Width = 110;
            dataGridView1.Columns[7].Width = 300;




            //TextBox
            txtNIS1.MaxLength = 9;
            txtTujuan1.MaxLength = 60;


           
        }

        bool tglchange = false;
        private string FilterSQL(string search)
        {
            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            if (search != "") fltr.Add("k.NIS LIKE '%'+ @search+'%'  OR s.Nama LIKE '%' +@search+'%'  OR kls.NamaKelas LIKE'%' +@search+'%'");
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
                .Select(x => new 
                { 
                    Id = x.Id,
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
            jamKeluarDT.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            jamMasukDT.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            txtTujuan1.Clear();

        }

        private void SaveData()
        {
            string nis, nama, kelas, tujuan;
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
                new MesWarningOK("Seluruh Data Wajib Diisi !").ShowDialog();
                return;
            }

            if (jamMasuk == jamKeluar)
            {
                new MesWarningOK("Jam Keluar & Jam Masuk Tidak Valid!").ShowDialog();
                return;
            }

            var keluar = new KeluarModel
            {
                Id = globalId,
                Nis = Convert.ToInt32(nis),
                Tanggal= tgl,
                JamKeluar = jamKeluar,
                JamMasuk = jamMasuk,
                Tujuan = tujuan
            };

            if(keluar.Id == 0)
            {
                if (new MesQuestionYN("Input Data ?").ShowDialog() != DialogResult.Yes) return;
                keluarDal.Insert(keluar);
                LoadData();
                globalId = 0;
                ClearInput();
            }
           
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
            TextSearch.TextChanged += filter_TextChanged;

            tglsatu.ValueChanged += filter_tglChanged;
            tgldua.ValueChanged += filter_tglChanged;
            comboPerPage.SelectedIndexChanged += ComboPerPage_SelectedIndexChanged;
            dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;
            editToolStripMenuItem.Click += EditToolStripMenuItem_Click;
            deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
            
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (globalId == 0)
            {
                new MesWarningOK("Pilih data dahulu ").ShowDialog();
                return;
            }
            if (new MesWarningYN("Hapus Data ?").ShowDialog() != DialogResult.Yes) return;
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


        private void ComboPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();

        }

      

        private void filter_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void filter_tglChanged(object sender, EventArgs e)
        {
            Page = 1;
            tglchange = true;
            LoadData();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearInput();
            globalId = 0;
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

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Page > totalPage)
            {
                Page--;
                LoadData();
            }
        }
        #endregion
    }
}
