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
    public partial class FormTerlambat : Form
    {
        private DbDal db;
        private  MasukDal terlambatDal;
        public FormTerlambat()
        {
            InitializeComponent();
            db = new DbDal();
            terlambatDal = new MasukDal();

            LoadData();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = terlambatDal.ListData();
        }
        private void btn_terlambat_Click(object sender, EventArgs e)
        {
            PrintTerlambat telat = new PrintTerlambat();
            telat.Show();
        }

        string sqlglobal=string.Empty;
        public string Filter(string nis, string nama, string kelas, DateTime tgl1, DateTime tgl2)
        {
            List<string> fltr = new List<string>();
            string sql = @"SELECT m.id, m.NIS, s.Nama, s.Kelas, m.Tanggal, m.JamMasuk, m.Alasan
                                    FROM Masuk m INNER JOIN siswa s ON m.NIS = s.Nis";

            if (nis != "") fltr.Add("m.NIS LIKE @nis+'%'");
            if (nama != "") fltr.Add("s.Nama LIKE @nama+'%'");
            if (kelas != "") fltr.Add("s.Kelas LIKE @kelas+'%'");
            if (sqlglobal != "") fltr.Add("m.Tanggal BETWEEN @tgl1 AND @tgl2");

            if (fltr.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", fltr);
            }
            sqlglobal = "";
            return sql;

        }

        public void Filter2()
        {
            string nis, nama, kelas;
            DateTime tgl1, tgl2;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;

            tgl1 = tglsatu.Value;
            tgl2 = tgldua.Value;


            string sql = Filter(nis, nama, kelas,  tgl1, tgl2);
            var select = terlambatDal.GetTerlambatFilter(sql, new { nis = nis, nama = nama, kelas = kelas, tgl1 = tgl1, tgl2 = tgl2 });
            dataGridView1.DataSource = select;
        }

        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void tglsatu_ValueChanged(object sender, EventArgs e)
        {
            sqlglobal = "0";
            Filter2();
        }

        private void tgldua_ValueChanged(object sender, EventArgs e)
        {
            sqlglobal = "0";
            Filter2();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }
    }
}
