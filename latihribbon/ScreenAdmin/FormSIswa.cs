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

namespace latihribbon
{
    public partial class FormSIswa : Form
    {
        private readonly SiswaDal siswaDal;
        private readonly JurusanDal jurusanDal;
        public FormSIswa()
        {
            InitializeComponent();
            siswaDal = new SiswaDal(); 
            jurusanDal = new JurusanDal();
            loadSiswa();

            InitialEvent();
            InitJurusan();
        }

        public void InitJurusan()
        {
            var jurusan = jurusanDal.ListData();
            List<JurusanItem> daftarJurusan = new List<JurusanItem>();
            foreach (var item in jurusan)
            {
                daftarJurusan.Add(new JurusanItem { Id = item.Id, NamaJurusan = item.NamaJurusan });
            }

            jurusanCombo.DataSource = daftarJurusan;
            jurusanCombo.DisplayMember = "NamaJurusan";
            jurusanCombo.ValueMember = "Id";
        }

        public class JurusanItem
        {
            public int Id { get; set; }
            public string NamaJurusan { get; set; }
        }
        private void InitialEvent()
        {
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var data = dataGridView1.CurrentRow.Cells["Nis"].Value.ToString();

        }

        public void loadSiswa()
        {
            dataGridView1.DataSource = siswaDal.ListData();
        }

        public void GetData()
        {
            string nis = dataGridView1.CurrentRow.Cells["Nis"].Value?.ToString()?? string.Empty;
            if (nis == string.Empty) return;
            var getSiswa = siswaDal.GetData(Convert.ToInt32(nis));
            txtNIS_FormSiswa.Text = getSiswa.Nis.ToString();
            txtNama_FormSiswa.Text =getSiswa.Nama;
            txtPersensi_FormSiswa.Text = getSiswa.Persensi.ToString();

            txtTahun_FormSiswa.Text = getSiswa.Tahun;
        }

        public void SaveData()
        {
            string nis, nama, tingkat, jurusan;
        }

        private void Clear()
        {
            txtNIS_FormSiswa.Clear();
            txtNama_FormSiswa.Clear();
            txtPersensi_FormSiswa.Clear();
            XRadio.Checked = false;
            XIIRadio.Checked = false;
            XIRadio.Checked = false;
            jurusanCombo.SelectedIndex = 0;
            txtTahun_FormSiswa.Clear();
        }


        #region FILTER
        public void filter(string nis,string nama, string kelas, string tahun)
        {
            string sql = CekIsi(nis,nama,kelas,tahun);

            var fltr = siswaDal.GetSiswaFilter(sql, new { nis=nis,nama = nama, kelas = kelas, tahun = tahun });
            dataGridView1.DataSource = fltr;
        }

        public string CekIsi(string nis,string nama, string kelas, string tahun)
        {
            List<string> kondisi = new List<string>();
            if (nama != "") kondisi.Add(" Nama LIKE @nama+'%'");
            if (kelas != "") kondisi.Add(" Kelas LIKE @kelas+'%'");
            if (tahun != "") kondisi.Add(" Tahun LIKE @tahun+'%'");
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
            tahun = txtTahun.Text;

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
    }
}
