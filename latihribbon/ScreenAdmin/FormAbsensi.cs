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
    public partial class FormAbsensi : Form
    {
        private readonly AbsensiDal absensiDal;
        public FormAbsensi()
        {
            InitializeComponent();
            absensiDal = new AbsensiDal();

            dataGridView1.DataSource = absensiDal.ListData();
        }
       
        string tglchange = string.Empty;
        private string FilterSQL(string nis, string nama, string kelas, string keterangan)
        {
            List<string> fltr = new List<string>();
            string sql = @"SELECT p.ID,p.NIS,s.Nama,s.Persensi,s.Kelas,p.Tanggal,p.Keterangan
                                     FROM Persensi p INNER JOIN siswa s ON p.NIS=s.NIS";
            if (nis != "") fltr.Add("NIS LIKE @nis+'%'");
            if (nama != "") fltr.Add("Nama LIKE @nama+'%'");
            if (kelas != "") fltr.Add("Kelas LIKE @kelas+'%'");
            if (keterangan != "Semua") fltr.Add("Keterangan LIKE @keterangan+'%'");
            if (tglchange != "") fltr.Add("Tanggal BETWEEN @tgl1 AND @tgl2");

            if (fltr.Count > 0)
                sql += " WHERE " + string.Join(" AND ",fltr);

            return sql;
        }

        private void Filter()
        {
            string nis, nama, kelas, keterangan;
            DateTime tgl1, tgl2;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            keterangan = KeteranganCombo.SelectedItem
                .ToString() ?? string.Empty;
            FilterSQL(nis,nama,kelas,keterangan);
        }
        #region EVENT FILTER
        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void tglsatu_ValueChanged(object sender, EventArgs e)
        {
            tglchange = "change";
        }

        private void tgldua_ValueChanged(object sender, EventArgs e)
        {
            tglchange = "change";
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void KeteranganCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }
        #endregion
    }
}
