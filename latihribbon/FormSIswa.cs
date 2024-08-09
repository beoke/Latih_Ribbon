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
            string sql;
            sql = @"SELECT * FROM siswa";
            if(nama != "" && kelas == "" && tahun == "")
            {
                sql = @"SELECT * FROM siswa WHERE Nama LIKE @nama+'%'";
            } else if (nama == "" && kelas != "" && tahun == "")
            {
                sql = @"SELECT * FROM siswa WHERE Kelas LIKE @kelas+'%'";
            }else if (nama == "" && kelas == "" && tahun != "")
            {
                sql = @"SELECT * FROM siswa WHERE Tahun LIKE @tahun+'%'";
            } else if (nama != "" && kelas != "" && tahun == "")
            {
                sql = @"SELECT * FROM siswa WHERE Nama LIKE @nama+'%' AND Kelas LIKE @kelas+'%'";
            } else if (nama== "" && kelas != "" && tahun != "")
            {
                sql = @"SELECT * FROM siswa WHERE Kelas LIKE @kelas+'%' AND Tahun LIKE @tahun+'%'";
            } else if (nama != "" && kelas == "" && tahun != "")
            {
                sql = @"SELECT * FROM siswa WHERE Nama LIKE @nama+'%' AND Tahun LIKE @tahun+'%'";
            }
            else
            {
                sql = @"SELECT * FROM siswa WHERE Nama LIKE @nama+'%' AND Kelas LIKE @kelas+'%' AND Tahun LIKE @tahun+'%'";
            }
            
            //sql = @"SELECT * FROM siswa WHERE Nama LIKE @nama+'%' OR Kelas LIKE @kelas+'%' OR Tahun LIKE @tahun+'%'";
            //string sql = @"SELECT * FROM siswa WHERE Nama LIKE @nama+'%'";
            var fltr = db.GetSiswaFilter(sql, new {nama=nama,kelas=kelas,tahun=tahun});
            dataGridView1.DataSource= fltr.ToList();


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
