using Dapper;
using latihribbon.Dal;
using latihribbon.Model;
using latihribbon.UpdateInsert;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace latihribbon.ScreenAdmin
{
    public partial class FormRekapPersensi : Form
    {
        private readonly RekapPersensiDal rekapPersensiDal;
        private readonly HistoryDal historyDal;


        public FormRekapPersensi()
        {
            InitializeComponent();
            buf();
            rekapPersensiDal = new RekapPersensiDal();
            historyDal = new HistoryDal();
            InitComponen();
            Event();
            LoadHistory();
        }

        public void buf()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.SetProperty,
            null,
            dataGridView1,
            new object[] { true });
        }

        public void InitComponen()
        {
            List<string> Keterangan = new List<string>() { "Semua", "A", "I", "S" };
            KeteranganCombo.DataSource = Keterangan;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.RowTemplate.Height = 30;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
           /* dataGridView1.Columns["Nis"].Width = 100;
            dataGridView1.Columns["Persensi"].Width = 100;
            dataGridView1.Columns["Nama"].Width = 250;
            dataGridView1.Columns["Kelas"].Width = 100;
            dataGridView1.Columns["Tanggal"].Width = 100;
            dataGridView1.Columns["Keterangan"].Width = 120;
            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.RowHeadersWidth = 51;*/
        }

        public void LoadHistory()
        {
            var gethistory = historyDal.GetData("RekapPersensi");
            txtKelas.Text = gethistory.History;
            LoadData();
        }


        bool tglchange = false;
        string sqlc2 = string.Empty;
        private string FilterSQL(bool data,string nis, string nama, string persensi,string keterangan)
        {
            string sqlc = string.Empty;
            
            List<string> fltr = new List<string>();
            List<string> fltr2 = new List<string>();
            
            if (data)
            {
                fltr.Add("sd.NamaKelas LIKE @Kelas+'%'");
                if (nis != "") fltr.Add("sd.NIS LIKE @NIS+'%'");
                if (nama != "") fltr.Add("sd.Nama LIKE '%'+@Nama+'%'");
                if (persensi != "") fltr.Add("sd.Persensi LIKE '%'+@Persensi+'%'");
                if (keterangan != "Semua") fltr.Add("COALESCE(a.Keterangan, '*') LIKE @Keterangan+'%'");
                if (tglchange) fltr.Add("sd.Tanggal BETWEEN @tgl1 AND @tgl2");
            }
            else
            {
                fltr.Add("k.NamaKelas LIKE @Kelas+'%'");
                if (nis != "") fltr.Add("NIS LIKE @NIS+'%'");
                if (nama != "") fltr.Add("Nama LIKE '%'+@Nama+'%'");
                if (persensi != "") fltr.Add("Persensi LIKE @Persensi+'%'");
                if (keterangan != "Semua") fltr2.Add("Keterangan LIKE @Keterangan+'%'");
                if (tglchange) fltr2.Add("Tanggal BETWEEN @tgl1 AND @tgl2");
            }

            if (fltr.Count > 0)
                sqlc += " WHERE "+string.Join(" AND ", fltr);
            if (fltr2.Count > 0)
                sqlc2 += " WHERE "+string.Join(" AND ", fltr2);
            return sqlc;
        }

        int Page = 1;
        int totalPage;
        private void LoadData()
        {
            string nis = txtNIS.Text;
            string nama = txtNama.Text;
            string persensi = txtPersensi.Text;
            string kelas = txtKelas.Text;
            string keterangan = KeteranganCombo.SelectedItem.ToString() ?? string.Empty;
            DateTime tgl1 = tglsatu.Value.Date;
            DateTime tgl2 = tgldua.Value.Date;

            var sqlc = FilterSQL(true,nis, nama,persensi, keterangan);
            var sqlcRow = FilterSQL(false,nis, nama,persensi, keterangan);

            var dp = new DynamicParameters();
            dp.Add("@Kelas", kelas);
            if (nis != "") dp.Add("@NIS", nis);
            if (nama != "") dp.Add("@Nama", nama);
            if (persensi != "") dp.Add("@Persensi", persensi);
            if (nis != "Semua") dp.Add("@Keterangan", keterangan);
            if (tglchange)
            {
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
            }

            string text = "Halaman ";
            int RowPerPage = 20;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = rekapPersensiDal.CekRows(sqlcRow,sqlc2,dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            dp.Add("Offset",inRowPage);
            dp.Add("Fetch",RowPerPage);
            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dataGridView1.DataSource = rekapPersensiDal.ListData2(sqlc,dp);

            sqlc2 = string.Empty;
        }

        #region     EVENT
        private void Event()
        {
            btnPrintRekap.Click += BtnPrintRekap_Click;

            txtNIS.TextChanged += txt_TextChanged;
            txtNama.TextChanged += txt_TextChanged;
            txtPersensi.TextChanged += txt_TextChanged;
            KeteranganCombo.SelectedIndexChanged += txt_TextChanged;
            tglsatu.ValueChanged += tgl_ValueChanged;
            tgldua.ValueChanged += tgl_ValueChanged;

            btnResetFilter.Click += btnReset_Click;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void tgl_ValueChanged(object sender,EventArgs e)
        {
            Page = 1;
            tglchange = true;
            LoadData();
        }

        private void BtnPrintRekap_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Eksport Data ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FormSetPrint formPrint = new FormSetPrint();

                formPrint.ShowDialog();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtNIS.Clear();
            txtNama.Clear();
            txtPersensi.Clear();
            KeteranganCombo.SelectedIndex = 0;
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            tglchange = false;
            LoadData();
        }
        #endregion


      

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Page < totalPage)
            {
                Page++;
                LoadData();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (Page > 1)
            {
                Page--;
                LoadData();
            }

        }

        private void btnKelas_Click(object sender, EventArgs e)
        {
            PopUpKelas kelas = new PopUpKelas("RekapPersensi");
            kelas.ShowDialog();
            if (kelas.DialogResult == DialogResult.OK) 
            {
                LoadHistory();
                InitComponen();
            }
        }
    }
}