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
            GridListKelas.DataSource = kelasDal.listKelas(string.Empty, new { });

            GridListKelas.Columns[0].Width = 100;
            GridListKelas.Columns[1].Width = 300;

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

        private void CekInsertUpdate()
        {
            lblInfo.Text = txtIdKelas.Text == string.Empty ? "INSERT" : "UPDATE";
        }

        public void SetNamaKelas()
        {
            string tingkat = XRadio.Checked ? "X" : XIRadio.Checked ? "XI" : XIIRadio.Checked ? "XII" : string.Empty;
            string jurusan = ((JurusanModel)jurusanCombo.SelectedItem).NamaJurusan;
            string rombel = txtRombel.Text;
            txtNamaKelas.Text = $"{tingkat} {jurusan} {rombel}";
        }

        public void SaveData()
        {
            string Rombel, idJurusan, tingkat;
            int idKelas = txtIdKelas.Text==string.Empty ? 0: Convert.ToInt32(txtIdKelas.Text) ;
            idJurusan = jurusanCombo.SelectedValue.ToString();
            Rombel = txtRombel.Text;
            tingkat = XRadio.Checked ? "X" : XIRadio.Checked ? "XI" : XIIRadio.Checked ? "XII" : string.Empty;

            var kelas = new KelasModel
            {
                Id = idKelas,
                NamaKelas = txtNamaKelas.Text,
                Rombel = Rombel,
                IdJurusan = int.Parse(idJurusan),
                Tingkat = tingkat
            };

            if(idKelas == 0)
            {
                kelasDal.Insert(kelas);
                LoadData();
            }
            else
            {
                kelasDal.Update(kelas);
                LoadData();
            }
        }

        public void GetData(int Id)
        {
            var kelas = kelasDal.GetData(Id);
            if (kelas == null) return;
            txtIdKelas.Text = kelas.Id.ToString();
            txtNamaKelas.Text = kelas.NamaKelas;
            string[] arrKelas = kelas.NamaKelas.Split(' ');
            if (arrKelas[0] == "X") XRadio.Checked = true;
            if (arrKelas[0] == "XI") XIRadio.Checked = true; 
            if (arrKelas[0] == "XII") XIIRadio.Checked = true;
            jurusanCombo.SelectedValue = kelas.IdJurusan;

            txtRombel.Text = kelas.Rombel==string.Empty ? string.Empty : kelas.Rombel;
            CekInsertUpdate();
        }

        private void DeleteData()
        {
            if(txtIdKelas.Text == string.Empty)
            { 
                mesBox.MesInfo("Pilih Data Terlebih Dahulu!");
                return;
            }
            if (!mesBox.MesKonfirmasi("Hapus Data?")) return;
            kelasDal.Delete(int.Parse(txtIdKelas.Text));
            LoadData();
        }

        private void ClearData()
        {
            txtIdKelas.Clear();
            txtNamaKelas.Clear();
            XRadio.Checked = true;
            XIRadio.Checked = false;
            XIIRadio.Checked = false;
            jurusanCombo.SelectedIndex = 0;
            txtRombel.Text=string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        #region EventSetNamakelas
        private void XRadio_CheckedChanged(object sender, EventArgs e)
        {
            SetNamaKelas();
        }

        private void XIRadio_CheckedChanged(object sender, EventArgs e)
        {
            SetNamaKelas();
        }

        private void XIIRadio_CheckedChanged(object sender, EventArgs e)
        {
            SetNamaKelas();
        }

        private void jurusanCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetNamaKelas();
        }

        private void txtRombel_TextChanged(object sender, EventArgs e)
        {
            SetNamaKelas();
        }
        #endregion

        private void GridListKelas_SelectionChanged(object sender, EventArgs e)
        {
            var id = GridListKelas.CurrentRow.Cells[0].Value.ToString();
            GetData(Convert.ToInt32(id));
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearData();
            CekInsertUpdate();  
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
    }
}
