using latihribbon.Dal;
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

        public FormSIswa()
        {
            InitializeComponent();
            siswaDal = new SiswaDal();  
            loadSiswa();

            InitialEvent();
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

        #region GET DATA

        public void GetData()
        {
            string nis = dataGridView1.CurrentRow.Cells["Nis"].Value?.ToString()?? string.Empty;
            if (nis == string.Empty) return;
            var getSiswa = siswaDal.GetData(Convert.ToInt32(nis));
            var siswa = new SiswaModel()
            {
                Nis = getSiswa.Nis,
                Nama = getSiswa.Nama,
                Kelas = getSiswa.Kelas,
                
            };
        }

        #endregion

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

    }
}
