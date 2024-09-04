using Dapper;
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
            ComboInit();
            dataGridView1.DataSource = absensiDal.ListData();
        }

        public void ComboInit()
        {
            List<string> ketCombo = new List<string>() { "Semua","A","I","S"};
            KeteranganCombo.DataSource = ketCombo;
        }
       
        string tglchange = string.Empty;
        private string FilterSQL(string nis, string nama, string kelas, string keterangan)
        {
            List<string> fltr = new List<string>();
            string sql = @"SELECT p.ID,p.NIS,s.Nama,s.Kelas,p.Tanggal,p.Keterangan
                                     FROM Persensi p INNER JOIN siswa s ON p.NIS=s.NIS";
            if (nis != "") fltr.Add("p.NIS LIKE @NIS+'%'");
            if (nama != "") fltr.Add("s.Nama LIKE @Nama+'%'");
            if (kelas != "") fltr.Add("s.Kelas LIKE @Kelas+'%'");
            if (keterangan != "Semua") fltr.Add("p.Keterangan LIKE @keterangan+'%'");
            if (tglchange != "") fltr.Add("p.Tanggal BETWEEN @tgl1 AND @tgl2");

            if (fltr.Count > 0)
                sql += " WHERE " + string.Join(" AND ",fltr);
            tglchange = string.Empty;
            return sql;
        }

        private void Filter()
        {
            string nis, nama, kelas, keterangan;
            DateTime tgl1, tgl2;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            keterangan = KeteranganCombo.SelectedItem.ToString() ?? string.Empty;
            tgl1 = tglsatu.Value.Date;
            tgl2 = tgldua.Value.Date;

            var sql = FilterSQL(nis,nama,kelas,keterangan);
            var dp = new DynamicParameters();
            dp.Add("@NIS",nis);
            dp.Add("@Nama",nama);
            dp.Add("@Keterangan",keterangan);
            dp.Add("@Kelas",kelas);
            dp.Add("@tgl1",tgl1);
            dp.Add("@tgl2",tgl2);

            dataGridView1.DataSource = absensiDal.Filter(sql,dp);

        }
        #region EVENT FILTER
        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void tglsatu_ValueChanged(object sender, EventArgs e)
        {
            tglchange = "change";
            Filter();
        }

        private void tgldua_ValueChanged(object sender, EventArgs e)
        {
            tglchange = "change";
            Filter();
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

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtNIS.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            KeteranganCombo.SelectedIndex = 0;
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            Filter();
        }
    }
}
