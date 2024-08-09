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

      

        public void filter(string nama, string kelas, string tahun)
        {
            string sql = CekIsi(nama,kelas,tahun);

            var fltr = db.GetSiswaFilter(sql, new { nama = nama, kelas = kelas, tahun = tahun });
            dataGridView1.DataSource = fltr.ToList();
        }

        public string CekIsi(string nama, string kelas, string tahun)
        {
            List<string> kondisi = new List<string>();
            if (nama != "") kondisi.Add(" Nama LIKE @nama+'%'");
            if (kelas != "") kondisi.Add(" Kelas LIKE @kelas+'%'");
            if (tahun != "") kondisi.Add(" Tahun LIKE @tahun+'%'");

            string sql = "SELECT * FROM siswa";

            if (kondisi.Count > 0)
            {
                sql += " WHERE" + string.Join(" AND ", kondisi);
            }
            return sql;
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            string nama, kelas, tahun;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tahun = txtTahun.Text;

            filter(nama, kelas, tahun);
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            string nama, kelas, tahun;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tahun = txtTahun.Text;

            filter(nama, kelas, tahun);
        }

        private void txtTahun_TextChanged(object sender, EventArgs e)
        {
            string nama, kelas, tahun;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tahun = txtTahun.Text;

            filter(nama, kelas, tahun);
        }
    }
}
