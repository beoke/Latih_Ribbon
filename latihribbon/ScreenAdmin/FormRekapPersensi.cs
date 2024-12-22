using Dapper;
using latihribbon.Dal;
using latihribbon.Model;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace latihribbon.ScreenAdmin
{
    public partial class FormRekapPersensi : Form
    {
        private readonly RekapPersensiDal rekapPersensiDal;
        private readonly HistoryDal historyDal;
        private System.Threading.Timer timer;
        private ToolTip toolTip = new ToolTip();

        public FormRekapPersensi()
        {
            InitializeComponent();
            buf();
            rekapPersensiDal = new RekapPersensiDal();
            historyDal = new HistoryDal();
            InitCombo();
            LoadHistory();
            Event();
            InitGrid();

            this.Load += FormRekapPersensi_Load;
        }

        private void FormRekapPersensi_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
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

        private void InitCombo()
        {
            comboPerPage.Items.Add(10);
            comboPerPage.Items.Add(20);
            comboPerPage.Items.Add(50);
            comboPerPage.Items.Add(100);
            comboPerPage.Items.Add(200);
            comboPerPage.SelectedIndex = 0;
            comboPerPage.DropDownStyle = ComboBoxStyle.DropDownList;

            List<string> Keterangan = new List<string>() { "Semua", "A", "I", "S" };
            KeteranganCombo.DataSource = Keterangan;
            KeteranganCombo.DropDownStyle = ComboBoxStyle.DropDownList;

            toolTip.SetToolTip(btnPrintRekap,"Export To Excel");
            toolTip.SetToolTip(btnResetFilter, "Reset Filter");
        }
        public void InitGrid()
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.RowTemplate.Height = 30;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Columns["NamaKelas"].HeaderText = "Nama Kelas";

            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 350;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 100;
        }

        public void LoadHistory()
        {
            var gethistory = historyDal.GetData("RekapPersensi");
            txtKelas.Text = gethistory?.History ?? string.Empty;
            LoadData();
        }


        bool tglchange = false;
        string sqlc2 = string.Empty;
        private string FilterSQL(bool data,string search,string keterangan)
        {
            string sqlc = string.Empty;
            
            List<string> fltr = new List<string>();
            List<string> fltr2 = new List<string>();
            
            if (data)
            {
                fltr.Add("sd.NamaKelas LIKE @Kelas||'%'");
                if (search != "") fltr.Add("(sd.NIS LIKE @Search||'%' OR sd.Nama LIKE '%'||@Search||'%' OR sd.Persensi LIKE @Search||'%')");
                if (keterangan != "Semua") fltr.Add("COALESCE(a.Keterangan, '*') LIKE @Keterangan||'%'");
                if (tglchange) fltr.Add("sd.Tanggal BETWEEN @tgl1 AND @tgl2");
            }
            else
            {
                fltr.Add("k.NamaKelas LIKE @Kelas||'%'");
                if (search != "") fltr.Add("(NIS LIKE @Search||'%' OR Nama LIKE '%'||@Search||'%' OR Persensi LIKE @Search||'%')");
                if (keterangan != "Semua") fltr2.Add("Keterangan LIKE @Keterangan||'%'");
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
            string search = txtSearch.Text;
            string kelas = txtKelas.Text;
            string keterangan = KeteranganCombo.SelectedItem.ToString() ?? string.Empty;
            DateTime tgl1 = tglsatu.Value.Date;
            DateTime tgl2 = tgldua.Value.Date;

            var sqlc = FilterSQL(true,search, keterangan);
            var sqlcRow = FilterSQL(false,search, keterangan);

            var dp = new DynamicParameters();
            dp.Add("@Kelas", kelas);
            if (search != "") dp.Add("@Search", search);
            if (keterangan != "Semua") dp.Add("@Keterangan", keterangan);
            if (tglchange)
            {
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
            }

            string text = "Halaman ";
            int RowPerPage = (int)comboPerPage.SelectedItem;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = rekapPersensiDal.CekRows(sqlcRow,sqlc2,dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            dp.Add("Offset",inRowPage);
            dp.Add("Fetch",RowPerPage);
            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dataGridView1.DataSource = rekapPersensiDal.ListData2(sqlc,dp)
                .Select((x, index) => new
                {
                    No = inRowPage + index + 1,
                    x.Nis,
                    x.Persensi,
                    x.Nama,
                    x.NamaKelas,
                    x.Tanggal,
                    x.Keterangan
                }).ToList();

            sqlc2 = string.Empty;
        }

        #region     EVENT
        private void Event()
        {
            btnPrintRekap.Click += BtnPrintRekap_Click;

            txtSearch.TextChanged += txt_TextChanged;
            KeteranganCombo.SelectedIndexChanged += txt_TextChanged;
            tglsatu.ValueChanged += tgl_ValueChanged;
            tgldua.ValueChanged += tgl_ValueChanged;

            txtSearch.TextChanged += Filter_ChangeLeave;
            txtSearch.Leave += Filter_ChangeLeave;
            lblFilter.Click += LblFilter_Click;

            btnResetFilter.Click += btnReset_Click;

            comboPerPage.SelectedIndexChanged += txt_TextChanged;
        }

        private void LblFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void Filter_ChangeLeave(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
                lblFilter.Visible = false;
            else
                lblFilter.Visible = true;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            timer?.Dispose();
            timer = new System.Threading.Timer(x =>
            {
                this.Invoke(new Action(LoadData));
            }, null, 300, Timeout.Infinite);
        }

        private void tgl_ValueChanged(object sender,EventArgs e)
        {
            Page = 1;
            tglchange = true;
            LoadData();
        }

        private void BtnPrintRekap_Click(object sender, EventArgs e)
        {
            FormSetPrint formPrint = new FormSetPrint();
            formPrint.ShowDialog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
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
            PopUpKelas kelas = new PopUpKelas("RekapPersensi",txtKelas.Text);
            kelas.ShowDialog(this);
            if (kelas.DialogResult == DialogResult.OK) 
            {
                LoadHistory();
                InitGrid();
            }
        }
    }
}