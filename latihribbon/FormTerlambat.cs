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
        public FormTerlambat()
        {
            InitializeComponent();
            db = new DbDal();
        }

        private void btn_terlambat_Click(object sender, EventArgs e)
        {
            PrintTerlambat telat = new PrintTerlambat();
            telat.Show();
        }

        public string Filter(string nis, string nama, string kelas, string tahun, DateTime tgl1, DateTime tgl2)
        {
            List<string> fltr = new List<string>();
            string sql = "SELECT * FROM Keluar";
            if (nis != "") fltr.Add("NIS LIKE @nama+'%'");
            if (nama != "") fltr.Add("Nama LIKE @nama+'%'");
            if (kelas != "") fltr.Add("Kelas LIKE @kelas+'%'");
            if (tahun != "") fltr.Add("Tahun LIKE @tahun+'%'");
            if (tgl1 != null && tgl2 != null) fltr.Add("Tanggal BETWEEN @tgl1 AND @tgl2");

            if (fltr.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", fltr);
            }
            return sql;
        }

        public void Filter2()
        {
            string nis, nama, kelas, tahun;
            DateTime tgl1, tgl2;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tahun = txtTahun.Text;

            tgl1 = tglsatu.Value;
            tgl2 = tgldua.Value;


            string sql = Filter(nis, nama, kelas, tahun, tgl1, tgl2);
            var select = db.GetKeluarFilter(sql, new { nis = nis, nama = nama, kelas = kelas, tahun = tahun, tgl1 = tgl1, tgl2 = tgl2 });
        }
    }
}
