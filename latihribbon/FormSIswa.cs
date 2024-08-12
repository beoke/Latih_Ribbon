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
        private DbDal db;
        public FormSIswa()
        {
            InitializeComponent();
            db = new DbDal();
            loadSiswa();
        }

        public void loadSiswa()
        {
            dataGridView1.DataSource = db.GetSiswa();
        }

        private void FormSIswa_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

      

        public void filter(string nis,string nama, string kelas, string tahun)
        {
            string sql = CekIsi(nis,nama,kelas,tahun);

            var fltr = db.GetSiswaFilter(sql, new { nis=nis,nama = nama, kelas = kelas, tahun = tahun });
            dataGridView1.DataSource = fltr.ToList();
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

        public void fltr()
        {
            string nis, nama, kelas, tahun;
            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tahun = txtTahun.Text;

            filter(nis, nama, kelas, tahun);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            fltr();
        }
    }
}
