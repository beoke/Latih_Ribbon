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
        private readonly HistoryDal historyDal;
        private MesBox mesBox = new MesBox();
        private int globalId = 0;
        private bool InternalTextChange = true;
        public FormAbsensi()
        {
            absensiDal = new AbsensiDal();
            siswaDal = new SiswaDal();
            historyDal = new HistoryDal();
            InitializeComponent();
            buf();
            RegisterEvent();
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
  
        private void RegisterEvent()
        {
            txtPersensi1.TextChanged += perkas_TextChanged;
            txtKelas1.TextChanged += perkas_TextChanged;
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


        bool tglchange = false;
        private string FilterSQL(string nis, string nama, string kelas, string keterangan)
        {
            string sqlc = string.Empty;
            List<string> fltr = new List<string>();
            if (nis != "") fltr.Add("p.NIS LIKE @NIS+'%'");
            if (nama != "") fltr.Add("s.Nama LIKE '%'+@Nama+'%'");
            if (kelas != "") fltr.Add("s.Kelas LIKE '%'+@Kelas+'%'");
            if (keterangan != "Semua") fltr.Add("p.Keterangan LIKE @Keterangan+'%'");
            if (tglchange) fltr.Add("p.Tanggal BETWEEN @tgl1 AND @tgl2");

            if (fltr.Count > 0)
                sqlc += " WHERE " + string.Join(" AND ", fltr);
            return sqlc;
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

            var sqlc = FilterSQL(nis, nama, kelas, keterangan);

            var dp = new DynamicParameters();
            if (nis != "") dp.Add("@NIS", nis);
            if (nama != "") dp.Add("@Nama", nama);
            if (keterangan != "Semua") dp.Add("@Keterangan", keterangan);
            if (kelas != "") dp.Add("@Kelas", kelas);
            if (tglchange)
            {
                dp.Add("@tgl1", tgl1);
                dp.Add("@tgl2", tgl2);
            }

            string text = "Halaman ";
            int RowPerPage = 15;
            int inRowPage = (Page - 1) * RowPerPage;
            var jumlahRow = absensiDal.CekRows(sqlc, dp);
            totalPage = (int)Math.Ceiling((double)jumlahRow / RowPerPage);

            text += $"{Page.ToString()}/{totalPage.ToString()}";
            lblHalaman.Text = text;
            dp.Add("@Offset", inRowPage);
            dp.Add("@Fetch", RowPerPage);
            dataGridView1.DataSource = absensiDal.ListData(sqlc, dp);
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
            tglDT.Value = DateTime.Now;
            Izinradio.Checked = false;
            sakitRadio.Checked = false;
            alphaRadio.Checked = false;
        }

        private AbsensiModel ValidasiInput()
        {
            int Persensi = string.IsNullOrEmpty(txtPersensi1.Text) ? 0 : Convert.ToInt32(txtPersensi1.Text);
            string Kelas = txtKelas1.Text;
            var cekData = absensiDal.GetByPerKas(" WHERE Persensi=@Persensi AND Kelas=@Kelas", new {Persensi = Persensi,Kelas=Kelas});
            var absensi = new AbsensiModel 
            {
                Nis = cekData?.Nis ?? 0,
                Nama = cekData?.Nama ?? string.Empty,
            };
            return absensi;
        }
        private void SaveData()
        {
            string nis, nama,persensi, kelas, keterangan=string.Empty;
            DateTime tgl;

            nis = txtNIS1.Text;
            nama = txtNama1.Text;
            persensi = txtPersensi1.Text;
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

            var dataCek = absensiDal.GetByPerKas(" WHERE Nis=@Nis", new { Nis = Convert.ToInt32(nis) });
            if (dataCek != null)
            {
                mesBox.MesInfo($"{nama} sudah Absensi pada " + tgl.ToString("dd/MM/yyyy"));
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
                txtPersensi1.Text = string.Empty;
            }
            else
            {
                lblNisTidakDitemukan.Visible = false;
                txtNama1.Text = siswa.Nama;
                txtKelas1.Text = siswa.Kelas;
                txtPersensi1.Text = siswa.Persensi.ToString() ?? string.Empty;
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
            Page = 1;
            LoadData();
        }

        private void tglsatu_ValueChanged(object sender, EventArgs e)
        {
            Page = 1;
            tglchange = true;
            LoadData();
        }

        private void tgldua_ValueChanged(object sender, EventArgs e)
        {
            Page = 1;
            tglchange = true;
            LoadData();
        }

        private void txtNama_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void txtKelas_TextChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }

        private void KeteranganCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page = 1;
            LoadData();
        }
        #endregion

        private void perkas_TextChanged(object sender, EventArgs e)
        {
            if (!InternalTextChange) return;
            if(!string.IsNullOrEmpty(txtPersensi1.Text) && !string.IsNullOrEmpty(txtKelas1.Text))
            {
                InternalTextChange = false;

                var hasil = ValidasiInput();
                txtNIS1.Text = hasil.Nis != 0 ? hasil.Nis.ToString() : string.Empty; 
                txtNama1.Text = hasil.Nama;

                InternalTextChange = true;
            }
        }
        private void txtNIS1_TextChanged(object sender, EventArgs e)
        {
            if (!InternalTextChange) return;
            InternalTextChange= false;
            if (txtNIS1.Text.Length >= 5)
            {
                CekNis();
            }
            else
            {
                txtNama1.Text = string.Empty;
                txtPersensi1.Text = string.Empty;
                txtKelas1.Text = string.Empty;
                lblNisTidakDitemukan.Visible = false;
            }
            InternalTextChange = true;
        }
        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            txtNIS.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            KeteranganCombo.SelectedIndex = 0;
            tglsatu.Value = DateTime.Now;
            tgldua.Value = DateTime.Now;
            tglchange = false;
            LoadData();
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
            PopUpKelas kelas = new PopUpKelas("Absensi");
            kelas.ShowDialog();
            if (kelas.DialogResult == DialogResult.OK)
                txtKelas1.Text = historyDal.GetData("Absensi").History.ToString() ?? string.Empty;
        }

        private void tglDT_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
