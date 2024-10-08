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
    public partial class FormJurusan : Form
    {
        private readonly JurusanDal _jurusanDal;
        private readonly KelasDal kelasDal;
        private readonly MesBox mesBox;
        private string NamaJurusanGlobal;
        public FormJurusan()
        {
            InitializeComponent();
            buf();
            _jurusanDal = new JurusanDal();
            kelasDal = new KelasDal();
            mesBox = new MesBox();
            LoadData();
            InitEvent();
        }

        public void buf()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null,
            GridListJurusan,
            new object[] { true });
        }
        private void LoadData()
        {
            GridListJurusan.DataSource = _jurusanDal.ListData().Select(x => new 
            {
                IdJurusan = x.Id,
                NamaJurusan = x.NamaJurusan
            }).ToList();

            GridListJurusan.Columns[0].Width = 100;
            GridListJurusan.Columns[1].Width = 200;

                GridListJurusan.EnableHeadersVisualStyles = false;
                GridListJurusan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                GridListJurusan.DefaultCellStyle.Font = new Font("Sans Serif", 10);
                GridListJurusan.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
                GridListJurusan.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                GridListJurusan.RowTemplate.Height = 30;
                GridListJurusan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                GridListJurusan.ColumnHeadersHeight = 35;
        }

        private void InitEvent()
        {
            btnNewJurusan.Click += BtnNewJurusan_Click;
            btnSaveJurusan.Click += BtnSaveJurusan_Click;
            btnDeleteJurusan.Click += BtnDeleteJurusan_Click;
            GridListJurusan.SelectionChanged += GridListJurusan_SelectionChange;
        }
        string jurusanNameGlobal;
        private void GridListJurusan_SelectionChange(object sender, EventArgs e)
        {
            LabelJurusan.Text = "UPDATE";

            var jurusanId = GridListJurusan.CurrentRow.Cells[0].Value.ToString();
            NamaJurusanGlobal = GridListJurusan.CurrentRow.Cells[1].Value.ToString();

            txtIdJurusan.Text = jurusanId;
            txtNamaJurusan.Text = NamaJurusanGlobal;
        }

        private void BtnDeleteJurusan_Click(object sender, EventArgs e)
        {
            var jurusanId = Convert.ToInt32(GridListJurusan.CurrentRow.Cells[0].Value);
            var jurusanName = GridListJurusan.CurrentRow.Cells[1].Value;

            if (MessageBox.Show($"Anda yakin ingin menghapus data \" {jurusanName} \" ? \n Jika Dihapus, maka data yang terhubung akan ikut Terhapus", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                _jurusanDal.Delete(jurusanId);

            LoadData();
        }

        private void BtnSaveJurusan_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void BtnNewJurusan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Masukan data baru ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Cleardata();
            }
        }

        private void SaveData()
        {
            var namaJurusan = txtNamaJurusan.Text.Trim();
            if(namaJurusan == string.Empty)
            {
                mesBox.MesInfo("Nama Jurusan Wajib Diisi!");
                return;
            }

            if (txtIdJurusan.Text == string.Empty)
            {
                if (!mesBox.MesKonfirmasi("Input Data?")) return;
                _jurusanDal.Insert(namaJurusan);
                LoadData();
                Cleardata();
            }
            else
            {
                if (MessageBox.Show($"Update Data? \n Kelas dengan Jurusan {NamaJurusanGlobal} dan semua yang berhubungan, akan berubah menjadi {namaJurusan} ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var jurusan = new JurusanModel()
                    {
                        Id = Convert.ToInt32(txtIdJurusan.Text),
                        NamaJurusan = txtNamaJurusan.Text
                    };
                    _jurusanDal.Update(jurusan);
                    var dataKelas = kelasDal.listKelas("WHERE k.idJurusan=@idJurusan", new { idJurusan = jurusan.Id});
                    foreach (var x in dataKelas)
                    {
                        string namaKelas = $"{x.Tingkat} {x.NamaJurusan} {x.Rombel}".Trim();
                        kelasDal.UpdateNamaKelas(x.Id,namaKelas);
                    }
                    LoadData();
                }
            }
        }

        private void Cleardata()
        {
            txtIdJurusan.Text = string.Empty;
            txtNamaJurusan.Text= string.Empty;
            LabelJurusan.Text = "INSERT";
        }

        private void txtIdJurusan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}





