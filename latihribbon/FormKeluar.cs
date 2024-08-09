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
   
    public partial class FormKeluar : Form
    {
        private DbDal db;
        public FormKeluar()
        {
            InitializeComponent();
            db = new DbDal();
        }

        public string Filter(string nama, string kelas,string tahun,DateTime tgl1,DateTime tgl2)
        {
            List<string> fltr = new List<string>();
            string sql = "SELECT * FROM Keluar";
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
            string nama, kelas, tahun;
            DateTime tgl1, tgl2;

            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tahun = txtTahun.Text;

            tgl1 = tglsatu.Value;
            tgl2 = tgldua.Value;
            

            string sql = Filter(nama,kelas,tahun,tgl1,tgl2);
            var select = db.GetKeluarFilter(sql, new {nama=nama,kelas=kelas,tahun=tahun,tgl1=tgl1,tgl2=tgl2});

        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtTahun_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
