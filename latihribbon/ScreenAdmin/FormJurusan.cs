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
    public partial class FormJurusan : Form
    {
        private readonly JurusanDal _jurusanDal;
        public FormJurusan()
        {
            InitializeComponent();
            buf();
            _jurusanDal = new JurusanDal();
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
            GridListJurusan.DataSource = _jurusanDal.ListData();

            GridListJurusan.Columns["Id"].HeaderText = "Id Jurusan";
            GridListJurusan.Columns["NamaJurusan"].HeaderText = "Nama Jurusan";

            GridListJurusan.Columns["Id"].Width = 100;
            GridListJurusan.Columns["NamaJurusan"].Width = 200;

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
            GridListJurusan.DoubleClick += GridListJurusan_DoubleClick;
        }
        string jurusanNameGlobal
        private void GridListJurusan_DoubleClick(object sender, EventArgs e)
        {
            LabelJurusan.Text = "UPDATE";

            var jurusanId = GridListJurusan.CurrentRow.Cells[0].Value.ToString();
            var jurusanName = GridListJurusan.CurrentRow.Cells[1].Value.ToString();

            txtIdJurusan.Text = jurusanId;
            txtNamaJurusan.Text = jurusanName;
        }

        private void BtnDeleteJurusan_Click(object sender, EventArgs e)
        {
            var jurusanId = Convert.ToInt32(GridListJurusan.CurrentRow.Cells[0].Value);
            var jurusanName = GridListJurusan.CurrentRow.Cells[1].Value;

            if (MessageBox.Show($"Anda yakin ingin menghapus data \" {jurusanName} \" ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                _jurusanDal.Delete(jurusanId);

            LoadData();
        }

        private void BtnSaveJurusan_Click(object sender, EventArgs e)
        {
            if (txtIdJurusan.Text == string.Empty || txtNamaJurusan.Text == string.Empty)
            {
                MessageBox.Show("Pilih data terlebih dahulu !");
                return;
            }

            SaveData();
            LoadData();
            LabelJurusan.Text = "UPDATE";

        }

        private void BtnNewJurusan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Masukan data baru ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Cleardata();
                LabelJurusan.Text = "INSERT";
            }
        }

        private void SaveData()
        {
            var namaJurusan = txtNamaJurusan.Text;

            if (txtIdJurusan.Text == string.Empty)
            {
                if (MessageBox.Show($"Simpan Data \" {namaJurusan} \" ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    var jurusanInsert = new JurusanModel()
                    {
                        NamaJurusan = txtNamaJurusan.Text
                    };
                    _jurusanDal.Insert(jurusanInsert);
                }
            }
            else
            {
                if (MessageBox.Show($"Update Data? \n Kelas dengan Jurusan {namaJurusan} akan berubah menjadi {namaJurusan} ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var jurusanUpdate = new JurusanModel()
                    {
                        Id = Convert.ToInt32(txtIdJurusan.Text),
                        NamaJurusan = txtNamaJurusan.Text
                    };
                    _jurusanDal.Update(jurusanUpdate);
                }
            }
        }

        private void Cleardata()
        {
            txtIdJurusan.Text = string.Empty;
            txtNamaJurusan.Text= string.Empty;
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





