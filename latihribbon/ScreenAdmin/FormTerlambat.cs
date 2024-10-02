using Dapper;
using latihribbon.Dal;
using latihribbon.Helper;
using latihribbon.Model;
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
    public partial class FormTerlambat : Form
    {
        private readonly MasukDal masukDal;
        private readonly SiswaDal siswaDal;
        private readonly KelasDal kelasDal;
        
        private readonly MesBox mesBox;
        int globalId = 0;
        public FormTerlambat()
        {
            InitializeComponent();
            buf();
            masukDal = new MasukDal();
            siswaDal = new SiswaDal();
            kelasDal = new KelasDal();
            mesBox = new MesBox();
            RegisterEvent();
            LoadData();
            InitComponen();
            
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
            //textBox MaxLength
            txtNIS1.MaxLength = 10;
            txtAlasan1.MaxLength = 60;
            
            // DataGrid
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                dataGridView1.DefaultCellStyle.Font = new Font("Sans Serif", 10);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Sans Serif", 10, FontStyle.Bold);
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
                dataGridView1.RowTemplate.Height = 30;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.ColumnHeadersHeight = 35;
        }

        private void btn_terlambat_Click(object sender, EventArgs e)
        {
            PrintTerlambat telat = new PrintTerlambat();
            telat.Show();
        }


        bool tglchange = false;
        private string FilterSQL(string nis, string nama, string kelas)
        {
            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            if (nis != "") fltr.Add("m.NIS LIKE @NIS+'%'");
            if (nama != "") fltr.Add("s.Nama LIKE '%'+@Nama+'%'");
            if (kelas != "") fltr.Add("s.Kelas LIKE '%'+@Kelas+'%'");
            if (tglchange) fltr.Add("m.Tanggal BETWEEN @tgl1 AND @tgl2");
            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ", fltr);
            return sqlc;
        }

        int Page = 1;
        int totalPage;
        public void LoadData()
        {
            string nis, nama, kelas;
            DateTime tgl1, tgl2;

            nis = txtNIS.Text;
            nama = txtNama.Text;
            kelas = txtKelas.Text;
            tgl1 = tglsatu.Value.Date;
            tgl2 = tgldua.Value.Date;

            var sqlc = FilterSQL(nis, nama, kelas);
            var dp = new DynamicParameters();
            if(nis != "")dp.Add("@NIS", nis);
            if (nama != "") dp.Add("@Nama", nama);
            if (kelas != "") dp.Add("@Kelas", kelas);
            if (tglchange)
            {
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
            }
            string text = "Halaman ";
            int RowPerPage = 15;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = masukDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = masukDal.ListData(sqlc, dp);
        }

        private void GetData()
        {
            var Id = dataGridView1.CurrentRow.Cells["Id"].Value?.ToString() ?? string.Empty;
            globalId = Id == string.Empty ? 0 : int.Parse(Id);
            var data = masukDal.GetData(Convert.ToInt32(Id));
            if (data == null) return;
            txtNIS1.Text = data.NIS.ToString();
            txtNama1.Text = data.Nama;
            txtKelas1.Text = data.NamaKelas;
            tglDT.Value = data.Tanggal;
            jamMasukDT.Value = DateTime.Today.Add(data.JamMasuk);
            txtAlasan1.Text = data.Alasan;
        }

        private void ClearInput()
        {
            txtNIS1.Clear();
            txtNama1.Clear();
            txtKelas1.Clear();
            tglDT.Value = DateTime.Now;
            jamMasukDT.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            txtAlasan1.Clear();
        }

        private void SaveData()
        {
            string nis = txtNIS1.Text;
            string nama = txtNama1.Text;
            string alasan = txtAlasan1.Text;
            DateTime tgl = tglDT.Value;
            TimeSpan jamMasuk = jamMasukDT.Value.TimeOfDay;

            if (nis == "" || nama == "" || alasan == "")
            {
                mesBox.MesInfo("Seluruh Data Wajib Diisi!");
                return;
            }

            var masuk = new MasukModel
            {
                Id = globalId,
                NIS = Convert.ToInt32(nis),
                Tanggal = tgl,
                JamMasuk = jamMasuk,
                Alasan = alasan
            };

            if (masuk.Id == 0)
            {
                if (!mesBox.MesKonfirmasi("Input Data?")) return;
                masukDal.Insert(masuk);
                LoadData();
            }
            else
            {
                if (!mesBox.MesKonfirmasi("Update Data?")) return;
                masukDal.Update(masuk);
                LoadData();
            }
        }

        private void Delete()
        {
            if(globalId == 0)
            {
                mesBox.MesInfo("Pilih Data Terlebih Dahulu!");
                return;
            }
            if (!mesBox.MesKonfirmasi("Hapus Data?")) return;
            masukDal.Delete(globalId);
            LoadData();
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
                txtKelas1.Text = siswa.NamaKelas;
            }
        }

        #region EVENT
        private void RegisterEvent()
        {
            txtNIS.TextChanged += filter_TextChanged;
            txtNama.TextChanged += filter_TextChanged;
            txtKelas.TextChanged += filter_TextChanged;
            tglsatu.ValueChanged += filter_tglChanged;
            tgldua.ValueChanged += filter_tglChanged;
        }

        private void filter_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }
        private void filter_tglChanged(object sender, EventArgs e)
        {
            Page = 1;
            tglchange = true;
            LoadData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearInput();
            globalId = 0;
            lblInfo.Text = "INSERT";
            txtNIS1.ReadOnly = false;

        }
        private void btnSave_FormSiswa_Click(object sender, EventArgs e)
        {
            SaveData();
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

        private void btnDelete_FormSiswa_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            GetData();
            lblInfo.Text = "UPDATE";
            txtNIS1.ReadOnly = true;
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtNIS.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            tglchange = false;
            LoadData();
        }
        #endregion

    }
}
