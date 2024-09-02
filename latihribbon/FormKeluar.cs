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
    public partial class FormKeluar : Form
    {
        private readonly KeluarDal keluarDal;
        public FormKeluar()
        {
            InitializeComponent();
            keluarDal = new KeluarDal();
            LoadData();
        }

        public void LoadData()
        {
            dataGridView1.DataSource = keluarDal.ListData();
        }

        string sqlglobal;
        public string Filter(string nis,string nama, string kelas,DateTime tgl1,DateTime tgl2)
        {
            List<string> fltr = new List<string>();
            string sql = @"SELECT k.Id,k.Nis,s.Nama,s.Kelas,k.Tanggal,k.JamKeluar,k.JamMasuk,k.Tujuan 
                            FROM Keluar k INNER JOIN siswa s ON k.Nis=s.Nis";
            if (nis != "") fltr.Add("k.Nis LIKE @nis+'%'");
            if (nama != "") fltr.Add("s.Nama LIKE @nama+'%'");
            if (kelas != "") fltr.Add("s.Kelas LIKE @kelas+'%'");
            if (sqlglobal != "") fltr.Add("Tanggal BETWEEN @tgl1 AND @tgl2");

            if (fltr.Count > 0)
            {
                sql += " WHERE " + string.Join(" AND ", fltr);
            }
            sqlglobal = "";
            return sql;
        }

        public void FilterTGL()
        {
            sqlglobal = "0";
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

            string sql = Filter(nis,nama,kelas,tgl1,tgl2);
            var select = keluarDal.GetKeluarFilter(sql, new {nis=nis, nama=nama,kelas=kelas,tgl1=tgl1,tgl2=tgl2});
            dataGridView1.DataSource = select;
        }

        #region EVENT FILTER
        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void txtTahun_TextChanged(object sender, EventArgs e)
        {
            Filter2();
        }

        private void tglsatu_ValueChanged(object sender, EventArgs e)
        {
            FilterTGL();
        }

        private void tgldua_ValueChanged(object sender, EventArgs e)
        {
            FilterTGL();
        }
        #endregion
    }
}
