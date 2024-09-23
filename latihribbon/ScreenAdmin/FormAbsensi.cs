using Dapper;
using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
using latihribbon.ScreenAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace latihribbon
{
    public partial class FormAbsensi : Form
    {
        private readonly AbsensiDal absensiDal;
        private readonly SiswaDal siswaDal;
        private MesBox mesBox = new MesBox();
        private int globalId = 0;
        public FormAbsensi()
        {
            absensiDal = new AbsensiDal();
            siswaDal = new SiswaDal();
            InitializeComponent();
            buf();
            InitComponent();
            LoadData();
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
  

        public void InitComponent()
        {
            List<string> ketCombo = new List<string>() { "Semua","A","I","S"};
            KeteranganCombo.DataSource = ketCombo;
    
            // DataGrid
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersHeight = 35;
            }

            //Max Length
            txtNIS1.MaxLength = 9;

        }

        int Page = 1;
        int totalPage;
        public void LoadData()
        {
            string nis, nama, kelas, keterangan;
            DateTime tgl1, tgl2;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            keterangan = KeteranganCombo.SelectedItem.ToString() ?? string.Empty;
            tgl1 = tglsatu.Value.Date;
            tgl2 = tgldua.Value.Date;

            var sqlc = FilterSQL("data", nis, nama, kelas, keterangan);
            var sqlcRow = FilterSQL("JumlahBaris", nis, nama, kelas, keterangan);
            var dp = new DynamicParameters();
            dp.Add("@NIS", nis);
            dp.Add("@Nama", nama);
            dp.Add("@Keterangan", keterangan);
            dp.Add("@Kelas", kelas);
            dp.Add("@tgl1", tgl1);
            dp.Add("@tgl2", tgl2);

            string text = "Halaman ";
            int RowPerPage = 15;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = absensiDal.CekRows(sqlcRow, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = absensiDal.ListData(sqlc, dp);


        }

        string tglchange = string.Empty;
        private string FilterSQL(string digunakanUntuk,string nis, string nama, string kelas, string keterangan)
        {
            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            if (digunakanUntuk == "data")
            {
                if (nis != "") fltr.Add("p.NIS LIKE @NIS+'%'");
                if (nama != "") fltr.Add("s.Nama LIKE '%'+@Nama+'%'");
                if (kelas != "") fltr.Add("s.Kelas LIKE '%'+@Kelas+'%'");
                if (keterangan != "Semua") fltr.Add("p.Keterangan LIKE @Keterangan+'%'");
                if (tglchange != "") fltr.Add("p.Tanggal BETWEEN @tgl1 AND @tgl2");
            }
            else
            {
                if (nis != "") fltr.Add("p.NIS LIKE @NIS+'%'");
                if (nama != "") fltr.Add("Nama LIKE '%'+@Nama+'%'");
                if (kelas != "") fltr.Add("Kelas LIKE '%'+@Kelas+'%'");
                if (keterangan != "Semua") fltr.Add("Keterangan LIKE @Keterangan+'%'");
                if (tglchange != "") fltr.Add("Tanggal BETWEEN @tgl1 AND @tgl2");
            }
       
            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ",fltr);
            tglchange = string.Empty;
            return sqlc;
        }

       

        private void GetData()
        {
            var Id = dataGridView1.CurrentRow.Cells["Id"].Value?.ToString() ?? string.Empty;
            globalId = Id == string.Empty ? 0 : int.Parse(Id); // set global varibel
            var data = absensiDal.GetData(Convert.ToInt32(Id));
            if (data == null) return;
            txtNIS1.Text = data.Nis.ToString();
            txtNama1.Text = data.Nama;
            txtKelas1.Text = data.Kelas;
            tglDT.Value = data.Tanggal;
            if(data.Keterangan == "I") Izinradio.Checked = true;
            if(data.Keterangan == "S") sakitRadio.Checked = true;
            if(data.Keterangan == "A") alphaRadio.Checked = true;
        }

        private void ClearInput()
        {
            txtNIS1.Clear();
            txtNama1.Clear();
            txtKelas1.Clear();
            tglDT.Value = new DateTime(2000, 01, 01);
            Izinradio.Checked = false;
            sakitRadio.Checked = false;
            alphaRadio.Checked = false;
        }

        private void SaveData()
        {
            string nis, nama, kelas, keterangan=string.Empty;
            DateTime tgl;

            nis = txtNIS1.Text;
            nama = txtNama1.Text;
            kelas = txtKelas1.Text;
            tgl = tglDT.Value;
            if (Izinradio.Checked) keterangan = "I";
            if (sakitRadio.Checked) keterangan = "S";
            if (alphaRadio.Checked) keterangan = "A";

            if (nis == "" || nama == "" || keterangan == "")
            {
                MessageBox.Show("Seluruh Data Wajib Diisi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var masuk = new AbsensiModel
            {
                Id = globalId,
                Nis = Convert.ToInt32(nis),
                Tanggal = tgl,
                Keterangan = keterangan
            };

            if (masuk.Id == 0)
            {
                if (MessageBox.Show("Input Data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    absensiDal.Insert(masuk);
                    LoadData();
                }

            }
            else
            {
                if (MessageBox.Show("Update Data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    absensiDal.Update(masuk);
                    LoadData();
                }
            }
        }

        private void Delete()
        {
            if(globalId == 0)
            {
                mesBox.MesInfo("Pilih Data Terlebih Dahulu!");
                return;
            }
            if (mesBox.MesKonfirmasi("Hapus Data?"))
            {
                absensiDal.Delete(globalId);
                LoadData();
                ClearInput();
                globalId = 0;
            }
        }

        private void CekNis()
        {
            var siswa = siswaDal.GetData(Convert.ToInt32(txtNIS1.Text));
            if (siswa == null)
            {
                lblNisTidakDitemukan.Visible = true;
                txtNama1.Text = string.Empty;
                txtKelas1.Text = string.Empty;
            }
            else
            {
                lblNisTidakDitemukan.Visible = false;
                txtNama1.Text = siswa.Nama;
                txtKelas1.Text = siswa.Kelas;
            }
        }

        private void ControlInsertUpdate()
        {
            if(globalId == 0)
            {
                lblInfo.Text = "INSERT";
                txtNIS1.ReadOnly = false;
            }
            else
            {
                lblInfo.Text = "UPDATE";
                txtNIS1.ReadOnly = true;
            }
        }

    
        #region EVENT FILTER
        private void txtNIS_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tglsatu_ValueChanged(object sender, EventArgs e)
        {
            tglchange = "change";
            LoadData();
        }

        private void tgldua_ValueChanged(object sender, EventArgs e)
        {
            tglchange = "change";
            LoadData();
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void KeteranganCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
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
            LoadData();
        }

        private void txtNIS1_TextChanged(object sender, EventArgs e)
        {
            if (txtNIS1.Text.Length >= 5)
            {
                CekNis();
            }
            else
            {
                txtNama1.Text = string.Empty;
                txtKelas1.Text = string.Empty;
                lblNisTidakDitemukan.Visible = false;
            }
        }

        private void txtNIS1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            GetData();
            ControlInsertUpdate();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearInput();
            globalId = 0;
            ControlInsertUpdate();
        }

        private void btnDelete_FormSiswa_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

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
    }
}
