using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using latihribbon.Dal;
using latihribbon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon.ScreenAdmin
{
    public partial class FormKelas : Form
    {
        private readonly JurusanDal jurusanDal;
        private readonly KelasDal kelasDal;
        public FormKelas()
        {
            InitializeComponent();
            buf();
            jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();
            HapusDataLulus();
            InitComponent();
            LoadData();
            InitGrid();
            RegisterEvent();

            this.Load += FormKelas_Load;
        }

        private void FormKelas_Load(object sender, EventArgs e)
        {
            GridListKelas.Focus();
        }

        private void RegisterEvent()
        {
            GridListKelas.CellMouseClick += DataGridView1_CellMouseClick;
            EditMenuStrip.Click += EditMenuStrip_Click;
            DeleteMenuStrip.Click += DeleteMenuStrip_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            ClearData();
        }

        private void DeleteMenuStrip_Click(object sender, EventArgs e)
        {
            string namaKelas = GridListKelas.CurrentRow.Cells[2].Value?.ToString() ?? string.Empty;
            if (new MesWarningYN($"Anda yakin ingin menghapus data \" {namaKelas} \" ? \nJika Dihapus, maka data yang terhubung akan ikut Terhapus", 2).ShowDialog() != DialogResult.Yes) return;

            var id = GridListKelas.CurrentRow.Cells[0].Value;

            kelasDal.Delete(Convert.ToInt32(id));
            LoadData();
        }

        private void EditMenuStrip_Click(object sender, EventArgs e)
        {
            int Nis = Convert.ToInt32(GridListKelas.CurrentRow.Cells[0].Value);
            if (new EditKelas(Nis).ShowDialog() == DialogResult.Yes)
                LoadData();
        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                GridListKelas.ClearSelection();
                GridListKelas.CurrentCell = GridListKelas[e.ColumnIndex, e.RowIndex];
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
        public void buf()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null,
            GridListKelas,
            new object[] { true });
        }

        public void LoadData()
        {
            GridListKelas.DataSource = 
                kelasDal.listKelas("", new {})
                .Select((x,index) => new {
                    IdKelas = x.Id,
                    No = index+1,
                    NamaKelas = x.NamaKelas
                }).ToList();
        }

        public void InitComponent()
        {
            var listJurusan = jurusanDal.ListData();
            if (!listJurusan.Any()) return;
            jurusanCombo.DataSource = listJurusan
                .Select(x => new JurusanModel()
                {
                    Id = x.Id,
                    NamaJurusan = $"{x.Kode} - {x.NamaJurusan}",
                    Kode = x.Kode
                }).ToList();
            jurusanCombo.DisplayMember = "NamaJurusan";
            jurusanCombo.ValueMember = "Id";
            XRadio.Checked = true;
        }

        private void InitGrid()
        {
            GridListKelas.Columns[1].Width = 60;
            GridListKelas.Columns[2].Width = 300;
            GridListKelas.Columns["NamaKelas"].HeaderText = "Nama Kelas";
            GridListKelas.Columns["IdKelas"].Visible = false;

            GridListKelas.EnableHeadersVisualStyles = false;
            GridListKelas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            GridListKelas.DefaultCellStyle.Font = new System.Drawing.Font("Sans Serif", 10);
            GridListKelas.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Sans Serif", 10, FontStyle.Bold);
            GridListKelas.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
            GridListKelas.RowTemplate.Height = 30;
            GridListKelas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            GridListKelas.ColumnHeadersHeight = 35;
        }

        public void SaveData()
        {
            if (jurusanCombo.Items.Count == 0) { new MesError("Data Jurusan Kosong!").ShowDialog(); return; }

            int idJurusan = Convert.ToInt32(jurusanCombo.SelectedValue.ToString());
            string tingkat = XRadio.Checked ? "X" : XIRadio.Checked ? "XI" : XIIRadio.Checked ? "XII" : string.Empty;
            string kode = ((JurusanModel)jurusanCombo.SelectedItem)?.Kode ?? string.Empty;
            string rombel = txtRombel.Text;
            string kelasName = $"{tingkat} {kode} {txtRombel.Text}";

            var kelas = new KelasModel
            {
                Id = 0,
                NamaKelas = kelasName.Trim(),
                Rombel = rombel,
                IdJurusan = idJurusan,
                Tingkat = tingkat,
                status = 1
            };

            if (kelasName == "" || tingkat == "")
            {
                new MesWarningOK("Seluruh Data Wajib Diisi!").ShowDialog();
                return;
            }
            if (kelasDal.CekDuplikasi(kelas, false))
            {
                new MesError($"Kelas {kelasName.Trim()} Sudah Tersedia.").ShowDialog(this);
                return;
            }
            
            if (new MesQuestionYN("Input Data?").ShowDialog() != DialogResult.Yes) return;
            kelasDal.Insert(kelas);
            LoadData();
            ClearData();
        }

        private void ClearData()
        {
            XRadio.Checked = false;
            XIRadio.Checked = false;
            XIIRadio.Checked = false;
            txtRombel.Text=string.Empty;

            if (jurusanCombo.Items.Count == 0) return;
                
            jurusanCombo.SelectedIndex = 0;
        }

        private void HapusDataLulus()
        {
            kelasDal.DeleteDataLulus();
        }
    }
}
