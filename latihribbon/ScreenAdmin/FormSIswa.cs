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

namespace latihribbon
{
    public partial class FormSIswa : Form
    {
        private readonly DbDal db;
        private readonly SiswaDal siswaDal;
        private readonly JurusanDal jurusanDal;
        private readonly MesBox mesBox;
        bool SaveCondition = true;
        public FormSIswa()
        {
            InitializeComponent();
            db = new DbDal();
            siswaDal = new SiswaDal();
            jurusanDal = new JurusanDal();
            mesBox = new MesBox();
            LoadData();

            InitialEvent();
            InitComponent();
        }

        public void InitComponent()
        {
            // Jurusan Combo
            var jurusan = jurusanDal.ListData();
            if (!jurusan.Any()) return;
            List<string> listJurusan = new List<string>();
            foreach (var item in jurusan)
                listJurusan.Add(item.NamaJurusan);
            jurusanCombo.DataSource = listJurusan;
            txtNIS_FormSiswa.MaxLength = 9;

            // DataGrid
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.EnableHeadersVisualStyles = false;

                dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersHeight = 35;
            }

            // Combo Filter
            var data = db.ListTahun();
            List<string> listTahun = new List<string>();
            listTahun.Add("Semua");
            foreach (var item in data)
            {
                listTahun.Add(item.Tahun);
            }
            comboTahunFilter.DataSource = listTahun;

        }

        public void ControlInsertUpdate()
        {
            if (SaveCondition)
            {
                lblInfo.Text = "INSERT";
                txtNIS_FormSiswa.ReadOnly = false;
            }
            else
            {
                lblInfo.Text = "UPDATE";
                txtNIS_FormSiswa.ReadOnly = true;
                lblNisSudahAda.Visible = false;
            }
        }

        private void InitialEvent()
        {
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var data = dataGridView1.CurrentRow.Cells["Nis"].Value.ToString();

        }

        public void LoadData()
        {
            dataGridView1.DataSource = siswaDal.ListData();
        }


        public void GetData()
        {
            string nis = dataGridView1.CurrentRow.Cells["Nis"].Value?.ToString() ?? string.Empty;
            if (nis == string.Empty) return;
            var getSiswa = siswaDal.GetData(Convert.ToInt32(nis));
            txtNIS_FormSiswa.Text = getSiswa.Nis.ToString();
            txtPersensi_FormSiswa.Text = getSiswa.Persensi.ToString();
            txtNama_FormSiswa.Text = getSiswa.Nama;
            txtTahun_FormSiswa.Text = getSiswa.Tahun;
            if (getSiswa.JenisKelamin == "L")
                lakiRadio.Checked = true;
            else
                perempuanRadio.Checked = true;
            string[] tingkat = getSiswa.Kelas.Split(' ');
            if (tingkat[0] == "X")
                XRadio.Checked = true;
            else if (tingkat[0] == "XI")
                XIRadio.Checked = true;
            else
                XIIRadio.Checked = true;
            string resultJurusan = (tingkat.Length > 2) ? $"{tingkat[1]} {tingkat[2]}" : $"{tingkat[1]}";
            foreach (var item in jurusanCombo.Items)
                if ((string)item == resultJurusan)
                    jurusanCombo.SelectedItem = item;
            SaveCondition = false;
            ControlInsertUpdate();
        }

        public void SaveData()
        {
            string nis, persensi, nama, jenisKelamin = string.Empty, tingkat = string.Empty, jurusan, tahun;
            nis = txtNIS_FormSiswa.Text;
            nama = txtNama_FormSiswa.Text;
            persensi = txtPersensi_FormSiswa.Text;
            if (lakiRadio.Checked) jenisKelamin = "L";
            if (perempuanRadio.Checked) jenisKelamin = "P";

            if (XRadio.Checked) tingkat = "X";
            if (XIRadio.Checked) tingkat = "XI";
            if (XIIRadio.Checked) tingkat = "XII";
            jurusan = jurusanCombo.SelectedItem.ToString() ?? string.Empty;
            tahun = txtTahun_FormSiswa.Text;

            if (nis == "" || persensi == "" || nama == "" || jenisKelamin == "" || tingkat == "" || tahun == "")
            {
                mesBox.MesInfo("Seluruh Data Wajib Diisi!");
                return;
            }
            string namaKelas = $"{tingkat} {jurusan}";
            var siswa = new SiswaModel
            {
                Nis = int.Parse(nis),
                Nama = nama,
                Persensi = int.Parse(persensi),
                JenisKelamin = jenisKelamin,
                Kelas = namaKelas,
                Tahun = tahun,
            };
            if (lblNisSudahAda.Visible == true)
            {
                mesBox.MesInfo("Nis Sudah Ada!!");
                return;
            };

            if (SaveCondition)
            {
                if(mesBox.MesKonfirmasi("Input Data?"))
                {
                    siswaDal.Insert(siswa);
                    LoadData();
                }
            }
            else
            {
                if (mesBox.MesKonfirmasi("Update Data?"))
                {
                    siswaDal.Update(siswa);
                    LoadData();
                }    
            }
        }

        private void Clear()
        {
            SaveCondition = true;
            ControlInsertUpdate();
            txtNIS_FormSiswa.Clear();
            txtNama_FormSiswa.Clear();
            txtPersensi_FormSiswa.Clear();
            lakiRadio.Checked = false;
            perempuanRadio.Checked = false;
            XRadio.Checked = false;
            XIIRadio.Checked = false;
            XIRadio.Checked = false;
            jurusanCombo.SelectedIndex = 0;
            txtTahun_FormSiswa.Clear();
        }
        private void Delete()
        {
            string nis = txtNIS_FormSiswa.Text;
            if (SaveCondition)
            {
                mesBox.MesInfo("Pilih Data Terlebih Dahulu!");
                return;
            }
            if (mesBox.MesKonfirmasi("Hapus Data?"))
            {
                siswaDal.Delete(Convert.ToInt32(nis));
                LoadData();
            }
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

        private void InputNumber(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }


        #region FILTER
        public void filter(string nis, string nama, string kelas, string tahun)
        {
            string sql = CekIsi(nis, nama, kelas, tahun);

            var fltr = siswaDal.GetSiswaFilter(sql, new { nis = nis, nama = nama, kelas = kelas, tahun = tahun });
            dataGridView1.DataSource = fltr;
        }

        public string CekIsi(string nis, string nama, string kelas, string tahun)
        {
            List<string> kondisi = new List<string>();
            if (nama != "") kondisi.Add(" Nama LIKE @nama+'%'");
            if (kelas != "") kondisi.Add(" Kelas LIKE @kelas+'%'");
            if (tahun != "Semua") kondisi.Add(" Tahun LIKE @tahun+'%'");
            if (nis != "") kondisi.Add(" NIS LIKE @nis+'%'");

            string sql = "SELECT * FROM siswa";

            if (kondisi.Count > 0)
            {
                sql += " WHERE" + string.Join(" AND ", kondisi);
            }
            return sql;
        }
        public void fltr()
        {
            string nis, nama, kelas, tahun;
            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tahun = comboTahunFilter.SelectedItem.ToString() ?? string.Empty;

            filter(nis, nama, kelas, tahun);
        }
        #endregion

        #region EVENT FILTER
        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            fltr();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            fltr();
        }

        private void txtTahun_TextChanged(object sender, EventArgs e)
        {
            fltr();
        }
        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            fltr();
        }

        private void comboTahunFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            fltr();
        }
        #endregion

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            GetData();
        }

        private void txtNIS_FormSiswa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void txtNIS_FormSiswa_TextChanged(object sender, EventArgs e)
        {
            string nis = txtNIS_FormSiswa.Text;
            if (nis.Length >= 5)
                CekNis(Convert.ToInt32(nis));
            else
                lblNisSudahAda.Visible = false;
        }

        private void txtNIS_FormSiswa_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            InputNumber(e);
        }

        private void txtPersensi_FormSiswa_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(e);
        }

        private void btnDelete_FormSiswa_Click(object sender, EventArgs e)
        {
            Delete();
            LoadData();
        }

        private void txtTahun_FormSiswa_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(e);
        }

        private void txtNIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            InputNumber(e);
        }
    }
}
