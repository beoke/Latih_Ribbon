using DocumentFormat.OpenXml.Wordprocessing;
using latihribbon.Dal;
using latihribbon.Helper;
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
        private readonly MesBox mesBox = new MesBox();
        public FormKelas()
        {
            InitializeComponent();
            buf();
            jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();
            InitComponent();
            LoadData();
            RegisterEvent();
            HapusDataLulus();
        }
        private void RegisterEvent()
        {
            GridListKelas.CellMouseClick += DataGridView1_CellMouseClick;
            EditMenuStrip.Click += EditMenuStrip_Click;
            DeleteMenuStrip.Click += DeleteMenuStrip_Click;
            XRadio.CheckedChanged += Change_Value;
            XIRadio.CheckedChanged += Change_Value;
            XIIRadio.CheckedChanged += Change_Value;
            jurusanCombo.SelectedIndexChanged += Change_Value;
            txtRombel.TextChanged += Change_Value;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            ClearData();
        }

        private void Change_Value(object sender, EventArgs e)
        {
            SetNamaKelas();
        }
        private void DeleteMenuStrip_Click(object sender, EventArgs e)
        {
            string namaKelas = GridListKelas.CurrentRow.Cells[1].Value?.ToString() ?? string.Empty;
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
                kelasDal.listKelas("", new { })
                .Select((x,index) => new {
                    IdKelas = x.Id,
                    No = index+1,
                    NamaKelas = x.NamaKelas
                }).ToList();

            GridListKelas.Columns[0].Width = 100;
            GridListKelas.Columns[1].Width = 300;
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

        public void InitComponent()
        {
            var listJurusan = jurusanDal.ListData();
            if (!listJurusan.Any()) return;
            jurusanCombo.DataSource = listJurusan;
            jurusanCombo.DisplayMember = "NamaJurusan";
            jurusanCombo.ValueMember = "Id";
            XRadio.Checked = true;
        }

        public void SetNamaKelas()
        {
            string tingkat = XRadio.Checked ? "X" : XIRadio.Checked ? "XI" : XIIRadio.Checked ? "XII" : string.Empty;
            string jurusan = ((JurusanModel)jurusanCombo.SelectedItem)?.NamaJurusan ?? string.Empty;
            string rombel = txtRombel.Text;
            txtNamaKelas.Text = $"{tingkat} {jurusan} {rombel}";
        }

        public void SaveData()
        {
            if (jurusanCombo.Items.Count == 0) { new MesWarningOK("Data Jurusan Kosong!").ShowDialog(); return; }
            var kelas = new KelasModel
            {
                Id = txtIdKelas.Text == string.Empty ? 0 : Convert.ToInt32(txtIdKelas.Text),
                NamaKelas = txtNamaKelas.Text.Trim(),
                Rombel = txtRombel.Text,
                IdJurusan = int.Parse(jurusanCombo.SelectedValue.ToString()),
                Tingkat = XRadio.Checked ? "X" : XIRadio.Checked ? "XI" : XIIRadio.Checked ? "XII" : string.Empty,
                status = 1
            };

            if(kelas.NamaKelas == "" || kelas.Tingkat == "")
            {
                new MesWarningOK("Seluruh Data Wajib Diisi Kecuali ID !").ShowDialog();
                return;
            }
            if (new MesQuestionYN("Input Data?").ShowDialog() != DialogResult.Yes) return;
            kelasDal.Insert(kelas);
            LoadData();
            ClearData();
        }

        private void ClearData()
        {
            txtIdKelas.Clear();
            txtNamaKelas.Clear();
            
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

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
